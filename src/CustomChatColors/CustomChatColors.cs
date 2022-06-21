using System.Linq;
using Rocket.Core.Plugins;
using SDG.Unturned;
using UnityEngine;

namespace CustomChatColors
{
    public class CustomChatColors : RocketPlugin<CustomChatColorsConfiguration>
    {
        public static CustomChatColors Instance;
        private Color _goldColor = Palette.PRO;
        
        protected override void Load()
        {
            Instance = this;
            
            ChatManager.onChatted += OnChatted;

            Rocket.Core.Logging.Logger.Log("Loaded! Made by papershredder432#0883, join the support Discord here: https://discord.gg/ydjYVJ2");
            Rocket.Core.Logging.Logger.Log($"Loaded {Configuration.Instance.CustomColors.Count} unique player color(s)!");
            
            if (ColorUtility.TryParseHtmlString($"#{Configuration.Instance.GoldColor}", out Color goldColor))
            {
                _goldColor = goldColor;
                return;
            }
            
            Rocket.Core.Logging.Logger.Log("Could not parse gold color, using default color!");
            _goldColor = Palette.PRO;
        }

        private void OnChatted(SteamPlayer player, EChatMode mode, ref Color chatted, ref bool isrich, string text, ref bool isvisible)
        {
            isrich = Configuration.Instance.RichTextEnabled;
            
            if (text.StartsWith("/"))
                return;

            var playerId = player.playerID.steamID;
            
            if (Configuration.Instance.GoldEnabled)
            {
                if (player.isPro)
                    chatted = _goldColor;
            }
            

            if (Configuration.Instance.CustomColors.FirstOrDefault(x => x.PlayerId == playerId.m_SteamID) == null)
                return;
            
            var multipleEntries = Configuration.Instance.CustomColors.FindAll(x => x.PlayerId == playerId.m_SteamID);
            if (multipleEntries.Count > 1)
            {
                Rocket.Core.Logging.Logger.LogWarning($"Found {multipleEntries.Count} for {playerId}! Remove the duplicate entries in the config for their custom color to work!");
                return;
            }
                
            var selectedEntry = Configuration.Instance.CustomColors.FirstOrDefault(x => x.PlayerId == playerId.m_SteamID);
            if (!ColorUtility.TryParseHtmlString($"#{selectedEntry.HexColorCode}", out Color ourColor))
            {
                Rocket.Core.Logging.Logger.LogWarning($"{selectedEntry.PlayerId} has an invalid color code! Please fix the entry in the config for their custom color to work!");
                return;
            }
                
            chatted = ourColor;
        }

        protected override void Unload()
        {
            Instance = null;
            
            ChatManager.onChatted -= OnChatted;
            
            Rocket.Core.Logging.Logger.Log("Unloaded");
        }
    }
}