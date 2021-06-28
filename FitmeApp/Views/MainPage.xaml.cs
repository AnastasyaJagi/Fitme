using System;
using System.ComponentModel;
using FitmeApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitmeApp.Views
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
