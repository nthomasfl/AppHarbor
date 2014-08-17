using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
namespace Business.Entities.DAL
{
    public class PollContext : DbContext
    {
        public PollContext()
            : base("pollConnectionString")
        {
          
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<PollQuestion> Polls { get; set; }
        public DbSet<PollAnswer> Answers { get; set; }
        public DbSet<PollVote> PollVotes { get; set; }
       
    }

    public class UserContext : DbContext
    {
        public UserContext()
            : base("pollConnectionString")
        {
          
        }
        public DbSet<UserProfile> UserProfiles { get; set; }
    }
}