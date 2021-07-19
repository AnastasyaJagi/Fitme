using System;
using System.Collections.Generic;

namespace FitmeApp.Models.SubModel
{
    public class SubWorkout
    {
        public WorkoutType workout_type { get; set; }
        public List<WorkoutList> workout_list { get; set; }
    }
}
