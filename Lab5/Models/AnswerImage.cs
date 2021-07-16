using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab5.Models
{
    public enum Question  // Question is an enum that has 2 values (earth=0, computer=1)
    {
        Earth, Computer
    }

    public class AnswerImage
    {
        public int AnswerImageId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "File Name")]
        public string FileName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "URL")]
        public string Url { get; set; }

        [Required]
        public Question Question { get; set; }
    }
}
