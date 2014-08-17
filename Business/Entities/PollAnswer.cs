using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Business.Entities
{
    public class PollAnswer
    {
        [Required]
        public int ID { get; set; }

        [Required(ErrorMessage = "Vul een antwoord in.")]
        public string Answer { get; set; }

        public virtual PollQuestion Poll { get; set; }

        public virtual ICollection<PollVote> PollVotes { get; set; }
        
    }
}