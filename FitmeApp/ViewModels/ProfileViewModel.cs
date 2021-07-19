using System;
using FitmeApp.Models;
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

        public async void putPenggunaAsync()
        {
            if (UserData != null)
            {
                ResponseUser resultAdd = await UserRepository.Instance.UpdateUser(UserData);
                if (resultAdd != null)
                {
                    // navigate to home
                    NavigateToHome();
                }
            }
        }

        public async void NavigateToHome()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new MainPage());
        }
    }
}
