using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kerry.Sync.Utility.Text
{
    public class TextHelper
    {
        public static bool TryGetValue<T>(string path, out T outvalue)
        {
            if (File.Exists(path))
            {
                outvalue = JsonHelper.TextToJson<T>(path);
                return true;
            }
            else
                outvalue = default(T);
            return false;
        }


        public static bool TryGetValue<T>(out T outvalue)
        {
            var path = System.Environment.GetEnvironmentVariable("AppData") +"\\Kerry.Sync\\";
            if (File.Exists(path))
            {
                outvalue = JsonHelper.TextToJson<T>(path);
                return true;
            }
            else
                outvalue = default(T);
            return false;
        }


        public static string Escape(string str)
        {


            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                sb.Append((
                c == '\'' ) ? "\'\'" : c.ToString());
            }
            return sb.ToString();
        }

        public static string UnEscape(string str)
        {
            StringBuilder sb = new StringBuilder();
            int len = str.Length;
            int i = 0;
            while (i != len)
            {
                if (Uri.IsHexEncoding(str, i))
                    sb.Append(Uri.HexUnescape(str, ref i));
                else
                    sb.Append(str[i++]);
            }
            return sb.ToString();
        }
    }
}
