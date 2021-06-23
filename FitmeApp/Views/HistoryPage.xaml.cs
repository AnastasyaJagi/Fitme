using System;
using System.Collections.Generic;

using Xamarin.Forms;
using FitmeApp.ViewModels;

namespace FitmeApp.Views
{
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage()
        {
            InitializeComponent();
            BindingContext = new HistoryPageViewModel();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    } 
     
    }
