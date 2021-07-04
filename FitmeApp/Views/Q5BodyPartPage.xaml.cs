using System;
using System.Collections.Generic;
using FitmeApp.Models;
using FitmeApp.ViewModels;
using Xamarin.Forms;

namespace FitmeApp.Views
{
    public partial class Q5BodyPartPage : ContentPage
    {
        private Q5BodyPartViewModels ViewModel = new Q5BodyPartViewModels();
        public Q5BodyPartPage()
        {
            InitializeComponent();
            BindingContext = ViewModel;
            NavigationPage.SetHasNavigationBar(this, false);
        }
        public void NavigateToQ4ActivityLevelPage(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new Q4ActivityLevelPage());
        }

        public void NavigateToGetRecommendationPages(object sender, System.EventArgs a)

        {
            for (int i = 0; i < ViewModel.ListSelectedBodyPart.Count; i++)
            {
                Console.WriteLine(ViewModel.ListSelectedBodyPart[i]._id);
            }
            // save user to db
            ViewModel.putPenggunaAsync();
            // save body part to files
            ViewModel.saveBodyPart();
            // navigate
            Navigation.PushAsync(new MainPage());
        }

        WorkoutType previousModel;
        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            WorkoutType currentModel = ((CheckBox)sender).BindingContext as WorkoutType;

            bool isfound = ViewModel.ListSelectedBodyPart.Exists(x => x._id == currentModel._id);
            if (isfound)
            {
                ViewModel.ListSelectedBodyPart.Remove(currentModel);
            }
            else
            {
                ViewModel.ListSelectedBodyPart.Add(currentModel);
            }
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //WorkoutType currentModel = e.SelectedItem as WorkoutType;
            //bool isfound = ViewModel.ListSelectedBodyPart.Exists(x => x._id == currentModel._id);
            //if (isfound)
            //{
            //    ViewModel.ListSelectedBodyPart.Remove(currentModel);
            //}
            //else
            //{
            //    ViewModel.ListSelectedBodyPart.Add(currentModel);
            //}
        }


    }
}

