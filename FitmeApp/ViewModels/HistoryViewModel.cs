using System;
using System.Collections.Generic;
using FitmeApp.Models;
using FitmeApp.Repository;
using FitmeApp.Utilities.Models;
using FitmeApp.Utilities.Models.Response;
using FitmeApp.Utils;
using FitmeApp.Views;
using Xamarin.Forms;
namespace FitmeApp.ViewModels

{
    public class HistoryViewModel : ValidatableModel
    {
        public HistoryViewModel()
        {
            Console.WriteLine("this is history vm");
            // loading true showcontent false
            ShowContent = new LoadingPopup().showLoading(true);
            // Get user data from JSON
            FilesWriter.SharedInstance.ReadJson<User>("user.json", out User resultObj);
            UserData = resultObj;
            if (UserData != null)
            {
                getHistoryUser();
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

        private List<HistoryRecommendation> histories;
        public List<HistoryRecommendation> Histories
        {
            get => histories;
            set
            {
                histories = value;
                RaisePropertyChanged(nameof(Histories));
            }
        }

        public async void getHistoryUser()
        {
            List<HistoryRecommendation> resultHistory = await HistoryRepository.Instance.GetHistory(UserData._id);
            if (resultHistory != null)
            {
                Histories = resultHistory;
            }
            ShowContent = new LoadingPopup().showLoading(false);
        }
    }
}
