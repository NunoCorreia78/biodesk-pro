using System.IO;

namespace BioDesk.App.Services;

public static class AppPaths
{
    public static string BaseDir => AppContext.BaseDirectory;
    public static string Images => Path.Combine(BaseDir, "images_interface");
    public static string Logo => Path.Combine(Images, "logo.png");
    public static string Util => Path.Combine(BaseDir, "utilitarios");
    public static string Assets => Path.Combine(Util, "assets");
    public static string Templates => Path.Combine(Util, "templates");
    public static string Consentimentos => Path.Combine(Util, "consentimentos");
    public static string Logs => Path.Combine(Util, "logs");
}