using System.Security.Principal;

namespace CodePluse.API.Models.Domain
{
    public class BlogPost
    {
        public Guid id { get; set; }

        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string content { get; set; }

        public string FeaturedImageUrl { get; set; }

        public string UrlHandle { get; set; }

        public DateTime PublishedDate { get; set; }

        public string Author { get; set; }

        public string IsVisible { get; set; }
    }
}
