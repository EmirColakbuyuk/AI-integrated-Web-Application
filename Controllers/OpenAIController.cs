using Microsoft.AspNetCore.Mvc;
using OpenAI_API;
using OpenAI_API.Completions;

namespace CizgiWebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpenAIController : ControllerBase
    {
        [HttpGet]
        [Route("UseChatGPT")]
        public async Task<IActionResult> UseChatGPT(string prompt)
        {
            string apiKey = "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";
            string answer = string.Empty;

            var openAI = new OpenAIAPI(apiKey);
            
            CompletionRequest completionRequest = new CompletionRequest();
            prompt = "generate me 19 char random key where each 4 chars are divided by '-' for example aaaa-bbbb-ccc-dddd all upper case";
            completionRequest.Prompt = prompt;
            completionRequest.Model = OpenAI_API.Models.Model.DavinciText;
            completionRequest.MaxTokens = 4000;

            var result = await openAI.Completions.CreateCompletionAsync(completionRequest);
            if (result is not null)
            {
                foreach (var item in result.Completions)
                {
                    answer = item.Text;
                }

                return Ok(answer);
            }
            else
            {
                return BadRequest("Not found");
            }
        }
        
        
        
        [HttpGet]
        [Route("CreateUrl")]
        public async Task<IActionResult> CreateUrl(string prompt)
        {
            string apiKey = "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";
            string answer = string.Empty;

            var openAI = new OpenAIAPI(apiKey);
            
            CompletionRequest completionRequest = new CompletionRequest();
            prompt = "generate me a random url where it starts with http://localhost:5279/tweets/   and the else randomly generated appropiate to url rules with the added length max 15";
            completionRequest.Prompt = prompt;
            completionRequest.Model = OpenAI_API.Models.Model.DavinciText;
            completionRequest.MaxTokens = 4000;

            var result = await openAI.Completions.CreateCompletionAsync(completionRequest);
            if (result is not null)
            {
                foreach (var item in result.Completions)
                {
                    answer = item.Text;
                }

                return Ok(answer);
            }
            else
            {
                return BadRequest("Not found");
            }
        }
    }
}