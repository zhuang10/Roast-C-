using RoastMeApplication.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoastMeApplication.Models.DAL
{
    public class ParticipantManager
    {
        /*
         * *methods for Participant
         * */
        public static void Add(Participant participant)
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                ctx.Participants.Add(participant);
                ctx.SaveChanges();
            }
        }

        public static List<Participant> GetAll()
        {
            List<Participant> allParts = null;

            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                allParts = ctx.Participants.ToList();
            }

            return allParts;
        }

        public static Participant GetById(int id)
        {
            Participant part = null;
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                //eager loading of the navigation property Program
                part = ctx.Participants.Where(s => s.Id == id).FirstOrDefault();

            }

            return part;
        }

        public static Participant GetByEmail(string email)
        {
            Participant part = null;
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                //eager loading of the navigation property Program
                part = ctx.Participants.Where(s => s.Email == email).FirstOrDefault();

            }

            return part;
        }

        //(Roles = "Admin")
        public static void DeleteById(int id)
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                //search
                Participant part = ctx.Participants.Where(s => s.Id == id).FirstOrDefault();
                if (part != null)
                {
                    //delete
                    ctx.Participants.Remove(part);
                }
                //save changes
                ctx.SaveChanges();
            }
        }

    }
}