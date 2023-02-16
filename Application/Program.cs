// LFInteractive LLC. - All Rights Reserved

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;

namespace ChaseWebComplier.Application;

internal class Program
{
    private static void Build()
    {
        BuildConfigurationOptions options = new();
        string settingsFile = Path.Combine(Environment.CurrentDirectory, "cwc.json");
        if (!File.Exists(settingsFile))
        {
            using FileStream fs = new(settingsFile, FileMode.Create, FileAccess.Write);
            using StreamWriter writer = new(fs);
            writer.Write(JsonConvert.SerializeObject(options));
        }
        else
        {
            try
            {
                using FileStream fs = new(settingsFile, FileMode.Open, FileAccess.Read, FileShare.Read);
                using StreamReader reader = new(fs);
                options = JObject.Parse(reader.ReadToEnd()).ToObject<BuildConfigurationOptions>();
            }
            catch
            {
                File.Delete(settingsFile);
                Build();
                return;
            }
        }
        List<Task> tasks = new();

        Log.Information("Starting Async Tasks...");

        if (options.HTML.Enabled)
        {
            // HTML
            Log.Debug("Starting HTML");
            tasks.Add(Task.Run(() =>
            {
            }).ContinueWith(i => Log.Debug("Done processing html!")));
        }
        if (options.CSS.Enabled)
        {
            Log.Debug("Starting CSS");
            // CSS
            tasks.Add(Task.Run(() =>
            {
            }).ContinueWith(i => Log.Debug("Done processing css!")));
        }
        if (options.Javascript.Enabled)
        {
            Log.Debug("Starting Javascript");
            // JS
            tasks.Add(Task.Run(() =>
            {
            }).ContinueWith(i => Log.Debug("Done processing javascript!")));
        }
        if (options.Multimedia.Images.Enabled)
        {
            Log.Debug("Starting Images");
            // Images
            tasks.Add(Task.Run(() =>
            {
            }).ContinueWith(i => Log.Debug("Done processing images!")));
        }
        if (options.Multimedia.Videos.Enabled)
        {
            Log.Debug("Starting Videos");
            // Videos
            tasks.Add(Task.Run(() =>
            {
            }).ContinueWith(i => Log.Debug("Done processing videos!")));
        }
        if (options.Fonts.Enabled)
        {
            Log.Debug("Starting Fonts");
            tasks.Add(Task.Run(() =>
            {
                // Download Font CSS

                if (options.Fonts.DownloadFontFaceFiles)
                {
                    // Download FontFace Files
                }
            }).ContinueWith(i => Log.Debug("Done processing fonts!")));
        }
        Task.WaitAll(tasks.ToArray());
        Log.Information("Done processing all tasks!");
    }

    private static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .CreateLogger();

        if (args.Any())
        {
            Environment.CurrentDirectory = Path.GetFullPath(args[0]);
        }
        string test = "<link rel=\"stylesheet\" href=\"https://google.com\">";
        string url = test.Split("href=\"").Last().Split("\"").First();
        Console.WriteLine(url);

        //Build();

        Log.CloseAndFlush();
    }
}