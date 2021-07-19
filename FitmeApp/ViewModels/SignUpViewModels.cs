using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FitmeApp.Models;
using FitmeApp.Models.SubModel.Request;
using FitmeApp.Repository;
using FitmeApp.Settings;
using FitmeApp.Utilities.Helper;
using FitmeApp.Utilities.Models;
using FitmeApp.Utilities.Models.Response;
using FitmeApp.Utils;
using FitmeApp.Views;
using Xamarin.Forms;
namespace FitmeApp.ViewModels

{
    public class SignUpViewModels : ValidatableModel
    {
        private User users;

        public SignUpViewModels()
        {
            Users = new User();
            Users.gender = 0;
            Users.age = 0;
            Users.weight = 0;
            Users.height = 0;
            RegisterCommand = new Command(() => postPenggunaAsync());
        }

        public User Users
        {
            get => users;
            set
            {
                users = value;
                RaisePropertyChanged(nameof(Users));
            }
        }

        Command _registerUser;
        public Command RegisterCommand
        {
            get => _registerUser;
            set
            {
                _registerUser = value;
                RaisePropertyChanged(nameof(RegisterCommand));
            }
        }

        public async void postPenggunaAsync()
        {
            try
            {
                ResponseUser resultAdd = await UserRepository.Instance.AddUser(Users);
                if(resultAdd != null)
                {
                    NavigateToQ1BodyGoalsPage();
                }
                
            }
            catch (InvalidOperationException exception)
            {
                App.Current.MainPage.DisplayAlert("Alert", exception.Message, "Ok");
            }
        }

        public async void NavigateToQ1BodyGoalsPage()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new Q1BodyGoalsPage());
        }
    }
}
