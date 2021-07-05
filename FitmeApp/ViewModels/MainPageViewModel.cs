using System;
using System.Net;
using System.Net.Http;
using FitmeApp.Models;
using FitmeApp.Models.SubModel.Request;
using FitmeApp.Services;
using FitmeApp.Settings;
using FitmeApp.Utilities.Helper;
using FitmeApp.Utilities.Models;
using FitmeApp.Utilities.Models.Response;
using FitmeApp.Utils;
using FitmeApp.Views;
using Xamarin.Forms;

namespace FitmeApp.ViewModels
{
    public class MainPageViewModel : ValidatableModel
    {


        public MainPageViewModel()
        {

            FilesWriter.SharedInstance.ReadJson<User>("user.json", out User resultObj);
            UserData = resultObj;
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
    }
}
