using System;
using System.Collections.Generic;
using FitmeApp.Models;
using FitmeApp.Models.SubModel.Request;
using FitmeApp.Repository;
using FitmeApp.Utilities.Models;
using FitmeApp.Utilities.Models.Response;
using FitmeApp.Utils;
using FitmeApp.Views;
using Xamarin.Forms;
namespace FitmeApp.ViewModels
{
    public class ProfileViewModel : ValidatableModel
    {
        public ProfileViewModel()
        {
            FilesWriter.SharedInstance.ReadJson<User>("user.json", out User resultObj);
            UserData = resultObj;
            if (UserData != null)
            {
                getAllActivites();
                getAllBodyGoal();
            }
            SelectedViewModelIndex = 0;
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

        private ActivityLevel selectedActivity;
        public ActivityLevel SelectedActivity
        {
            get => selectedActivity;
            set
            {
                selectedActivity = value;
                RaisePropertyChanged(nameof(SelectedActivity));
            }
        }

        private List<ActivityLevel> activityLevels;
        public List<ActivityLevel> ActivityLevels
        {
            get => activityLevels;
            set
            {
                activityLevels = value;
                RaisePropertyChanged(nameof(ActivityLevels));
            }
        }

        private BodyGoal selectedBody;
        public BodyGoal SelectedBody
        {
            get => selectedBody;
            set
            {
                selectedBody = value;
                RaisePropertyChanged(nameof(SelectedBody));
            }
        }

        private List<BodyGoal> bodygoals;
        public List<BodyGoal> BodyGoals
        {
            get => bodygoals;
            set
            {
                bodygoals = value;
                RaisePropertyChanged(nameof(BodyGoals));
            }
        }

        private int _selectedViewModelIndex;
        public int SelectedViewModelIndex
        {
            get => _selectedViewModelIndex;
            set
            {
                _selectedViewModelIndex = value;
                // get laguage by selected Index and set SelectedLanguage
                RaisePropertyChanged(nameof(SelectedViewModelIndex));
            }
        }

        private CaseResult userResult;
        public CaseResult UserResult
        {
            get => userResult;
            set
            {
                userResult = value;
                RaisePropertyChanged(nameof(UserResult));
            }
        }

        public async void getAllActivites()
        {
            List<ActivityLevel> resultActivity = await ActivityRepository.Instance.GetActivityLevels();
            if (resultActivity != null)
            {
                ActivityLevels = resultActivity;
            }
            ActivityLevel userActivity = await ActivityRepository.Instance.GetActivityById(UserData.activityId);
            if (userActivity != null)
            {
                SelectedActivity = userActivity;
                Console.WriteLine(selectedActivity.activity);
            }
            
        }

        public async void getAllBodyGoal()
        {
            List<BodyGoal> resultBody = await BodyGoalRepository.Instance.GetBodyGoals();
            if (resultBody != null)
            {
                BodyGoals = resultBody;
            }
            BodyGoal userBody = await BodyGoalRepository.Instance.GetBodyGoalById(UserData.bodygoalId);
            if (userBody != null)
            {
                SelectedBody = userBody;
            }
        }

        public async void putPenggunaAsync()
        {
            if (UserData != null)
            {
                ResponseUser resultAdd = await UserRepository.Instance.UpdateUser(UserData);
                if (resultAdd != null)
                {
                    int k = 3;
                    // Get similarity data & save to local
                    UserResult = await PerhitunganRepository.Instance.GetPerhitungan(UserData._id, k);
                    saveUserCase();
                    // Save to case with 'not confirmed status'
                    ResponseUser resultAddCase = await CaseBaseRepository.Instance.AddCaseBase(UserData._id, UserResult.output._id, 0);
                    if (resultAddCase != null)
                    {
                        // Save to history
                        HistoryRequest historyRequest = new HistoryRequest();
                        historyRequest.k = k;
                        historyRequest.userId = UserData._id;
                        historyRequest.caseSimilarity = UserResult.dataSimilarity;
                        historyRequest.exercise_type = UserResult.output.exercise_level_detail;
                        ResponseUser resultHistory = await HistoryRepository.Instance.AddHistory(historyRequest);
                        if (resultHistory != null)
                        {
                            // save body part to files
                            //saveBodyPart();
                            // navigate to home
                            NavigateToHome();
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Failed", "Cannot Add User!", "Ok");
                        }
                    }
                }
            }

        }

        public void saveUserCase()
        {
            // Save to JSON Local
            if (UserResult != null)
            {
                FilesWriter.SharedInstance.SaveToJson(UserResult, "user_case.json");
            }
        }

        public async void NavigateToHome()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new MainPage());
        }

    }
}
