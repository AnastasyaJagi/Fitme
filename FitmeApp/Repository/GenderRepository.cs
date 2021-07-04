using System;
using System.Collections.Generic;

namespace FitmeApp.Repository
{
    public class GenderRepository
    {
        public static GenderRepository SharedInstance { get; } = new GenderRepository();

        private readonly List<Gender> genderList
           = new List<Gender>()
           {
                new Gender(){ id=0, gender = "Male", img = "loseweighticon2"},
                new Gender(){ id=1, gender = "Female", img = "gainmuscleicon"},
           };

        public List<Gender> getGender()
        {
            return this.genderList;
        }
    }


    public class Gender
    {
        public int id { get; set; }
        public string gender { get; set; }
        public string img { get; set; }
    }
}
