using System;
using System.Collections.Generic;
using FitmeApp.ViewModels;
using Xamarin.Forms;

namespace FitmeApp.Views
{
    public partial class GetStartedPage : ContentPage
    {
        public GetStartedPage()
        {
            InitializeComponent();
            BindingContext = new GetStartedViewModels();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        public void NavigateToLogin(object sender , EventArgs e)
        {
            Navigation.PushAsync(new Login());
        }
    }
}
