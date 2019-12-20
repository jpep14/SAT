using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SAT.DATA
{
    #region Course
    [MetadataType(typeof(CourseMetadata))] //referencing the metadata class
    public partial class Course { }
    public class CourseMetadata
    {
        /*
            [Display] - change the display text (on the view)
            [DisplayFormat] - apply formatting to a value (i.e. display a date as mm/dd/yyyy, currency as $x.xx, etc.)
            - can add ApplyFormatInEditMode property if the formatting should be applied while editing
            [Required] - the field is non-nullable in the data structure
            [StringLength] - limit the number of characters that can be input (match field data type - DB)
            [Range] - used for numeric ranges (i.e. user should enter a number between 1 and 5)
            [UIHint("HintText")] - change the way an MVC control (DisplayFor, EditorFor) will render (on the view)
            [DataType(type)] - use .NET default validation for a specific data type (i.e. dates, email, etc. vs. RegEx) 
        */

        [Required(ErrorMessage = "*REQUIRED*")]
        [Display(Name = "Course")]
        [StringLength(50, ErrorMessage = " Course name must be 50 characters or less")]
        public string CourseName { get; set; }

        [Required(ErrorMessage = "*REQUIRED*")]
        [Display(Name = "Description")]
        [UIHint("MultilineText")]
        public string CourseDescription { get; set; }

        [Required(ErrorMessage = "*REQUIRED*")]
        [Display(Name = "Credit Hours")]
        [Range(1, 6, ErrorMessage = "Classes must have 1-6 credit hours")]
        public byte CreditHours { get; set; }
        [StringLength(250, ErrorMessage = "Curriculum must be less than 250 characters")]
        [Display(Name = "Curriculum")]
        public string Curriculum { get; set; }

        [UIHint("MultilineText")]
        public string Notes { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }
    }
    #endregion

    #region Student
    [MetadataType(typeof(StudentMetadata))]
    public partial class Student {
        //this is where I would add custom read only properties
        public string FullName
        {
            get { return LastName + ", " + FirstName; }
        }
    }
    public class StudentMetadata
    { 
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "*REQUIRED")]
        [StringLength(20, ErrorMessage = "Name must be 20 characters or less")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "*REQUIRED")]
        [StringLength(20, ErrorMessage = "Name must be 20 characters or less")]
        public string LastName { get; set; }
        [StringLength(30, ErrorMessage = "Must be 30 characters or less")]
        public string Major { get; set; }

        [StringLength(50, ErrorMessage = "Address must be 50 characters or less")]
        public string Address { get; set; }

        [StringLength(25, ErrorMessage = "City must be 25 characters or less")]
        public string City { get; set; }

        [StringLength(2, MinimumLength = 2, ErrorMessage = "Please use two letter abbreviation")]
        public string State { get; set; }

        [Display(Name = "Zip Code")]
        [StringLength(10, ErrorMessage = "No more than 10 digits please")]
        [DataType(DataType.PostalCode, ErrorMessage = "Please enter a valid zip code")]
        public string ZipCode { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "*REQUIRED")]
        [StringLength(50, ErrorMessage = "Must be 50 characters or less")]
        public string Email { get; set; }

        [Display(Name = "Photo URL")]
        [StringLength(100, ErrorMessage = "URL must be 100 characters or less")]
        public string PhotoURL { get; set; }
    }
    #endregion

    #region Enrollments
    [MetadataType(typeof(EnrollmentMetadata))]
    public partial class Enrollment { }
    public class EnrollmentMetadata
    {
        [Display(Name = "Enrollement Date")]
        [Required(ErrorMessage = "*REQUIRED*")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}", NullDisplayText = "[-N/A-]")]
        [DataType(DataType.Date)]
        public System.DateTime EnrollmentDate { get; set; }
    }

    #endregion

    #region ScheduledClass
    [MetadataType(typeof(ScheduledClassMetadata))]
    public partial class ScheduledClass { }
    public class ScheduledClassMetadata
    {
        [Display(Name = "Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}", NullDisplayText = "[-N/A-]")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "*REQUIRED*")]
        public System.DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}", NullDisplayText = "[-N/A-]")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "*REQUIRED*")]
        public System.DateTime EndDate { get; set; }

        [Required(ErrorMessage = "*REQUIRED*")]
        [StringLength(40, ErrorMessage = "Instructor name must be 40 characters or less")]
        [Display(Name = "Instructor")]
        public string InstructorName { get; set; }

        [Required(ErrorMessage = "*REQUIRED*")]
        [StringLength(20, ErrorMessage = "Must be 20 characters or less")]
        public string Location { get; set; }
    }
    #endregion

    #region StudentStatus
    [MetadataType(typeof(StudentStatusMetadata))]
    public partial class StudentStatus { }
    public class StudentStatusMetadata
    {
        [StringLength(30, ErrorMessage = "Must be 30 characters or less")]
        [Display(Name = "Status")]
        [Required(ErrorMessage = "*REQUIRED*")]
        public string SSName { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Must be 250 characters or less")]
        [UIHint("MultilineText")]
        public string SSDescription { get; set; }
    }
    #endregion

    #region ScheduledClassStatus

    [MetadataType(typeof(ScheduledClassStatusMetadata))]
    public partial class ScheduledClassStatus { }
    public class ScheduledClassStatusMetadata
    {
        [Display(Name = "Class Status")]
        [StringLength(50, ErrorMessage = "Must be 50 characters or less")]
        public string SCSName { get; set; }

    }

    #endregion
}
