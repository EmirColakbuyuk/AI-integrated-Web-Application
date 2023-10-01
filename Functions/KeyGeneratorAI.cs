using OpenAI_API;
using OpenAI_API.Completions;


namespace CizgiWebServer.Functions
{
    public class KeyGenerator
    {
        public async Task<string> KeyGeneratorAI()
        {
            string prompt = "generate me 19 char random key where each 4 chars are divided by '-' for example aaaa-bbbb-ccc-dddd all upper case";

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