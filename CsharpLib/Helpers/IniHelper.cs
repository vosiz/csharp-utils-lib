using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Vosiz.Helpers
{
    public static class IniHelper
    {
        public static IniHelperInstance FromFile(string path)
        {
            return new IniHelperInstance(path);
        }
    }

    public class IniHelperInstance
    {
        private readonly string Filepath;
        private readonly FileIniDataParser Parser;
        private IniData Data;

        public IniHelperInstance(string path)
        {
            Filepath = path;
            Parser = new FileIniDataParser();

            if (File.Exists(Filepath))
            {
                Data = Parser.ReadFile(Filepath);
            }
            else
            {
                Data = new IniData();
            }
        }

        public string ReadValue(string section, string key, string defaultValue = "")
        {
            return Data[section][key] ?? defaultValue;
        }

        public void WriteValue(string section, string key, string value)
        {
            Data[section][key] = value;
            Save();
        }

        public bool KeyExists(string section, string key)
        {
            return Data.Sections.ContainsSection(section) && Data[section].ContainsKey(key);
        }

        public void DeleteKey(string section, string key)
        {
            if (KeyExists(section, key))
            {
                Data[section].RemoveKey(key);
                Save();
            }
        }

        public void DeleteSection(string section)
        {
            if (Data.Sections.ContainsSection(section))
            {
                Data.Sections.RemoveSection(section);
                Save();
            }
        }

        public void Save()
        {
            Parser.WriteFile(Filepath, Data);
        }

        public void Reload()
        {
            if (File.Exists(Filepath))
                Data = Parser.ReadFile(Filepath);
        }
    }
}
