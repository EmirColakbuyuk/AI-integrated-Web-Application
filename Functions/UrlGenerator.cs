using OpenAI_API;
using OpenAI_API.Completions;


namespace CizgiWebServer.Functions
{
    public class UrlGenerator
    {
        public async Task<string> UrlGeneratorAI()
        {
            string prompt = "generate me a random url where it starts with http://localhost:5279/tweets/   and the else randomly generated appropiate to url rules with the added length max 15,only return the added part";

            var openAI = new OpenAIAPI("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 ");

            CompletionRequest completionRequest = new CompletionRequest();
            completionRequest.Prompt = prompt;
            completionRequest.Model = OpenAI_API.Models.Model.DavinciText;
            completionRequest.MaxTokens = 4000;

            var result = await openAI.Completions.CreateCompletionAsync(completionRequest);
            if (result is not null && result.Completions.Count > 0)
            {
                return result.Completions[0].Text;
            }

            return string.Empty;
        }
    }
}