using RoastMeApplication.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoastMeApplication.Models.DAL
{
    public class CommentsManage
    {
        public static List<Comment> GetAll()
        {
            List<Comment> allComments = null;

            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                allComments = ctx.Comments.Include("Picture").Include("Participant").Include("Votes").Include("Replies").ToList();
            }

            return allComments;
        }

        //Get Comment By Picture Id
        public static List<Comment> GetCommentByPictureId(int picture_id)
        {
            List<Comment> comment = null;
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                comment = ctx.Comments.Include("Votes").Include("Participant").Where(c=>c.PictureId == picture_id).OrderBy( c =>c.Time).ToList();
                
            }
            return comment;
        }
        public static Comment GetCommentByDateTime(DateTime t)
        {
            Comment comment = null;
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                comment = ctx.Comments.Include("Picture").Where(c => c.Time == t).OrderBy(c => c.Time).FirstOrDefault();

            }
            return comment;
        }
        //Add Comment
        public static void AddComment(Comment comment)
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                ctx.Comments.Add(comment);
                ctx.SaveChanges();

            }
        }

        internal static Comment GetCommentById(int id)
        {
            Comment comment = null;
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                comment = ctx.Comments.Include("Picture").Include("Votes").Include("Replies").Include("Participant").Where(c => c.Id == id).FirstOrDefault();
            }
            return comment;
        }

        //Edit Flagged
        public static void EditCommentFlagged(Comment new_comment)
        {
            Comment comment = null;
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                comment = ctx.Comments.Where(c => c.Id == new_comment.Id).FirstOrDefault();

                if (comment != null)
                {
                    comment.IsFlagged = new_comment.IsFlagged;                    
                }
                ctx.SaveChanges();
            }
        }

        public static List<Comment> GetCommentByFlagged(int picture_id)
        {
            List<Comment> comment = null;
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                comment = ctx.Comments.Where(c => c.PictureId == picture_id).OrderBy(c => c.Time).ToList();

            }
            return comment;
        }

        //Edit VotedScore
        public static void EditCommentVotedScore(int comment_id,int score)
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                Comment comment = ctx.Comments.Where(c => c.Id == comment_id).FirstOrDefault();

                if (comment != null)
                {
                    comment.VoteScore = score;
                }
                ctx.SaveChanges();
            }

        }

        //Delete Comment
        public static void DeleteComment(Comment new_comment)
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                Comment comment = ctx.Comments.Include("Votes").Where(c => c.Id == new_comment.Id).FirstOrDefault();

                if (comment != null)
                {
                    //delete
                    ctx.Comments.Remove(comment);
                    ctx.Votes.RemoveRange(comment.Votes);
                }
                //save changes
                ctx.SaveChanges();
            }

        }

        public static List<Comment> SortByRecent(List<Comment> comments)
        {
            List<Comment> sortedComments = comments.OrderByDescending(c => c.Time).ToList();
            return sortedComments;
        }

        public static List<Comment> SortByPopular(List<Comment> comments)
        {
            List<Comment> sortedComments = comments.OrderByDescending(c => c.VoteScore).ToList();
            return sortedComments;
        }

        public static List<Comment> GetFlagged()
        {
            List<Comment> allComments = GetAll();
            List<Comment> flaggedComments = allComments.Where(c => c.IsFlagged == true).ToList();
            return flaggedComments;
        }
    }
}