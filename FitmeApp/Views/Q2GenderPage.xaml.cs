using System;
using System.Collections.Generic;
using FitmeApp.ViewModels;
using Xamarin.Forms;


namespace FitmeApp.Views
{
    public partial class Q2GenderPage : ContentPage
    {
        public Q2GenderPage() 
        {
            InitializeComponent();
            BindingContext = new Q2GenderViewModels();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        public void NavigateToQ3AgeHeightWeightPage(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new Q3AgeHeightWeightPage());
        }
        public void NavigateToQ1BodyGoalsPage(object sender, System.EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}

