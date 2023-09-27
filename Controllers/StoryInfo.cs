namespace ST.DeveloperCoding.HackerNews.API.Controllers
{
    public class StoryInfo
    {

        public string? By { get; set; }

        public int? Descendants { get; set; }

        public int? Id { get; set; }

        public int[]? Kids { get; set; }

        public string? Title { get; set; }

        public string? Url { get; set; }

        public string? Type { get; set; }

        public int? Score { get; set; }
    }

}