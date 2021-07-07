using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Context
{
    public class SuperUserSecurityContext : ISecurityContext
    {
        public bool Authenticated
        {
            get { return true; }
        }

        public bool HasActor
        {
            get { return false; }
        }

        public User CurrentUser
        {
            get { return null; }
        }

        public Profile CurrentProfile
        {
            get { return null; }
        }

        public IActor CurrentActor
        {
            get { return null; }
        }

        public IList<EntityHandle> AvailableActors
        {
            get { return new List<EntityHandle>(); }
        }

        public string ClientAddress
        {
            get { return IPAddress.Loopback.ToString(); }
        }

        public bool CanUpdate(ISecurable securable)
        {
            return true;
        }

        public bool CanUpdateAny(params ISecurable[] securables)
        {
            return true;
        }

        public bool CanDelete(ISecurable securable)
        {
            return true;
        }

        public bool IsOwner(ISecurable securable)
        {
            return true;
        }

        public bool IsSuperUser()
        {
            return true;
        }

        public void SetCurrentUser(User user, bool persist) {}

        public void SetCurrentActor(IActor actor) {}

        public void RefreshProfile() {}

        public void RefreshActors() {}

        // DEPLOYMENT OF YODAMEDS - Remove this
        public bool IsYogaMeds()
        {
            var yogaMedsSession = HttpContext.Current.Session["YogaMeds"];

            if (yogaMedsSession != null)
            {
                if ((string)yogaMedsSession == "True")
                {
                    return true;
                }
            }
            return false;
        }

        public string ActivateYogaMeds()
        {
            HttpContext.Current.Session["YogaMeds"] = "True";
            return "True";
        }

        public string DisableYogaMeds()
        {
            HttpContext.Current.Session.Remove("YogaMeds");
            return "False";
        }

        // --
    }
}