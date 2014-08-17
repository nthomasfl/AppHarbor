using Business.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class PollVoteAdminModel
    {
        public PollQuestion poll { get; set; }

        public UserProfile user { get; set; }
    }
}