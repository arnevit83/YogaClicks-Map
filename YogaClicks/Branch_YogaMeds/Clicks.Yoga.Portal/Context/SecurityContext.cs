using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Security;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Repositories;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Context;
using Newtonsoft.Json;

namespace Clicks.Yoga.Portal.Context
{
    public class SecurityContext : ISecurityContext, IPortalSecurityContext
    {
        private const int FormsAuthenticationVersion = 2;
        private const string AuthenticationCookieName = "Auth";
        private const string AuthenticationCookiePath = "/";
        private static readonly TimeSpan AuthenticationCookieExpiryTimeSpan = new TimeSpan(365, 0, 0, 0);

        private const string ActorCookieName = "Actor";

        private const string CurrentProfileKey = "SecurityContext.CurrentProfile";
        private const string CurrentActorKey = "SecurityContext.CurrentActor";
        private const string AvailableActorsKey = "SecurityContext.AvailableActors";

        private User _currentUser;
        private bool _currentUserKnown;
        private IActor _currentActor;

        public SecurityContext(
            IEntityService entityService,
            IRepository<User> userRepository,
            IRepository<Profile> profileRepository,
            IRepository<EntityHandle> entityHandleRepository)
        {
            EntityService = entityService;
            UserRepository = userRepository;
            ProfileRepository = profileRepository;
            EntityHandleRepository = entityHandleRepository;
        }

        private IEntityService EntityService { get; set; }

        private IRepository<User> UserRepository { get; set; }

        private IRepository<Profile> ProfileRepository { get; set; }

        private IRepository<EntityHandle> EntityHandleRepository { get; set; }

        public bool Authenticated
        {
            get { return CurrentUser != null; }
        }

        public bool HasActor
        {
            get { return CurrentActor != null; }
        }

        public User CurrentUser
        {
            get
            {
                if (!_currentUserKnown)
                {
                    var id = GetUserFromCookie();
                    if (id != null)
                    {
                        _currentUser = UserRepository.Get(u => u.Id == id);

                        if (!_currentUser.Active)
                            _currentUser = null;
                    }
                    _currentUserKnown = true;
                }

                return _currentUser;
            }
        }

        public Profile CurrentProfile
        {
            get { return CurrentUser == null ? null : CurrentUser.Profile; }
        }

        public IActor CurrentActor
        {
            get
            {
                if (!Authenticated) return null;

                if (_currentActor == null)
                {
                    _currentActor = GetActorFromCookie();

                    if (_currentActor == null || !IsOwner(_currentActor))
                    {
                        _currentActor = null;

                        if (CurrentUser.Profile.Professional)
                        {
                            if (AvailableActors.Count > 0)
                            {
                                var handle = AvailableActors.FirstOrDefault();
                                _currentActor = EntityService.GetEntity<IActor>(handle);
                            }
                        }
                        else
                        {
                            _currentActor = CurrentUser.Profile;
                        }
                    }
                }

                return _currentActor;
            }
        }

        public IList<EntityHandle> AvailableActors
        {
            get
            {
                if (!Authenticated) return new List<EntityHandle>();

                return EntityHandleRepository
                    .Include(h => h.EntityType)
                    .Where(h => h.Owner.Id == _currentUser.Id)
                    .Where(h => h.EntityType.IsActor)
                    .Where(h => h.Active)

                    .OrderBy(h => h.EntityType.ActorOrdinal)
                    .ThenBy(h => h.Name)
                    .ToList();
            }
        }

        public string ClientAddress
        {
            get { return HttpContext.Current.Request.UserHostAddress; }
        }

        public bool CanUpdate(ISecurable securable)
        {
            return Authenticated && CurrentUser.CanUpdate(securable);
        }

        public bool CanUpdateAny(params ISecurable[] securables)
        {
            foreach (var securable in securables)
            {
                if (CanUpdate(securable)) return true;
            }

            return false;
        }

        public bool CanDelete(ISecurable securable)
        {
            return Authenticated && CurrentUser.CanDelete(securable);
        }

        public bool IsOwner(ISecurable securable)
        {
            return Authenticated && CurrentUser.IsOwner(securable);
        }

        public bool IsSuperUser()
        {
            return Authenticated && CurrentUser.IsSuperUser;
        }

        public void SetCurrentUser(User user, bool persist)
        {
            SetUserCookie(user == null ? null : (int?)user.Id, persist);

            _currentUser = user;
            _currentUserKnown = true;

            RefreshProfile();
            RefreshActors();
        }

        public void SetCurrentActor(IActor actor)
        {
            SetActorCookie(actor);

            _currentActor = actor;

            RefreshActors();
        }

        public void RefreshProfile()
        {
            RemoveSessionValue(CurrentProfileKey);
        }

        public void RefreshActors()
        {
            RemoveSessionValue(CurrentActorKey);
            RemoveSessionValue(AvailableActorsKey);
        }

        CurrentProfileModel IPortalSecurityContext.CurrentProfile
        {
            get
            {
                if (!Authenticated) return null;

                var profile = GetSessionValue<CurrentProfileModel>(CurrentProfileKey);

                if (profile == null)
                {
                    profile = new CurrentProfileModel(CurrentProfile);
                    SetSessionValue(CurrentProfileKey, profile);
                }

                return profile;
            }
        }

        CurrentActorModel IPortalSecurityContext.CurrentActor
        {
            get
            {
                if (!Authenticated) return null;

                var actor = GetSessionValue<CurrentActorModel>(CurrentActorKey);

                if (actor == null)
                {
                    var handle = EntityService.GetEntityHandle(CurrentActor);
                    actor = new CurrentActorModel(handle);
                    SetSessionValue(CurrentActorKey, actor);
                }

                return actor;
            }
        }

        IList<AvailableActorModel> IPortalSecurityContext.AvailableActors
        {
            get
            {
                if (!Authenticated) return new List<AvailableActorModel>();

                var actors = GetSessionValue<IList<AvailableActorModel>>(AvailableActorsKey);

                if (actors == null)
                {
                    actors = AvailableActors
                        .Select(h => new AvailableActorModel(h)
                        {
                            Current =
                                h.EntityId == CurrentActor.Id &&
                                h.EntityType.Name == CurrentActor.GetEntityTypeName()
                        })
                        .ToList();

                    SetSessionValue(AvailableActorsKey, actors);
                }

                return actors;
            }
        }

        private int? GetUserFromCookie()
        {
            bool overridden;
            int userId;

            bool.TryParse(ConfigurationManager.AppSettings["Clicks.Yoga.Security.UserOverridden"], out overridden);
            int.TryParse(ConfigurationManager.AppSettings["Clicks.Yoga.Security.UserId"], out userId);

            if (overridden)
            {
                return userId;
            }

            var cookie = HttpContext.Current.Request.Cookies[AuthenticationCookieName];

            if (cookie != null)
            {
                try
                {
                    int id;
                    var ticket = FormsAuthentication.Decrypt(cookie.Value);

                    if (int.TryParse(ticket.Name, out id))
                    {
                        return id;
                    }
                }
                catch (CryptographicException)
                {
                    return null;
                }
            }

            return null;
        }

        private void SetUserCookie(int? userId, bool persist)
        {
            HttpCookie cookie = null;

            if (userId != null)
            {

                var ticket = new FormsAuthenticationTicket(
                    FormsAuthenticationVersion,
                    userId.Value.ToString(CultureInfo.InvariantCulture),
                    DateTime.Now,
                    persist ? DateTime.Now.Add(AuthenticationCookieExpiryTimeSpan) : DateTime.MinValue,
                    true,
                    string.Empty,
                    AuthenticationCookiePath);

                var encrypted = FormsAuthentication.Encrypt(ticket);

                cookie = new HttpCookie(AuthenticationCookieName, encrypted)
                {
                    Expires = ticket.Expiration,
                    Path = AuthenticationCookiePath
                };
            }
            else
            {
                cookie = new HttpCookie(AuthenticationCookieName, "")
                {
                    Expires = DateTime.Now.AddDays(-1),
                    Path = AuthenticationCookiePath
                };
            }

            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        private IActor GetActorFromCookie()
        {
            var cookie = HttpContext.Current.Request.Cookies["Actor"];

            if (cookie == null) return null;

            Tuple<int, string> reference = null;

            try
            {
                reference = JsonConvert.DeserializeObject<Tuple<int, string>>(cookie.Value);
            }
            catch (JsonSerializationException) {}

            if (reference == null) return null;

            var type = EntityService.GetEntityType(reference.Item2);

            if (type == null) return null;

            return EntityService.GetEntity<IActor>(reference.Item1, type);
        }

        private void SetActorCookie(IActor actor)
        {
            var reference = new Tuple<int, string>(actor.Id, actor.GetEntityTypeName());
            var serialized = JsonConvert.SerializeObject(reference);
            var cookie = new HttpCookie(ActorCookieName, serialized);

            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        private T GetSessionValue<T>(string name) where T : class
        {
            if (HttpContext.Current.Session != null)
            {
                return HttpContext.Current.Session[name] as T;
            }

            return null;
        }

        private void SetSessionValue(string name, object value)
        {
            if (HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session[name] = value;
            }
        }

        private void RemoveSessionValue(string name)
        {
            if (HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session.Remove(name);
            }
        }
    }
}