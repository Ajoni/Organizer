﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace Organizer.Models
{
    public class Post
    {
        [Key] public int Id { get; set; }
        [Required] public string content { get; set; }

    }
}