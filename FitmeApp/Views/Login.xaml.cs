using System;
using System.Collections.Generic;
using FitmeApp.ViewModels;
using Xamarin.Forms;

namespace FitmeApp.Views
{
    public partial class Login : ContentPage
    {
        LoginViewModel ViewModel = new LoginViewModel();
        public Login()
        {
            InitializeComponent();
            BindingContext = ViewModel;
            NavigationPage.SetHasNavigationBar(this, false);
        }
        public void NavigateToSignUp(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignUpPage());
        }

        public void NavigateToHome(object sender, EventArgs e)
        {
            ViewModel.postLoginAsync();
            Console.WriteLine("test");
        }
    }
}
