﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class GroupUser
    {
        public int GroupId { get; set; }

        public int UserId { get; set; }

        public Group Group { get; set; }

        public User User { get; set; }
    }
}