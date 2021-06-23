using System;
namespace FitmeApp.Models
{
    public class User
    {
        public string name { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int age { get; set; }
        public double height { get; set; }
        public double weight { get; set; }
        public int gender { get; set; }
        public ActivityLevel activityId { get; set; }
        public BodyGoal bodygoalId { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
    }
}
