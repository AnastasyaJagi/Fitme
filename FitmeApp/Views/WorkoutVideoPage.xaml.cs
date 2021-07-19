using System;
using System.Collections.Generic;
using FitmeApp.ViewModels;
using Xamarin.Forms;

namespace FitmeApp.Views
{
    public partial class WorkoutVideoPage : ContentPage
    {
        public WorkoutVideoPage()
        {

            InitializeComponent();
            BindingContext = new WorkoutVideoViewModel();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        public void NavigateToHome(object sender, System.EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            
        }

    }
}
