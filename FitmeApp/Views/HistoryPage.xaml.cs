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
            BindingContext = new HistoryViewModel();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            
        }
    } 
     
}
