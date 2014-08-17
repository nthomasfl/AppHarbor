using Business.Entities;
using Business.Entities.DAL;
using Business.Entities.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace App.Controllers
{
    public class PollValuesController : ApiController
    { 
         private IPollRepository pollRepository;

         public PollValuesController()
         {
            pollRepository = new PollRepository(new PollContext());

        }
        // GET api/<controller>
        public IEnumerable<PollQuestion> Get()
        {
            return pollRepository.GetPolls().AsEnumerable();
        }

        public IEnumerable<PollQuestion> GetMostPopular()
        {
            return pollRepository.GetMostPopularPolls().AsEnumerable();
        }

        public IEnumerable<PollQuestion> GetMostRecent()
        {
            return pollRepository.GetNewestPolls().AsEnumerable();
        }

        // GET api/<controller>/5
        public PollQuestion Get(int id)
        {
            PollQuestion poll = pollRepository.GetPollByID(id);

            if (poll == null)
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            return poll;
        }


      
    }
}