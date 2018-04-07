﻿using System.Collections.Generic;
using System.Diagnostics;

namespace HTMLParser {
    public class HTMLDocument {
        public CList<DOMElement> DOMTree = new CList<DOMElement>();
        public CList<DOMElement> MetaTags = new CList<DOMElement>();

        public Statistics Stats = new Statistics();

        public bool IsDownloaded = false;

        /// <param name="url">Source code or url</param>
        public HTMLDocument (string url, bool isURL = true, bool textInsideOneLine = false) {
            string source = isURL ? GetSourceCode(url) : url;

            IsDownloaded = isURL;

            this.DOMTree = HTML.Parse(source, ref Stats, textInsideOneLine);
            this.MetaTags = GetElementsByName("meta");           
        }

        private string GetSourceCode(string url) {
            Stopwatch watch = Stopwatch.StartNew();

            string html = Utils.GetWebSiteContent(url);

            // Get time
            watch.Stop();
            Stats.DownloadingTime = (int)watch.ElapsedMilliseconds;

            return html;
        }

        public CList<DOMElement> GetElementsByName (string name) {
            CList<DOMElement> list = new CList<DOMElement>();

            for (int i = 0; i < this.DOMTree.Count; i++) {
                DOMElement element = this.DOMTree[i];

                if (element.TagName == name) list.Add(element);
            }

            return list;
        }
    }
}
