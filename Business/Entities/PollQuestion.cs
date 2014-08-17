using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Business.Entities
{
    public class PollQuestion
    {
        [Required]
        public int ID { get; set; }

        [Required(ErrorMessage="Vul een vraag in.")]

        public string Question { get; set; }

        public bool Active { get; set; }

        public bool Public { get; set; }

         [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        public Guid GUID { get; set; }

        [Required]
        public int UserID { get; set; }
        public virtual ICollection<PollAnswer> Answers { get; set; }
        public virtual ICollection<PollVote> PollVotes { get; set; }

    }
}