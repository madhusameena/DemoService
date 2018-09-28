using System;
using System.Collections.Generic;

namespace DemoService.Models
{
    public partial class Project
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public byte[] Img { get; set; }
        public int? UserId { get; set; }

        public Userdetails User { get; set; }
    }
}
