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
        private static string folder = Json_Data.PersonalData("url").Split(new char[] { '/' })[Json_Data.PersonalData("url").Split(new char[] { '/' }).Length - 1];

        public static string Folder { get => folder; set => folder = value; }
    }

    public static class Json_Data
    {
        public static string PersonalData(string data)
        {
            string config_path = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar.ToString() + "config.json";
            JObject jsonObject = JObject.Parse(File.ReadAllText(config_path));
            return jsonObject[data].ToString();
        }
    }

}
