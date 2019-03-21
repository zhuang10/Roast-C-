using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RoastMeApplication.Models.Entities
{
    public class Picture
    {
        /*
         * *Atributes
         * */
        public int Id { get; set; }
        public string Caption { get; set; }
        public string ImagePath { get; set; }
        public DateTime Time { get; set; }
        public bool IsFlagged { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        /*
         * *foreign keys
         * */
        public int ParticipantId { get; set; }

        /*
         * *navigation props
         * */
        public Participant Participant { get; set; }
 
        public ICollection<Comment> Comments { get; set; }

        /*
         * *methods
         * */
         //METHODS WILL GO IN MANAGERS
    }
}