
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
    }
}





















