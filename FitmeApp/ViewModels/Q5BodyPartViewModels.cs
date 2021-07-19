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
                if (UserData != null)
                {
                    ResponseUser resultAdd = await UserRepository.Instance.UpdateUser(UserData);
                    if (resultAdd != null)
                    {
                        // save body part to files
                        saveBodyPart();
                        // navigate to home
                        NavigateToHome();
                    }
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
