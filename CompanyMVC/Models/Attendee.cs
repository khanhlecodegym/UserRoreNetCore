﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyMVC.Models
{
    public class Attendee
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int ConferenceId { get; set; }
        public Conference Conference { get; set; }
    }
}
