using ForumVanhackAccelerator.Data.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumVanhackAccelerator.Data
{
    public static class DbSeeder
    {
        #region Public Methods
        public static void Seed(ForumDbContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            // Create default Users (if there are none)
            if (!dbContext.Users.Any())
            {
                CreateUsers(dbContext, roleManager, userManager)
                    .GetAwaiter()
                    .GetResult();
            }

            // Create default Topics (if there are none)
            if (!dbContext.Topics.Any()) CreateTopics(dbContext);
        }
        #endregion

        #region Seed Methods
        private static async Task CreateUsers(ForumDbContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            // local variables
            DateTime createdDate = DateTime.Now;
            DateTime lastModifiedDate = DateTime.Now;

            string role_RegisteredUser = "RegisteredUser";

            // create roles (if they doesn't exist yet)
            bool exists = await roleManager.RoleExistsAsync(role_RegisteredUser);
            if (!await roleManager.RoleExistsAsync(role_RegisteredUser))
            {
                await roleManager.CreateAsync(new IdentityRole(role_RegisteredUser));
            }

            // Create some sample registered user accounts (if they don't exist already)
            var user_Adam = new ApplicationUser()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "adam",
                Email = "adam@forumvanhackaccelerator.com"
            };

            var user_Daniel = new ApplicationUser()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "daniel",
                Email = "daniel@forumvanhackaccelerator.com"
            };

            var user_Nigel = new ApplicationUser()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "nigel",
                Email = "nigel@forumvanhackaccelerator.com"
            };

            // Insert sample registered users into the Database and also assign the "Registered" role to him.
            if (await userManager.FindByNameAsync(user_Adam.UserName) == null)
            {
                await userManager.CreateAsync(user_Adam, "Pass4Admin");
                await userManager.AddToRoleAsync(user_Adam, role_RegisteredUser);

                // Remove Lockout and E-Mail confirmation.
                user_Adam.EmailConfirmed = true;
                user_Adam.LockoutEnabled = false;
            }
            if (await userManager.FindByNameAsync(user_Daniel.UserName) == null)
            {
                await userManager.CreateAsync(user_Daniel, "Pass4Admin");
                await userManager.AddToRoleAsync(user_Daniel, role_RegisteredUser);

                // Remove Lockout and E-Mail confirmation.
                user_Daniel.EmailConfirmed = true;
                user_Daniel.LockoutEnabled = false;
            }
            if (await userManager.FindByNameAsync(user_Nigel.UserName) == null)
            {
                await userManager.CreateAsync(user_Nigel, "Pass4Admin");
                await userManager.AddToRoleAsync(user_Nigel, role_RegisteredUser);

                // Remove Lockout and E-Mail confirmation.
                user_Nigel.EmailConfirmed = true;
                user_Nigel.LockoutEnabled = false;
            }

            await dbContext.SaveChangesAsync();
        }

        private static void CreateTopics(ForumDbContext dbContext)
        {
            // local variables
            DateTime createdDate = DateTime.Now;
            DateTime lastModifiedDate = DateTime.Now;

            // retrieve the adam user, which we'll use as default author.
            var authorId = dbContext.Users
                .Where(u => u.UserName == "adam")
                .FirstOrDefault()
                .Id;

            // insert sample topics into the Database and assigned to the user adam
            List<Topic> topics = new List<Topic>()
            {
                new Topic { Title = "Titulo 1", Description = "Descrição 1", UserId = authorId, CreatedDate = DateTime.Now, LastModifiedDate = DateTime.Now },
                new Topic { Title = "Titulo 2", Description = "Descrição 2", UserId = authorId, CreatedDate = DateTime.Now, LastModifiedDate = DateTime.Now },
                new Topic { Title = "Titulo 3", Description = "Descrição 3", UserId = authorId, CreatedDate = DateTime.Now, LastModifiedDate = DateTime.Now }
            };
            dbContext.AddRange(topics);

            // persist the changes on the Database
            dbContext.SaveChanges();
        }
        #endregion
    }
}
