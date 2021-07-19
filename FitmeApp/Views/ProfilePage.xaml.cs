﻿using System;
using System.Collections.Generic;
using FitmeApp.ViewModels;
using Xamarin.Forms;

namespace FitmeApp.Views
{
    public partial class ProfilePage : ContentPage
    {
        private ProfileViewModel ViewModel = new ProfileViewModel();
        public ProfilePage()
        {
            InitializeComponent();
            BindingContext = ViewModel;
            NavigationPage.SetHasNavigationBar(this, false);
        }

        void Save_Clicked(System.Object sender, System.EventArgs e)
        {
            ViewModel.putPenggunaAsync();
        }
    }
}
