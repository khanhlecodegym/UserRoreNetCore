using CompanyMVC.ConnectDB;
using CompanyMVC.Models;
using System.Collections.Generic;
using System.Linq;

namespace CompanyMVC.Repositories
{
    public class ConferenceRepository : IConferenceRepository
    {
        private readonly AppDbContext _dbContext;

        public ConferenceRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Conference> GetAll => _dbContext.Conferences;

        public void Add(Conference conference)
        {
            _dbContext.Conferences.Add(conference);
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public Conference GetById(int id)
        {
            return _dbContext.Conferences.First(c => c.Id == id);
        }

    }
}
