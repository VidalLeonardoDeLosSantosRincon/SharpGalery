using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SharpGalery.Models
{
    public class Image
    {
        [Key]
        public int ImageId { get; set; }

        [Required]
        [StringLength(120)]
        public string Title { get; set; }

        [DisplayName("Upload File")]
        //[Required]
        public string ImagePath { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}