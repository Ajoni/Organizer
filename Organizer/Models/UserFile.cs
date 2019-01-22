using Organizer.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Organizer.Models
{
    public class UserFile
    {
        public static readonly int KILOBYTE = 1024;
        public static readonly int MEGABYTE = KILOBYTE * 1024;
        public static readonly int MAX_FILESIZE = MEGABYTE;
        public static readonly int MAX_USERBYTES = 10 * MEGABYTE;

        [Key]
        // Local Id, for downloading
        public int Id { get; set; }
        public byte[] Bytes { get; set; }
        public string Name { get; set; }

        public UserFile() { }

        public UserFile(HttpPostedFileBase file)
        {
            if (file.InputStream.Length > MAX_FILESIZE)
            {
                throw new Exception("File is too big");
            }
            Bytes = new byte[file.InputStream.Length];
            file.InputStream.Read(Bytes, 0, Bytes.Length);

            Name = file.FileName;
        }

        public string GetSpaceString()
        {
            return GetFileSizeAsString(Bytes.Length);
        }

        public UserFileIndexViewModel GetIndexViewModel()
        {
            return new UserFileIndexViewModel()
            {
                Id = this.Id,
                Name = this.Name,
                SizeString = this.GetSpaceString()
            };
        }

        public static string GetSpaceUsedOverAvailable(IEnumerable<UserFile> files)
        {
            int usedSpace = 0;
            foreach(UserFile file in files)
            {
                usedSpace += file.Bytes.Length;
            }
            return GetFileSizeAsString(usedSpace) + "/" + GetFileSizeAsString(MAX_USERBYTES);
        }

        private static string GetFileSizeAsString(int size)
        {
            if (size < KILOBYTE)
            {
                return size + " B";
            }
            if (size < MEGABYTE)
            {
                return ((double)size / KILOBYTE).ToString("0.##") + " KB";
            }

            return ((double)size / MEGABYTE).ToString("0.##") + " MB";
        }
    }
}