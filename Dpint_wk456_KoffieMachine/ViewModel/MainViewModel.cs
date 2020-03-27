using Dpint_wk456_KoffieMachine.Factories;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using KoffieMachineDomain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;

namespace Dpint_wk456_KoffieMachine.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<string> LogText { get; }
        // private readonly payment dingen (vervang payment met zo min mogelijk code refereer naar pay by card of pay by coin).
        private DrinkFactory DrinkFactory { get; }
        private AdditionFactory AdditionFactory { get; }
        private PaymentFactory PaymentFactory { get; }

        public MainViewModel()
        {
            _coffeeStrength = Strength.Normal;
            _sugarAmount = Amount.Normal;
            _milkAmount = Amount.Normal;

            LogText = new ObservableCollection<string> {"Starting up...", "Done, what would you like to drink?"};

            PaymentFactory = new PaymentFactory();
            SelectedPaymentCardUsername = PaymentCardUsernames[0];

            DrinkFactory = new DrinkFactory();
            AdditionFactory = new AdditionFactory();

        }

        #region Drink properties to bind to
        private IDrink _selectedDrink;
        public string SelectedDrinkName => _selectedDrink?.Name;

        public double? SelectedDrinkPrice => _selectedDrink?.Price;

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

        #endregion Coffee buttons
    }
}