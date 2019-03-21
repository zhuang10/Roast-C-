using RoastMeApplication.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoastMeApplication.Models.DAL
{
    public class VoteManager
    {
        //Add Voted
        public static void AddVoted(Vote vote)
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                ctx.Votes.Add(vote);
                ctx.SaveChanges();

            }
        }
        public static Vote getVotedByComment_idAndParticipantId(int comment_id, int participant_id)
        {
            Vote vote = null;
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                vote = ctx.Votes.Where(v => v.CommentId == comment_id && v.ParticipantId == participant_id).FirstOrDefault();

            }
            return vote;
        }
        //Edit Voted Islike
        public static void EditVotedIslike(Vote new_vote)
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                Vote vote = ctx.Votes.Where(v => v.Id == new_vote.Id).FirstOrDefault();

                if(vote != null)
                {
                    vote.IsLike = new_vote.IsLike;
                }
                ctx.SaveChanges();
            }
        }

        //Sum Vote Score
        public static void SumVotedScore(int commentId)
        {
            int score =0;
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                List<Vote> votes = ctx.Votes.Where(v => v.CommentId == commentId).ToList();
                foreach (Vote v in votes)
                {
                    if (v.IsLike == true)
                    {
                        score++;
                    }else if(v.IsLike == false)
                    {
                        score--;
                    }
                }

            }
            //comment edit score
            CommentsManage.EditCommentVotedScore(commentId, score);
        }
    }
}