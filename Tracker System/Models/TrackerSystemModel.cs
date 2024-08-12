using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tracker_System.Models
{
    public class TrackerSystemModel
    {
        [Display(Name = "ID")]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Please Enter UserName")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Password \"{0}\" must have {2} characters", MinimumLength = 8)]
        [RegularExpression(@"^([a-zA-Z0-9@*#]{8,15})$", ErrorMessage = "Password must contain: Minimum 8 characters, at least 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number, and 1 Special Character")]
        public string Password { get; set; }
        public string IpAddress { get; set; }

        public string AppName { get; set; }
        public int RememberOrNot { get; set; }
        public int EntryOrCheck { get; set; }
    }
}