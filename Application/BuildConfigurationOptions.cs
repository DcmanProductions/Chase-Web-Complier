// LFInteractive LLC. - All Rights Reserved
namespace ChaseWebComplier.Application;

internal struct BuildConfigurationOptions
{
    public BuildConfigurationOptions()
    { }

    public CSSConfigurationOptions CSS { get; set; } = new();
    public FontsConfigurationOptions Fonts { get; set; } = new();
    public HTMLConfigurationOptions HTML { get; set; } = new();
    public JavaScriptConfigurationOptions Javascript { get; set; } = new();
    public MultimediaConfigurationOptions Multimedia { get; set; } = new();
    public string WorkingDirectory { get; set; } = "./";
}

internal struct CSSConfigurationOptions
{
    public CSSConfigurationOptions()
    { }

    public bool Enabled { get; set; } = true;
}

internal struct FontsConfigurationOptions
{
    public FontsConfigurationOptions()
    { }

    public bool DownloadFontFaceFiles { get; set; } = true;
    public bool Enabled { get; set; } = true;
}

internal struct HTMLConfigurationOptions
{
    public HTMLConfigurationOptions()
    { }

    public bool Enabled { get; set; } = true;
}

internal struct ImageConfigurationOptions
{
    public ImageConfigurationOptions()
    { }

    public bool ConvertToWebp { get; set; } = true;
    public bool Enabled { get; set; } = true;
    public float[] ScaleVarients { get; set; } = { .25f, .5f, .75f, 1f };
    public bool SimplifyName { get; set; } = true;
}

internal struct JavaScriptConfigurationOptions
{
    public JavaScriptConfigurationOptions()
    { }

    public bool Enabled { get; set; } = true;
    public bool SimplifyConditionals { get; set; } = true;
    public bool SimplifyFunctionNames { get; set; } = true;
    public bool SimplifyVariableNames { get; set; } = true;
}

internal struct MultimediaConfigurationOptions
{
    public MultimediaConfigurationOptions()
    { }

    public ImageConfigurationOptions Images { get; set; } = new();
    public VideoConfigurationOptions Videos { get; set; } = new();
}

internal struct VideoConfigurationOptions
{
    public VideoConfigurationOptions()
    { }

    public bool ConvertToWebm { get; set; } = true;
    public bool Enabled { get; set; } = true;
    public float[] ScaleVarients { get; set; } = { .25f, .5f, .75f, 1f };
    public bool SimplifyName { get; set; } = true;
}