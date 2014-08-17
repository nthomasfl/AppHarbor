using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class PollModel
    {
        [Required(ErrorMessage="Vul een vraag in.")]
        [Display(Name="Vraag")]
        public string Question { get; set; }
        [Display(Name = "Actief")]
        public bool Active { get; set; }
        [Display(Name = "Publiek?")]
        public bool Public { get; set; }
        [Required(ErrorMessage = "Vul minstens 2 antwoorden in.")]
        [Display(Name = "Antwoorden")]
        public string Answers { get; set; }
    }
}