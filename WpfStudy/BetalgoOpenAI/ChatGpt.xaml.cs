using OpenAI.GPT3.Managers;
using OpenAI.GPT3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using OpenAI.GPT3.ObjectModels.RequestModels;
using OpenAI.GPT3.ObjectModels;

namespace WpfStudy.BetalgoOpenAI
{
    /// <summary>
    /// Interaction logic for ChatGpt.xaml
    /// </summary>
    public partial class ChatGpt : Window
    {
        string apiKey = "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";
        public ChatGpt()
        {
            InitializeComponent();
        }

        private async void btnAskGpt_Click(object sender, RoutedEventArgs e)
        {
            var openAiService = new OpenAIService(new OpenAiOptions()
            {
                ApiKey = Environment.GetEnvironmentVariable(apiKey)
            });

            var chat = new ChatCompletionCreateRequest()
            {
                Messages = new List<ChatMessage>
                {
                    ChatMessage.FromSystem("You are a helpful assistant."),
                    ChatMessage.FromUser("Who won the world series in 2020?"),
                    ChatMessage.FromAssistant("The Los Angeles Dodgers won the World Series in 2020."),
                    ChatMessage.FromUser("Where was it played?")
                },
                Model = Models.ChatGpt3_5Turbo,
                MaxTokens = 50//optional
            };
            var completionResult = await openAiService.ChatCompletion.CreateCompletion(chat);

            if (completionResult.Successful)
            {
                //Console.WriteLine(completionResult.Choices.First().Message.Content);

                this.txtAnswer.Text = completionResult.Choices.First().Message.Content;
            }
            else
            {
                if (completionResult.Error == null)
                {
                    throw new Exception("Unknown Error");
                }
                this.txtAnswer.Text=$"{completionResult.Error.Code}: {completionResult.Error.Message}";
            }
        }
    }
}
