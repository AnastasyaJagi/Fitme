using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using FitmeApp.Models;
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
    public class GetRecomendationViewModel : ValidatableModel
    {
        private bool _showContent;

        protected readonly string baseUrl;
        protected readonly string perhitunganUrl;

        public GetRecomendationViewModel()
        {
            baseUrl = AppSettingsManager.Settings["BaseUrl"];
            perhitunganUrl = AppSettingsManager.Settings["PerhitunganUrl"];
            // loading true showcontent false
            ShowContent = new LoadingPopup().showLoading(true);
            // Get user data from JSON
            FilesWriter.SharedInstance.ReadJson<User>("user.json", out User resultObj);
            UserData = resultObj;
            if (UserData != null)
            {
                Console.WriteLine(UserData._id);
                DisplayUsername = $"Hi, {UserData.name}";
                PostSimilarityAsync();
            }
            // Get selected workout data from json
            FilesWriter.SharedInstance.ReadJson<List<WorkoutType>>("bodypart.json", out List<WorkoutType> resultWo);
            ListSelectedBodyPart = resultWo;
        }

        private CaseResult caseResult;
        public CaseResult CaseResult
        {
            get => caseResult;
            set
            {
                caseResult = value;
                RaisePropertyChanged(nameof(CaseResult));
            }
        }

        private List<SubWorkout> listWorkout;
        public List<SubWorkout> ListWorkout {
            get => listWorkout;
            set
            {
                listWorkout = value;
                RaisePropertyChanged(nameof(ListWorkout));
            }
        }

        private List<SubWorkout> yourWorkout = new List<SubWorkout>();
        public List<SubWorkout> YourWorkout
        {
            get => yourWorkout;
            set
            {
                yourWorkout = value;
                RaisePropertyChanged(nameof(YourWorkout));
            }
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

        public bool ShowContent
        {
            get => _showContent;
            set
            {
                _showContent = value;
                RaisePropertyChanged(nameof(ShowContent));
            }
        }

        private User user;
        public User UserData
        {
            get => user;
            set
            {
                user = value;
                RaisePropertyChanged(nameof(UserData));
            }
        }

        private string displayUsername;
        public string DisplayUsername
        {
            get => displayUsername;
            set
            {
                displayUsername = value;
                RaisePropertyChanged(nameof(DisplayUsername));
            }
        }


        // Variables

        private string _titleWorkoutResult;
        public string TitleWorkoutResult
        {
            get => _titleWorkoutResult;
            set
            {
                _titleWorkoutResult = value;
                RaisePropertyChanged(nameof(TitleWorkoutResult));
            }
        }

        public async void PostSimilarityAsync()
        {
            try
            {
                if (UserData != null)
                {
                    var url = $"{baseUrl}{perhitunganUrl}";
                    if (await HttpServiceHelper.ProcessHttpRequestAsync<MobileSimilarityRequest, CaseResult>(
                            HttpMethod.Post, $"{url}",
                            new MobileSimilarityRequest { dataUser = UserData._id, k = 3 })
                        is BaseHttpResponse<CaseResult> response)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            
                            CaseResult = response.Result;
                            ListWorkout = CaseResult.output.workout;
                            TitleWorkoutResult = CaseResult.output.exercise_level_detail;
                            Console.WriteLine(YourWorkout.Count);
                            Console.WriteLine(response.StatusCode);
                            Console.WriteLine(response.Message);
                            Console.WriteLine(CaseResult.dataSimilarity[0].name);
                            Console.WriteLine(CaseResult.k);

                            ShowContent = new LoadingPopup().showLoading(false);
                        }
                        else
                        {
                            App.Current.MainPage.DisplayAlert("Alert", response.Message, "Ok");
                            return;
                        }
                    }
                }

            }
            catch (InvalidOperationException exception)
            {
                App.Current.MainPage.DisplayAlert("Alert", exception.Message, "Ok");
            }
        }

        public void saveTempWorkoutList(List<WorkoutList> workouts)
        {
            // Save to JSON Local
            if (workouts.Count > 0)
            {
                FilesWriter.SharedInstance.SaveToJson(workouts, "temp_workoutlist.json");
            }
        }
    }
}
