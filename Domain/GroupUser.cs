﻿using System;

namespace Domain
{
    public class GroupUser
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
    }
}
