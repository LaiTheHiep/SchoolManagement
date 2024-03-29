//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SchoolManagement.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class MessageContact
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [EmailAddress(ErrorMessage ="Email not true")]
        public string Email { get; set; }
        public Nullable<System.DateTime> DateSend { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string MessageGuest { get; set; }
        public Nullable<bool> isRep { get; set; }
    
        public virtual MessageReponse MessageReponse { get; set; }
    }
}
