using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.DAL.Interfaces
{
    public interface IVoteRepository : IDisposable
    {
        IEnumerable<PollVote> GetVotes();
        PollVote GetVoteByID(int id);
        PollVote GetVoteByAnswerID(int id);
        PollVote GetVoteByUserID(int id);
        int getVotedAnswerID(int userId, int pollId);
        bool userVoted(int pollId, int userId);
        void InsertVote(PollVote vote);
        void DeleteVote(int id);
        void UpdateVote(PollVote vote);
        int Save();
    }
}
