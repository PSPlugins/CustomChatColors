using System.Collections.Generic;
using CustomChatColors.Models;
using Rocket.API;

namespace CustomChatColors
{
    public class CustomChatColorsConfiguration : IRocketPluginConfiguration
    {
        public bool RichTextEnabled;
        public bool GoldEnabled;
        public string GoldColor;
        public List<MCustomColor> CustomColors;
        
        public void LoadDefaults()
        {
            RichTextEnabled = true;
            GoldEnabled = true;
            GoldColor = "C6B41F";
            CustomColors = new List<MCustomColor>
            {
                new MCustomColor { PlayerId = 76561198132469161, HexColorCode = "FF00FF" }
            };
        }
    }
}