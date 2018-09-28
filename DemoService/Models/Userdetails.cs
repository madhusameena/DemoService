using System;
using System.Collections.Generic;

namespace DemoService.Models
{
    public partial class Userdetails
    {
        public Userdetails()
        {
            Project = new HashSet<Project>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }

        public ICollection<Project> Project { get; set; }
    }
}
