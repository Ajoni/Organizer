using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Organizer.ViewModels
{
    public class FilesUploadViewModel
    {
        [Required]
        public HttpPostedFileBase File { get; set; }
    }
}