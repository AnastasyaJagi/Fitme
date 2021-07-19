using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FitmeApp.Services;
using FitmeApp.Views;

namespace FitmeApp
{
    public partial class App : Application
    {

        public App()
        {
           InitializeComponent();
           Sharpnado.Tabs.Initializer.Initialize(true, false);
           MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
