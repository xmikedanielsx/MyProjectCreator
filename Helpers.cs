using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CreateProjectApp
{
    // string assetURL = @"https://gist.githubusercontent.com/xmikedanielsx/03983482114e41e5a4358645e31db7be/raw/66608bd60a2fe6ea1ef30c226f0e5e2c076b3ec4/gitignore%2520VisualStudio";
    public static class Helpers
    {
        public static string[] GetFileContent(string assetURL)
        {
            var textFromFile = (new WebClient()).DownloadString(assetURL);
            string[] fileLines = textFromFile.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            return fileLines;
        }
    }
}
