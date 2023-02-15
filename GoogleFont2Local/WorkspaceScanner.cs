// LFInteractive LLC. - All Rights Reserved
namespace GoogleFont2Local;

public class WorkspaceScanner
{
    private string[] SearchCSS()
    {
        List<string> results = new();
        string[] files = Directory.GetFiles(Environment.CurrentDirectory, "*.css", SearchOption.AllDirectories);
        foreach (string file in files)
        {
            using FileStream fs = new(file, FileMode.Open, FileAccess.Read, FileShare.Read);
            using StreamReader reader = new(fs);
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Trim().StartsWith("@import"))
                {
                }
            }
        }
        return results.ToArray();
    }
}