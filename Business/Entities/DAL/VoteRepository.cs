using Business.Entities.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Business.Entities.DAL
{
    public class VoteRepository : IVoteRepository, IDisposable
    {
        public PollContext context;

        public VoteRepository(PollContext context)
        {
            this.context = context;
        }

        public IEnumerable<PollVote> GetVotes()
        {
            return context.PollVotes.ToList();
        }

        public PollVote GetVoteByID(int id)
        {
            return context.PollVotes.Find(id);
        }

        public PollVote GetVoteByAnswerID(int id)
        {
            return context.PollVotes.FirstOrDefault(v => v.Answer.ID == id);
        }

        public PollVote GetVoteByUserID(int id)
        {
            return context.PollVotes.FirstOrDefault(v => v.UserId == id);
        }

        public bool userVoted(int pollId, int userId)
        {
            return context.PollVotes.FirstOrDefault(v => v.Answer.Poll.ID == pollId && v.UserId == userId) != null;
        }

        public void InsertVote(PollVote vote)
        {
            context.PollVotes.Add(vote);
        }

        public void DeleteVote(int id)
        {
            context.PollVotes.Remove(GetVoteByID(id));
        }

        public void UpdateVote(PollVote vote)
        {
            context.Entry(vote).State = EntityState.Modified;
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



        public int getVotedAnswerID(int userId, int pollId)
        {
            var vote = context.PollVotes.FirstOrDefault(v => v.UserId == userId && v.Answer.Poll.ID == pollId);
            return vote == null ? 0 : vote.Answer.ID;   
        }
    }
}