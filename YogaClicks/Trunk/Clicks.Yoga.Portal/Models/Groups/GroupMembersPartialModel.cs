using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Groups
{
    public class GroupMembersPartialModel
    {
        public GroupMembersPartialModel(IEnumerable<GroupMember> members, 
                                        int? groupOwnerId, 
                                        bool permissionsManage, 
                                        bool permissionsmanageAdmin) : this()
        {
            PermissionToManage = permissionsManage;
            PermissionToManageAdmin = permissionsmanageAdmin;
            GroupOwnerId = groupOwnerId;

            foreach (var member in members)
            {
               Members.Add(new GroupMemberModel(member));
            }

        }

        private GroupMembersPartialModel()
        {
            Members = new List<GroupMemberModel>();
        }

        public IList<GroupMemberModel> Members { get; private set; }
        public bool PermissionToManage { get; private set; }
        public bool PermissionToManageAdmin { get; private set; }
        public int? GroupOwnerId { get; set; }
    }
}