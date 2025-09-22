using Discord;

namespace TheEchoFlowerReborn
{
    public class Logger
    {
        public static Task Log(LogMessage msg)
        {
            Console.WriteLine($"[{msg.Severity}/{msg.Source}] {msg.Message}");
            return Task.CompletedTask;
        }
        public static Task Log(string msg)
        {
            Console.WriteLine($"[Info/Main] {msg}");
            return Task.CompletedTask;
        }
        public static Task Log(string msg, string source)
        {
            Console.WriteLine($"[Info/{source}] {msg}");
            return Task.CompletedTask;
        }
        public static Task Log(Exception ex)
        {
            Console.WriteLine($"[Error/{ex.Source}] {ex.Message}");
            return Task.CompletedTask;
        }
    }
}