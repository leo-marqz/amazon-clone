
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Models.Authorization;
using Ecommerce.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using Newtonsoft.Json;

namespace Infrastructure.Persistence
{
    public class EcommerceDataFake
    {
        public static async Task LoadAsync(
            EcommerceDBContext context, 
            UserManager<User> userManager, 
            RoleManager<IdentityRole> roleManager, 
            ILoggerFactory loggerFactory)
        {
            try
            {
                if(!roleManager.Roles.Any()){
                    await roleManager.CreateAsync(new IdentityRole(Role.ADMIN));
                    await roleManager.CreateAsync(new IdentityRole(Role.USER));
                    await roleManager.CreateAsync(new IdentityRole(Role.GUEST));
                }

                if(!userManager.Users.Any()){
                    var userAdmin = new User {
                        Name = "Leo",
                        LastName = "Marqz",
                        UserName = "leomarqz",
                        Email = "leomarqz@dark.net",
                        AvatarUrl = "https://firebasestorage.googleapis.com/v0/b/edificacion-app.appspot.com/o/avatar-1.webp?alt=media&token=58da3007-ff21-494d-a85c-25ffa758ff6d",
                    };

                    await userManager.CreateAsync(userAdmin, "ABC498yy087y67knjlkjnkij$");
                    await userManager.AddToRoleAsync(userAdmin, Role.ADMIN);

                }

                if(!context.Categories.Any()){
                    var categories = File.ReadAllText("../Infrastructure/Data/category.json");
                    var categoriesObjs = JsonConvert.DeserializeObject<List<Category>>(categories);
                    await context.Categories.AddRangeAsync(categoriesObjs!);
                    await context.SaveChangesAsync();
                }

                if(!context.Products.Any()){
                    var products = File.ReadAllText("../Infrastructure/Data/product.json");
                    var productsObjs = JsonConvert.DeserializeObject<List<Product>>(products);
                    await context.Products.AddRangeAsync(productsObjs!);
                    await context.SaveChangesAsync();
                }

                if(!context.Images.Any()){
                    var images = File.ReadAllText("../Infrastructure/Data/image.json");
                    var imagesObjs = JsonConvert.DeserializeObject<List<Image>>(images);
                    await context.Images.AddRangeAsync(imagesObjs!);
                    await context.SaveChangesAsync();
                }

                if(!context.Reviews.Any()){
                    var reviews = File.ReadAllText("../Infrastructure/Data/review.json");
                    var reviewsObjs = JsonConvert.DeserializeObject<List<Review>>(reviews);
                    await context.Reviews.AddRangeAsync(reviewsObjs!);
                    await context.SaveChangesAsync();
                }

                if(!context.Countries.Any()){
                    var countries = File.ReadAllText("../Infrastructure/Data/countries.json");
                    var countriesObjs = JsonConvert.DeserializeObject<List<Country>>(countries);
                    await context.Countries.AddRangeAsync(countriesObjs!);
                    await context.SaveChangesAsync();
                }
            }
            catch (System.Exception e)
            {
                var logger = loggerFactory.CreateLogger<EcommerceDataFake>();
                logger.LogError(e.Message);
            }
        }
    }
}