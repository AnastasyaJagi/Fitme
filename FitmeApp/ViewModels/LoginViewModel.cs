using System;
using System.IO;
using System.Net;
using System.Net.Http;
using FitmeApp.Models.SubModel;
using FitmeApp.Models.SubModel.Request;
using FitmeApp.Settings;
using FitmeApp.Utilities.Helper;
using FitmeApp.Utilities.Models;
using FitmeApp.Utils;
using FitmeApp.Views;
using Xamarin.Forms;

namespace FitmeApp.ViewModels
{
    public class LoginViewModel : ValidatableModel
    {
        private string formValidation;
        protected readonly string baseUrl;
        protected readonly string loginUrl;


        public LoginViewModel()
        {
            baseUrl = AppSettingsManager.Settings["BaseUrl"];
            loginUrl = AppSettingsManager.Settings["LoginUrl"];
        }

        public string CurrentUserId
        {
            get
            {
                string id = PreferencesWriter.UserId;
                if (id == "")
                {
                    id = "";
                }
                return id;
            }
            set => PreferencesWriter.UserId = value;
        }

        private string _uname;
        public string Uname
        {
            get => _uname;
            set
            {
                _uname = value;
                RaisePropertyChanged(nameof(Uname));
            }
        }

        private string _passwordLogin;
        public string PasswordLogin
        {
            get => _passwordLogin;
            set
            {
                _passwordLogin = value;
                RaisePropertyChanged(nameof(PasswordLogin));
            }
        }

        public async void postLoginAsync()
        {
            Console.WriteLine("start login...");
            try
            {
                var url = $"{baseUrl}{loginUrl}";
                if (await HttpServiceHelper.ProcessHttpRequestAsync<MobileLoginRequest, ResponseUserLogin>(
                        HttpMethod.Post, $"{url}",
                        new MobileLoginRequest { username = Uname, password = PasswordLogin })
                    is BaseHttpResponse<ResponseUserLogin> response)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        //AppSettings.AddOrUpdateValue("Username", UserName);
                        Console.WriteLine(response.StatusCode);
                        Console.WriteLine(response.Message);
                        Console.WriteLine(response.Result);
                        App.Current.MainPage.DisplayAlert("Alert", response.Message, "Ok");
                        CurrentUserId = response.Result._id;
                        NavigateToHome();
                    }
                    else
                    {
                        App.Current.MainPage.DisplayAlert("Alert", response.Message, "Ok");
                        return;
                    }
                }
            }
            catch (InvalidOperationException exception)
            {
                App.Current.MainPage.DisplayAlert("Alert", exception.Message, "Ok");
            }
        }

        public async void NavigateToHome()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new MainPage());
        }
    }
}
