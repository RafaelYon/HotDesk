using System;
using System.Collections.Generic;

namespace Domain
{
	public class GroupPermission : ISeed<GroupPermission>
    {
        public int GroupId { get; set; }

        public Group Group { get; set; }

        public Permission Permission { get; set; }

        public override bool Equals(object obj)
        {
            return GetHashCode() == obj?.GetHashCode();
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(GroupId, Permission, GetType());
        }

		public List<GroupPermission> GetSeedData()
		{
			var groupPermissions = new List<GroupPermission>();

			// Default permissions:
			groupPermissions.Add(new GroupPermission
			{
				GroupId = 1,
				Permission = Permission.IssueCreate
			});

			groupPermissions.Add(new GroupPermission
			{
				GroupId = 1,
				Permission = Permission.IssueClose
			});

			groupPermissions.Add(new GroupPermission
			{
				GroupId = 1,
				Permission = Permission.IssueRateAssistence
			});

			// Support permissions:
			groupPermissions.Add(new GroupPermission
			{
				GroupId = 2,
				Permission = Permission.IssueCreate
			});

			groupPermissions.Add(new GroupPermission
			{
				GroupId = 2,
				Permission = Permission.IssueAccept
			});

			groupPermissions.Add(new GroupPermission
			{
				GroupId = 2,
				Permission = Permission.IssueClose
			});

			// Admin permissions:
			groupPermissions.Add(new GroupPermission
			{
				GroupId = 3,
				Permission = Permission.IssueCreate
			});

			groupPermissions.Add(new GroupPermission
			{
				GroupId = 3,
				Permission = Permission.IssueAccept
			});

			groupPermissions.Add(new GroupPermission
			{
				GroupId = 3,
				Permission = Permission.IssueClose
			});

			groupPermissions.Add(new GroupPermission
			{
				GroupId = 3,
				Permission = Permission.ManageAccounts
			});

			groupPermissions.Add(new GroupPermission
			{
				GroupId = 3,
				Permission = Permission.ManageGroups
			});

			groupPermissions.Add(new GroupPermission
			{
				GroupId = 3,
				Permission = Permission.ManageCategories
			});

			return groupPermissions;
		}
	}
}
