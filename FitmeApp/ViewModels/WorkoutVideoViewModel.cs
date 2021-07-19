using System;
using System.Collections.Generic;
using FitmeApp.Models.SubModel;
using FitmeApp.Utilities.Models;
using FitmeApp.Utils;
using Xamarin.Forms;
namespace FitmeApp.ViewModels
{
    public class WorkoutVideoViewModel : ValidatableModel
    {
        public WorkoutVideoViewModel()
        {
            FilesWriter.SharedInstance.ReadJson<List<WorkoutList>>("temp_workoutlist.json", out List<WorkoutList> resultObj);
            WorkoutList = resultObj;
            for(var i = 0; i < WorkoutList.Count; i++)
            {
                WorkoutList[i].repetition = $"{WorkoutList[i].repetition} Repetitions";
            }
        }

        private List<WorkoutList> workoutLists;
        public List<WorkoutList> WorkoutList
        {
            get => workoutLists;
            set
            {
                workoutLists = value;
                RaisePropertyChanged(nameof(WorkoutList));
            }
        }

        private string _repetition;
        public string Repetition
        {
            get => _repetition;
            set
            {
                _repetition = value;
                RaisePropertyChanged(nameof(Repetition));
            }
        }

        private string _breaktime;
        public string BreakTime
        {
            get => _breaktime;
            set
            {
                _breaktime = value;
                RaisePropertyChanged(nameof(BreakTime));
            }
        }


    }
}
