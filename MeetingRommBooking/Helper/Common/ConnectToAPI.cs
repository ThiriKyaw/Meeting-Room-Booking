using BusinessLayer.BusinessModels;
using DataLayer.DBModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MeetingRommBooking.Helper.Common
{
    public class ConnectToAPI
    {
       

        public async Task<List<MeetingRoomModel>> GetAllMeetingRoomAsync()
        {
            try
            {
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build();
                string urlParameters = config["ApiUrl:GetAllMeetingRoom"] + config["ApiUrl:APIversion"];
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(config["ApiUrl:URL"]);
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<MeetingRoomModel>>(content);
                    return result;
                }
                client.Dispose();
                return null;
            }
            catch (Exception ex) {
                return null;
            }
        }

        public async Task<List<MeetingEventModel>> GetAllEventsAsync()
        {
            try
            {
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build();
                string urlParameters = config["ApiUrl:GetAllMeetingEvents"] + config["ApiUrl:APIversion"];
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(config["ApiUrl:URL"]);
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<MeetingEventModel>>(content);
                    return result;
                }
                client.Dispose();
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<MeetingEventModel> BookMeeting(MeetingEventModel meetingEventMdl)
        {
            try
            {
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build();
                string urlParameters = config["ApiUrl:BookMeetingRoom"] + config["ApiUrl:APIversion"];
                var stringContent = new StringContent(JsonConvert.SerializeObject(meetingEventMdl), Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(config["ApiUrl:URL"]);
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PostAsync(urlParameters, stringContent).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
                if (response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<MeetingEventModel>(content);
                    if (!string.IsNullOrEmpty(result.EventId)) 
                    {
                        return result;
                    }
                    return new MeetingEventModel();

                }
                client.Dispose();
                return null;
            }
            catch (Exception ex) 
            {
                return null;
            }
        }

        public async Task<string> UpdateMeeting(MeetingEventModel meetingEventMdl)
        {
            try
            {
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build();
                string urlParameters = config["ApiUrl:UpdateBooking"] + config["ApiUrl:APIversion"];
                var stringContent = new StringContent(JsonConvert.SerializeObject(meetingEventMdl), Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(config["ApiUrl:URL"]);
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PostAsync(urlParameters, stringContent).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
                if (response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<string>(content);
                    return result;
                }
                client.Dispose();
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<MeetingEventModel>> GetEventsbyRoomIdAsync(string roomid)
        {
            try
            {
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build();
                string urlParameters = config["ApiUrl:GetMeetingEventsByRoomId"] + roomid + config["ApiUrl:APIversion"];
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(config["ApiUrl:URL"]);
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<MeetingEventModel>>(content);
                    return result;
                }
                client.Dispose();
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<string> CancelMeeting(MeetingEventModel meetingEventMdl)
        {
            try
            {
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build();
                string urlParameters = config["ApiUrl:CancelBooking"] + config["ApiUrl:APIversion"];
                var stringContent = new StringContent(JsonConvert.SerializeObject(meetingEventMdl), Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(config["ApiUrl:URL"]);
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PostAsync(urlParameters, stringContent).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<string>(content);
                    return result;
                }
                client.Dispose();
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
