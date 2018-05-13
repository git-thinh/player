using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace player
{
    public class fPlayer: Form, IRequestHandler
    {
        readonly WebView browser;
        const string url = "http://media.com:55559/player/jw-5.10.html";

        public fPlayer() {
            this.Icon = Properties.Resources.play;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.Black;

            browser = new WebView(url, new BrowserSettings());
            browser.Dock = DockStyle.Fill;
            browser.RequestHandler = this;
            this.Controls.Add(browser);
        }

        public bool OnBeforeBrowse(IWebBrowser browser, IRequest request, NavigationType naigationvType, bool isRedirect)
        {
            return false;
        }

        public bool OnBeforeResourceLoad(IWebBrowser browser, IRequestResponse requestResponse)
        {
            return false;
        }

        public void OnResourceResponse(IWebBrowser browser, string url, int status, string statusText, string mimeType, WebHeaderCollection headers)
        { 
        }
    }
}
