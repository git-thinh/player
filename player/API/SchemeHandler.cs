using CefSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace player
{
    class SchemeHandlerFactory : ISchemeHandlerFactory
    {
        public ISchemeHandler Create()
        {
            return new SchemeHandler();
        }
    }

    class SchemeHandler : ISchemeHandler
    {
        private static IDictionary<string, byte[]> caches;
        private readonly IDictionary<string, string> resources;

        public SchemeHandler()
        {
            resources = new Dictionary<string, string>
            {
                //{ "BindingTest.html", Resources.BindingTest },
                //{ "PopupTest.html", Resources.PopupTest },
                //{ "SchemeTest.html", Resources.SchemeTest },
                //{ "TooltipTest.html", Resources.TooltipTest },
            };


        }

        public bool ProcessRequest(IRequest request, ref string mimeType, ref Stream stream)
        {
            var uri = new Uri(request.Url);
            //var segments = uri.Segments;
            //var file = segments[segments.Length - 1]; 
            //string resource;
            //if (resources.TryGetValue(file, out resource) && !String.IsNullOrEmpty(resource))
            //{
            //    var bytes = Encoding.UTF8.GetBytes(resource);
            //    stream = new MemoryStream(bytes);
            //    mimeType = "text/html";

            //    return true;
            //}

            if (caches.ContainsKey(uri.Host))
            {
            }
            else {
                byte[] bytes = null;
                switch (uri.Scheme)
                {
                    case "player.html":
                        mimeType = "text/html";
                        bytes = getDataFromResource(uri.Host);
                        break;
                }
                if (bytes == null)
                {
                    stream = new MemoryStream(caches[uri.Scheme]);
                }
            }








            return false;
        }

        private static byte[] getDataFromResource(string name)
        {
            byte[] buffer = new byte[] { };

            string comName = name.Split(',')[0];
            string resourceName = @"LIB\" + comName + ".dll";
            var assembly = Assembly.GetExecutingAssembly();
            resourceName = typeof(app).Namespace + "." + resourceName.Replace(" ", "_").Replace("\\", ".").Replace("/", ".");
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream != null)
                {
                    buffer = new byte[stream.Length];
                    using (MemoryStream ms = new MemoryStream())
                    {
                        int read;
                        while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                            ms.Write(buffer, 0, read);
                        buffer = ms.ToArray();
                    }
                }
            }
            return buffer;
        }
    }
}
