using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Customer
    {
        public int id { get; set; }
        [Required]
        [StringLength(255)]
        [Display(Name = "UserName")]
        public string name { get; set; }
        public bool IsSubscribedToNewsLetter { get; set; }
        public MemberShipType MemberShipType { get; set; }
        [Display(Name = "MemberShip Type")]
        public byte MemberShipTypeId { get; set; }
        [Display(Name = "DateOfBirth")]
        public string BirthDate { get; set; }
    }
}