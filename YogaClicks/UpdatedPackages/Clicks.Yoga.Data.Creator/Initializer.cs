using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Group = Clicks.Yoga.Domain.Entities.Group;
using TeacherService = Clicks.Yoga.Domain.Entities.TeacherService;
using VenueService = Clicks.Yoga.Domain.Entities.VenueService;

namespace Clicks.Yoga.Data.Creator
{
    internal class Initializer
    {
        private YogaDataContext Database { get; set; }

        private IEntityContext Context { get; set; }

        private SqlConnection ImportConnection { get; set; }

        public Initializer(IEntityContext context)
        {
            Context = context;
        }

        public void Seed(YogaDataContext context)
        {
            Database = context;

            ImportConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["YogaClicksImport"].ConnectionString);
            ImportConnection.Open();

            //CreateAccreditationBodies();
            //CreateVenues();

            CreateTeacherTraining();

            /*
            CreateQuotes();
            CreateStyles();
            CreatePoses();
            CreateVenues();
            CreateTeacherTraining();
            CreateTestEntities();

            CreateEntityTypes();
            CreateGenders();
            CreateCountries();
            CreateTimescales();
            CreateLoginProviders();
            CreateImageTypes();
            CreateVideoTypes();
            CreateTeacherServices();
            CreateVenueServices();
            CreateVenueTypes();
            CreateVenueAmenities();
            CreateReviewExperiences();
            CreateReviewRatingSubjects();
            CreateGlossaryEntries();
            CreateAbilityLevels();
            CreatePoseCategories();
            CreateProfileQuestions();
            CreateNotificationTypes();
            CreateRequestTypes();
            CreateActivityTypes();
            CreateActivityRepeatFrequencies();
            CreateNewsStoryTypes();


            CreateSearchIndex();
            */

            //ImportConnection.Dispose();
        }

        private void CreateEntityTypes()
        {
            var controllers = new Dictionary<string, string>
            {
                { "AccreditationBody", "AccreditationBodys" },
                { "Group", "Groups" },
                { "Profile", "Profiles" },
                { "Pose", "Poses" },
                { "Style", "Styles" },
                { "StyleOrganisation", "StyleOrganisations" },
                { "Teacher", "Teachers" },
                { "TeacherTrainingCourse", "TeacherTrainingCourses" },
                { "TeacherTrainingOrganisation", "TeacherTrainingOrganisations" },
                { "Venue", "Venues" },
                { "WallPost", "WallPosts" }
            };

            var professional = new List<string>
            {
                "StyleOrganisation",
                "Teacher",
                "TeacherTrainingOrganisation",
                "Venue"
            };

            foreach (var type in Assembly.GetAssembly(typeof(Entity)).GetTypes())
            {
                if (type.IsSubclassOf(typeof(Entity)) && !type.IsAbstract)
                {
                    var name = Regex.Replace(type.Name, "(?<=[a-z])([A-Z])", " $1");
                    var plural = name.EndsWith("y") ? Regex.Replace(name, "y$", "ies") : name == "Profile" ? "Profiles" : string.Format("{0}s", name);

                    var entityType = new EntityType
                    {
                        Name = type.Name,
                        SystemName = type.FullName,
                        DisplayName = name,
                        DisplayPlural = plural,
                        Controller = controllers.ContainsKey(type.Name) ? controllers[type.Name] : null,
                        IsProfessional = professional.Contains(type.Name),
                        IsFanable = typeof(IFanable).IsAssignableFrom(type),
                        IsReviewable = typeof(IReviewable).IsAssignableFrom(type),
                        IsSearchable = typeof(ISearchable).IsAssignableFrom(type),
                        IsGalleryAssociate = typeof(IGalleryAssociate).IsAssignableFrom(type)
                    };

                    Context.EntityTypes.Add(entityType);
                }
            }

            Database.SaveChanges();
        }

        private void CreateGenders()
        {
            Context.Genders.Add(new Gender { Tag = "Male", Name = "Male" });
            Context.Genders.Add(new Gender { Tag = "Female", Name = "Female" });

            Database.SaveChanges();
        }

        private void CreateCountries()
        {
            using (var command = new SqlCommand("select * from Country", ImportConnection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Country country = new Country();

                        country.Code = EnsureValue<string>(reader["Code"]);
                        country.EnglishName = EnsureValue<string>(reader["Name"]);
                        country.Visible = true;

                        Context.Countries.Add(country);
                    }
                }
            }

            Database.SaveChanges();
        }

        private void CreateTimescales()
        {
            Context.Timescales.Add(new Timescale { Tag = "Day", Name = "Day", IsDay = true });
            Context.Timescales.Add(new Timescale { Tag = "Week", Name = "Week", IsWeek = true });
            Context.Timescales.Add(new Timescale { Tag = "Month", Name = "Month", IsMonth = true });
        }

        private void CreateLoginProviders()
        {
            Context.FederatedLoginProviders.Add(new FederatedLoginProvider { Tag = "Facebook", Name = "Facebook", IsFacebook = true });

            Database.SaveChanges();
        }

        private void CreateImageTypes()
        {
            Context.ImageTypes.Add(new ImageType { MimeType = "image/jpeg", Name = "JPEG", Extension = "jpg" });
            Context.ImageTypes.Add(new ImageType { MimeType = "image/png", Name = "PNG", Extension = "png" });
            Context.ImageTypes.Add(new ImageType { MimeType = "image/gif", Name = "GIF", Extension = "gif" });

            Database.SaveChanges();
        }

        private void CreateVideoTypes()
        {
            Context.VideoTypes.Add(new VideoType { Name = "YouTube", UrlPattern = @"youtube\.com.*[?&]v=([\w-]+)", EmbedHtml = "<iframe width=\"{1}\" height=\"{2}\" src=\"//www.youtube.com/embed/{0}\" frameborder=\"0\" allowfullscreen></iframe>" });
            Context.VideoTypes.Add(new VideoType { Name = "Vimeo", UrlPattern = @"vimeo\.com/(\d+)", EmbedHtml = "<iframe src=\"http://player.vimeo.com/video/{0}\" width=\"{1}\" height=\"{2}\" frameborder=\"0\" webkitAllowFullScreen mozallowfullscreen allowFullScreen></iframe>" });

            Database.SaveChanges();
        }

        private void CreateVenues()
        {
            var typeMappings = new Dictionary<string, string> {
                { "Studio", "Yoga studio/center" },
                { "Studio/ Retreat Centre", "Yoga studio/center" },
                { "Studio/ Spa", "Yoga studio/center" },
                { "Town Hall", "Town/village hall" },
                { "Outdoor Space", "Outdoor space (park, campsite)" }
            };

            var styleMappings = new Dictionary<string, string> {
                { "Prayanama", "Pranayama" },
                { "Sivanada", "Sivananda" },
                { "Baptiste Power Vinyasa", "Baptiste Power Vinyasa Yoga" },
                { "Mother and baby", "Mum and baby" },
                { "Resorative", "Restorative" },
                { "Vinyassa Flow", "Vinyasa Flow" },
                { "Baptiste Power Vinyassa", "Baptiste Power Vinyasa Yoga" },
                { "Prana Flow", "Prana Flow Yoga" },
                { "Yin", "Yin Yoga" },
                { "Hatha Flow", "Hatha" },
                { "Kudalini", "Kundalini" },
                { "Yoga for Sport", "Yoga for Sports" },
                { "Retorative", "Restorative" },
                { "Pilates and Yoga", "Yoga and Pilates" },
                { "Flow", "Vinyasa Flow" },
                { "Hot Yoga", "Hot" },
                { "Prenatal", "Pre-natal" },
                { "Vinyasa", "Vinyasa Flow" },
                { "Mocksha", "Moksha" },
                { "Hot Power", "Hot" },
                { "Forrect Yoga", "Forrest" },
                { "Hot Yin", "Hot" },
                { "Yoga Nidra", "Nidra" },
                { "Restoratiive", "Restorative" },
                { "Reetorative", "Restorative" },
                { "Mysore Self Practise", "Mysore Self Practice" },
                { "Mum and Toddller", "Mum and toddler" },
                { "Nia", "Nidra" },
                { "Baptise Power Vinyasa", "Baptiste Power Vinyasa Yoga" },
                { "Baptistte Power Vinyasa", "Baptiste Power Vinyasa Yoga" },
                { "Vinysasa Flow", "Vinyasa Flow" },
                { "Ho", "Hot" },
                { "Ahstanga", "Ashtanga" },
                { "Cores Strength", "Core Strength" },
                { "Prentatal", "Pre-natal" },
                { "Dynamic", "" },
                { "Tantric", "Tantra" },
                { "Mysore Seld Practice", "Mysore Self Practice" },
                { "Meditaiton", "Meditation" },
                { "Sivandana", "Sivananda" },
                { "Prenatal Gentle", "Pre-natal" },
                { "Prenata", "Pre-natal" },
                { "Prenatal Kids", "Pre-natal" },
                { "Juvamukti", "Jivamukti" },
                { "Satyandana", "Satyananda" },
                { "Beginner", "Beginners" },
                { "Dynnamic", "" },
                { "Meditate", "Meditation" },
                { "Mediation", "Meditation" },
                { "Kundalin", "Kundalini" },
                { "Prenstsl", "Pre-natal" },
                { "Vinyasa Flow Ashtanga", "Ashtanga" },
                { "Jivamuktii", "Jivamukti" },
                { "An usara", "Anusara" },
                { "Styles not listed", "" },
                { "Hatham Bikram", "Hatha" },
                { "Pranyama", "Pranayama" },
                { "Power Beginners", "Beginners" },
                { "Prana", "Prana Flow Yoga" },
                { "Mysore Self Practive", "Mysore Self Practice" },
                { "Freedom Style", "Freedom Style Yoga" },
                { "Baptiste Power Vinyasa Flow", "Baptiste Power Vinyasa Yoga" },
                { "Teen", "Teens" },
                { "Core Alignment", "Critical Alignment" },
                { "Prana Yoga", "Prana Flow Yoga" },
                { "Prana Yoga Flow", "Prana Flow Yoga" },
                { "Gentlre", "Gentle" },
                { "Ashtange", "Ashtanga" },
                { "Vinyaysa Flow", "Vinyasa Flow" },
                { "Senior", "Seniors" },
                { "Pregnancy", "Pre-natal" },
                { "Postnatal", "Post-natal" }
            };

            var venues = new List<Venue>();

            Venue venue = null;

            using (var command = new SqlCommand("select * from Venue", ImportConnection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        venue = new Venue();

                        venue.Active = true;
                        venue.Name = EnsureValue<string>(reader["Name"], "");
                        
                        if (reader["WebsiteUrl"] != DBNull.Value)
                        {
                            venue.Websites.Add(new Website(EnsureValue<string>(reader["WebsiteUrl"], "")));
                        }

                        venue.Address = new Address();

                        var code = EnsureValue<string>(reader["Country"], "");
                        var latitude = EnsureValue<double?>(reader["Latitude"], null);
                        var longitude = EnsureValue<double?>(reader["Longitude"], null);

                        venue.Address.Line1 = EnsureValue<string>(reader["Address1"], "");
                        venue.Address.Line2 = EnsureValue<string>(reader["Address2"], "");
                        venue.Address.City = EnsureValue<string>(reader["Town"], "");
                        venue.Address.Area = EnsureValue<string>(reader["County"], "");
                        venue.Address.Postcode = EnsureValue<string>(reader["Postcode"], "");
                        venue.Address.Country = Context.Countries.Get(c => c.Code == code);

                        if (latitude != null && longitude != null)
                        {
                            venue.Address.Location = new Geography.GeographicPoint(latitude.Value, longitude.Value);
                        }

                        venue.EmailAddress = EnsureValue<string>(reader["Email"], "");
                        venue.Telephone = EnsureValue<string>(reader["Telephone"], "");

                        var residential = EnsureValue(reader["Residential"], false);
                        var typeName = EnsureValue(reader["Type"], "");

                        if (typeMappings.ContainsKey(typeName))
                        {
                            typeName = typeMappings[typeName];
                        }

                        var type = Context.VenueTypes.FirstOrDefault(t => t.Name == typeName && t.IsResidential == residential);

                        if (!string.IsNullOrWhiteSpace(typeName) && type == null)
                        {
                            throw new Exception(string.Format("Invalid venue type '{0}'.", typeName));
                        }

                        venue.Type = type;

                        var styleNames = Regex.Split(EnsureValue(reader["Styles"], ""), "\\s*[.,]\\s*");

                        foreach (var styleName in styleNames)
                        {
                            if (string.IsNullOrWhiteSpace(styleName)) continue;

                            var realName = styleName;

                            realName = Regex.Replace(realName, "[.']", "").Trim();

                            if (styleMappings.ContainsKey(realName))
                            {
                                realName = styleMappings[realName];

                                if (realName == "") continue;
                            }

                            var style = Context.Styles.FirstOrDefault(s => s.Name == realName);

                            if (style == null)
                            {
                                throw new Exception(string.Format("Invalid style '{0}'.", realName));
                            }

                            venue.Styles.Add(style);
                        }

                        venues.Add(venue);

                        Context.Venues.Add(venue);
                    }
                }
            }

            Database.SaveChanges();
        }

        private void CreateStyles()
        {
            var groups = new Dictionary<string, StyleTraitGroup>();
            var traits = new Dictionary<string, StyleTrait>();

            var styles = new List<Style>();

            StyleTraitGroup group;
            StyleTrait trait;

            group = new StyleTraitGroup { Name = "Mind", DisplayOrder = 2 }; Context.StyleTraitGroups.Add(group); groups[group.Name] = group;
            group = new StyleTraitGroup { Name = "Body", DisplayOrder = 1 }; Context.StyleTraitGroups.Add(group); groups[group.Name] = group;
            group = new StyleTraitGroup { Name = "Spirit", DisplayOrder = 3 }; Context.StyleTraitGroups.Add(group); groups[group.Name] = group;
            group = new StyleTraitGroup { Name = "Teaching focus", DisplayOrder = 4 }; Context.StyleTraitGroups.Add(group); groups[group.Name] = group;
            group = new StyleTraitGroup { Name = "Type of person", DisplayOrder = 5 }; Context.StyleTraitGroups.Add(group); groups[group.Name] = group;
            group = new StyleTraitGroup { Name = "Environment", DisplayOrder = 6 }; Context.StyleTraitGroups.Add(group); groups[group.Name] = group;

            trait = new StyleTrait { Name = "Asana/poses", Group = groups["Body"] }; Context.StyleTraits.Add(trait); traits[trait.Name] = trait;
            trait = new StyleTrait { Name = "Cardio fitness", Group = groups["Body"] }; Context.StyleTraits.Add(trait); traits[trait.Name] = trait;
            trait = new StyleTrait { Name = "Flowing movement", Group = groups["Body"] }; Context.StyleTraits.Add(trait); traits[trait.Name] = trait;
            trait = new StyleTrait { Name = "Gentle exercise", Group = groups["Body"] }; Context.StyleTraits.Add(trait); traits[trait.Name] = trait;
            trait = new StyleTrait { Name = "Physical therapy", Group = groups["Body"] }; Context.StyleTraits.Add(trait); traits[trait.Name] = trait;
            trait = new StyleTrait { Name = "Relaxation", Group = groups["Body"] }; Context.StyleTraits.Add(trait); traits[trait.Name] = trait;
            trait = new StyleTrait { Name = "Affirmations", Group = groups["Mind"] }; Context.StyleTraits.Add(trait); traits[trait.Name] = trait;
            trait = new StyleTrait { Name = "Breathing exercises", Group = groups["Mind"] }; Context.StyleTraits.Add(trait); traits[trait.Name] = trait;
            trait = new StyleTrait { Name = "Chanting", Group = groups["Mind"] }; Context.StyleTraits.Add(trait); traits[trait.Name] = trait;
            trait = new StyleTrait { Name = "Hand gestures", Group = groups["Mind"] }; Context.StyleTraits.Add(trait); traits[trait.Name] = trait;
            trait = new StyleTrait { Name = "Mantra", Group = groups["Mind"] }; Context.StyleTraits.Add(trait); traits[trait.Name] = trait;
            trait = new StyleTrait { Name = "Meditation", Group = groups["Mind"] }; Context.StyleTraits.Add(trait); traits[trait.Name] = trait;
            trait = new StyleTrait { Name = "Emotionally restorative", Group = groups["Spirit"] }; Context.StyleTraits.Add(trait); traits[trait.Name] = trait;
            trait = new StyleTrait { Name = "Energy flow", Group = groups["Spirit"] }; Context.StyleTraits.Add(trait); traits[trait.Name] = trait;
            trait = new StyleTrait { Name = "Self inquiry", Group = groups["Spirit"] }; Context.StyleTraits.Add(trait); traits[trait.Name] = trait;
            trait = new StyleTrait { Name = "Selfless service", Group = groups["Spirit"] }; Context.StyleTraits.Add(trait); traits[trait.Name] = trait;
            trait = new StyleTrait { Name = "Whole-life approach", Group = groups["Spirit"] }; Context.StyleTraits.Add(trait); traits[trait.Name] = trait;
            trait = new StyleTrait { Name = "Alignment", Group = groups["Teaching focus"] }; Context.StyleTraits.Add(trait); traits[trait.Name] = trait;
            trait = new StyleTrait { Name = "Anatomy", Group = groups["Teaching focus"] }; Context.StyleTraits.Add(trait); traits[trait.Name] = trait;
            trait = new StyleTrait { Name = "Support props", Group = groups["Teaching focus"] }; Context.StyleTraits.Add(trait); traits[trait.Name] = trait;
            trait = new StyleTrait { Name = "Personalised", Group = groups["Teaching focus"] }; Context.StyleTraits.Add(trait); traits[trait.Name] = trait;
            trait = new StyleTrait { Name = "Set routines", Group = groups["Teaching focus"] }; Context.StyleTraits.Add(trait); traits[trait.Name] = trait;
            trait = new StyleTrait { Name = "Maternal", Group = groups["Type of person"] }; Context.StyleTraits.Add(trait); traits[trait.Name] = trait;
            trait = new StyleTrait { Name = "Kids", Group = groups["Type of person"] }; Context.StyleTraits.Add(trait); traits[trait.Name] = trait;
            trait = new StyleTrait { Name = "Teens", Group = groups["Type of person"] }; Context.StyleTraits.Add(trait); traits[trait.Name] = trait;
            trait = new StyleTrait { Name = "Senior", Group = groups["Type of person"] }; Context.StyleTraits.Add(trait); traits[trait.Name] = trait;
            trait = new StyleTrait { Name = "Partner", Group = groups["Type of person"] }; Context.StyleTraits.Add(trait); traits[trait.Name] = trait;
            trait = new StyleTrait { Name = "Beginners", Group = groups["Type of person"] }; Context.StyleTraits.Add(trait); traits[trait.Name] = trait;
            trait = new StyleTrait { Name = "Hot room", Group = groups["Environment"] }; Context.StyleTraits.Add(trait); traits[trait.Name] = trait;
            trait = new StyleTrait { Name = "Music", Group = groups["Environment"] }; Context.StyleTraits.Add(trait); traits[trait.Name] = trait;

            var imageType = Context.ImageTypes.Get(e => e.MimeType == "image/jpeg");
            var directoryImageType = Context.ImageTypes.Get(e => e.MimeType == "image/png");

            using (var command = new SqlCommand("select * from Style", ImportConnection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var style = new Style
                        {
                            Name = EnsureValue<string>(reader["Style Name"], ""),
                            Founder = EnsureValue<string>(reader["Founder"], ""),
                            FoundingTime = EnsureValue<string>(reader["Founded"], ""),
                            Description = EnsureValue<string>(reader["Description"], ""),
                            ImageCourtesyOf = EnsureValue(reader["Courtesy"], "")
                        };

                        if (style.Name.Trim() == "") continue;

                        style.Image = new Image
                        {
                            Path = "Style.Image",
                            Guid = EnsureValue<Guid>(reader["ImageGuid"]),
                            Type = imageType
                        };

                        style.DirectoryImage = new Image {
                            Path = "Style.DirectoryImage",
                            Guid = EnsureValue<Guid>(reader["DirectoryImageGuid"]),
                            Type = directoryImageType
                        };

                        foreach (var name in Regex.Split(EnsureValue<string>(reader["Selector Words"], ""), "\\s*,\\s*"))
                        {
                            if (traits.ContainsKey(name))
                            {
                                trait = traits[name];
                            }
                            else
                            {
                                throw new Exception(string.Format("Unknown trait {0}.", name));
                            }

                            style.Traits.Add(trait);
                        }

                        Context.Styles.Add(style);

                        styles.Add(style);
                    }
                }
            }

            Database.SaveChanges();
        }

        private void CreateTeacherServices()
        {
            Context.TeacherServices.Add(new TeacherService { Name = "Classes", DisplayOrder = 1 });
            Context.TeacherServices.Add(new TeacherService { Name = "Kids", DisplayOrder = 7 });
            Context.TeacherServices.Add(new TeacherService { Name = "Holidays", DisplayOrder = 4 });
            Context.TeacherServices.Add(new TeacherService { Name = "Teens", DisplayOrder = 8 });
            Context.TeacherServices.Add(new TeacherService { Name = "Pre-natal", DisplayOrder = 9 });
            Context.TeacherServices.Add(new TeacherService { Name = "Seniors", DisplayOrder = 10 });
            Context.TeacherServices.Add(new TeacherService { Name = "Beginners", DisplayOrder = 11 });
            Context.TeacherServices.Add(new TeacherService { Name = "Private lessons", DisplayOrder = 12 });
            Context.TeacherServices.Add(new TeacherService { Name = "Workshops", DisplayOrder = 2 });
            Context.TeacherServices.Add(new TeacherService { Name = "Corporate", DisplayOrder = 13 });
            Context.TeacherServices.Add(new TeacherService { Name = "Residential weekends", DisplayOrder = 3 });
            Context.TeacherServices.Add(new TeacherService { Name = "Teacher training", DisplayOrder = 5 });
            Context.TeacherServices.Add(new TeacherService { Name = "Treatments/therapies", DisplayOrder = 14 });
            Context.TeacherServices.Add(new TeacherService { Name = "Post-natal", DisplayOrder = 6 });

            Database.SaveChanges();
        }

        private void CreateVenueAmenities()
        {
            Context.VenueAmenities.Add(new VenueAmenity { Name = "Sprung studio floor" });
            Context.VenueAmenities.Add(new VenueAmenity { Name = "Studio mirrors" });
            Context.VenueAmenities.Add(new VenueAmenity { Name = "Studio rope wall" });
            Context.VenueAmenities.Add(new VenueAmenity { Name = "Practice room views" });
            Context.VenueAmenities.Add(new VenueAmenity { Name = "Communal area" });
            Context.VenueAmenities.Add(new VenueAmenity { Name = "Juice bar" });
            Context.VenueAmenities.Add(new VenueAmenity { Name = "Single occupancy bedrooms" });
            Context.VenueAmenities.Add(new VenueAmenity { Name = "Private bathrooms" });
            Context.VenueAmenities.Add(new VenueAmenity { Name = "Pool" });
            Context.VenueAmenities.Add(new VenueAmenity { Name = "Sun loungers" });
            Context.VenueAmenities.Add(new VenueAmenity { Name = "Close to beach" });
            Context.VenueAmenities.Add(new VenueAmenity { Name = "Walking/trekking" });
            Context.VenueAmenities.Add(new VenueAmenity { Name = "Area of outstanding natural beauty" });
            Context.VenueAmenities.Add(new VenueAmenity { Name = "Disabled access" });
            Context.VenueAmenities.Add(new VenueAmenity { Name = "Easy transport links" });

            Database.SaveChanges();
        }

        private void CreateVenueServices()
        {
            Context.VenueServices.Add(new VenueService { Name = "Classes", DisplayOrder = 1 });
            Context.VenueServices.Add(new VenueService { Name = "Kids", DisplayOrder = 7 });
            Context.VenueServices.Add(new VenueService { Name = "Holidays", DisplayOrder = 4 });
            Context.VenueServices.Add(new VenueService { Name = "Teens", DisplayOrder = 8 });
            Context.VenueServices.Add(new VenueService { Name = "Pre-natal", DisplayOrder = 9 });
            Context.VenueServices.Add(new VenueService { Name = "Seniors", DisplayOrder = 10 });
            Context.VenueServices.Add(new VenueService { Name = "Beginners", DisplayOrder = 11 });
            Context.VenueServices.Add(new VenueService { Name = "Private lessons", DisplayOrder = 12 });
            Context.VenueServices.Add(new VenueService { Name = "Workshops", DisplayOrder = 2 });
            Context.VenueServices.Add(new VenueService { Name = "Corporate", DisplayOrder = 13 });
            Context.VenueServices.Add(new VenueService { Name = "Residential weekends", DisplayOrder = 3 });
            Context.VenueServices.Add(new VenueService { Name = "Teacher training", DisplayOrder = 5 });
            Context.VenueServices.Add(new VenueService { Name = "Treatments/therapies", DisplayOrder = 14 });
            Context.VenueServices.Add(new VenueService { Name = "Post-natal", DisplayOrder = 6 });

            Database.SaveChanges();
        }

        private void CreateVenueTypes()
        {
            Context.VenueTypes.Add(new VenueType { Name = "Yoga studio/center", IsResidential = false });
            Context.VenueTypes.Add(new VenueType { Name = "Retreat centre", IsResidential = false });
            Context.VenueTypes.Add(new VenueType { Name = "Gym", IsResidential = false });
            Context.VenueTypes.Add(new VenueType { Name = "Religious space", IsResidential = false });
            Context.VenueTypes.Add(new VenueType { Name = "Community hall", IsResidential = false });
            Context.VenueTypes.Add(new VenueType { Name = "School hall", IsResidential = false });
            Context.VenueTypes.Add(new VenueType { Name = "Town/village hall", IsResidential = false });
            Context.VenueTypes.Add(new VenueType { Name = "Shop", IsResidential = false });
            Context.VenueTypes.Add(new VenueType { Name = "Ashram", IsResidential = false });
            Context.VenueTypes.Add(new VenueType { Name = "Spa", IsResidential = false });
            Context.VenueTypes.Add(new VenueType { Name = "Office", IsResidential = false });
            Context.VenueTypes.Add(new VenueType { Name = "Beach hut", IsResidential = false });
            Context.VenueTypes.Add(new VenueType { Name = "Yurt", IsResidential = false });
            Context.VenueTypes.Add(new VenueType { Name = "Outdoor space (park, campsite)", IsResidential = false });
            Context.VenueTypes.Add(new VenueType { Name = "Hotel", IsResidential = false });
            Context.VenueTypes.Add(new VenueType { Name = "B&B", IsResidential = false });
            Context.VenueTypes.Add(new VenueType { Name = "Self catering", IsResidential = false });

            Context.VenueTypes.Add(new VenueType { Name = "Retreat centre", IsResidential = true });
            Context.VenueTypes.Add(new VenueType { Name = "Private home", IsResidential = true });
            Context.VenueTypes.Add(new VenueType { Name = "Spa", IsResidential = true });
            Context.VenueTypes.Add(new VenueType { Name = "Beach hut", IsResidential = true });
            Context.VenueTypes.Add(new VenueType { Name = "Ashram", IsResidential = true });
            Context.VenueTypes.Add(new VenueType { Name = "Hotel", IsResidential = true });
            Context.VenueTypes.Add(new VenueType { Name = "B&B", IsResidential = true });
            Context.VenueTypes.Add(new VenueType { Name = "Self catering", IsResidential = true });
            Context.VenueTypes.Add(new VenueType { Name = "Outdoor space (park, campsite)", IsResidential = true });

            Database.SaveChanges();
        }

        private void CreateAccreditationBodies()
        {
            var bodies = new List<AccreditationBody>();

            bodies.Add(new AccreditationBody { Name = "British Council for Yoga Therapy", Description = "Upholds the highest standards in Yoga Therapy, including training and accreditation.\r\nUpholds the regulation process.\r\nPromotes Yoga Therapy, and Yoga Therapy research, to the public, the medical professions, and other complementary therapy organisations.", Abbreviation = "BCYT", Affiliations = "The Complementary and Natural Healthcare Council (CNHC.)", Address = new Address(Context.Countries.FirstOrDefault(c => c.Code == "GB")) });
            bodies.Add(new AccreditationBody { Name = "British Wheel of Yoga", Description = "To promote an understanding of yoga, and its safe practice, through 'experience, education, discussion, study and training.'", Abbreviation = "BWY", DateFounded = new DateTime(1965, 1, 1), Affiliations = "Sport England's national governing body for yoga.", Address = new Address(Context.Countries.FirstOrDefault(c => c.Code == "GB")) });
            bodies.Add(new AccreditationBody { Name = "The Friends of Yoga Society International", Description = "To promote yoga globally, to conduct research, to train teachers and to advise on training and education.", Abbreviation = "FRYOG", DateFounded = new DateTime(1970, 1, 1), Affiliations = "Affiliated with BCYT (The British Council for Yoga Therapy.)\r\nRecognised by the Institute for Complementary Medicine.", Address = new Address(Context.Countries.FirstOrDefault(c => c.Code == "GB")) });
            bodies.Add(new AccreditationBody { Name = "The Independent Yoga Network ", Description = "The aim is to protect the spirit and diversity of yoga, and to preserve its future as an authentic method of self-inquiry.\r\nSupports its teacher, and teacher training school, membership.\r\nLobbys to protect yoga's freedom and independence.", Abbreviation = "IYN", DateFounded = new DateTime(2004, 1, 1), Address = new Address(Context.Countries.FirstOrDefault(c => c.Code == "GB")) });
            bodies.Add(new AccreditationBody { Name = "Register of Exercise Professionals", Description = "To give confidence in fitness professional qualifications which meet nationally recognised standards.\r\nTo protect the public from those who don’t meet these standards.", Abbreviation = "REPS", DateFounded = new DateTime(2002, 1, 1), Affiliations = "Owned by SkillsActive, the Sector Skills Council for Active Leisure and Wellbeing.", Address = new Address(Context.Countries.FirstOrDefault(c => c.Code == "GB")) });
            bodies.Add(new AccreditationBody { Name = "Yoga Alliance", Description = "The national education and support organisation for yoga in the US.\r\nConfers credibility on teachers and schools by defining high minimum standards of quality and consistency in yoga instruction.\r\nPromotes public understanding of yoga's benefits.\r\nEnsures teachers respect and value yoga's history and traditions.", Abbreviation = "YA", DateFounded = new DateTime(1999, 1, 1), Address = new Address(Context.Countries.FirstOrDefault(c => c.Code == "US")) });
            bodies.Add(new AccreditationBody { Name = "Yoga Alliance UK", Description = "To create a community which supports and safeguards the interests of yoga students, teachers and venues.\r\nTo maintain a high standard register of teachers, trainee teachers, schools and venues.\r\nTo support teachers and schools wishing to develop their potential.", Abbreviation = "YAUK", Address = new Address(Context.Countries.FirstOrDefault(c => c.Code == "GB")) });
            bodies.Add(new AccreditationBody { Name = "Canadian Yoga Alliance", Description = "To support the integrity of yoga in Canada - both in terms of practice and philosophy.\r\nTo respect and nurture the alliance with the East.\r\nTo unite all yoga practitioners in a growing, creative network.", Abbreviation = "CYA", Founder = "Violet Pasztor Wilson", Affiliations = "International Yoga Federation member.\r\nAllied to the Patanjali International Yoga Federation in India.", Address = new Address(Context.Countries.FirstOrDefault(c => c.Code == "CA")) });
            bodies.Add(new AccreditationBody { Name = "Yoga Alliance International", Description = "To create a global yoga community committed to authentic yoga traditions, united by Spirit.\r\nTo promote standards that regulate the industry and protect the integrity of yoga professionals.\r\nTo register teachers and schools that meet these standards.", Abbreviation = "YAI", Founder = "Inspired by Sri Aurobindo and the Mother.", Address = new Address(Context.Countries.FirstOrDefault(c => c.Code == "IN")) });
            bodies.Add(new AccreditationBody { Name = "International Yoga Federation", Description = "To lead the global yoga community - setting standards, fostering integrity and providing resources.\r\nTo celebrate, protect and nurture the authentic yoga teachings of all traditions.", Abbreviation = "IYF", DateFounded = new DateTime(1987, 1, 1), Address = new Address(Context.Countries.FirstOrDefault(c => c.Code == "UY")) });
            bodies.Add(new AccreditationBody { Name = "International Yoga Teachers Association", Description = "To train teachers and support their ongoing professional development with further education.\r\nTo promote the benefits of yoga for all.\r\nTo build understanding and applications of yogic principles and philosophy.\r\nTo uphold the standards and ethics of yoga professionals.", Abbreviation = "IYTA", DateFounded = new DateTime(1967, 1, 1), Founder = "Roma Blair (Swami Nirmalananda)", Address = new Address(Context.Countries.FirstOrDefault(c => c.Code == "AU")) });

            foreach (var body in bodies)
                Context.AccreditationBodies.Add(body);

            Database.SaveChanges();
        }

        private void CreateReviewExperiences()
        {
            var type = Context.EntityTypes.Where(e => e.Name == "Teacher").First();

            Context.ReviewExperiences.Add(new ReviewExperience { EntityType = type, Name = "Class" });
            Context.ReviewExperiences.Add(new ReviewExperience { EntityType = type, Name = "Workshop" });
            Context.ReviewExperiences.Add(new ReviewExperience { EntityType = type, Name = "Residential weekend" });
            Context.ReviewExperiences.Add(new ReviewExperience { EntityType = type, Name = "Holiday" });
            Context.ReviewExperiences.Add(new ReviewExperience { EntityType = type, Name = "Teacher training" });
            Context.ReviewExperiences.Add(new ReviewExperience { EntityType = type, Name = "Event" });

            Database.SaveChanges();
        }

        private void CreateReviewRatingSubjects()
        {
            var type = Context.EntityTypes.Where(e => e.Name == "Venue").First();

            Context.ReviewRatingSubjects.Add(new ReviewRatingSubject { EntityType = type, FilterKey = "Residential", Name = "Practice space" });
            Context.ReviewRatingSubjects.Add(new ReviewRatingSubject { EntityType = type, FilterKey = "Residential", Name = "Bedroom" });
            Context.ReviewRatingSubjects.Add(new ReviewRatingSubject { EntityType = type, FilterKey = "Residential", Name = "Ambience" });
            Context.ReviewRatingSubjects.Add(new ReviewRatingSubject { EntityType = type, FilterKey = "Residential", Name = "Value for money" });
            Context.ReviewRatingSubjects.Add(new ReviewRatingSubject { EntityType = type, FilterKey = "Residential", Name = "Food" });
            Context.ReviewRatingSubjects.Add(new ReviewRatingSubject { EntityType = type, FilterKey = "Residential", Name = "Treatments" });
            Context.ReviewRatingSubjects.Add(new ReviewRatingSubject { EntityType = type, FilterKey = "Residential", Name = "Location" });
            Context.ReviewRatingSubjects.Add(new ReviewRatingSubject { EntityType = type, FilterKey = "Residential", Name = "Eco-friendliness" });
            Context.ReviewRatingSubjects.Add(new ReviewRatingSubject { EntityType = type, FilterKey = "Residential", Name = "Cleanliness" });

            Context.ReviewRatingSubjects.Add(new ReviewRatingSubject { EntityType = type, FilterKey = "NonResidential", Name = "Practice space" });
            Context.ReviewRatingSubjects.Add(new ReviewRatingSubject { EntityType = type, FilterKey = "NonResidential", Name = "Cleanliness" });
            Context.ReviewRatingSubjects.Add(new ReviewRatingSubject { EntityType = type, FilterKey = "NonResidential", Name = "Ambience" });
            Context.ReviewRatingSubjects.Add(new ReviewRatingSubject { EntityType = type, FilterKey = "NonResidential", Name = "Value for money" });

            Database.SaveChanges();
        }

        private void CreateGlossaryEntries()
        {
            using (var command = new SqlCommand("select * from GlossaryEntry", ImportConnection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var entry = new GlossaryEntry
                        {
                            Term = EnsureValue<string>(reader["Term"]),
                            Definition = EnsureValue<string>(reader["Definition"])
                        };

                        Context.GlossaryEntries.Add(entry);
                    }
                }
            }

            Database.SaveChanges();
        }

        private void CreateAbilityLevels()
        {
            Context.AbilityLevels.Add(new AbilityLevel { Index = 2, Name = "Level 1" });
            Context.AbilityLevels.Add(new AbilityLevel { Index = 3, Name = "Level 2" });
            Context.AbilityLevels.Add(new AbilityLevel { Index = 4, Name = "Level 3" });
            Context.AbilityLevels.Add(new AbilityLevel { Index = 1, Name = "Beginner" });

            Database.SaveChanges();
        }

        private void CreatePoseCategories()
        {
            Context.PoseCategories.Add(new PoseCategory { Name = "Standing", Caption = "Uh oh sounds like hard work", SortKey = 1 });
            Context.PoseCategories.Add(new PoseCategory { Name = "Weight on hands", Caption = "Want arms like Arnie? Work them here", SortKey = 2 });
            Context.PoseCategories.Add(new PoseCategory { Name = "Kneeling", Caption = "Need to kneel? You’re in the right place", SortKey = 3 });
            Context.PoseCategories.Add(new PoseCategory { Name = "Seated and twists", Caption = "Why zig when you can zag?", SortKey = 4 });
            Context.PoseCategories.Add(new PoseCategory { Name = "Lying face down", Caption = "Cheek to mat? Cool with us", SortKey = 5 });
            Context.PoseCategories.Add(new PoseCategory { Name = "Lying face up", Caption = "Fancy a lie down? We understand", SortKey = 6 });

            Database.SaveChanges();
        }

        private void CreatePoses()
        {
            var categories = Context.PoseCategories.ToDictionary(category => category.Id);
            var levels = Context.AbilityLevels.ToDictionary(level => level.Id);

            var imageType = Context.ImageTypes.Get(e => e.MimeType == "image/png");
            var poses = new List<Pose>();

            using (var command = new SqlCommand("select * from Pose", ImportConnection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var pose = new Pose
                        {
                            EnglishName = EnsureValue<string>(reader["EnglishName"]),
                            SanskritName = EnsureValue<string>(reader["SanskritName"]),
                            Roots = StripBlanks(EnsureValue<string>(reader["Roots"])),
                            Pronounciation = StripBlanks(EnsureValue<string>(reader["Pronounciation"])),
                            Instructions = StripBlanks(EnsureValue<string>(reader["Instructions"])),
                            Effects = StripBlanks(EnsureValue<string>(reader["Effects"])),
                            Tips = StripBlanks(EnsureValue<string>(reader["Tips"])),
                            Indications = StripBlanks(EnsureValue<string>(reader["Indications"])),
                            Contraindications = StripBlanks(EnsureValue<string>(reader["Contraindications"])),
                            AbilityLevel = levels[EnsureValue<int>(reader["AbilityLevelId"])],
                            Category = categories[EnsureValue<int>(reader["CategoryId"])],
                            Image = new Image { Path = "Pose.Image", Guid = EnsureValue<Guid>(reader["ImageGuid"]), Type = imageType }
                        };

                        poses.Add(pose);

                        Context.Poses.Add(pose);
                    }
                }
            }

            Database.SaveChanges();
        }

        string StripBlanks(string input)
        {
            return input.Replace("\n\r\n", "\r\n");
        }

        private void CreateTeacherTraining()
        {
            var orgs = new Dictionary<int, TeacherTrainingOrganisation>();

            using (var cmd = new SqlCommand("select * from TeacherTrainingOrganisations", ImportConnection))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var country = EnsureValue<string>(reader["Country"]);

                        var org = new TeacherTrainingOrganisation {
                            Name = EnsureValue<string>(reader["Name"]),
                            Founder = EnsureValue<string>(reader["Founder"]),
                            Address = new Address
                            {
                                City = EnsureValue<string>(reader["Town"]),
                                Area = EnsureValue<string>(reader["County"]),
                                Country = Context.Countries.Get(c => c.Code == country)
                            }
                        };

                        orgs.Add(EnsureValue<int>(reader["Id"]), org);

                        Context.TeacherTrainingOrganisations.Add(org);
                    }
                }
            }

            var courses = new List<TeacherTrainingCourse>();

            using (var cmd = new SqlCommand("select * from TeacherTrainingCourses", ImportConnection))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    var imageType = Context.ImageTypes.Get(t => t.MimeType == "image/jpeg");

                    while (reader.Read())
                    {
                        var course = new TeacherTrainingCourse {
                            Name = EnsureValue<string>(reader["Name"]),
                            Website = new Website(EnsureValue<string>(reader["Website"])),
                            PreRequisites = EnsureValue<string>(reader["PreReq"]),
                            Duration = EnsureValue<string>(reader["Duration"]),
                            Notes = EnsureValue<string>(reader["Notes"]),
                            Accreditation = EnsureValue<string>(reader["Accreditation"]),
                            Image = new Image {
                                Path = "TeacherTrainingCourse.Image",
                                Guid = EnsureValue<Guid>(reader["ImageGuid"]),
                                Type = imageType
                            },
                            ImageCourtesyOf = EnsureValue<string>(reader["ImageCourtesyOf"]),
                            Active = true,
                        };

                        var style = EnsureValue<string>(reader["StyleName"]);

                        if (!string.IsNullOrEmpty(style))
                        {
                            var entity = Context.Styles.FirstOrDefault(s => s.Name.ToLower() == style.ToLower());
                            if (entity != null) course.Style = entity;
                        }

                        var org = orgs[EnsureValue<int>(reader["OrganisationId"])];

                        if (course.Style != null) org.Styles.Add(course.Style);
                        org.Courses.Add(course);

                        courses.Add(course);
                    }
                }
            }

            Database.SaveChanges();
        }

        private void CreateQuotes()
        {
            using (var command = new SqlCommand("select * from Quote", ImportConnection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var quote = new Quote
                        {
                            Author = EnsureValue<string>(reader["Author"]),
                            Quotation = EnsureValue<string>(reader["Quotation"])
                        };

                        Context.Quotes.Add(quote);
                    }
                }
            }

            Database.SaveChanges();
        }

        private void CreateProfileQuestions()
        {
            Context.ProfileQuestions.Add(new ProfileQuestion { Text = "I got into yoga because", DisplayOrder = 1 });
            Context.ProfileQuestions.Add(new ProfileQuestion { Text = "On my mat I feel", DisplayOrder = 2 });
            Context.ProfileQuestions.Add(new ProfileQuestion { Text = "After yoga I like to", DisplayOrder = 3 });
            Context.ProfileQuestions.Add(new ProfileQuestion { Text = "People who do yoga are", DisplayOrder = 4 });
            Context.ProfileQuestions.Add(new ProfileQuestion { Text = "The hardest thing about yoga is", DisplayOrder = 5 });
            Context.ProfileQuestions.Add(new ProfileQuestion { Text = "The best thing about yoga is", DisplayOrder = 6 });
            Context.ProfileQuestions.Add(new ProfileQuestion { Text = "Yoga's taught me ", DisplayOrder = 7 });
            
            Database.SaveChanges();
        }

        private void CreateNotificationTypes()
        {
            Context.NotificationTypes.Add(new NotificationType { Tag = "Alert", Message = "", Icon = "icon-alerts.png" });
            Context.NotificationTypes.Add(new NotificationType { Tag = "ReviewCreated", Message = "{0:N} reviewed your {1:t}.", Icon = "icon-review.png", Category = new NotificationCategory { Name = "New reviews of me" } });
            Context.NotificationTypes.Add(new NotificationType { Tag = "Fanned", Message = "{0:N} became a fan of your {1:t}.", Icon = "icon-fan.png", Category = new NotificationCategory { Name = "New fans" } });
            Context.NotificationTypes.Add(new NotificationType { Tag = "FriendshipAccepted", Message = "{0:N} accepted your friend request.", Icon = "" });
            Context.NotificationTypes.Add(new NotificationType { Tag = "TeacherPlacementAccepted", Message = "{0:N} confirmed that you teach at {1:N}.", Icon = "icon-venue.png" });
            Context.NotificationTypes.Add(new NotificationType { Tag = "VenuePlacementAccepted", Message = "{0:N} confirmed that they teach at {2:N}.", Icon = "icon-venue.png" });
            Context.NotificationTypes.Add(new NotificationType { Tag = "TeacherTrainingCourseTeacherAccepted", Message = "{0:N} confirmed that they teach at {1:N}.", Icon = "icon-venue.png" });
            Context.NotificationTypes.Add(new NotificationType { Tag = "TeacherTrainingCourseVenueAccepted", Message = "{0:N} confirmed that {1:N} is hosted at {2:N}.", Icon = "icon-venue.png" });
            Context.NotificationTypes.Add(new NotificationType { Tag = "GroupJoined", Message = "{0:N} joined your group {1:N}.", Icon = "icon-venue.png" });
            Context.NotificationTypes.Add(new NotificationType { Tag = "GroupMembershipRequestAccepted", Message = "{0:N} accepted your request to join {1:N}.", Icon = "icon-venue.png" });
            Context.NotificationTypes.Add(new NotificationType { Tag = "ActivityTeacherAccepted", Message = "{0:N} confirmed that they are attending {1:N}.", Icon = "icon-venue.png" });
            Context.NotificationTypes.Add(new NotificationType { Tag = "ActivityTeacherRejected", Message = "{0:N} denied that they are attending {1:N}.", Icon = "icon-venue.png" });
            Context.NotificationTypes.Add(new NotificationType { Tag = "ActivityVenueAccepted", Message = "{0:N} confirmed that {1:N} is being held at {2:N}.", Icon = "icon-venue.png" });
            Context.NotificationTypes.Add(new NotificationType { Tag = "ActivityVenueRejected", Message = "{0:N} denied that {1:N} is being held at {2:N}.", Icon = "icon-venue.png" });
            Context.NotificationTypes.Add(new NotificationType { Tag = "CommentAdded", Message = "{0:N} commented on your {3:t}.", Icon = "icon-venue.png" });
            Context.NotificationTypes.Add(new NotificationType { Tag = "Recommendation", Message = "{0:N} recommended {1:N} to you.", Icon = "icon-venue.png" });

            Database.SaveChanges();
        }

        private void CreateRequestTypes()
        {
            Context.RequestTypes.Add(new RequestType { Tag = "FriendshipRequested", Message = "{0:N} sent you a friend request.", Icon = "", CompletionHandlerTypeName = "Clicks.Yoga.Requests.Handlers.FriendshipRequestedHandler, Clicks.Yoga", IsFriend = true });
            Context.RequestTypes.Add(new RequestType { Tag = "TeacherPlacementAdded", Message = "{0:N} said they teach at {2:N}.", Icon = "icon-venue.png", CompletionHandlerTypeName = "Clicks.Yoga.Requests.Handlers.TeacherPlacementAddedHandler, Clicks.Yoga" });
            Context.RequestTypes.Add(new RequestType { Tag = "VenuePlacementAdded", Message = "{0:N} said you teach at {1:N}.", Icon = "icon-venue.png", CompletionHandlerTypeName = "Clicks.Yoga.Requests.Handlers.VenuePlacementAddedHandler, Clicks.Yoga" });
            Context.RequestTypes.Add(new RequestType { Tag = "TeacherTrainingCourseTeacherAdded", Message = "{0:N} said you teach at the course {1:N}.", Icon = "icon-venue.png", CompletionHandlerTypeName = "Clicks.Yoga.Requests.Handlers.TeacherTrainingCourseTeacherAddedHandler, Clicks.Yoga" });
            Context.RequestTypes.Add(new RequestType { Tag = "TeacherTrainingCourseVenueAdded", Message = "{0:N} said the course {1:N} is hosted at {2:N}.", Icon = "icon-venue.png", CompletionHandlerTypeName = "Clicks.Yoga.Requests.Handlers.TeacherTrainingCourseVenueAddedHandler, Clicks.Yoga" });
            Context.RequestTypes.Add(new RequestType { Tag = "GroupInvitation", Message = "{0:N} invited you to the group {1:N}.", Icon = "icon-venue.png", CompletionHandlerTypeName = "Clicks.Yoga.Requests.Handlers.GroupInvitationHandler, Clicks.Yoga" });
            Context.RequestTypes.Add(new RequestType { Tag = "GroupMembershipRequest", Message = "{0:N} wants to join your group {1:N}.", Icon = "icon-venue.png", CompletionHandlerTypeName = "Clicks.Yoga.Requests.Handlers.GroupMembershipRequestHandler, Clicks.Yoga" });
            Context.RequestTypes.Add(new RequestType { Tag = "ActivityInvitation", Message = "{0:N} invited you to {1:N}.", Icon = "icon-venue.png", CompletionHandlerTypeName = "Clicks.Yoga.Requests.Handlers.ActivityInvitationHandler, Clicks.Yoga" });
            Context.RequestTypes.Add(new RequestType { Tag = "ActivityTeacherAdded", Message = "{0:N} said you are attending {1:N}.", Icon = "icon-venue.png", CompletionHandlerTypeName = "Clicks.Yoga.Requests.Handlers.ActivityTeacherAddedHandler, Clicks.Yoga" });
            Context.RequestTypes.Add(new RequestType { Tag = "ActivityVenueAdded", Message = "{0:N} said that {1:N} is being held at {2:N}.", Icon = "icon-venue.png", CompletionHandlerTypeName = "Clicks.Yoga.Requests.Handlers.ActivityVenueAddedHandler, Clicks.Yoga" });

            Database.SaveChanges();
        }

        private void CreateActivityTypes()
        {
            Context.ActivityTypes.Add(new ActivityType { Name = "Class", IsClass = true });
            Context.ActivityTypes.Add(new ActivityType { Name = "Workshop" });
            Context.ActivityTypes.Add(new ActivityType { Name = "Weekend" });
            Context.ActivityTypes.Add(new ActivityType { Name = "Holiday" });
            Context.ActivityTypes.Add(new ActivityType { Name = "Conference", IsEvent = true });
            Context.ActivityTypes.Add(new ActivityType { Name = "Concert", IsEvent = true });
            Context.ActivityTypes.Add(new ActivityType { Name = "Festival", IsEvent = true });
            Context.ActivityTypes.Add(new ActivityType { Name = "Kids' Stuff", IsEvent = true });
            Context.ActivityTypes.Add(new ActivityType { Name = "Charity Function", IsEvent = true });
            Context.ActivityTypes.Add(new ActivityType { Name = "Dance", IsEvent = true });

            Database.SaveChanges();
        }

        private void CreateActivityRepeatFrequencies()
        {
            Context.ActivityRepeatFrequencies.Add(new ActivityRepeatFrequency { Name = "Single", IsSingle = true });
            Context.ActivityRepeatFrequencies.Add(new ActivityRepeatFrequency { Name = "Daily", IsDaily = true });
            Context.ActivityRepeatFrequencies.Add(new ActivityRepeatFrequency { Name = "Weekly", IsWeekly = true });
            Context.ActivityRepeatFrequencies.Add(new ActivityRepeatFrequency { Name = "Fortnightly", IsFortnightly = true });

            Database.SaveChanges();
        }

        private void CreateNewsStoryTypes()
        {
            Context.NewsStoryTypes.Add(new NewsStoryType { Tag = NewsStoryType.FriendMadeFriend, Name = "" });
            Context.NewsStoryTypes.Add(new NewsStoryType { Tag = NewsStoryType.FriendFanned, Name = "" });
            Context.NewsStoryTypes.Add(new NewsStoryType { Tag = NewsStoryType.FriendAddedReview, Name = "" });
            Context.NewsStoryTypes.Add(new NewsStoryType { Tag = NewsStoryType.FriendJoinedGroup, Name = "" });
            Context.NewsStoryTypes.Add(new NewsStoryType { Tag = NewsStoryType.FriendAttendingActivity, Name = "" });
            Context.NewsStoryTypes.Add(new NewsStoryType { Tag = NewsStoryType.FriendAddedEntity, Name = "" });
            Context.NewsStoryTypes.Add(new NewsStoryType { Tag = NewsStoryType.FriendAddedPhotos, Name = "", Vain = true, Deduped = true });
            Context.NewsStoryTypes.Add(new NewsStoryType { Tag = NewsStoryType.FriendPosted, Name = "", Vain = true });
            Context.NewsStoryTypes.Add(new NewsStoryType { Tag = NewsStoryType.ActivityPosted, Name = "" });
            Context.NewsStoryTypes.Add(new NewsStoryType { Tag = NewsStoryType.GroupPosted, Name = "" });
        }

        private void CreateTestEntities()
        {
            var user = new User { EmailAddress = "admin@yogaclicks.com", IsSuperUser = true };
            var login = new PasswordLogin { User = user, Username = "admin@yogaclicks.com", Password = "password" };
            var profile = new Profile { Owner = user, Forename = "Test", Surname = "Administrator", EmailAddress = "admin@yogaclicks.com", Wall = new ProfileWall() };

            Context.Users.Add(user);
            Context.PasswordLogins.Add(login);
            Context.Profiles.Add(profile);

            Database.SaveChanges();
            /*
            user = new User { EmailAddress = "professional@yogaclicks.com" };
            login = new PasswordLogin { User = user, Username = "professional@yogaclicks.com", Password = "password" };
            profile = new Profile { Owner = user, Forename = "Test", Surname = "Professional", EmailAddress = "professional@yogaclicks.com", Wall = new ProfileWall() };
            var teacher = new Teacher { Owner = user, Name = "Test Teacher" };
            var venue = new Venue { Owner = user, Name = "Test Venue" };
            var group = new Group { Owner = user, Name = "Test Group", Wall = new GroupWall() };
            group.Members.Add(new GroupMember { Group = group, User = user, Administrator = true, Confirmed = true });

            Context.Users.Add(user);
            Context.PasswordLogins.Add(login);
            Context.Profiles.Add(profile);
            Context.Teachers.Add(teacher);
            Context.Venues.Add(venue);
            Context.Groups.Add(group);

            teacher = new Teacher { Name = "Stub Teacher", Stubbed = true };
            venue = new Venue { Name = "Stub Venue", Stubbed = true };

            Context.Teachers.Add(teacher);
            Context.Venues.Add(venue);

            var friendship = new Friendship { Profile = profile, Confirmed = true, Inverse = new Friendship { Friend = profile, Confirmed = true } };
            user = new User { EmailAddress = "friend@yogaclicks.com" };
            profile = new Profile { Owner = user, Forename = "Test", Surname = "Friend", EmailAddress = "friend@yogaclicks.com", Wall = new ProfileWall() };
            friendship.Friend = profile;
            friendship.Inverse.Profile = profile;

            Context.RegisterTransientEntityDependency(friendship.Inverse, e => e.Inverse, friendship);

            Context.Users.Add(user);
            Context.Profiles.Add(profile);
            Context.Friendships.Add(friendship);

            Database.SaveChanges();
             */
        }

        private void CreateSearchIndex()
        {
            Database.Database.ExecuteSqlCommand(Sql.SearchIndex);
        }

        private T EnsureValue<T>(object value)
        {
            return EnsureValue(value, default(T));
        }

        private T EnsureValue<T>(object value, T placeholder)
        {
            return value is DBNull ? placeholder : (T)value;
        }
    }
}