using Microsoft.AspNetCore.Mvc;

namespace ST.DeveloperCoding.HackerNews.API.Controllers
{
    public class HackerNewsController : ControllerBase
    {
        private readonly HackerNewsService _hackerNewsService;

        private readonly ILogger<HackerNewsController> _logger;
        public HackerNewsController(HackerNewsService hackerNewsService, ILogger<HackerNewsController> logger)
        {
            _logger = logger;
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

                var logMessage = $"Invalid value for 'n' : {n}";
                _logger.LogWarning(logMessage);
                return BadRequest(logMessage);
            }
            catch (Exception ex)
            {
                var logMessage = $"An error occurred: {ex.Message}";
                _logger.LogWarning(logMessage);
                return StatusCode(500, logMessage);
            }
        }

        public async Task<ActionResult> GetStory(int storyId)
        {
            try
            {
                return Ok(await _hackerNewsService.GetBestStoryAsync(storyId));
            }
            catch (Exception ex)
            {
                var logMessage = $"An error occurred: {ex.Message}";
                _logger.LogWarning(logMessage);
                return StatusCode(500, logMessage);
            }
        }
    }
}
