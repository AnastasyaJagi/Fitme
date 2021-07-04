using System;
using System.Collections.Generic;
using FitmeApp.Models;
using FitmeApp.Repository;
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

        private List<Gender> _genders;
        public List<Gender> Genders
        {
            get => _genders;
            set
            {
                _genders = value;
                RaisePropertyChanged(nameof(Genders));
            }
        }

        public Q2GenderViewModels()
        {
            FilesWriter.SharedInstance.ReadJson<User>("user.json", out User resultObj);
            UserData = resultObj;
            Console.WriteLine(UserData.bodygoalId);

            Genders = GenderRepository.SharedInstance.getGender();
        }

        public void saveCoice(int gender)
        {
            UserData.gender = gender;
            FilesWriter.SharedInstance.SaveToJson(UserData, "user.json");
        }
    }
}
