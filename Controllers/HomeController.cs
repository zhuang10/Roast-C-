using RoastMeApplication.Models.DAL;
using RoastMeApplication.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoastMeApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if(Session["participantID"] != null)//check participant id
            {
                ViewBag.participantId = Session["participantID"];
            }

            string listSort = Request.QueryString["ListSort"];//check if user prompted a type of sorting
            List<Picture> picsList = null; //prepare pictures list

            if (listSort == null || listSort == "Recent")
            {
                picsList = PictureManager.SortByRecent();//default sorting, new first
            }
            else
            {
                picsList = PictureManager.SortByPopular();//popular sorting, most comments
            }

            ViewBag.pages = Math.Ceiling(((double)picsList.Count / 10));//number of pages, of 10 pictures each, in the pictures list

            if (Request.QueryString["page"] == null) //if no page selected *DEFAULT*
            {
                if(picsList.Count <= 10) //if 10 pictures or less in total
                {
                    ViewBag.Pictures = picsList; //show all
                }
                else //if more than 10 pictures in total
                {
                    ViewBag.Pictures = picsList.GetRange(0, 10); //show first ten
                }
            }
            else //if a page has been selected by the user
            {
                int pageNum = Int32.Parse(Request.QueryString["page"]);//get page number 

                if (picsList.Count <= pageNum*10) //if current page has 10 pictures or less in total (last page)
                {
                    int i = picsList.Count;
                    if(((pageNum - 1) * 10) == (picsList.Count - 1)){
                        ViewBag.Pictures = new List<Picture>() { picsList[(pageNum - 1) * 10] };//if only one picture in page
                    }else
                    {
                        ViewBag.Pictures = picsList.GetRange((pageNum - 1) * 10, picsList.Count - 10 * (pageNum - 1)); //show remaining
                    }
                }
                else //if page has 10 pictures
                {
                    ViewBag.Pictures = picsList.GetRange((pageNum - 1) * 10, 10);//if page selected is "1" it will show range from 0 to 10
                }
                
            }

            

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}