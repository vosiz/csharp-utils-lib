using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vosiz.Helpers
{
    public static class PathHelper
    {

        public static string UserProfile(string adds = "")
        {
            string user_path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            return Path.Combine(user_path, adds);
        }

        public static string Appdata(string adds = "")
        {
            string user_path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            return Path.Combine(user_path, adds);
        }

        public static string Combine(params string[] paths)
        {
            return Path.Combine(paths);
        }
        
    }
}
