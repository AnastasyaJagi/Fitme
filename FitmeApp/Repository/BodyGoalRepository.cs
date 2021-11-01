using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitmeApp.Models;
using FitmeApp.Services;
using FitmeApp.Settings;

namespace FitmeApp.Repository
{
    class BodyGoalRepository
    {
        private static readonly Lazy<BodyGoalRepository>
            lazy =
            new Lazy<BodyGoalRepository>
            (() => new BodyGoalRepository());
        public static BodyGoalRepository Instance { get { return lazy.Value; } }

        protected readonly string baseUrl = AppSettingsManager.Settings["BaseUrl"];
        protected readonly string bodyUrl = AppSettingsManager.Settings["BodyGoalUrl"];

        private BodyGoalRepository() { }

        public async Task<List<BodyGoal>> GetBodyGoals()
        {
            try
            {
                var httpRequest = await HttpService.GetAsync<List<BodyGoal>>($"{baseUrl}{bodyUrl}"); //{AppSettingsManager.Settings["PertanyaanUrl"]}
                var msg = $"{(httpRequest.Successful ? "" : "Not ")} Found BodyGoal";
                List<BodyGoal> ListBodyGoals = httpRequest.Result;
                ListBodyGoals[0].img = "loseweighticon2";
                ListBodyGoals[1].img = "gainmuscleicon";
                Console.WriteLine(msg);
                return ListBodyGoals;
            }
            catch(Exception e)
            {
                Console.WriteLine("Api - an error has occured : " +e.Message);
                return null;
            }
        }

        public async Task<BodyGoal> GetBodyGoalById(string id)
        {
            try
            {
                var httpRequest = await HttpService.GetAsync<BodyGoal>($"{baseUrl}{bodyUrl}/{id}"); //{AppSettingsManager.Settings["PertanyaanUrl"]}
                var msg = $"{(httpRequest.Successful ? "" : "Not ")} Found BodyGoal";
                BodyGoal userBody = httpRequest.Result;
                userBody.img = "loseweighticon2";
                userBody.img = "gainmuscleicon";
                Console.WriteLine(msg);
                return userBody;
            }
            catch (Exception e)
            {
                Console.WriteLine("Api - an error has occured : " + e.Message);
                return null;
            }
        }
    }
}
