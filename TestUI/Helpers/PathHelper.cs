using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUI.Helpers
{
    class PathHelper
    {
        public static string GetStartupPath()
        {
            String exePath = System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName;
            return System.IO.Path.GetDirectoryName(exePath);
        }
    }
}
