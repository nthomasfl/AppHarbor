using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.DAL.Interfaces
{
    public interface IPollRepository : IDisposable
    {
        PollContext Context();
        IEnumerable<PollQuestion> GetPolls();
        PollQuestion GetPollByID(int id);
        bool userVoted(int userID, int pollID);
        PollAnswer GetAnswerbyID(int id);
        PollQuestion GetPollByGUID(string guid);
        List<PollQuestion> getVotedPolls(int userID);
        IEnumerable<PollAnswer> GetPollAnswers(int id);
        IEnumerable<PollQuestion> GetPollsFromUser(int id);
        IEnumerable<PollQuestion> GetNewestPolls();
        IEnumerable<PollQuestion> GetMostPopularPolls();
        void InsertPoll(PollQuestion poll);
        void DeletePoll(int id);
        void UpdatePoll(PollQuestion poll);
        int Save();
    }
}
