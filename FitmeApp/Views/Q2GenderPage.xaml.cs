using System;
using System.Collections.Generic;
using FitmeApp.Repository;
using FitmeApp.ViewModels;
using Xamarin.Forms;


namespace FitmeApp.Views
{
    public partial class Q2GenderPage : ContentPage
    {
        private Q2GenderViewModels ViewModel = new Q2GenderViewModels();
        public Q2GenderPage() 
        {
            InitializeComponent();
            BindingContext = ViewModel;
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

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Gender gender = (Gender)e.Item;
            ViewModel.saveCoice(gender.id);
            Navigation.PushAsync(new Q3AgeHeightWeightPage());
        }
    }
}

