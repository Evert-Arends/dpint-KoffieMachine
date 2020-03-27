using Dpint_wk456_KoffieMachine.Factories;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using KoffieMachineDomain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using TeaAndChocoLibrary;

namespace Dpint_wk456_KoffieMachine.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<string> LogText { get; }
        public ObservableCollection<string> TeaBlends
        {
            get => new ObservableCollection<string>(_teaBlendFactory.BlendNames);
        }
        // private readonly payment dingen (vervang payment met zo min mogelijk code refereer naar pay by card of pay by coin).
        private DrinkFactory DrinkFactory { get; }
        private PaymentFactory PaymentFactory { get; }
        private TeaBlendRepository _teaBlendFactory;

        public ObservableCollection<CustomCoffee> CustomClasses { get; set; }
        public ObservableCollection<string> CustomClassNames { get; set; }
        private readonly Dictionary<string, CustomCoffee> CustomCoffees;

        public MainViewModel()
        {
            CustomClassNames = new ObservableCollection<string>();
            CustomCoffees = new Dictionary<string, CustomCoffee>();
            var _ = new CustomClassParser();
            CustomClasses = _.GetCustomClasses().coffees;
            foreach (CustomCoffee customCoffee in CustomClasses)
            {
                CustomClassNames?.Add(customCoffee.Name);
                CustomCoffees.Add(customCoffee.Name, new CustomCoffee
                {
                    Name = customCoffee.Name,
                    Price = customCoffee.Price,
                    steps = customCoffee.steps
                });
            }


            _coffeeStrength = Strength.Normal;
            _sugarAmount = Amount.Normal;
            _milkAmount = Amount.Normal;

            LogText = new ObservableCollection<string> {"Starting up...", "Done, what would you like to drink?"};

            PaymentFactory = new PaymentFactory();
            SelectedPaymentCardUsername = PaymentCardUsernames[0];

            DrinkFactory = new DrinkFactory();
            _teaBlendFactory = new TeaBlendRepository();

            SelectedTea = TeaBlends.First();

        }

        #region Drink properties to bind to
        private IDrink _selectedDrink;
        public string SelectedDrinkName => _selectedDrink?.Name;

        public double? SelectedDrinkPrice => _selectedDrink?.Price;
        public string SelectedTea { get; set; }
        public string SelectedCustomCoffee { get; set; }

        #endregion Drink properties to bind to

        #region Payment
        public RelayCommand PayByCardCommand => new RelayCommand(PayWithCard);
        private void LogPayment(double inserted)
        {
            if (_selectedDrink == null) return;
            LogText.Add($"Inserted {inserted.ToString("C", CultureInfo.CurrentCulture)}, Remaining: {RemainingPriceToPay.ToString("C", CultureInfo.CurrentCulture)}.");

            // Wait for further payment
            if (RemainingPriceToPay > 0) return;


            _selectedDrink.LogStartDrink(LogText);
            LogText.Add("------------------");
            _selectedDrink = null;
        }
        private void PayWithCard()
        {
            double cardValue = PaymentFactory.GetCardValueByUsername(SelectedPaymentCardUsername);
            double remain = RemainingPriceToPay;

            // Pay the amount (with the amount received from card (Factory)
            RemainingPriceToPay = PaymentFactory.GetPaymentMethodByName("Card").Pay(RemainingPriceToPay, cardValue);
            // Update the remaining amount on the card
            PaymentFactory.SetAmountCard(name: SelectedPaymentCardUsername, cardValue - (remain - RemainingPriceToPay));

            LogPayment(cardValue);
            RaisePropertyChanged(() => PaymentCardRemainingAmount);
        }

        public ICommand PayByCoinCommand => new RelayCommand<double>(coinValue =>
        {
            RemainingPriceToPay = PaymentFactory
                .GetPaymentMethodByName("Coin")
                .Pay(RemainingPriceToPay, coinValue);

            // Log payment and check if the coin is enough.
            this.LogPayment(coinValue);
        });

        public double PaymentCardRemainingAmount =>
            PaymentFactory.Cards.Contains(SelectedPaymentCardUsername ?? "")
                ? PaymentFactory.GetCardValueByUsername(SelectedPaymentCardUsername)
                : 0;

        public ObservableCollection<string> PaymentCardUsernames
        {
            get => new ObservableCollection<string>(this.PaymentFactory.Cards);
        }
        private string _selectedPaymentCardUsername;
        public string SelectedPaymentCardUsername
        {
            get => _selectedPaymentCardUsername;
            set
            {
                _selectedPaymentCardUsername = value;
                RaisePropertyChanged(() => SelectedPaymentCardUsername);
                RaisePropertyChanged(() => PaymentCardRemainingAmount);
            }
        }

        private double _remainingPriceToPay;
        public double RemainingPriceToPay
        {
            get => _remainingPriceToPay;
            set { _remainingPriceToPay = value; RaisePropertyChanged(() => RemainingPriceToPay); }
        }
        #endregion Payment

        #region Coffee buttons
        private Strength _coffeeStrength;
        public Strength CoffeeStrength
        {
            get => _coffeeStrength;
            set { _coffeeStrength = value; RaisePropertyChanged(() => CoffeeStrength); }
        }

        private Amount _sugarAmount;
        public Amount SugarAmount
        {
            get => _sugarAmount;
            set { _sugarAmount = value; RaisePropertyChanged(() => SugarAmount); }
        }

        private Amount _milkAmount;
        public Amount MilkAmount
        {
            get => _milkAmount;
            set { _milkAmount = value; RaisePropertyChanged(() => MilkAmount); }
        }

        private void Update()
        {
            RaisePropertyChanged(() => RemainingPriceToPay);
            RaisePropertyChanged(() => SelectedDrinkName);
            RaisePropertyChanged(() => SelectedDrinkPrice);
        }

        private void LogSelectedDrink()
        {
            LogText.Add($"Selected {_selectedDrink.Name}, price: {RemainingPriceToPay.ToString("C", CultureInfo.CurrentCulture)}");
        }

        private void UpdateDrinkState()
        {
            if (_selectedDrink != null)
            {
                RemainingPriceToPay = _selectedDrink.Price;
                LogSelectedDrink();
                this.Update();
            }
        }

        public ICommand DrinkCommand => new RelayCommand<string>((drinkName) =>
        {
            _selectedDrink = DrinkFactory.MixDrink(drinkName, null, null, CoffeeStrength);
            UpdateDrinkState();
        });

        public ICommand DrinkWithSugarCommand => new RelayCommand<string>((drinkName) =>
        {
            _selectedDrink = DrinkFactory.MixDrink(drinkName, SugarAmount, null, CoffeeStrength);
            UpdateDrinkState();
        });

        public ICommand DrinkWithMilkCommand => new RelayCommand<string>((drinkName) =>
        {
            _selectedDrink = DrinkFactory.MixDrink(drinkName, null, MilkAmount, CoffeeStrength);
            UpdateDrinkState();
        });

        public ICommand DrinkWithSugarAndMilkCommand => new RelayCommand<string>((drinkName) =>
        {
            _selectedDrink = DrinkFactory.MixDrink(drinkName, SugarAmount, MilkAmount, CoffeeStrength);
            UpdateDrinkState();
        });

        public ICommand DrinkWithBlendCommand => new RelayCommand<string>((drinkName) =>
        {
            _selectedDrink = DrinkFactory.MixDrink(drinkName, null, null, null, _teaBlendFactory.GetTeaBlend(SelectedTea));
            UpdateDrinkState();
        });

        public ICommand DrinkWithBlendAndSugarCommand => new RelayCommand<string>((drinkName) =>
        {
            _selectedDrink = DrinkFactory.MixDrink(drinkName, SugarAmount, null, null, _teaBlendFactory.GetTeaBlend(SelectedTea));
            UpdateDrinkState();
        });
        public ICommand CustomDrinkCommand => new RelayCommand<string>((ok) =>
        {
            string drinkName = SelectedCustomCoffee;
            _selectedDrink = DrinkFactory.MixDrink("Custom", null, null, null, null, CustomCoffees[drinkName]);
            UpdateDrinkState();
        });
        #endregion Coffee buttons
    }
}