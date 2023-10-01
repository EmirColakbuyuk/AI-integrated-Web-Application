using OpenAI_API;
using OpenAI_API.Completions;

namespace CizgiWebServer.Functions
{
    public class AskGpt
    {
        public static string AIResponse(string input)
        {
            string prompt = input;

            var openAI = new OpenAIAPI("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");

            CompletionRequest completionRequest = new CompletionRequest();
            completionRequest.Prompt = prompt;
            completionRequest.Model = OpenAI_API.Models.Model.DavinciText;
            completionRequest.MaxTokens = 4000;

            var result =  openAI.Completions.CreateCompletionAsync(completionRequest).Result;
            if (result is not null && result.Completions.Count > 0)
            {
                return result.Completions[0].Text;
            }

            return string.Empty;
        }
    }
}