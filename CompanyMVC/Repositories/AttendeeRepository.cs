using CompanyMVC.ConnectDB;
using CompanyMVC.Models;
using System.Linq;

namespace CompanyMVC.Repositories
{
    public class AttendeeRepository : IAttendeeRepository
    {
        private readonly AppDbContext _dbContext;

        public AttendeeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Attendee Add(Attendee attendee)
        {
            _dbContext.Attendees.Add(attendee);
            return attendee;
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public int GetAttendeesTotal(int conferenceId)
        {
            return _dbContext.Attendees.Count(a => a.ConferenceId == conferenceId);
        }

        public Attendee GetById(int attendeeId)
        {
            return _dbContext.Attendees.First(a => a.Id == attendeeId);
        }
    }
}
