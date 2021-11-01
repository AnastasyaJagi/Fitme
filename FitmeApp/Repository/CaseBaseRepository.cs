using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FitmeApp.Models.SubModel.Request;
using FitmeApp.Settings;
using FitmeApp.Utilities.Helper;
using FitmeApp.Utilities.Models.Response;

namespace FitmeApp.Repository
{
    public class CaseBaseRepository
    {
        private static readonly Lazy<CaseBaseRepository> lazy = new Lazy<CaseBaseRepository>(() => new CaseBaseRepository());
        public static CaseBaseRepository Instance { get { return lazy.Value; } }

        private CaseBaseRepository() { }

        protected readonly string baseUrl = AppSettingsManager.Settings["BaseUrl"];
        protected readonly string caseUrl = AppSettingsManager.Settings["CaseBaseUrl"];

        public async Task<ResponseUser> AddCaseBase(string userId, string workoutId, int status)
        {
            var url = $"{baseUrl}{caseUrl}";
            try
            {
                if (await HttpServiceHelper.ProcessHttpRequestAsync<MobileCaseRequest, ResponseUser>(
                        HttpMethod.Post, $"{url}",
                        new MobileCaseRequest { workoutId = workoutId, userId= userId, status= status })
                    is BaseHttpResponse<ResponseUser> response)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Console.WriteLine(response.StatusCode);
                        Console.WriteLine(response.Message);
                        Console.WriteLine(response.Result);
                        return response.Result;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }

            }
            catch(Exception e)
            {
                Console.WriteLine("Api - an error has occured : " + e.Message);
                return null;
            }
        }
    }
}
