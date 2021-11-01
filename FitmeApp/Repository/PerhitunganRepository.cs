using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FitmeApp.Models;
using FitmeApp.Models.SubModel.Request;
using FitmeApp.Settings;
using FitmeApp.Utilities.Helper;

namespace FitmeApp.Repository
{
    public class PerhitunganRepository
    {
        private static readonly Lazy<PerhitunganRepository> lazy = new Lazy<PerhitunganRepository>(() => new PerhitunganRepository());
        public static PerhitunganRepository Instance { get { return lazy.Value; } }

        protected readonly string baseUrl = AppSettingsManager.Settings["BaseUrl"];
        protected readonly string perhitunganUrl = AppSettingsManager.Settings["PerhitunganUrl"];

        private PerhitunganRepository() { }

        public async Task<CaseResult> GetPerhitungan(string userId, int kValue)
        {
            try
            {
                var url = $"{baseUrl}{perhitunganUrl}";
                if (await HttpServiceHelper.ProcessHttpRequestAsync<MobileSimilarityRequest, CaseResult>(
                        HttpMethod.Post, $"{url}",
                        new MobileSimilarityRequest { dataUser = userId, k = kValue })
                    is BaseHttpResponse<CaseResult> response)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {

                        CaseResult caseData = response.Result;
                        Console.WriteLine(caseData.output.workout.Count);
                        Console.WriteLine(response.StatusCode);
                        Console.WriteLine(response.Message);
                        Console.WriteLine(caseData.dataSimilarity[0].name);
                        Console.WriteLine(caseData.k);
                        Console.WriteLine(caseData.output.exercise_level_detail);
                        return caseData;
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
