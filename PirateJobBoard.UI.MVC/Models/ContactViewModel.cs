using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PirateJobBoard.UI.MVC.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "* Aye, yer name is a requirement, not a guideline!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "* Aye, the subject of yer message is a requirement, not a guideline!")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "* Aye, yer message is a requirement, not a guideline!")]
        [UIHint("MultilineText")]
        public string Message { get; set; }


        [EmailAddress(ErrorMessage = "* That ain't a proper email address, Matey. Try yer hand at it again.")]
        [Required(ErrorMessage = "* Aye, yer email address is a requirement, not a guideline!")]
        [Display(Name = "Yer Email")]
        public string EmailAddress { get; set; }
    }
}