using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class GroupPermission
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
    }
}
