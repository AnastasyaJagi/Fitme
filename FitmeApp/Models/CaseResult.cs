using System;
using System.Collections.Generic;

namespace FitmeApp.Models
{
    public class CaseResult
    {
        public Workout output { get; set; }
        public int k { get; set; }
        public List<DataSimilarity> dataSimilarity { get; set; }
    }

    public class DataSimilarity
    {
        public string _id { get; set; }
        public string name { get; set; }
        public double similarity { get; set; }
        public string label { get; set; }
    }
}
