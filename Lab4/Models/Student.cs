using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; //added 

namespace Lab4.Models
{
    public class Student
    {

        public int Id 
        { 
            get; 
            set; 
        }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName 
        { 
            get; 
            set; 
        }

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName 
        { 
            get; 
            set; 
        }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Enrollment Data")]
        public DateTime EnrollmentDate 
        { 
            get; 
            set; 
        }

        [Required]
        [Display(Name = "Full Name")]
        public string FullName 
        {
            get
            {
                return LastName + ", " + FirstName;
            }
        }

        // Student can have 1 or no CommunityMembership. Referenced the following: https://docs.microsoft.com/en-us/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-simple-key%2Csimple-key
        public List<CommunityMembership> Memberships 
        { 
            get; 
            set; 
        }
    }
}
