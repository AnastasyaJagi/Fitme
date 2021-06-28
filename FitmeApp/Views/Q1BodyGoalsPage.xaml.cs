using System;
using System.Collections.Generic;
using FitmeApp.Models;
using FitmeApp.ViewModels;
using Xamarin.Forms;

namespace FitmeApp.Views
{
    public partial class Q1BodyGoalsPage : ContentPage
    {
        private Q1BodyGoalsViewModels ViewModel = new Q1BodyGoalsViewModels();
        public Q1BodyGoalsPage()
        {
            InitializeComponent();
            BindingContext = ViewModel;
            NavigationPage.SetHasNavigationBar(this, false);
      
        }
        public void NavigateToQ2GenderPage(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new Q2GenderPage());
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            BodyGoal body = (BodyGoal)e.Item;
            ViewModel.saveCoice(body._id);
            Navigation.PushAsync(new Q2GenderPage());
        }
    }


}
