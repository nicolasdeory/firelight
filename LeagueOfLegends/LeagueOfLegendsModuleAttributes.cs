using FirelightCore;
using Games.LeagueOfLegends.ChampionModules.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Games.LeagueOfLegends
{
   // [LEDModuleAttributes(typeof(LeagueOfLegendsModule))]
    public class LeagueOfLegendsModuleAttributes : GameModuleAttributes
    {

        public bool ReducedAnimations => Boolean.Parse(SettingsDictionary["reduced-animations"]);
        public bool LoadingAnimation => Boolean.Parse(SettingsDictionary["loading-animation"]);

        public AbilityCastPreference DefaultCastMode => GetCastMode("default-cast-mode");

        public ItemCastPreference ItemCastPreference => GetItemCastPreference();

        public MouseKeyBinding QBinding => GetBinding("q-binding");
        public MouseKeyBinding WBinding => GetBinding("w-binding");
        public MouseKeyBinding EBinding => GetBinding("e-binding");
        public MouseKeyBinding RBinding => GetBinding("r-binding");

        public MouseKeyBinding Summoner1Binding => GetBinding("summoner1-binding");
        public MouseKeyBinding Summoner2Binding => GetBinding("summoner2-binding");

        public MouseKeyBinding Item1Binding => GetBinding("item1-binding");
        public MouseKeyBinding Item2Binding => GetBinding("item2-binding");
        public MouseKeyBinding Item3Binding => GetBinding("item3-binding");
        public MouseKeyBinding Item4Binding => GetBinding("item4-binding");
        public MouseKeyBinding Item5Binding => GetBinding("item5-binding");
        public MouseKeyBinding Item6Binding => GetBinding("item6-binding");
        public MouseKeyBinding Item7Binding => GetBinding("item7-binding");

        private AbilityCastPreference GetCastMode(string key)
        {
            if (!SettingsDictionary.ContainsKey(key))
                throw new KeyNotFoundException();

            string code = SettingsDictionary[key];

            return (code) switch
            {
                "1" => AbilityCastPreference.Normal,
                "2" => AbilityCastPreference.Quick,
                "3" => AbilityCastPreference.QuickWithIndicator,
                _ => throw new FormatException("Invalid cast mode")
            };
        }

        private ItemCastPreference GetItemCastPreference()
        {
            return new ItemCastPreference()
            {
                Item1 = GetCastMode("item1-cast-mode"),
                Item2 = GetCastMode("item2-cast-mode"),
                Item3 = GetCastMode("item3-cast-mode"),
                Item4 = GetCastMode("item4-cast-mode"),
                Item5 = GetCastMode("item5-cast-mode"),
                Item6 = GetCastMode("item6-cast-mode"),
                Item7 = GetCastMode("item7-cast-mode"),
            };
        }

        public LeagueOfLegendsModuleAttributes()
        {
            this.Id = "leagueoflegends";
            this.Name = "League of Legends";
            this.ProcessName = "leagueoflegends";
            DefaultValues = new Dictionary<string, string>()
            {
                { "enabled", "true" },
                { "reduced-animations", "false" },
                { "loading-animation", "true" },
                { "default-cast-mode", "1" },
                { "item1-cast-mode", "1" },
                { "item2-cast-mode", "1" },
                { "item3-cast-mode", "1" },
                { "item4-cast-mode", "1" },
                { "item5-cast-mode", "1" },
                { "item6-cast-mode", "1" },
                { "item7-cast-mode", "1" },
                { "q-binding", "k81" },
                { "w-binding", "k87" },
                { "e-binding", "k69" },
                { "r-binding", "k82" },
                { "summoner1-binding", "k68" },
                { "summoner2-binding", "k70" },
                { "item1-binding", "k49" },
                { "item2-binding", "k50" },
                { "item3-binding", "k51" },
                { "item4-binding", "k52" },
                { "item5-binding", "k53" },
                { "item6-binding", "k54" },
                { "item7-binding", "k55" }
            };
        }

        public override void GenerateDefaultSettings()
        {
            List<Type> champions = Utils.GetTypesWithAttribute<ChampionAttribute>();
            List<string> championNames = champions
                .Select(x => ((ChampionAttribute)Attribute.GetCustomAttribute(champions[0], typeof(ChampionAttribute))).ChampionName).ToList();

            foreach (var item in DefaultValues)
            {
                SettingsDictionary.Add(item.Key, item.Value);
            }

            foreach (string champ in championNames)
            {
                SettingsDictionary.Add(champ + "-q-cast-mode", "1");
                SettingsDictionary.Add(champ + "-w-cast-mode", "1");
                SettingsDictionary.Add(champ + "-e-cast-mode", "1");
                SettingsDictionary.Add(champ + "-r-cast-mode", "1");
            }
        }

        /// <summary>
        /// Goes through all setting properties, and if there's a parsing error or is non-existent, assigns a default value to it.
        /// </summary>
        /// <returns></returns>
        public override bool ValidateSettingsDictionary()
        {
            try
            {
                ValidateSetting("enabled", () => Boolean.Parse(SettingsDictionary["enabled"]));
                ValidateSetting("reduced-animations", () => Boolean.Parse(SettingsDictionary["reduced-animations"]));
                ValidateSetting("loading-animation", () => Boolean.Parse(SettingsDictionary["loading-animation"]));
                ValidateSetting("default-cast-mode", () => GetCastMode("default-cast-mode"));
                ValidateSetting("q-binding", () => GetBinding("q-binding"));
                ValidateSetting("w-binding", () => GetBinding("w-binding"));
                ValidateSetting("e-binding", () => GetBinding("e-binding"));
                ValidateSetting("r-binding", () => GetBinding("r-binding"));
                ValidateSetting("summoner1-binding", () => GetBinding("summoner1-binding"));
                ValidateSetting("summoner2-binding", () => GetBinding("summoner2-binding"));
                for (int i = 1; i <= 7; i++)
                {
                    ValidateSetting($"item{i}-cast-mode", () => GetCastMode($"item{i}-cast-mode"));
                    ValidateSetting($"item{i}-binding", () => GetBinding($"item{i}-binding"));
                }

            }
            catch (Exception)
            {
                return false;
            }
            return true;

        }
    }
}
