using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Eternal.Invasion
{
    public class DroxClanPlayer : ModPlayer
    {
        public override void PostUpdate()
        {
            if (DroxClanWorld.DCPoints >= 1000)
            {
                EternalWorld.downedDroxClan = true;
                Main.NewText("The Drox Clan has been defeated!", 175, 75, 255);
                DroxClanWorld.DCPoints = 0;
                DroxClanWorld.DClan = false;
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.WorldData);
                }
            }
        }
    }
}
