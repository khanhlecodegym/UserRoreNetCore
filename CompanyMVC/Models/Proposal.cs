﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyMVC.Models
{
    public class Proposal
    {
        public int Id { get; set; }
        
        public string Speaker { get; set; }
        public string Title { get; set; }
        public bool Approved { get; set; }

        public int ConferenceId { get; set; }
        public Conference Conference { get; set; }
    }
}
