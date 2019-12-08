using System;
using System.Collections.Generic;

namespace Domain
{
    public class GroupUser : ISeed<GroupUser>
    {
        public int GroupId { get; set; }

        public int UserId { get; set; }

        public virtual Group Group { get; set; }

        public virtual User User { get; set; }

        public override int GetHashCode()
        {
            return HashCode.Combine(GroupId, UserId, GetType());
        }

        public override bool Equals(object obj)
        {
            return GetHashCode() == obj?.GetHashCode();
        }

        public List<GroupUser> GetSeedData()
        {
            var result = new List<GroupUser>();

            result.Add(new GroupUser
            {
                UserId = 1,
                GroupId = 3
            });

            return result;
        }
    }
}
