using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateProjectApp
{
    internal class php
    {
        public string bin;
        public string type = "PHP";
        public php()
        {
            bin = getPhpLocation();
            checkForComposer();
        }


        void checkForComposer()
        {

            if (RunProcess.GetPath("composer") == null)
            {
                throw new Exception("Sorry, but it looks like you do not have composer installed. Please install and try again");
            }
       
        }
        string getPhpLocation()
        {
            return RunProcess.Now("php", "-r \"echo PHP_BINARY;\"");
        }

    }
}
    
