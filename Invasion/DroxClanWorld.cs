using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal
{
    public class DroxClanWorld : ModWorld
    {
        public static int DCPoints = 0;
        public static bool DClan;

        public static bool downedDroxOverlord = false;

        public override void Initialize()
        {
            downedDroxOverlord = false;
            DClan = false;
        }

        public override void PostUpdate()
        {
            if (DCPoints >= 1000)
            {
                EternalWorld.downedDroxClan = true;
                Main.NewText("The Drox Clan has been defeated!", 175, 75, 255);
                DCPoints = 0;
                DClan = false;
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.WorldData);
                }
            }
        }
    }
}
