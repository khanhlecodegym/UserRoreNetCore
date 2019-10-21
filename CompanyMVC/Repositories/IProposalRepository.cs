using CompanyMVC.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyMVC.Repositories
{
    public interface IProposalRepository
    {
        IEnumerable<Proposal> GetAllForConference(int conferenceId);
        IEnumerable<Proposal> GetAllApprovedForConference(int conferenceId);
        void Add(Proposal proposal);
        Proposal Approve(int proposalId);
        void Commit();
    }
}
