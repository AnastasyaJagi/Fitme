using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FitmeApp.Models;
using FitmeApp.Models.SubModel.Request;
using FitmeApp.Services;
using FitmeApp.Settings;
using FitmeApp.Utilities.Helper;
using FitmeApp.Utilities.Models.Response;
using FitmeApp.Utils;
using Xamarin.Forms;

namespace FitmeApp.Repository
{
    public class UserRepository
    {
        private static readonly Lazy<UserRepository>
            lazy =
            new Lazy<UserRepository>
            (() => new UserRepository());
        public static UserRepository Instance { get { return lazy.Value; } }

        protected readonly string baseUrl = AppSettingsManager.Settings["BaseUrl"];
        protected readonly string userUrl = AppSettingsManager.Settings["UserUrl"];

        private UserRepository() { }

        public string CurrentUserId
        {
            get
            {
                string id = PreferencesWriter.UserId;
                if (id == "")
                {
                    id = "";
                }
                return id;
            }
            set => PreferencesWriter.UserId = value;
        }

        private User userData;
        public User UserData
        {
            get => userData;
            set
            {
                userData = value;
            }
        }

        public async Task<User> GetUser(String id)
        {
            try
            {
                var httpRequest = await HttpService.GetAsync<User>($"{baseUrl}{userUrl}/{id}"); //{AppSettingsManager.Settings["PertanyaanUrl"]}
                var msg = $"{(httpRequest.Successful ? "" : "Not ")} Found User";
                User userData = httpRequest.Result;
                Console.WriteLine(msg);
                return userData;
            }
            catch (Exception e)
            {
                Console.WriteLine("Api - an error has occured : " + e.Message);
                return null;
            }
        }

        public async Task<ResponseUser> AddUser(User user)
        {
            try
            {
                var url = $"{baseUrl}{userUrl}";
                if (await HttpServiceHelper.ProcessHttpRequestAsync<MobileUserRequest, ResponseUser>(
                        HttpMethod.Post, $"{url}",
                        new MobileUserRequest { name = user.name, email = user.email, password = user.password, username = user.username, age = user.age, height = user.height, weight = user.weight, gender = user.age, activityId = "null", bodygoalId = "null" })
                    is BaseHttpResponse<ResponseUser> response)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        //AppSettings.AddOrUpdateValue("Username", UserName);
                        Console.WriteLine(response.StatusCode);
                        Console.WriteLine(response.Message);
                        Console.WriteLine(response.Result);
                        CurrentUserId = response.Result._id;
                        user._id = CurrentUserId;
                        UserData = user;
                        // Save to JSON Local
                        FilesWriter.SharedInstance.SaveToJson(UserData, "user.json");
                        Device.BeginInvokeOnMainThread(async () => {
                            await App.Current.MainPage.DisplayAlert("Success", response.Message, "Ok");
                        });
                        return response.Result;
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () => {
                            await App.Current.MainPage.DisplayAlert("Alert", response.Message, "Ok");
                        });
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Api - an error has occured : " + e.Message);
                return null;
            }
        }

        public async Task<ResponseUser> UpdateUser(User user)
        {
            try
            {
                var url = $"{baseUrl}{userUrl}/{user._id}";
                if (await HttpServiceHelper.ProcessHttpRequestAsync<MobileUserRequest, ResponseUser>(
                        HttpMethod.Put, $"{url}",
                        new MobileUserRequest { name = user.name, email = user.email, password = user.password, username = user.username, age = user.age, height = user.height, weight = user.weight, gender = user.gender, activityId = user.activityId, bodygoalId = user.bodygoalId })
                    is BaseHttpResponse<ResponseUser> response)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Console.WriteLine(response.StatusCode);
                        Console.WriteLine(response.Message);
                        UserData = user;
                        FilesWriter.SharedInstance.SaveToJson(UserData, "user.json");
                        Device.BeginInvokeOnMainThread(async () => {
                            await App.Current.MainPage.DisplayAlert("Success", response.Message, "Ok");
                        });
                        return response.Result;
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () => {
                            await App.Current.MainPage.DisplayAlert("Alert", response.Message, "Ok");
                        });
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Api - an error has occured : " + e.Message);
                return null;
            }
        }
    }
}
