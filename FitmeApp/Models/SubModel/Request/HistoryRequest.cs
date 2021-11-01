using System;
using System.Collections.Generic;

namespace FitmeApp.Models.SubModel.Request
{
    public class HistoryRequest
    {
        public string userId { get; set; }
        public List<DataSimilarity> caseSimilarity { get; set; }
        public int k { get; set; }
        public string exercise_type { get; set; }
    }
}
