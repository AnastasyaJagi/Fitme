
using System;
using System.Collections.Generic;
using FitmeApp.ViewModels;
using Xamarin.Forms;

namespace FitmeApp.Views
{
    public partial class SignUpPage: ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
            BindingContext = new SignUpViewModels();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        public void NavigateToSignUp(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Q1BodyGoalsPage());
        }
    }
}





















