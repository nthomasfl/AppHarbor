using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Business.Entities
{
    public class PollVote
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public virtual PollQuestion Poll { get; set; }

        [Required]
        public virtual PollAnswer Answer { get; set; }
    }
}