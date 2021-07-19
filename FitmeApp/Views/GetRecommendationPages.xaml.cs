using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitmeApp.Models.SubModel;
using FitmeApp.ViewModels;
using MediaManager;
//using MediaManager;
using Xamarin.Forms;

namespace FitmeApp.Views
{
    public partial class GetRecommendationPages : ContentPage
    {
        private GetRecomendationViewModel ViewModel = new GetRecomendationViewModel();
        public GetRecommendationPages()
        {

            InitializeComponent();
            BindingContext = ViewModel;
            NavigationPage.SetHasNavigationBar(this, false);
        }

        public void NavigateToWorkoutVideoPage(object sender, System.EventArgs e)
        {
            
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            SubWorkout activity = (SubWorkout)e.Item;
            ViewModel.saveTempWorkoutList(activity.workout_list);
            Navigation.PushAsync(new WorkoutVideoPage());
        }

        //public void PlayWithMedia(object sender, System.EventArgs e)
        //{
        //    startMedia();
        //}

        //public void PlayWithExo(object sender, System.EventArgs e)
        //{
        //    startExo();
        //}

        //public async void startMedia()
        //{
        //    await CrossMediaManager.Current.Play("https://s5.radio.co/s6e27496ef/low ");
        //    Console.WriteLine("playing ");
        //}

        //public void startExo()
        //{
        //    DependencyService.Get<IMediaPlayer>().Play("https://s5.radio.co/s6e27496ef/low ");
        //}

        //public async void stopMedia()
        //{
        //    await CrossMediaManager.Current.Stop();
        //}
    }
}