using System;
namespace FitmeApp.Models
{
    public class CaseBase
    {
        public User userId {get; set;}
        public Workout workoutId { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
    }
}
