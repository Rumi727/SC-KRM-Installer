using Newtonsoft.Json;
using SCKRM.Installer.Language;
using System;
using System.Collections.Generic;
using System.IO;
using System.Resources;
//using System.Resources;

namespace SCKRM.Language
{
    public static class LanguageManager
    {
        public static Language currentLanguage = Language.en_us;

        public static ResourceManager ko_kr = Installer.Language.ko_kr.ResourceManager;
        public static ResourceManager en_us = Installer.Language.en_us.ResourceManager;

        public static string LanguageLoad(string key) => LanguageLoad(key, currentLanguage);

        public static string LanguageLoad(string key, Language language)
        {
            if (language == Language.ko_kr)
                return ko_kr.GetObject(key).ToString();
            else
                return en_us.GetObject(key).ToString();
        }

        public enum Language
        {
            en_us,
            ko_kr
        }
    }
}