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

            var role = new Role
            {
                Title = "Admin"
            };

            context.Categories.Add(category);
            context.Roles.Add(role);
            context.SaveChanges();

            var user = new User
            {
                Email = "email@email.com",
                Password = "5E884898DA28047151D0E56F8DC6292773603D0D6AABBDD62A11EF721D1542D8",
                PhoneNumber = "8654123694",
                FirstName = "Vardas",
                LastName = "Pavarde",
                PhotoCode = "",
                RoleId = role.Id
            };

            context.Add(user);
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
                UserId = user.Id
            };

            context.Add(pet);
            context.SaveChanges();

            context.Add(new Comment
            {
                PetId = pet.Id,
                CreatedByUserId = user.Id,
                CreatedAt = DateTime.Now,
                Text = "Comment text"
            });

            context.Add(new Like
            {
                PetId = pet.Id,
                UserId = user.Id
            });

            context.SaveChanges();
        }
    }
}
