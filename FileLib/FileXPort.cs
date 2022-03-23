using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace FileLib
{
    public static class FileXPort
    {
        public enum Extension
        {
            hjprojcet = 0,
            hjboard
        }

        public static bool ExportFile(object obj, string filePath, string fileName, int extension)
        {
            string str = JsonConvert.SerializeObject(obj);
            fileName += ((Extension)Enum.ToObject(typeof(Extension), extension)).ToString();
            return ExportFile(str, filePath, fileName, extension);
        }

        public static bool ExportFile(object obj, string filePath, string fileName)
        {
            Newtonsoft.Json.Serialization.ITraceWriter traceWriter = new Newtonsoft.Json.Serialization.MemoryTraceWriter();
            string str = JsonConvert.SerializeObject(obj, new JsonSerializerSettings { TraceWriter = traceWriter });
            Console.WriteLine(traceWriter);
            return ExportFile(str, filePath, fileName);
        }

        public static bool ExportFile(string str, string filePath, string fileName, int extension)
        {
            fileName += ((Extension)Enum.ToObject(typeof(Extension), extension)).ToString();
            return ExportFile(str, filePath, fileName);
        }

        public static bool ExportFile(string str, string filePath, string fileName)
        {
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            str = Encode(str);
            StreamWriter sw =
                new StreamWriter(string.Format(@"{0}\{1}", filePath, fileName), false);
            sw.AutoFlush = true;
            var result = SplitByCount(str, 500);
            result.ForEach(item => sw.Write(item));
            //sw.WriteLine(str);

            sw.Close();
            return true;
        }

        public static string ImportFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                StreamReader sr = new StreamReader(filePath, Encoding.UTF8);
                string str = Decode(sr.ReadToEnd());
                sr.Close();

                return str;
            }
            return null;
        }

        private static string Encode(string source)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(source);
            var str = Convert.ToBase64String(bytes);
            return str;
        }
        private static string Decode(string result)
        {
            byte[] bytes = Convert.FromBase64String(result);
            var str = Encoding.UTF8.GetString(bytes);
            return str;
        }

        private static List<string> SplitByCount(string raw, int count)
        {
            List<string> result = new List<string>();
            string copy = raw;
            do
            {
                string sub = string.Empty;
                sub = copy.Length >= count ? copy.Substring(0, count) : copy.Substring(0);
                result.Add(sub);
                copy = copy.Length >= count ? copy.Substring(count) : string.Empty;
            } while (copy.Length > 0);
            return result;
        }
    }
}
