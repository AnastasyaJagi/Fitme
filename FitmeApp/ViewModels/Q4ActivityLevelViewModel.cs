using System;
using System.Collections.Generic;
using FitmeApp.Models;
using FitmeApp.Services;
using FitmeApp.Settings;
using FitmeApp.Utilities.Models;
using FitmeApp.Utils;
using FitmeApp.Views;
using Xamarin.Forms;
namespace FitmeApp.ViewModels
{
    public class Q4ActivityLevelViewModel :ValidatableModel
    {
        protected readonly string baseUrl;
        protected readonly string bodyUrl;

        public Q4ActivityLevelViewModel()
        {
            baseUrl = AppSettingsManager.Settings["BaseUrl"];
            bodyUrl = AppSettingsManager.Settings["ActivityLevelUrl"];
            ShowContent = new LoadingPopup().showLoading(true);

            FilesWriter.SharedInstance.ReadJson<User>("user.json", out User resultObj);
            UserData = resultObj;
            Console.WriteLine($"Age : {UserData.age}");
            Console.WriteLine($"W : {UserData.weight}");
            Console.WriteLine($"H : {UserData.height}");
            Console.WriteLine($"Gender : {UserData.gender}");

            getActivityLevelData();
        }


        private User userData;
        public User UserData
        {
            get => userData;
            set
            {
                userData = value;
                RaisePropertyChanged(nameof(UserData));
            }
        }

        private List<ActivityLevel> _activityLevels;
        public List<ActivityLevel> ActivityLevels
        {
            get => _activityLevels;
            set
            {
                _activityLevels = value;
                RaisePropertyChanged(nameof(ActivityLevels));
            }
        }

        private bool _showContent;
        public bool ShowContent
        {
            get => _showContent;
            set
            {
                _showContent = value;
                RaisePropertyChanged(nameof(ShowContent));
            }
        }

        private async void getActivityLevelData()
        {
            var httpRequest = await HttpService.GetAsync<List<ActivityLevel>>($"{baseUrl}{bodyUrl}"); //{AppSettingsManager.Settings["PertanyaanUrl"]}
            var msg = $"{(httpRequest.Successful ? "" : "Not ")} Found ActivityLevel";
            ActivityLevels = httpRequest.Result;
            ShowContent = new LoadingPopup().showLoading(false);
            Console.WriteLine(msg);
        }

        public void saveChoice(string activityid)
        {
            UserData.activityId = activityid;
            FilesWriter.SharedInstance.SaveToJson(UserData, "user.json");
        }


    }
}
