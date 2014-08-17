using Business.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class PollVoteModel
    {
        public PollQuestion poll { get; set; }

         [Required]
         [Range(1,999)]
        public int selectedID { get; set; }
    }
}