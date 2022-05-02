using System.Collections.Generic;
using CustomChatColors.Models;
using Rocket.API;

namespace CustomChatColors
{
    public class CustomChatColorsConfiguration : IRocketPluginConfiguration
    {
        public List<MCustomColor> CustomColors;
        
        public void LoadDefaults()
        {
            CustomColors = new List<MCustomColor>
            {
                new MCustomColor { PlayerId = 76561198132469161, HexColorCode = "FF00FF"}
            };
        }
    }
}