using System;
using System.Collections.Generic;
using FitmeApp.Models;
using FitmeApp.ViewModels;
using Xamarin.Forms;

namespace FitmeApp.Views
{
    public partial class Q4ActivityLevelPage : ContentPage
    {
        private Q4ActivityLevelViewModel ViewModel = new Q4ActivityLevelViewModel();
        public Q4ActivityLevelPage()
        {
            InitializeComponent();
            BindingContext = ViewModel;
            NavigationPage.SetHasNavigationBar(this, false);
        }

        public void NavigateToQ3AgeHeightWeightPage(object sender, System.EventArgs e)
        {
            Navigation.PopAsync();
        }
        public void NavigateToGetRecommendationPages(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new GetRecommendationPages());

        }
        public void NavigateToQ5BodyPartPage(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new Q5BodyPartPage());
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ActivityLevel activity = (ActivityLevel)e.Item;
            ViewModel.saveChoice(activity._id);
            Console.WriteLine(activity._id);
            Navigation.PushAsync(new Q5BodyPartPage());
        }
    }
}

