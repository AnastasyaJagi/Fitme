using System;
using System.Collections.Generic;
using FitmeApp.ViewModels;
using Xamarin.Forms;

namespace FitmeApp.Views
{
    public partial class Q4ActivityLevelPage : ContentPage
    {
        public Q4ActivityLevelPage()
        {
            InitializeComponent();
            BindingContext = new Q4ActivityLevelViewModel();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        public void NavigateToQ3AgeHeightWeightPage(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new Q3AgeHeightWeightPage());
        }
        public void NavigateToGetRecommendationPages(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new GetRecommendationPages());

        }
        public void NavigateToQ5BodyPartPage(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new Q5BodyPartPage());
        }
    }
}

