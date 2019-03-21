using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoastMeApplication.Models.Entities
{
    public class Vote
    {
        /*
         * *Atributes
         * */
        public int Id { get; set; }
        public bool? IsLike { get; set; }

        /*
         * *foreign keys
         * */
        public int ParticipantId { get; set; }
        public int CommentId { get; set; }

        /*
         * *navigation props
         * */
        public Participant Participant { get; set; }
        public Comment Comment { get; set; }

        /*
         * *methods
         * */
        //METHODS WILL GO IN MANAGERS
        //public void ChangeVote(bool isLike)
        //{

        //}

    }
}