using System;
using System.Collections.Generic;
using FitmeApp.ViewModels;
using Xamarin.Forms;

namespace FitmeApp.Views
{
    public partial class GetRecommendationPages : ContentPage
    {
        public GetRecommendationPages()
        {
           
            InitializeComponent();
            BindingContext = new GetRecomendationViewModel();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}