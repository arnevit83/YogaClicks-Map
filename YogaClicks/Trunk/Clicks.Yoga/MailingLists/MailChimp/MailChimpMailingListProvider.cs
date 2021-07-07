using System;
using System.Configuration;
using Common.ExceptionHandling;
using RestSharp;

namespace Clicks.Yoga.MailingLists.MailChimp
{
    public class MailChimpMailingListProvider : IMailingListProvider
    {
        public void Subsribe(MailingList list, string address, string forename, string surname)
        {
            var body = new
            {
                apikey = GetApiKey(),
                id = GetListId(list),
                email_address = address,
                double_optin = false,
                update_existing = true,
                send_welcome = false,
                merge_vars = new
                {
                    FNAME = forename,
                    LNAME = surname
                }
            };

            try
            {
                Execute("1.3/?method=listSubscribe", body);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        public void Unsubscribe(MailingList list, string address)
        {
            var body = new
            {
                apikey = GetApiKey(),
                id = GetListId(list),
                email_address = address,
                send_goodbye = false
            };

            try
            {
                Execute("1.3/?method=listUnsubscribe", body);
            }
            catch (MailingListAddressUnknownException)
            {
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        public void ChangeEmailAddress(string currentAddress, string newAddress)
        {
            foreach (var list in new MailingList[] { MailingList.Service, MailingList.Newsletter })
            {
                var body = new
                {
                    apikey = GetApiKey(),
                    id = GetListId(list),
                    email_address = currentAddress,
                    merge_vars = new
                    {
                        EMAIL = newAddress
                    }
                };

                try
                {
                    Execute("1.3/?method=listUpdateMember", body);
                }
                catch (MailingListAddressUnknownException)
                {
                }
                catch (Exception ex)
                {
                    ExceptionHandler.Handle(ex);
                }
            }
        }

        public void AddToGroup(MailingList list, string address, string option, string group)
        {
            var body = new
            {
                apikey = GetApiKey(),
                id = GetListId(list),
                email_address = address,
                merge_vars = new
                {
                    GROUPINGS = new[]
                    {
                        new { name = option, groups = group }
                    }
                },
                replace_interests = false
            };

            try
            {
                Execute("1.3/?method=listUpdateMember", body);
            }
            catch (MailingListAddressUnknownException)
            {
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        protected void Execute(string uri, object body)
        {
            var client = new RestClient(GetApiHost());
            var request = new RestRequest(uri, Method.POST);

            request.RequestFormat = DataFormat.Json;

            request.AddBody(body);

            var response = client.Execute(request);

            if (response.Content != "true")
            {
                dynamic error = new JsonFx.Json.JsonReader().Read(response.Content);

                if (error.code == 215 || error.code == 232)
                {
                    throw new MailingListAddressUnknownException();
                }
                else
                {
                    throw new MailingListProviderException(string.Format("{0}: {1}", error.code, error.error));
                }
            }
        }

        protected string GetApiHost()
        {
            var value = ConfigurationManager.AppSettings["Clicks.Yoga.MailingLists.MailChimp.ApiHost"];
            if (value == null) throw new ConfigurationErrorsException("Missing MailChimp app setting.");
            return value;
        }

        protected string GetApiKey()
        {
            var value = ConfigurationManager.AppSettings["Clicks.Yoga.MailingLists.MailChimp.ApiKey"];
            if (value == null) throw new ConfigurationErrorsException("Missing MailChimp app setting.");
            return value;
        }

        protected string GetListId(MailingList list)
        {
            var key = string.Format("Clicks.Yoga.MailingLists.MailChimp.Lists.{0}", list.ToString());
            var value = ConfigurationManager.AppSettings[key];
            if (value == null) throw new ConfigurationErrorsException("Missing MailChimp app setting.");
            return value;
        }
    }
}