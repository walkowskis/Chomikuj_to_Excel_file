using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace ChomikujToExcel.Utils
{
    public static class CommonHelpers
    {
        public static bool ExcelVisible = true;
        public static string config_path = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar.ToString() + "config.json";
        private static string folder = Json_Data.WriteData("url").Split(new char[] { '/' })[Json_Data.WriteData("url").Split(new char[] { '/' }).Length - 1];
        
        public static string Folder { get => folder; set => folder = value; }
    }

    public static class Json_Data
    {
        public static string WriteData(string data)
        {
            //Console.WriteLine("Taki path: --{0}--", CommonHelpers.config_path);
            JObject jsonObject = JObject.Parse(File.ReadAllText(CommonHelpers.config_path));
            //Console.WriteLine(jsonObject.ToString());
            return jsonObject[data].ToString();
        }

        public static void ModifyData(string key, string value)
        {
            JObject jsonObject = JObject.Parse(File.ReadAllText(CommonHelpers.config_path));
            jsonObject[key] = value;
            File.WriteAllText(CommonHelpers.config_path, jsonObject.ToString());
        }
    }

}
