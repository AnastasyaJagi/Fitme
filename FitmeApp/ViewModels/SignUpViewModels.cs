using System;
using System.Net;
using System.Net.Http;
using FitmeApp.Models;
using FitmeApp.Models.SubModel.Request;
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
        protected readonly string baseUrl;
        protected readonly string userUrl;

        public SignUpViewModels()
        {
            Users = new User();
            Users.gender = 0;
            Users.age = 0;
            Users.weight = 0;
            Users.height = 0;
            baseUrl = AppSettingsManager.Settings["BaseUrl"];
            userUrl = AppSettingsManager.Settings["UserUrl"];
            RegisterCommand = new Command(() => postPenggunaAsync());
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
                var url = $"{AppSettingsManager.Settings["BaseUrl"]}{AppSettingsManager.Settings["UserUrl"]}";
                if (await HttpServiceHelper.ProcessHttpRequestAsync<MobileUserRequest, ResponseUser>(
                        HttpMethod.Post, $"{url}",
                        new MobileUserRequest { name = Users.name, email = Users.email, password = Users.password, username = Users.username, age = Users.age, height = Users.height, weight = Users.weight, gender= Users.age, activityId= "null", bodygoalId="null" })
                    is BaseHttpResponse<ResponseUser> response)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        //AppSettings.AddOrUpdateValue("Username", UserName);
                        Console.WriteLine(response.StatusCode);
                        Console.WriteLine(response.Message);
                        Console.WriteLine(response.Result);
                        App.Current.MainPage.DisplayAlert("Alert", response.Message, "Ok");
                        CurrentUserId = response.Result._id;
                        Users._id = CurrentUserId;
                        // Save to JSON Local
                        FilesWriter.SharedInstance.SaveToJson(Users,"user.json");
                        NavigateToQ1BodyGoalsPage();
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

        public async void NavigateToQ1BodyGoalsPage()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new Q1BodyGoalsPage());
        }
    }
}
