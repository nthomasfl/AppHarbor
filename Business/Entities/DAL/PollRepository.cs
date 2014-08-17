using Business.Entities.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Business.Entities.DAL
{
    public class PollRepository: IPollRepository,  IDisposable
    {
        public PollContext context;

        public PollRepository(PollContext context) {
            this.context = context;
        }



        public IEnumerable<PollQuestion> GetPolls()
        {
            return context.Polls.OrderBy(p => p.Question).ToList();
        }

        public IEnumerable<PollQuestion> GetNewestPolls()
        {
            return context.Polls.Where(p => p.Public && p.Active).OrderByDescending(p => p.CreateDate).ThenBy(p => p.Question).ToList();
        }

        public IEnumerable<PollQuestion> GetMostPopularPolls()
        {
            return context.Polls.Where(p => p.Public && p.Active).OrderByDescending(p => p.PollVotes.Count).ThenBy(p => p.Question).ToList();
        }

        public PollQuestion GetPollByID(int id)
        {
            return context.Polls.Find(id);
        }

        public PollQuestion GetPollByGUID(string guid)
        {
            return context.Polls.FirstOrDefault(p => p.GUID.ToString().Equals(guid));
        }

        public void InsertPoll(PollQuestion poll)
        {
            context.Polls.Add(poll);
        }

        public void DeletePoll(int id)
        {
            context.Polls.Remove(GetPollByID(id));
        }

        public void UpdatePoll(PollQuestion poll)
        {
            context.Entry(poll).State = EntityState.Modified;
        }

        public int Save()
        {
            return context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }





        public IEnumerable<PollAnswer> GetPollAnswers(int id)
        {
            return context.Answers.Where(p => p.Poll.ID == id).OrderBy(a => a.Answer).ToList();
        }


        public IEnumerable<PollQuestion> GetPollsFromUser(int id)
        {
            return context.Polls.Where(p => p.UserID == id).OrderBy(p => p.Question).ToList();
        }

        PollContext IPollRepository.Context()
        {
            return context;
        }

        public PollAnswer GetAnswerbyID(int id)
        {
            return context.Answers.Find(id);
        }


        public bool userVoted(int userID, int pollID)
        {
            return GetPollByID(pollID).PollVotes.SingleOrDefault(p => p.UserId==userID) != null;
        }


        public List<PollQuestion> getVotedPolls(int userID)
        {
            return context.PollVotes.Where(p => p.UserId == userID).Select(p => p.Poll).ToList();
        }
    }
}