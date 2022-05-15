using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using OW21BB.Logic;
using OW21BB.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OW21BB.ViewModel
{
    public class MainWindowViewModel : ObservableRecipient
    {
        IShopLogic logic;

        public ObservableCollection<Part> Shop { get; set; }
        public ObservableCollection<Part> Cart { get; set; }

        private Part selectedFromShop;

        public Part SelectedFromShop
        {
            get { return selectedFromShop; }
            set
            {
                SetProperty(ref selectedFromShop, value);
                (AddToCartCommand as RelayCommand).NotifyCanExecuteChanged();
                (AkcioCommand as RelayCommand).NotifyCanExecuteChanged();

            }
        }

        private Part selectedFromCart;

        public Part SelectedFromCart
        {
            get { return selectedFromCart; }
            set
            {
                SetProperty(ref selectedFromCart, value);
                (RemoveCommand as RelayCommand).NotifyCanExecuteChanged();
                (FinishCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        public double AllCost
        {
            get
            {
                return logic.AllCost;
            }
        }

        public ICommand LoadCommand { get; set; }
        public ICommand AddToCartCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public ICommand FinishCommand { get; set; }
        public ICommand AkcioCommand { get; set; }


        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public MainWindowViewModel()
            : this(IsInDesignMode ? null : Ioc.Default.GetService<IShopLogic>())
        {

        }
        public MainWindowViewModel(IShopLogic logic)
        {
            this.logic = logic;
            this.Shop = new ObservableCollection<Part>();
            this.Cart = new ObservableCollection<Part>();

            logic.SetupCollections(this.Shop, this.Cart);


            //Shop.Add(new Part()
            //{
            //    Name = "cpu",
            //    Type = EnumTypes.CPU,
            //    Price = 500
            //});

            //Shop.Add(new Part()
            //{
            //    Name = "drive",
            //    Type = EnumTypes.DRIVE,
            //    Price = 200
            //});

            //Shop.Add(new Part()
            //{
            //    Name = "mb",
            //    Type = EnumTypes.MOTHERBOARD,
            //    Price = 600
            //});

            //Shop.Add(new Part()
            //{
            //    Name = "ram",
            //    Type = EnumTypes.RAM,
            //    Price = 100
            //});

            LoadCommand = new RelayCommand(
                () => logic.Load(),
                () => Shop.Count == 0
                );

            AddToCartCommand = new RelayCommand(
                () => logic.AddToCart(SelectedFromShop),
                () => selectedFromShop != null);

            RemoveCommand = new RelayCommand(
                () => logic.Remove(SelectedFromCart),
                () => selectedFromCart != null);

            FinishCommand = new RelayCommand(
                () => logic.Finish(),
                () => this.Cart.Count != 0);

            AkcioCommand = new RelayCommand(
                () => logic.Akcio(SelectedFromShop),
                () => SelectedFromShop != null);

            Messenger.Register<MainWindowViewModel, string, string>(this, "ShopInfo",
                (recipient, msg) =>
                {
                    OnPropertyChanged("AllCost");
                });
        }
    }
}
