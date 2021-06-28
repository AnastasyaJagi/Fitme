using System;
using FitmeApp.Models;
using FitmeApp.Utilities.Models;
using FitmeApp.Utils;
using Xamarin.Forms;
namespace FitmeApp.ViewModels
{
    public class Q2GenderViewModels : ValidatableModel
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

        public Q2GenderViewModels()
        {
            FilesWriter.SharedInstance.ReadJson<User>("user.json", out User resultObj);
            UserData = resultObj;
            Console.WriteLine(UserData.bodygoalId);
            Console.WriteLine(UserData.age);
        }
    }
}
