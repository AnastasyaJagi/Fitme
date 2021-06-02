using System;
using System.Collections.Generic;
using FitmeApp.ViewModels;
using Xamarin.Forms;

namespace FitmeApp.Views
{
    public partial class Q3AgeHeightWeightPage : ContentPage
    {
        public Q3AgeHeightWeightPage()
        {
            InitializeComponent();
            BindingContext = new Q3AgeWeigtHeigtViewModel();
            NavigationPage.SetHasNavigationBar(this, false);

        }
        public void NavigateToQ2GenderPage(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new Q2GenderPage());
        }
        public void NavigateToQ4ActivityLevelPage(object sender, System.EventArgs e)
        {

            Navigation.PushAsync(new Q4ActivityLevelPage());

        }
    }
}

