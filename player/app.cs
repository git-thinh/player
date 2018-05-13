using CefSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace player
{
    public class app
    {
        static app()
        {
            AppDomain.CurrentDomain.AssemblyResolve += (se, ev) =>
            {
                Assembly asm = null;
                string comName = ev.Name.Split(',')[0];
                string resourceName = @"LIB\" + comName + ".dll";
                var assembly = Assembly.GetExecutingAssembly();
                resourceName = typeof(app).Namespace + "." + resourceName.Replace(" ", "_").Replace("\\", ".").Replace("/", ".");
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream == null)
                    {
                    }
                    else
                    {
                        byte[] buffer = new byte[stream.Length];
                        using (MemoryStream ms = new MemoryStream())
                        {
                            int read;
                            while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                                ms.Write(buffer, 0, read);
                            buffer = ms.ToArray();
                        }
                        try
                        {
                            asm = Assembly.Load(buffer);
                        }
                        catch (Exception ex) {
                            string s =ex.Message;
                        }
                    }
                }
                return asm;
            };
        }

        static fPlayer player;
        public static void Run()
        {
            Settings settings = new Settings();
            if (CEF.Initialize(settings))
            {
                //CEF.RegisterScheme("test", new SchemeHandlerFactory());
                //CEF.RegisterJsObject("bound", new BoundObject());
                player = new fPlayer();

                Application.Run(player);
            }

        }
    }

    static class program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            app.Run();
        }
    }
}
