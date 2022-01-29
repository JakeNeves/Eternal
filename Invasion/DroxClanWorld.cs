using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Eternal
{
    public class DroxClanWorld : ModWorld
    {
        public static int DCPoints = 0;
        public static int DCStage;
        public static bool DClan;

        public static bool downedDroxOverlord = false;

        public override void Initialize()
        {
            downedDroxOverlord = false;
            DClan = false;
            DCStage = 0;
        }

        public override TagCompound Save()
        {
            var DCCompound = new TagCompound
            {
                {"DClan", DClan},
                {"downedDroxOverlord", downedDroxOverlord},
                {"DCStage", DCStage}
            };
            return DCCompound;
        }

        public override void Load(TagCompound tag)
        {
            DClan = tag.GetBool("DClan");
            downedDroxOverlord = tag.GetBool("downedDroxOverlord");
            DCStage = tag.GetAsInt("DCStage");
        }

        public override void NetSend(BinaryWriter writer)
        {
            writer.Write(DClan);
            writer.Write(downedDroxOverlord);
            writer.Write(DCStage);
        }

        public override void NetReceive(BinaryReader reader)
        {
            DClan = reader.ReadBoolean();
            downedDroxOverlord = reader.ReadBoolean();
            DCStage = reader.ReadInt32();
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
