using System;
using System.Collections.Generic;
using FitmeApp.Models.SubModel;

namespace FitmeApp.Models
{
    public class HistoryRecommendation
    {
        public User userId { get; set; }
        public List<Cases> caseSimilarity { get;set;}
        public int k { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
    }
}
