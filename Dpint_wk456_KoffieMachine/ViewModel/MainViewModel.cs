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

        public double? SelectedDrinkPrice => _selectedDrink?.GetPrice();

        #endregion Drink properties to bind to

        #region Payment
        public RelayCommand PayByCardCommand => new RelayCommand(PayWithCard);
        private void LogPayment(double inserted)
        {
            if (_selectedDrink == null) return;
            LogText.Add($"Inserted {inserted.ToString("C", CultureInfo.CurrentCulture)}, Remaining: {RemainingPriceToPay.ToString("C", CultureInfo.CurrentCulture)}.");

            // Wait for coins
            if (RemainingPriceToPay > 0) return;


            _selectedDrink.LogDrinkMaking(LogText);
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
            get { return _selectedPaymentCardUsername; }
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
            get { return _remainingPriceToPay; }
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

        private void Update(string log)
        {
            LogText.Add(log);
            RaisePropertyChanged(() => RemainingPriceToPay);
            RaisePropertyChanged(() => SelectedDrinkName);
            RaisePropertyChanged(() => SelectedDrinkPrice);
        }

        public ICommand DrinkCommand => new RelayCommand<string>((drinkName) =>
        {
            _selectedDrink = null;
            IDrink drink = this.DrinkFactory.GetDrink(drinkName);
            // Add strength to the drinks who have the IStrength type
            _selectedDrink = AdditionFactory.AddStrength(drink, CoffeeStrength);
            RemainingPriceToPay = _selectedDrink.GetPrice();
            // Call the RaisePropertyChangeds, and add a log.
            this.Update($"Selected {_selectedDrink.Name}, price: {RemainingPriceToPay.ToString("C", CultureInfo.CurrentCulture)}");
        });


        public ICommand DrinkWithSugarCommand => new RelayCommand<string>((drinkName) =>
        {
            _selectedDrink = null;
            RemainingPriceToPay = 0;
            IDrink drink = this.DrinkFactory.GetDrink(drinkName);

            // Add strength to the drinks who have the IStrength type
            _selectedDrink = AdditionFactory.AddStrength(drink, CoffeeStrength);
            _selectedDrink = AdditionFactory.AddSugar(_selectedDrink, SugarAmount);

            // Call the RaisePropertyChangeds, and add a log.
            RemainingPriceToPay = _selectedDrink.GetPrice() + Drink.SugarPrice;
            this.Update($"Selected {_selectedDrink.Name} with sugar, price: {RemainingPriceToPay.ToString("C", CultureInfo.CurrentCulture)}");
        });

        public ICommand DrinkWithMilkCommand => new RelayCommand<string>((drinkName) =>
        {
            _selectedDrink = null;
            IDrink drink = this.DrinkFactory.GetDrink(drinkName);

            // Add extra's to the drinks who have the required type.
            _selectedDrink = AdditionFactory.AddStrength(drink, CoffeeStrength);
            _selectedDrink = AdditionFactory.AddMilk(drink, MilkAmount);

            if (_selectedDrink == null) return;
            RemainingPriceToPay = _selectedDrink.GetPrice() + Drink.MilkPrice;

            this.Update($"Selected {_selectedDrink.Name} with milk, price: {RemainingPriceToPay}");
        });

        public ICommand DrinkWithSugarAndMilkCommand => new RelayCommand<string>((drinkName) =>
        {
            _selectedDrink = null;
            IDrink drink = this.DrinkFactory.GetDrink(drinkName);

            // Add all the extra's to the drinks who have the required type.
            _selectedDrink = AdditionFactory.AddAll(drink, CoffeeStrength, MilkAmount, SugarAmount);

            if (_selectedDrink == null) return;
            RemainingPriceToPay = _selectedDrink.GetPrice() + Drink.SugarPrice + Drink.MilkPrice;

            this.Update($"Selected {_selectedDrink.Name} with sugar and milk, price: {RemainingPriceToPay}");
        });

        #endregion Coffee buttons
    }
}