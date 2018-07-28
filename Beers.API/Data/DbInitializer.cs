using System.Data;
using Beers.API.Extensions;
using Beers.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Beers.API.Data
{
    public class DbInitializer
    {
        public static void Initialize(BeerContext context)
        {
            var connection = context.Database.GetDbConnection();
            if (connection.State != ConnectionState.Open) connection.Open();

            context.Breweries.AddOrUpdate(new Brewery { Id = 1, Name = "Moortgat", Address = "Breendonk-Dorp 58", City = "Puurs", Country = "Belgium" });
            context.Breweries.AddOrUpdate(new Brewery { Id = 2, Name = "Brouwerij Achouffe", Address = "Achouffe 3", City = "Houffalize", Country = "Belgium" });
            context.Breweries.AddOrUpdate(new Brewery { Id = 3, Name = "Brouwrij bosteels", Address = "Kerkstraat 96", City = "Buggenhout", Country = "Belgium" });
            context.Breweries.AddOrUpdate(new Brewery { Id = 4, Name = "Inbev", Address = "Brouwerijplein 1", City = "Leuven", Country = "Belgium" });
            context.Breweries.AddOrUpdate(new Brewery { Id = 5, Name = "Brouwrij d'Oude Caert", Address = "Kortestraat 72", City = "Brasschaat", Country = "Belgium" });
            context.Breweries.AddOrUpdate(new Brewery { Id = 6, Name = "Brouwrij bosteels", Address = "Kerkstraat 96", City = "Buggenhout", Country = "Belgium" });
            context.Breweries.AddOrUpdate(new Brewery { Id = 7, Name = "Brouwerij Omer Vander Ghinste", Address = "Kwabrugstraat 5", City = "Bellegem", Country = "Belgium" });

            //SaveChanges for every table, identity insert is only allowed for 1 table at a time: https://stackoverflow.com/questions/23832598/identity-insert-is-already-on-for-table-x-cannot-perform-set-operation-for-ta
            context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Breweries ON");
            context.SaveChanges();
            context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Breweries OFF");

            context.Beers.AddOrUpdate(new Beer { Id = 1, Name = "Duvel", Rating = 5, BreweryId = 1, ImageUrl = $"/img/full/Duvel.png", ThumbnailImageUrl = $"/img/thumbnail/Duvel.png" });
            context.Beers.AddOrUpdate(new Beer { Id = 2, Name = "La Chouffe", Rating = 5, BreweryId = 2, ImageUrl = $"/img/full/Chouffe.png", ThumbnailImageUrl = $"/img/thumbnail/Chouffe.png" });
            context.Beers.AddOrUpdate(new Beer { Id = 3, Name = "Kwak", Rating = 5, BreweryId = 3, ImageUrl = $"/img/full/Kwak.png", ThumbnailImageUrl = $"/img/thumbnail/Kwak.png" });
            context.Beers.AddOrUpdate(new Beer { Id = 4, Name = "Stella", Rating = 5, BreweryId = 4, ImageUrl = $"/img/full/Stella.png", ThumbnailImageUrl = $"/img/thumbnail/Stella.png" });
            context.Beers.AddOrUpdate(new Beer { Id = 5, Name = "Oude Caert", Rating = 5, BreweryId = 5, ImageUrl = $"/img/full/Oude Caert.png", ThumbnailImageUrl = $"/img/thumbnail/Oude Caert.png" });
            context.Beers.AddOrUpdate(new Beer { Id = 6, Name = "Tripel Karmeliet", Rating = 5, BreweryId = 6, ImageUrl = $"/img/full/Karmeliet.png", ThumbnailImageUrl = $"/img/thumbnail/Karmeliet.png" });
            context.Beers.AddOrUpdate(new Beer { Id = 7, Name = "Jupiler", Rating = 5, BreweryId = 4, ImageUrl = $"/img/full/Jupiler.png", ThumbnailImageUrl = $"/img/thumbnail/Jupiler.png" });
            context.Beers.AddOrUpdate(new Beer { Id = 8, Name = "Omer", Rating = 5, BreweryId = 7, ImageUrl = $"/img/full/Omer.png", ThumbnailImageUrl = $"/img/thumbnail/Omer.png" });
            context.Beers.AddOrUpdate(new Beer { Id = 9, Name = "Vedett", Rating = 5, BreweryId = 1, ImageUrl = $"/img/full/Vedett.png", ThumbnailImageUrl = $"/img/thumbnail/Vedett.png" });
            context.Beers.AddOrUpdate(new Beer { Id = 10, Name = "Blauw", Rating = 5, BreweryId = 7, ImageUrl = $"/img/full/Blauw.png", ThumbnailImageUrl = $"/img/thumbnail/Blauw.png" });

            context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Beers ON");
            context.SaveChanges();
            context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Beers OFF");
            connection.Close();
        }
    }
}
