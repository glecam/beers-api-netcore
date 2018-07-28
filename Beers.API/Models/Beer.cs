namespace Beers.API.Models
{
    public class Beer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public string ImageUrl { get; set; }
        public string ThumbnailImageUrl { get; set; }

        public int BreweryId { get; set; }
        public virtual Brewery Brewery{ get; set; }
    }
}
