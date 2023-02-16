// LFInteractive LLC. - All Rights Reserved
using Serilog;

namespace ChaseWebComplier.GoogleFont2Local;

public class WorkspaceScanner
{
    private Uri[] SearchCSS()
    {
        List<Uri> results = new();
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
                    foreach (string dist in FontManager.FONT_DIST_APIS)
                    {
                        if (line.Contains(dist))
                        {
                            string url = line.Split("url(").Last().Split("").First();
                        }
                    }
                    try
                    {
                        string url = line.Replace("@import", "").Replace("url(", "").Replace("\"", "").Split(");").First().Trim();
                        Uri uri = new(url);
                        if (FontManager.FONT_DIST_APIS.Contains(uri.Host))
                            results.Add(uri);
                    }
                    catch (UriFormatException) { }
                    catch (Exception e)
                    {
                        Log.Error("Unknown error while attempting to get a css import url: {error}", e.Message, e);
                    }
                }
            }
        }
        return results.ToArray();
    }

    private Uri[] SearchHTML()
    {
        List<Uri> results = new();
        string[] files = Directory.GetFiles(Environment.CurrentDirectory, "*.html", SearchOption.AllDirectories);
        foreach (string file in files)
        {
            using FileStream fs = new(file, FileMode.Open, FileAccess.Read, FileShare.Read);
            using StreamReader reader = new(fs);
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Trim().StartsWith("<link") && line.Contains("href="))
                {
                    foreach (string dist in FontManager.FONT_DIST_APIS)
                    {
                        if (line.Contains(dist))
                        {
                            try
                            {
                                string url = line.Split("href=\"").Last().Split("\"").First();
                                results.Add(new(url));
                            }
                            catch (UriFormatException) { }
                            catch (Exception e)
                            {
                                Log.Error("Unable to extract link from html: {error}", e.Message, e);
                            }
                            break;
                        }
                    }
                }
            }
        }
        return results.ToArray();
    }
}