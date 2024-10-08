using DiscordRPC;
using DiscordRPC.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_RPC_Identity_V
{
    class DiscordRPC
    {
        public DiscordRpcClient client;
        private Config conf = new Config();
        public void Initialize()
        {
            client = new DiscordRpcClient(conf.clientId);
            client.OnReady += (sender, e) =>
            {
                Debug.Info("Discord",string.Format("Received Ready from user {0}", e.User.Username));
                Debug.Info("Main", "Ready!");
            };
            client.OnPresenceUpdate += (sender, e) =>
            {
                Debug.Info("Discord", string.Format("Update Presence"));
            };
            client.Initialize();
        }
        public void Update(string state, string details, string largeImageKey, string largeImageText)
        {
            client.SetPresence(new RichPresence()
            {
                Details = details,
                State = state,
                Assets = new Assets()
                {
                    LargeImageKey = largeImageKey,
                    LargeImageText = largeImageText,
                }
            });
        }
    }
}
