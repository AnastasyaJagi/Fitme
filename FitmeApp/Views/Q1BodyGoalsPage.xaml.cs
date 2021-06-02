using System;
using System.Collections.Generic;
using FitmeApp.ViewModels;
using Xamarin.Forms;

namespace FitmeApp.Views
{
    public partial class Q1BodyGoalsPage : ContentPage
    {
        public Q1BodyGoalsPage()
        {
            InitializeComponent();
            BindingContext = new Q1BodyGoalsViewModels();
            NavigationPage.SetHasNavigationBar(this, false);
      
        }
        public void NavigateToQ2GenderPage(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new Q2GenderPage());
        }
    }
}
