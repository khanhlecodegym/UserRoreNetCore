using CompanyMVC.ConnectDB;
using CompanyMVC.Models;
using System.Collections.Generic;
using System.Linq;

namespace CompanyMVC.Repositories
{
    public class ProposalRepository : IProposalRepository
    {
        private readonly AppDbContext _dbContext;

        public ProposalRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Proposal proposal)
        {
            _dbContext.Proposals.Add(proposal);
        }

        public Proposal Approve(int proposalId)
        {
            var proposal = _dbContext.Proposals.FirstOrDefault(p => p.Id == proposalId);
            proposal.Approved = true;

            return proposal;
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public IEnumerable<Proposal> GetAllApprovedForConference(int conferenceId)
        {
            return GetAllForConference(conferenceId).Where(p => p.Approved);
        }

        public IEnumerable<Proposal> GetAllForConference(int conferenceId)
        {
            return _dbContext.Proposals.Where(p => p.ConferenceId == conferenceId);
        }
    }
}
