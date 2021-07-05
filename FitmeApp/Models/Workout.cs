using System;
using System.Collections.Generic;
using FitmeApp.Models.SubModel;

namespace FitmeApp.Models
{
    public class Workout
    {
       public int exercise_level { get; set; }
       public string exercise_level_detail { get; set; }
       public List<SubWorkout> workout { get; set; }
       public string _id { get; set; }
       public string createdAt { get; set; }
       public string updatedAt { get; set; }
    }
}
