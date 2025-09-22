using Discord;
using TheEchoFlowerReborn;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("SURVEY PROGRAM v0.0.1");
try
{
    if (!File.Exists("Token.txt"))
    {
        File.WriteAllText("Token.txt", "Place your fucking token here!");
        throw new Exception("Place your bot token in Token.txt dumb ass");
    }
    await Flower.Main(File.ReadAllText("Token.txt"));
}
catch (Exception ex)
{
    Console.WriteLine($"[CRITICALERROR/{ex.Source}] {ex.Message}");
}
