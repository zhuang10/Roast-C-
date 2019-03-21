using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RoastMeApplication.Models.Entities
{
    public class Participant
    {
        /*
         * *Atributes
         * */
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        /*
         * *foreign keys
         * */
        public string ApplicationUserId { get; set; }

        /*
         * *navigation props
         * */
        public ApplicationUser ApplicationUser;

        public ICollection<Vote> Votes { get; set; }
        public ICollection<Picture> Pictures { get; set; }
        public ICollection<Comment> Comments { get; set; }

        
      
    }
}