using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Organizer.ViewModels
{
    public class FilesIndexViewModel
    {
        public string SpaceUsedOverAvailable { get; set; }
        public IEnumerable<UserFileIndexViewModel> Files { get; set; }
    }
}