using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab4.Models
{
    // CommunityMembership can have none or 1 student / and none or 1 Community
    public class CommunityMembership
    {
        [Required]
        public int StudentId
        {
            get;
            set;
        }

        [Required]
        public string CommunityId
        {
            get;
            set;
        }
    }
}
