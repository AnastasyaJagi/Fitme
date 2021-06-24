using System;
using FitmeApp.Models;
using FitmeApp.Services;
using FitmeApp.Settings;
using FitmeApp.Utilities.Models;
using FitmeApp.Utils;
using FitmeApp.Views;
using Xamarin.Forms;
namespace FitmeApp.ViewModels
{
    public class Q1BodyGoalsViewModels : ValidatableModel
    {
        private bool _showContent;

        protected readonly string baseUrl;
        protected readonly string userUrl;


        public Q1BodyGoalsViewModels()
        {
            baseUrl = AppSettingsManager.Settings["BaseUrl"];
            userUrl = AppSettingsManager.Settings["UserUrl"];
            // loading true showcontent false
            ShowContent = new LoadingPopup().showLoading(true);
            getUserDetail();
        }

        private async void getUserDetail()
        {
            var httpRequest = await HttpService.GetAsync<User>($"{baseUrl}{userUrl}/{PreferencesWriter.UserId}"); //{AppSettingsManager.Settings["PertanyaanUrl"]}
            var msg = $"{(httpRequest.Successful ? "" : "Not ")} Found User";
            Users = httpRequest.Result;
            Console.WriteLine($"{msg} : {Users.name}");
            ShowContent = new LoadingPopup().showLoading(false);
        }

        private User users;
        public User Users
        {
            get => users;
            set
            {
                users = value;
                RaisePropertyChanged(nameof(Users));
            }
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

        public bool ShowContent
        {
            get => _showContent;
            set
            {
                _showContent = value;
                RaisePropertyChanged(nameof(ShowContent));
            }
        }
    }
}
