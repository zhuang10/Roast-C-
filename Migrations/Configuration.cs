namespace RoastMeApplication.Migrations
{
    using Microsoft.AspNet.Identity;
    using Models;
    using Models.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RoastMeApplication.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(RoastMeApplication.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            //context.Participants.Add(new Participant("admin", "admin", "admin@abc.com"));

            //Comment comment1 = new Comment("I hate you", DateTime.Now.AddDays(-5), 0, true);
            //comment1.ParticipantId = 2;
            //comment1.PictureId = 1;

            //Comment comment2 = new Comment("I will kill your puppy", DateTime.Now.AddDays(-5), 0, true);
            //comment2.ParticipantId = 2;
            //comment2.PictureId = 2;

            //Comment comment3 = new Comment("yer face is funney", DateTime.Now.AddDays(-5), 0, false);
            //comment3.ParticipantId = 5;
            //comment3.PictureId = 4;

            //Comment comment4 = new Comment("yer face is funney", DateTime.Now.AddDays(-5), 0, false);
            //comment4.ParticipantId = 3;
            //comment4.PictureId = 4;

            //Comment comment5 = new Comment("yer face is funney", DateTime.Now.AddDays(-5), 0, false);
            //comment5.ParticipantId = 4;
            //comment5.PictureId = 5;

            //Comment comment6 = new Comment("yer face is funney", DateTime.Now.AddDays(-5), 0, false);
            //comment6.ParticipantId = 5;
            //comment6.PictureId = 5;

            //context.Comments.AddRange(new List<Comment>() { comment1, comment2 });
            //AddAdminRole(context);
        }

        private void AddAdminRole(RoastMeApplication.Models.ApplicationDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                context.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole { Name = "Admin" });
            }
        }
    }
}
