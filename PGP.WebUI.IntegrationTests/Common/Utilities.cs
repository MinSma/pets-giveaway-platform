using Newtonsoft.Json;
using PGP.Domain.Entities;
using PGP.Persistence;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PGP.WebUI.IntegrationTests.Common
{
    public class Utilities
    {
        public static StringContent GetRequestContent(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }

        public static async Task<T> GetResponseContent<T>(HttpResponseMessage response)
        {
            var stringResponse = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(stringResponse);

            return result;
        }

        public static void InitializeDbForTests(PGPDbContext context)
        {
            var category = new Category
            {
                Title = "Dogs"
            };

            var category2 = new Category
            {
                Title = "Cats"
            };

            var adminRole = new Role
            {
                Title = "Admin"
            };

            var moderatorRole = new Role
            {
                Title = "Moderator"
            };

            var userRole = new Role
            {
                Title = "User"
            };


            context.Categories.Add(category);
            context.Categories.Add(category2);
            
            context.Roles.Add(adminRole);
            context.Roles.Add(moderatorRole);
            context.Roles.Add(userRole);
            
            context.SaveChanges();

            var admin = new User
            {
                Email = "admin@admin.com",
                Password = "5E884898DA28047151D0E56F8DC6292773603D0D6AABBDD62A11EF721D1542D8",
                PhoneNumber = "8654123694",
                FirstName = "Admin",
                LastName = "Adminas",
                PhotoCode = "",
                RoleId = adminRole.Id
            };

            var moderator = new User
            {
                Email = "moderator@email.com",
                Password = "5E884898DA28047151D0E56F8DC6292773603D0D6AABBDD62A11EF721D1542D8",
                PhoneNumber = "8654123694",
                FirstName = "Moderator",
                LastName = "Moderatorius",
                PhotoCode = "",
                RoleId = moderatorRole.Id
            };

            var user = new User
            {
                Email = "petras@petrauskas.com",
                Password = "5E884898DA28047151D0E56F8DC6292773603D0D6AABBDD62A11EF721D1542D8",
                PhoneNumber = "8654123694",
                FirstName = "Petras",
                LastName = "Petrauskas",
                PhotoCode = "",
                RoleId = userRole.Id
            };

            context.Users.Add(admin);
            context.Users.Add(moderator);
            context.Users.Add(user);
            
            context.SaveChanges();

            var pet = new Pet
            {
                Name = "Mur",
                Age = 5,
                Gender = Domain.Enums.Gender.Male,
                Weight = null,
                Height = null,
                IsSterilized = true,
                Description = "Something about",
                DateAdded = DateTime.Now,
                State = Domain.Enums.State.NotGivenAway,
                PhotoCode = "",
                CategoryId = category.Id,
                UserId = moderator.Id
            };

            context.Pets.Add(pet);
            context.SaveChanges();

            context.Comments.Add(new Comment
            {
                PetId = pet.Id,
                CreatedByUserId = user.Id,
                CreatedAt = DateTime.Now,
                Text = "Comment text"
            });

            context.Comments.Add(new Comment
            {
                PetId = pet.Id,
                CreatedByUserId = user.Id,
                CreatedAt = DateTime.Now,
                Text = "Comment text 2"
            });

            context.Likes.Add(new Like
            {
                PetId = pet.Id,
                UserId = user.Id
            });

            context.SaveChanges();
        }
    }
}
