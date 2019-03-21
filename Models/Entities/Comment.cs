using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoastMeApplication.Models.Entities
{
    public class Comment
    {
        /*
         * *Atributes
         * */
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
        public int VoteScore { get; set; }
        public bool IsFlagged { get; set; }

        /*
         * *foreign keys
         * */
        public int ParticipantId { get; set; }
        public int PictureId { get; set; }

        /*
         * *navigation props
         * */
        public Participant Participant { get; set; }
        public Picture Picture { get; set; }

        public ICollection<Vote> Votes { get; set; }
        public ICollection<Comment> Replies { get; set; } // if it has replies

        public Comment()
        {

        }

        public Comment(String msg, DateTime date, int score, bool isflagged) {
            this.Message = msg;
            this.Time = date;
            this.VoteScore = score;
            this.IsFlagged = IsFlagged;
        }

        /*
         * *methods
         * */
        ////METHODS WILL GO IN MANAGERS
        //public void DeleteComment(int commentId)
        //{
        //    //only admin can do this
        //}

        //public void SortComments(int option)
        //{
        //    //define switch-case
        //}

        //public void FlagComment(int pictureId)
        //{

        //}
    }
}