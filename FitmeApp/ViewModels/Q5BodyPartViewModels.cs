using System;
using System.Collections.Generic;
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

        public async void putPenggunaAsync()
        {
            try
            {
                if(UserData != null)
                {
                    var url = $"{AppSettingsManager.Settings["BaseUrl"]}{AppSettingsManager.Settings["UserUrl"]}/{UserData._id}";
                    if (await HttpServiceHelper.ProcessHttpRequestAsync<MobileUserRequest, ResponseUser>(
                            HttpMethod.Put, $"{url}",
                            new MobileUserRequest { name = UserData.name, email = UserData.email, password = UserData.password, username = UserData.username, age = UserData.age, height = UserData.height, weight = UserData.weight, gender = UserData.gender, activityId = UserData.activityId, bodygoalId = UserData.bodygoalId })
                        is BaseHttpResponse<ResponseUser> response)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            //AppSettings.AddOrUpdateValue("Username", UserName);
                            Console.WriteLine(response.StatusCode);
                            Console.WriteLine(response.Message);
                            Console.WriteLine(response.Result);
                            App.Current.MainPage.DisplayAlert("Alert", response.Message, "Ok");
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

        public void saveBodyPart()
        {
            // Save to JSON Local
            if (ListSelectedBodyPart.Count > 0) {
                FilesWriter.SharedInstance.SaveToJson(ListSelectedBodyPart, "bodypart.json");
            }
        }
    }
}
