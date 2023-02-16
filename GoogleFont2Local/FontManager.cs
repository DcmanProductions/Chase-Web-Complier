// LFInteractive LLC. - All Rights Reserved
using Serilog;

namespace ChaseWebComplier.GoogleFont2Local;

public static class FontManager
{
    /// <summary>
    /// A list of supported font distribution platforms
    /// </summary>
    public static readonly string[] FONT_DIST_APIS = { "fonts.googleapis.com", "fonts.bunny.net" };

    /// <summary>
    /// Downloads all fontface files to a temp directory
    /// </summary>
    /// <param name="dist_url"></param>
    public static void DownloadFontFaceFiles(Uri dist_url)
    {
        Uri[] urls = GetSourceUrlsFromCSS(GetFontCSS(dist_url));
        using HttpClient client = new();
        Parallel.ForEach(urls, url =>
        {
            using HttpResponseMessage message = client.GetAsync(url).Result;
        });
    }

    /// <summary>
    /// Gets the raw css code from a url
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    /// <exception cref="System.Net.WebException"></exception>
    public static string GetFontCSS(Uri url)
    {
        if (!FONT_DIST_APIS.Contains(url.Host))
        {
            Log.Error("Unsupported Font Distribution Platform: {host}", url.Host);
            return "";
        }
        using HttpClient client = new();
        using HttpResponseMessage message = client.GetAsync(url).Result;
        if (message.IsSuccessStatusCode)
        {
            return message.Content.ReadAsStringAsync().Result;
        }
        throw new System.Net.WebException($"Unable to retrieve fontface css from url: {url}");
    }

    /// <summary>
    /// Gets a list of all source urls in css code
    /// </summary>
    /// <param name="css"></param>
    /// <returns></returns>
    public static Uri[] GetSourceUrlsFromCSS(string css)
    {
        List<Uri> sources = new();

        foreach (string line in css.Split("\n"))
        {
            if (line.Trim().StartsWith("src:"))
            {
                string url = line.Replace("src:", "").Replace("url(", "").Split(")").First().Trim();
                try
                {
                    Uri uri = new(url);
                    sources.Add(uri);
                }
                catch (UriFormatException)
                {
                    Log.Error("Unable to parse url found: {url}", url);
                    Log.Error("Full css line is:\n\"{line}\"", line);
                }
            }
        }

        return sources.ToArray();
    }
}