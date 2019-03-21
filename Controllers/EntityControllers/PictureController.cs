using Newtonsoft.Json.Linq;
using RoastMeApplication.Models.DAL;
using RoastMeApplication.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoastMeApplication.Controllers.EntityControllers
{
    [Authorize]
    public class PictureController : Controller
    {
        // GET: Picture
        public ActionResult PictureDetail(int id)
        {
            ViewBag.picture = PictureManager.GetPictureById(id);
            
            
            if (Session["participantID"] != null)
            {
                ViewBag.Participant = ParticipantManager.GetById(Convert.ToInt32(Session["participantID"]));
            }            

            string listSort = Request.QueryString["ListSort"];//check if user prompted a type of sorting
           
            if (listSort == null || listSort == "Recent")
            {
                ViewBag.comments = CommentsManage.SortByRecent(CommentsManage.GetCommentByPictureId(id));
            }
            else
            {
                ViewBag.comments = CommentsManage.SortByPopular(CommentsManage.GetCommentByPictureId(id));//popular sorting, most votes
            }

            return View();
        }
        [HttpGet]
        public JsonResult LikeVote(string JsonResult)
        {
            string result = JsonResult;
            Vote vote = null;
            JObject jobj = JObject.Parse(JsonResult);
            vote = VoteManager.getVotedByComment_idAndParticipantId(Convert.ToInt32(jobj["CommentId"]), Convert.ToInt32(jobj["ParticipantId"]));
            int comment_id = Convert.ToInt32(jobj["CommentId"]);
            int partucipant_id = Convert.ToInt32(jobj["ParticipantId"]);
            int ck = Convert.ToInt32(jobj["isLike"]);
            if (vote == null)
            {
                vote = new Vote();
                vote.CommentId = comment_id;
                vote.ParticipantId = partucipant_id;
                
                vote.IsLike = null;
                if (ck == -1)
                {
                    vote.IsLike = null;
                }
                else if (ck == 0)
                {
                    vote.IsLike = false;
                }
                else if (ck == 1)
                {
                    vote.IsLike = true;
                }
                VoteManager.AddVoted(vote);
                VoteManager.SumVotedScore(Convert.ToInt32(jobj["CommentId"]));
            }else
            {
                vote.IsLike = null;
                if (ck ==-1)
                {
                    vote.IsLike = null;
                }
                else if(ck == 0)
                {
                    vote.IsLike = false;
                }
                else if(ck == 1)
                {
                    vote.IsLike = true;
                }
                VoteManager.EditVotedIslike(vote);
                VoteManager.SumVotedScore(Convert.ToInt32(jobj["CommentId"]));
            }
            Comment comment = CommentsManage.GetCommentById(Convert.ToInt32(jobj["CommentId"]));
            String res = comment.VoteScore+"";
            String[] str = new String[2];
            str[0] = res;
            str[1] = ck+"";
            return Json(str, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GiveFlagged(string JsonFlag)
        {
            string result = JsonFlag;
            JObject jobj = JObject.Parse(result);
            Comment comment = CommentsManage.GetCommentById(Convert.ToInt32(jobj["CommentId"]));
            comment.IsFlagged = true;
            CommentsManage.EditCommentFlagged(comment);
            String res = "Flag Comment Success";
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GivePictureFlagged(string JsonFlag)
        {
            string result = JsonFlag;
            JObject jobj = JObject.Parse(result);
            Picture picture = PictureManager.GetPictureById(Convert.ToInt32(jobj["id"]));
            picture.IsFlagged = true;
            PictureManager.EditPictureFlagged(picture);
            String res = "Flag Picture Success";
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SubmitComment(Comment comment)
        {
            comment.IsFlagged = false;
            DateTime t = DateTime.Now;
            comment.IsFlagged = false;
            comment.Time = t;
            comment.VoteScore = 0;
            CommentsManage.AddComment(comment);
            //comment = CommentsManage.GetCommentByDateTime(t);
            //comment = CommentsManage.GetCommentById(comment.Id);

            //Vote vote = new Vote();
            //vote.CommentId = comment.Id;
            //vote.ParticipantId = comment.;
            //vote.IsLike = null;
            //VoteManager.AddVoted(vote);

            return RedirectToAction("PictureDetail", new { id = comment.PictureId });
        }


        public ActionResult SubmitPicture()
        {
            if (Session["participantID"] != null)
            {
                ViewBag.ParticipantID = Session["participantID"];
            }

            return View();
        }

        [HttpPost]
        public ActionResult SubmitPicture(Picture img, HttpPostedFileBase file)
        {
            if (file != null)
            {
                if (!(file.FileName.Contains("jpg") || file.FileName.Contains("png") || file.FileName.Contains("gif") || file.FileName.Contains("jpeg")))
                {
                    ModelState.AddModelError("Path", "Our image just use jpg,png,gif and jpeg");
                    TempData["error"] = "Our image just use jpg,png,gif and jpeg";
                }
            }
            if (ModelState.IsValid)
            {
                if (file != null)
                {

                    file.SaveAs(Server.MapPath("~/Content/Images/" + file.FileName));

                    img.ImagePath = file.FileName;
                    img.Time = DateTime.Now;
                    img.ParticipantId = img.ParticipantId;
                    img.IsFlagged = false;
                    img.Caption = img.Caption;
                    PictureManager.AddPicture(img);
                }
                //img is true
                return RedirectToAction("Index", controllerName: "Home");
            }
            else
            {
                // If img is error 
                return View();
            }

        }

        [Authorize(Roles = "Admin")]
        public ActionResult ManageFlags()
        {
            ViewBag.FlaggedPics = PictureManager.GetFlagged();
            ViewBag.FlaggedComments = CommentsManage.GetFlagged();
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult ManageFlags(string Type, string Action, int id)
        {
                if(Type == "Picture")
            {
                Picture pic = PictureManager.GetPictureById(id);
                if(Action == "unflag")
                {
                    pic.IsFlagged = false;
                    PictureManager.EditPictureFlagged(pic);
                }
                else
                {
                    PictureManager.DeletePicture(pic);
                }
            }
            else
            {
                Comment comment = CommentsManage.GetCommentById(id);
                if (Action == "unflag")
                {
                    comment.IsFlagged = false;
                    CommentsManage.EditCommentFlagged(comment);
                }
                else
                {
                    CommentsManage.DeleteComment(comment);
                }
            }

            return RedirectToAction("ManageFlags");
        }
    }
}