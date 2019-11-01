using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class GroupUser
    {
        [Key]
        public int GroupId { get; set; }

        [Key]
        public int UserId { get; set; }

        public Group Group { get; set; }

        public User User { get; set; }
    }
}
