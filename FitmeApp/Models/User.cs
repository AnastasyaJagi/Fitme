using System;
namespace FitmeApp.Models
{
    public class User
    {
        public string _id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int age { get; set; }
        public double height { get; set; }
        public double weight { get; set; }
        public int gender { get; set; }
        public string activityId { get; set; }
        public string bodygoalId { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
    }
}
