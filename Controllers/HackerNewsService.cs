using Newtonsoft.Json;

namespace ST.DeveloperCoding.HackerNews.API.Controllers
{
    public class HackerNewsService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://hacker-news.firebaseio.com/v0";
        public HackerNewsService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<List<StoryInfo>> GetBestStoriesAsync(int n)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/beststories.json");
            response.EnsureSuccessStatusCode();
            var storyIds = await response.Content.ReadFromJsonAsync<int[]>();
            var stories = await GetBestFullStoriesAsync(storyIds.Take(n).ToArray());
            return await Task.FromResult(stories);
        }

        internal async Task<List<StoryInfo>> GetBestFullStoriesAsync(int[] storyIds)
        {
            List<StoryInfo> stories = new List<StoryInfo>();

            foreach (var storyId in storyIds)
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}/item/{storyId}.json");
                var storyJsons = await response.Content.ReadAsStringAsync();
                var storyinfo = JsonConvert.DeserializeObject<StoryInfo>(storyJsons);
                //StoryInfo? storyinfo = JsonSerializer.Deserialize(storyJsons);
                response.EnsureSuccessStatusCode();

                if (storyinfo != null)
                {
                    stories.Add(storyinfo);
                }
            }
            return await Task.FromResult(stories);
        }
    }



    public class StoryInfo
    {

        public string By { get; set; }

        public int Descendants { get; set; }

        public int Id { get; set; }

        public int[] Kids { get; set; }

        public string Title { get; set; }

        public string? Url { get; set; }

        public string Type { get; set; }
        //public DateTime Time { get; set; }

        public int Score { get; set; }
    }

}