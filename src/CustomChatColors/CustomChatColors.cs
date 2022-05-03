using System.Linq;
using Rocket.Core.Plugins;
using Rocket.Unturned.Player;
using SDG.Unturned;
using UnityEngine;

namespace CustomChatColors
{
    public class CustomChatColors : RocketPlugin<CustomChatColorsConfiguration>
    {
        public static CustomChatColors Instance;
        
        protected override void Load()
        {
            Instance = this;
            
            ChatManager.onChatted += OnChatted;
            
            Rocket.Core.Logging.Logger.Log("Loaded! Made by papershredder432#0883, join the support Discord here: https://discord.gg/ydjYVJ2");
            Rocket.Core.Logging.Logger.Log($"Loaded {Configuration.Instance.CustomColors.Count} unique player color(s)!");
        }

        private void OnChatted(SteamPlayer player, EChatMode mode, ref Color chatted, ref bool isrich, string text, ref bool isvisible)
        {
            if (text.StartsWith("/"))
                return;
            
            var playerId = player.playerID.steamID;
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