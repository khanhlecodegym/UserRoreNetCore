using CompanyMVC.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyMVC.Repositories
{
    public interface IAttendeeRepository
    {
        Attendee GetById(int attendeeId);
        Attendee Add(Attendee attendee);
        int GetAttendeesTotal(int conferenceId);
        void Commit();
    }
}
