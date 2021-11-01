using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FitmeApp.Models;
using FitmeApp.Models.SubModel;
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
    public class Q5BodyPartViewModels : ValidatableModel
    {
        public Q5BodyPartViewModels()
        {
            ShowContent = new LoadingPopup().showLoading(true);

            FilesWriter.SharedInstance.ReadJson<User>("user.json", out User resultObj);
            UserData = resultObj;
            Console.WriteLine(UserData._id);

            ListSelectedBodyPart = new List<WorkoutType>();

            InitializeWorkoutType();
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

        private List<WorkoutType> workoutTypes;
        public List<WorkoutType> WorkoutTypes
        {
            get => workoutTypes;
            set
            {
                workoutTypes = value;
                RaisePropertyChanged(nameof(WorkoutTypes));
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

        private void InitializeWorkoutType()
        {
            Task.Run(async () =>
            {
                WorkoutTypes = await WorkoutTypeRepository.SharedInstance.getWorkoutTypes();
                ShowContent = new LoadingPopup().showLoading(false);
            });
        }

        private List<WorkoutType> _listSelectedBodyPart;
        public List<WorkoutType> ListSelectedBodyPart
        {
            get => _listSelectedBodyPart;
            set
            {
                _listSelectedBodyPart = value;
                RaisePropertyChanged(nameof(ListSelectedBodyPart));
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
                        if(resultAddCase != null)
                        {
                            // Save to history
                            HistoryRequest historyRequest = new HistoryRequest();
                            historyRequest.k = k;
                            historyRequest.userId = UserData._id;
                            historyRequest.caseSimilarity = UserResult.dataSimilarity;
                            historyRequest.exercise_type = UserResult.output.exercise_level_detail;
                            ResponseUser resultHistory = await HistoryRepository.Instance.AddHistory(historyRequest);
                            if(resultHistory!= null)
                            {
                                // save body part to files
                                saveBodyPart();
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

        public void saveBodyPart()
        {
            // Save to JSON Local
            if (ListSelectedBodyPart.Count > 0) {
                FilesWriter.SharedInstance.SaveToJson(ListSelectedBodyPart, "bodypart.json");
            }
        }

        public async void NavigateToHome()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new MainPage());
        }
    }
}
