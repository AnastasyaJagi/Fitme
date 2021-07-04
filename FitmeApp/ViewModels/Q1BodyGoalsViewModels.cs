using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitmeApp.Models;
using FitmeApp.Repository;
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

        public Q1BodyGoalsViewModels()
        {
            ShowContent = new LoadingPopup().showLoading(true);

            FilesWriter.SharedInstance.ReadJson<User>("user.json", out User resultObj);
            UserData = resultObj;
            Console.WriteLine(UserData._id);

            InitializeBodyGoal();
        }

        public void InitializeBodyGoal()
        {
            Task.Run(async () =>
            {
                var bodyGoals = await BodyGoalRepository.Instance.GetBodyGoals();
                BodyGoals = bodyGoals;
                ShowContent = new LoadingPopup().showLoading(false);
            });
        }

        public void saveCoice(string bodygoalid)
        {
            UserData.bodygoalId = bodygoalid;
            FilesWriter.SharedInstance.SaveToJson(UserData, "user.json");
        }

    }
}
