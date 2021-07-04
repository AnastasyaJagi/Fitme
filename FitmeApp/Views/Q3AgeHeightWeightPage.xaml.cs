using System;
using System.Collections.Generic;
using FitmeApp.ViewModels;
using Xamarin.Forms;

namespace FitmeApp.Views
{
    public partial class Q3AgeHeightWeightPage : ContentPage
    {
        private Q3AgeWeigtHeigtViewModel ViewModel = new Q3AgeWeigtHeigtViewModel();
        public Q3AgeHeightWeightPage()
        {
            InitializeComponent();
            BindingContext = ViewModel;
            NavigationPage.SetHasNavigationBar(this, false);

        }
        public void NavigateToQ2GenderPage(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new Q2GenderPage());
        }
        public async void NavigateToQ4ActivityLevelPage(object sender, System.EventArgs e) { 

            if(ViewModel.Age != 0 || ViewModel.Weight != 0 || ViewModel.Height != 0){
                ViewModel.saveCoice();
                await Navigation.PushAsync(new Q4ActivityLevelPage());
            }
            else
            {
                //App.Current.MainPage.DisplayAlert("Alert", "Please input valid data!", "Ok");
                Console.WriteLine("Cannot Blank Age / Wight / Height");
            }


        }
    }
}

