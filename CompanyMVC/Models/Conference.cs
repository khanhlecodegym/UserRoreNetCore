using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyMVC.Models
{
    public class Conference
    {
        public Conference()
        {
            Start = DateTime.Now;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public string Location { get; set; }

        public List<Proposal> Proposals { get; set; }
        public List<Attendee> Attendees { get; set; }
    }
}
