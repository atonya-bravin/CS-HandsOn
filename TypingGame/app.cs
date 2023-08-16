using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TypingGame
{
    class app
    {
        static async Task Main(string[] args)
        {
            int score = 0;
            int wrong_spelling = 0;
            int numberOfTries = 0;
            while (true){
                using (HttpClient client = new HttpClient())
                {
                    string randomWordApiUrl = "https://random-word-api.herokuapp.com/word";
                    HttpResponseMessage response = await client.GetAsync(randomWordApiUrl);

                    if (response.IsSuccessStatusCode){
                        string responseBody = await response.Content.ReadAsStringAsync();
                        string randomWord = responseBody.Replace("[\"", "").Replace("\"]", "");
                        Console.WriteLine("Type exit() to Exit");
                        Console.Write("Random Word (");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"{randomWord}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("): ");
                        string TypedWord = Console.ReadLine();
                        if (TypedWord == randomWord){
                            score++;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Right!!\n");
                            Console.ForegroundColor = ConsoleColor.White;
                            continue;
                        }
                        else if(TypedWord == "exit()"){
                            Console.WriteLine("Good Bye!!\n\n");
                            break;
                        }
                        else{
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Wrong!!\n");
                            Console.ForegroundColor = ConsoleColor.White;
                            wrong_spelling++;
                            continue;
                        }
                    }
                    else{
                        Console.WriteLine("Failed to fetch a random word.");
                        Environment.Exit(0);
                    }
                }
            }
            numberOfTries = score + wrong_spelling;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"You have gotten {score} / {numberOfTries} right!");
            Console.WriteLine("Thank you for participating\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
