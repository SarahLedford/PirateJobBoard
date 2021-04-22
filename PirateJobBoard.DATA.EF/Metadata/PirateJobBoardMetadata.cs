using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace PirateJobBoard.DATA.EF//.Metadata
{
    #region Application
    [MetadataType(typeof(ApplicationMetadata))]
    public partial class Application { }

    public class ApplicationMetadata
    {
        //public int ApplicationID { get; set; }

        [Required(ErrorMessage = "* Aye, the assignment is a requirement, not a guideline!")]
        [Display(Name = "Assignment")]
        public int OpenAssignmentID { get; set; }

        [Display(Name = "Pirate")]
        [Required(ErrorMessage = "* Aye, the pirate is required to apply for a PIRATE position!")]
        public string PirateID { get; set; }

        [Display(Name = "Date Applied")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Required(ErrorMessage = "* Aye, the application date is a requirement, not a guideline!")]
        public System.DateTime ApplicationDate { get; set; }

        [StringLength(1000, ErrorMessage = "* Aye, there's only room for 1000 characters!")]
        [Display(Name = "Notes")]
        public string CaptainNotes { get; set; }

        [Required(ErrorMessage = "* Aye, the application status is a requirement, not a guideline!")]
        [Display(Name = "Status of Application")]
        public int ApplicationStatus { get; set; }

        [Required(ErrorMessage = "* Aye, the sesume is a requirement, not a guideline!")]
        [Display(Name = "Resume")]
        public string ResumeFilename { get; set; }
    }
    #endregion

    #region Application Status
    [MetadataType(typeof(ApplicationStatusMetadata))]
    public partial class ApplicationStatus { }

    public class ApplicationStatusMetadata
    {
        //public int ApplicationStatusID { get; set; }

        [Display(Name = "Status Name")]
        [StringLength(50, ErrorMessage = "* Aye, there's only room for 50 characters!")]
        [Required(ErrorMessage = "* Aye, the name of the status is a requirement, not a guideline!")]
        public string StatusName { get; set; }

        [Display(Name = "Description")]
        [StringLength(250, ErrorMessage = "* Aye, there's only room for 250 characters!")]
        public string StatusDescription { get; set; }
    }
    #endregion

    #region Assignment
    [MetadataType(typeof(AssignmentMetadata))]
    public partial class Assignment { }

    public class AssignmentMetadata
    {
        //public int AssignmentID { get; set; }

        [Display(Name = "Name of Assignment")]
        [StringLength(50, ErrorMessage = "* Aye, there's only room for 50 characters!")]
        [Required(ErrorMessage = "* Aye, you have to give the assignment's name!")]
        public string AssignmentName { get; set; }

        [Display(Name = "Description")]
        [StringLength(2000, ErrorMessage = "* Aye, there's only room for 2000 characters!")]
        public string AssignmentDescription { get; set; }
    }

    #endregion

    #region Open Assignment
    [MetadataType(typeof(OpenAssignmentMetadata))]
    public partial class OpenAssignment { }

    public class OpenAssignmentMetadata
    {
        //public int OpenAssignmentID { get; set; }

        [Required(ErrorMessage = "* Aye, the assignment is a requirement, not a guideline!")]
        [Display(Name = "Assignment")]
        public int AssignmentID { get; set; }

        [Required(ErrorMessage = "* Aye, what good would it do you without a ship? A ship's required!")]
        [Display(Name = "Ship")]
        public int ShipID { get; set; }
    }
    #endregion

    #region Ship
    [MetadataType(typeof(ShipMetadata))]
    public partial class Ship { }

    public class ShipMetadata
    {
        //public int ShipID { get; set; }

        [Display(Name = "Ship Name")]
        [StringLength(30, ErrorMessage = "* Aye, there's only room for 30 characters!")]
        [Required(ErrorMessage = "* Aye, the name of the ship is a requirement, not a guideline!")]
        public string ShipName { get; set; }

        [Display(Name = "Home Port")]
        [Required(ErrorMessage = "* Aye, the home port is required!")]
        [StringLength(50, ErrorMessage = "* Aye, there's only room for 50 characters!")]
        public string HomePort { get; set; }

        [Display(Name = "Captain")]
        [Required(ErrorMessage = "* Aye, you can't have a ship without a captain, now can you?")]
        public string CaptainID { get; set; }
    }
    #endregion

    #region PirateDetails
    [MetadataType(typeof(PirateDetailMetadata))]
    public partial class PirateDetail { public string FullName { get { return FirstName + " " + LastName; } } }

    public class PirateDetailMetadata
    {
        //public string PirateID { get; set; }

        [Required(ErrorMessage = "* Aye, yer first name is a requirement, not a guideline!")]
        [Display(Name ="First Name")]
        [StringLength(50, ErrorMessage = "* Aye, there's only room for 50 characters!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "* Aye, yer last name is a requirement, not a guideline!")]
        [Display(Name = "First Name")]
        [StringLength(50, ErrorMessage = "* Aye, there's only room for 50 characters!")]
        public string LastName { get; set; }


        [Display(Name = "Resume")]
        public string ResumeFilename { get; set; }

        [Display(Name = "Do ye have scurvy?")]
        public bool HasScurvy { get; set; }
    }
    #endregion
}
