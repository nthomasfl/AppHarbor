using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business.Entities.DAL
{
    public class UserRepository
    {
        public UserContext context;

        public UserRepository(UserContext context)
        {
            this.context = context;
        }

        public UserProfile GetUserByID(int id)
        {
            return context.UserProfiles.Find(id);
        }
        public List<UserProfile> GetUsers()
        {
            return context.UserProfiles.ToList();
        }
    }
}