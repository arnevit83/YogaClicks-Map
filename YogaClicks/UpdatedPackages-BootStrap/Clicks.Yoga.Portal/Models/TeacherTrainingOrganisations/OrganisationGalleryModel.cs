using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.TeacherTrainingOrganisations
{
    public class OrganisationGalleryModel
    {
        public OrganisationGalleryModel(TeacherTrainingOrganisation profile, int galleryId)
        {
            TeacherTrainingOrganisation = new TeacherTrainingOrganisationModel(profile);
            GalleryId = galleryId;
        }

        public TeacherTrainingOrganisationModel TeacherTrainingOrganisation { get; private set; }

        public int GalleryId { get; private set; }
    }
}