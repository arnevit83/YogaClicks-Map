using System;
using System.Collections.Generic;
using System.Configuration;
using Common.ExceptionHandling;
using RestSharp;
using System.Linq;

namespace Clicks.Yoga.Tasks.NotificationDigestTask
{
    internal class Program
    {
        private Program()
        {
            Client = new RestClient(ConfigurationManager.AppSettings["Clicks.Yoga.Tasks.Server"]);
        }

        private RestClient Client { get; set; }

        private static void Main(string[] args)
        {
            try
            {
                int userId = 0;

                if (args.Length > 0)
                {
                    if (!int.TryParse(args[0], out userId))
                    {
                        Usage();
                    };
                }

                new Program().Run(userId > 0 ? userId : (int?)null);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
                throw;
            }
        }

        private void Run(int? userId)
        {
            var userIds = userId.HasValue ? new List<int> { userId.Value } : GetUserIds();

            if (userIds.Any())
            {
                foreach (var id in userIds)
                {
                    Console.WriteLine("Sending notification digest to user {0}.", id);

                    SendDigest(id);
                }
            }

        }

        private IEnumerable<int> GetUserIds()
        {
            var request = new RestRequest("api/emailnotifications/overdueuserids", Method.GET);

            var response = Client.Execute<List<int>>(request);

            return response.Data;
        }

        private void SendDigest(int userId)
        {
            var request = new RestRequest("api/emailnotifications/senddigestemail", Method.POST)
            {
                RequestFormat = DataFormat.Json
            };

            request.AddBody(new
            {
                UserId = userId
            });

            Client.Execute(request);
        }

        private static void Usage()
        {
            Console.WriteLine("Clicks.Yoga.Tasks.NotificationDigestTask [UserId]");
            Console.WriteLine("");
        }
    }
}