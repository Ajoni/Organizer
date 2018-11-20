using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

public class Post
{
	[Key] public int postId { get; set; }
    [ForeignKey("Group")] public int Nr { get; set; }
    [Required] public string content { get; set; }

}
