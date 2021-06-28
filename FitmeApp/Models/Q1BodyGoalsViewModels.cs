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
    public class Q1BodyGoalsViewModels : ValidatableModel
    {
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

        private List<BodyGoal> _bodyGoal;
        public List<BodyGoal> BodyGoals
        {
            get => _bodyGoal;
            set
            {
                _bodyGoal = value;
                RaisePropertyChanged(nameof(BodyGoals));
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

        protected readonly string baseUrl;
        protected readonly string bodyUrl;

        public Q1BodyGoalsViewModels()
        {
            baseUrl = AppSettingsManager.Settings["BaseUrl"];
            bodyUrl = AppSettingsManager.Settings["BodyGoalUrl"];
            ShowContent = new LoadingPopup().showLoading(true);

            FilesWriter.SharedInstance.ReadJson<User>("user.json", out User resultObj);
            UserData = resultObj;
            Console.WriteLine(UserData._id);

            getBodyGoalData();
        }

        private async void getBodyGoalData()
        {
            var httpRequest = await HttpService.GetAsync<List<BodyGoal>>($"{baseUrl}{bodyUrl}"); //{AppSettingsManager.Settings["PertanyaanUrl"]}
            var msg = $"{(httpRequest.Successful ? "" : "Not ")} Found BodyGoal";
            BodyGoals = httpRequest.Result;
            ShowContent = new LoadingPopup().showLoading(false);
            BodyGoals[0].img = "loseweighticon2";
            BodyGoals[1].img = "gainmuscleicon";
            Console.WriteLine(msg);
        }

        public void saveCoice(string bodygoalid)
        {
            UserData.bodygoalId = bodygoalid;
            FilesWriter.SharedInstance.SaveToJson(UserData, "user.json");
        }

    }
}
