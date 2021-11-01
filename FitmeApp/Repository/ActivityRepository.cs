using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitmeApp.Models;
using FitmeApp.Services;
using FitmeApp.Settings;

namespace FitmeApp.Repository
{
    public class ActivityRepository
    {
        public ActivityRepository() {}
        private static readonly Lazy<ActivityRepository>
            lazy =
            new Lazy<ActivityRepository>
            (() => new ActivityRepository());
        public static ActivityRepository Instance { get { return lazy.Value; } }

        protected readonly string baseUrl = AppSettingsManager.Settings["BaseUrl"];
        protected readonly string activityUrl = AppSettingsManager.Settings["ActivityLevelUrl"];

        public async Task<List<ActivityLevel>> GetActivityLevels()
        {
            try
            {
                var httpRequest = await HttpService.GetAsync<List<ActivityLevel>>($"{baseUrl}{activityUrl}"); //{AppSettingsManager.Settings["PertanyaanUrl"]}
                var msg = $"{(httpRequest.Successful ? "" : "Not ")} Found Activities";
                List<ActivityLevel> ListActivity = httpRequest.Result;
                Console.WriteLine(msg);
                return ListActivity;
            }
            catch (Exception e)
            {
                Console.WriteLine("Api - an error has occured : " + e.Message);
                return null;
            }
        }

        public async Task<ActivityLevel> GetActivityById(string id)
        {
            try
            {
                var httpRequest = await HttpService.GetAsync<ActivityLevel>($"{baseUrl}{activityUrl}/{id}"); //{AppSettingsManager.Settings["PertanyaanUrl"]}
                var msg = $"{(httpRequest.Successful ? "" : "Not ")} Found Activity";
                ActivityLevel Activity = httpRequest.Result;
                Console.WriteLine(msg);
                return Activity;
            }
            catch (Exception e)
            {
                Console.WriteLine("Api - an error has occured : " + e.Message);
                return null;
            }
        }
    }
}
