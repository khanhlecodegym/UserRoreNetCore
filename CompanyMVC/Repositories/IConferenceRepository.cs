using CompanyMVC.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyMVC.Repositories
{
    public interface IConferenceRepository
    {
        IEnumerable<Conference> GetAll { get; }
        Conference GetById(int id);
        void Add(Conference conference);
        void Commit();
    }
}
