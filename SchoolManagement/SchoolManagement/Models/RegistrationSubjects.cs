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
    
    public partial class RegistrationSubjects
    {
        public int ID { get; set; }
        public string IDStudent { get; set; }
        public string IDSubject { get; set; }
    
        public virtual Users Users { get; set; }
        public virtual Subjects Subjects { get; set; }
    }
}
