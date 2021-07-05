using System;
using System.Net;
using System.Net.Http;
using FitmeApp.Models;
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

            FilesWriter.SharedInstance.ReadJson<User>("user.json", out User resultObj);
            UserData = resultObj;
            if (UserData != null)
            {
                Console.WriteLine(UserData._id);
                PostSimilarityAsync();
            }
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
                            Console.WriteLine(response.StatusCode);
                            Console.WriteLine(response.Message);
                            CaseResult = response.Result;
                            Console.WriteLine(CaseResult.k);
                            TitleWorkoutResult = CaseResult.output.exercise_level_detail;
                            Console.WriteLine(CaseResult.dataSimilarity[0].name);
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
    }
}
