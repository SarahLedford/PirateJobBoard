//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PirateJobBoard.DATA.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Application
    {
        public int ApplicationID { get; set; }
        public int OpenAssignmentID { get; set; }
        public string PirateID { get; set; }
        public System.DateTime ApplicationDate { get; set; }
        public string CaptainNotes { get; set; }
        public int ApplicationStatus { get; set; }
        public string ResumeFilename { get; set; }
    
        public virtual ApplicationStatus ApplicationStatu { get; set; }
        public virtual OpenAssignment OpenAssignment { get; set; }
        public virtual PirateDetail PirateDetail { get; set; }
    }
}
