using System;
using System.Collections.Generic;

namespace Domain
{
	public class GroupPermission : ISeed<GroupPermission>
    {
        public int GroupId { get; set; }

		public int PermissionId { get; set; }

		public virtual Group Group { get; set; }

		public virtual Permission Permission { get; set; }

        public override int GetHashCode()
        {
            return HashCode.Combine(GroupId, PermissionId, GetType());
        }

		public List<GroupPermission> GetSeedData()
		{
			var groupPermissions = new List<GroupPermission>();

			// Default permissions:
			groupPermissions.Add(new GroupPermission
			{
				GroupId = 1,
				PermissionId = (int) PermissionType.IssueCreate
			});

			groupPermissions.Add(new GroupPermission
			{
				GroupId = 1,
				PermissionId = (int) PermissionType.IssueClose
			});

			groupPermissions.Add(new GroupPermission
			{
				GroupId = 1,
				PermissionId = (int) PermissionType.IssueRateAssistence
			});

			// Support permissions:
			groupPermissions.Add(new GroupPermission
			{
				GroupId = 2,
				PermissionId = (int) PermissionType.IssueCreate
			});

			groupPermissions.Add(new GroupPermission
			{
				GroupId = 2,
				PermissionId = (int) PermissionType.IssueAccept
			});

			groupPermissions.Add(new GroupPermission
			{
				GroupId = 2,
				PermissionId = (int) PermissionType.IssueClose
			});

			// Admin permissions:
			groupPermissions.Add(new GroupPermission
			{
				GroupId = 3,
				PermissionId = (int) PermissionType.IssueCreate
			});

			groupPermissions.Add(new GroupPermission
			{
				GroupId = 3,
				PermissionId = (int) PermissionType.IssueAccept
			});

			groupPermissions.Add(new GroupPermission
			{
				GroupId = 3,
				PermissionId = (int) PermissionType.IssueClose
			});

			groupPermissions.Add(new GroupPermission
			{
				GroupId = 3,
				PermissionId = (int) PermissionType.ManageAccounts
			});

			groupPermissions.Add(new GroupPermission
			{
				GroupId = 3,
				PermissionId = (int) PermissionType.ManageGroups
			});

			groupPermissions.Add(new GroupPermission
			{
				GroupId = 3,
				PermissionId = (int) PermissionType.ManageCategories
			});

			return groupPermissions;
		}
	}
}
