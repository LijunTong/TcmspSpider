using Newtonsoft.Json.Linq;

namespace Tcmsp
{
    internal static class Program
    {
        public static string ConfigFilePath = Path.Combine(Application.StartupPath, "Config.json");
        public static JObject ConfigJson = JObject.Parse(File.ReadAllText(ConfigFilePath));
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MainFrm());
        }
    }
}