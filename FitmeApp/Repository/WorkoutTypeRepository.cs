using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitmeApp.Models;
using FitmeApp.Services;
using FitmeApp.Settings;

namespace FitmeApp.Repository
{
    public class WorkoutTypeRepository
    {
        public static WorkoutTypeRepository SharedInstance { get; } = new WorkoutTypeRepository();

        protected static string baseUrl = AppSettingsManager.Settings["BaseUrl"];
        protected static string workoutTypeUrl = AppSettingsManager.Settings["WorkoutTypeUrl"];

        public WorkoutTypeRepository()
        {
        }

        public async Task<List<WorkoutType>> getWorkoutTypes()
        {
            try
            {
                var httpRequest = await HttpService.GetAsync<List<WorkoutType>>($"{baseUrl}{workoutTypeUrl}"); //{AppSettingsManager.Settings["PertanyaanUrl"]}
                var msg = $"{(httpRequest.Successful ? "" : "Not ")} Found WorkoutType";
                List<WorkoutType> ListWorkoutType = httpRequest.Result;
                Console.WriteLine(msg);
                return ListWorkoutType;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
