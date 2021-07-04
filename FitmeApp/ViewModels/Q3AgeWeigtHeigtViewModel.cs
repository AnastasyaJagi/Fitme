using System;
using FitmeApp.Models;
using FitmeApp.Utilities.Models;
using FitmeApp.Utils;
using Xamarin.Forms;
namespace FitmeApp.ViewModels
{
    public class Q3AgeWeigtHeigtViewModel : ValidatableModel
    {
        public Q3AgeWeigtHeigtViewModel()
        {
            FilesWriter.SharedInstance.ReadJson<User>("user.json", out User resultObj);
            UserData = resultObj;
            Console.WriteLine(UserData.gender);
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

        private int _age;
        public int Age {
            get => _age;
            set {
                _age = value;
                RaisePropertyChanged(nameof(Age));
            }
        }

        private int _weight;
        public int Weight
        {
            get => _weight;
            set
            {
                _weight = value;
                RaisePropertyChanged(nameof(Weight));
            }
        }

        private int _height;
        public int Height
        {
            get => _height;
            set
            {
                _height = value;
                RaisePropertyChanged(nameof(Height));
            }
        }

        public void saveCoice()
        {
            UserData.age = Age;
            UserData.height = Height;
            UserData.weight = Weight;
            FilesWriter.SharedInstance.SaveToJson(UserData, "user.json");
        }
    }
}
