using Microsoft.AspNetCore.Mvc;

namespace ST.DeveloperCoding.HackerNews.API.Controllers
{
    public class HackerNewsController : ControllerBase
    {
        private readonly HackerNewsService _hackerNewsService;

        public HackerNewsController(HackerNewsService hackerNewsService)
        {
            _hackerNewsService = hackerNewsService ?? throw new ArgumentNullException(nameof(hackerNewsService));
        }

        [HttpGet("best-stories")]
        public async Task<ActionResult> GetBestStories(int n)
        {
            List<JsonContent> stories = new List<JsonContent>();
            try
            {
                if (n > 0)
                {
                    return Ok(await _hackerNewsService.GetBestStoriesAsync(n));
                }

                return BadRequest("Invalid value for 'n'");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
