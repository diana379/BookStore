using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;
namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.Category.Any())
                {
                    var CategoryData = File.ReadAllText("../Infrastructure/Data/SeedData/Category.json");
                    var category = JsonSerializer.Deserialize<List<Category>>(CategoryData);

                    foreach (var item in category)
                    {
                        context.Category.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Author.Any())
                {
                    var AuthorData = File.ReadAllText("../Infrastructure/Data/SeedData/Author.json");
                    var author = JsonSerializer.Deserialize<List<Author>>(AuthorData);

                    foreach (var item in author)
                    {
                        context.Author.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Products.Any())
                {
                    var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/Products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    
                    foreach (var item in products)
                    {
                        context.Products.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

            }

            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }

            
        }
    }
}