using System;
using System.Collections.Generic;
using FitmeApp.ViewModels;
using Xamarin.Forms;

namespace FitmeApp.Views
{
    public partial class Q5BodyPartPage : ContentPage
    {
        public Q5BodyPartPage()
        {
            InitializeComponent();
            BindingContext = new Q5BodyPartViewModels();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        public void NavigateToQ4ActivityLevelPage(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new Q4ActivityLevelPage());
        }

        public void NavigateToGetRecommendationPages(object sender, System.EventArgs a)

        {
            Navigation.PushAsync(new MainPage());

        }
    }
}

