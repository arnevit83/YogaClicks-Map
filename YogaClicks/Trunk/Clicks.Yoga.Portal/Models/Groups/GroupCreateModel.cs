using System;
using System.Runtime.Serialization;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;

namespace Clicks.Yoga.Portal.Models.Groups
{
    [DataContract]
    public class GroupCreateModel
    {
        [DataMember]
        public GroupCreateStep1Model Step1Model { get; set; }

        [DataMember]
        public GroupCreateStep2Model Step2Model { get; set; }

        [DataMember]
        public GroupCreateStep3Model Step3Model { get; set; }

        public void PopulateEntity(
            Group entity,
            Profile profile,
            ISecurityContext securityContext,
            IEntityService entityService,
            IFriendService friendService)
        {
            Step1Model.PopulateEntity(entity, securityContext, entityService);
            Step2Model.PopulateEntity(entity, entityService);
            Step3Model.PopulateEntity(entity, profile, friendService);
        }
    }
}