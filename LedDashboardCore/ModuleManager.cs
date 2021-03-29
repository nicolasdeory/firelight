using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace FirelightCore
{
    public class SettingChangedEventArgs {

        public string Key { get; private set; }
        public SettingChangedEventArgs(string key)
        {
            this.Key = key;
        }
    }


    public static class ModuleManager
    {
        private static string AppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private static string SettingsDirectory = Path.Join(AppData, "/Firelight/");
        private static string SettingsPath = Path.Join(SettingsDirectory, "/settings.dat");

        public static Dictionary<string, ModuleAttributes> AttributeDict;

        public static event EventHandler<SettingChangedEventArgs> SettingChanged;
        
        public static void Init(List<ModuleAttributes> modules)
        {
            AttributeDict = new Dictionary<string, ModuleAttributes>();
            //List<ModuleAttributes> ledModules = Utils.GetTypesWithAttribute<LEDModuleAttributesAttribute>().Select(x => Activator.CreateInstance(x) as ModuleAttributes).ToList();
            List<ModuleAttributes> ledModules = modules;

            foreach (ModuleAttributes module in ledModules)
            {
                AttributeDict.Add(module.Id, module);
            }
        }

        public static void SaveSettings()
        {
            Dictionary<string, Dictionary<string, string>> attributes = new Dictionary<string, Dictionary<string, string>>();
            foreach(var key in AttributeDict.Keys)
            {
                attributes.Add(key, AttributeDict[key].SettingsDictionary);
            }
            BinaryFormatter bf = new BinaryFormatter();
            if (!Directory.Exists(SettingsDirectory))
            {
                Directory.CreateDirectory(SettingsDirectory);
            }
            using (FileStream f = File.Create(SettingsPath))
            {
                bf.Serialize(f, attributes);
            }
        }

        /// <summary>
        /// Returns false if there was any error loading settings
        /// </summary>
        /// <returns></returns>
        public static bool LoadSettings()
        {
            if (AttributeDict == null)
                throw new InvalidOperationException("You have to initialize ModuleManager first");

            BinaryFormatter bf = new BinaryFormatter();
            Dictionary<string, Dictionary<string, string>> attributes;
            if (File.Exists(SettingsPath))
            {
                using (FileStream f = File.OpenRead(SettingsPath))
                {
                    attributes = bf.Deserialize(f) as Dictionary<string, Dictionary<string, string>>;
                }
                foreach (var key in attributes.Keys)
                {
                    if (attributes.ContainsKey(key))
                    {
                        AttributeDict[key].SettingsDictionary = new ObservableDictionary<string, string>(attributes[key]);
                        AttributeDict[key].SettingsDictionary.Modified += () => OnSettingChanged(key);
                    }
                    else
                    {
                        AttributeDict[key].GenerateDefaultSettings();
                    }
                }
            } else
            {
                foreach(var module in AttributeDict.Values)
                {
                    module.GenerateDefaultSettings();
                }
                SaveSettings();
            }
            return true;
        }

        private static void OnSettingChanged(string key)
        {
            SettingChanged.Invoke(typeof(ModuleManager), new SettingChangedEventArgs(key));
        }
    }
}
