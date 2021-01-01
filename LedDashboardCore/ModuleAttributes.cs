using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace FirelightCore
{
    public abstract class ModuleAttributes
    {
        public string Id { get; protected set; } = "unknown";

        public bool Enabled => SettingsDictionary["enabled"] == "true";

        public ObservableDictionary<string, string> SettingsDictionary
        {
            get
            {
                return _settingsDictionary;
            }
            set
            {
                _settingsDictionary = value;
                _settingsDictionary.Modified += () =>
                {
                    ValidateSettingsDictionary();
                    ModuleManager.SaveSettings();
                };
            }
        }

        private ObservableDictionary<string, string> _settingsDictionary;

        protected Dictionary<string, string> DefaultValues;

        public ModuleAttributes()
        {
            SettingsDictionary = new ObservableDictionary<string, string>();
        }

        public abstract void GenerateDefaultSettings();
        public abstract bool ValidateSettingsDictionary();

        protected void ValidateSetting(string key, Action parsingFunction)
        {
            if (!SettingsDictionary.ContainsKey(key))
            {
                Debug.WriteLine($"Key {key} doesn't exist in [{this.Id}] settings, assigning default value");
                SettingsDictionary.Add(key, DefaultValues[key]);
            }
            else
            {
                try
                {
                    parsingFunction.Invoke();
                }
                catch
                {
                    Debug.WriteLine($"Exception parsing value for key {key} in [{this.Id}] settings, assigning default value");
                    SettingsDictionary[key] = DefaultValues[key];
                }
            }
        }

        protected MouseKeyBinding GetBinding(string key)
        {
            if (!SettingsDictionary.ContainsKey(key))
                throw new KeyNotFoundException();

            string value = SettingsDictionary[key];
            if (value[0] == 'k')
            {
                int keycode = Int32.Parse(value[1..]);
                return new MouseKeyBinding((Keys)keycode);
            }
            else if (value[0] == 'm')
            {
                return (Int32.Parse(value[1..])) switch
                {
                    1 => new MouseKeyBinding(MouseButtons.Left),
                    2 => new MouseKeyBinding(MouseButtons.Right),
                    3 => new MouseKeyBinding(MouseButtons.Middle),
                    4 => new MouseKeyBinding(MouseButtons.XButton1),
                    5 => new MouseKeyBinding(MouseButtons.XButton2),
                    _ => throw new FormatException("Invalid mouse button"),
                };
            }
            else
            {
                throw new FormatException("Bad binding value");
            }
        }

    }
}
