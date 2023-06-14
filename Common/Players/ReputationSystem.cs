using Eternal.Content.Items.Misc;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Eternal.Common.Players
{
    public class ReputationSystem : ModSystem
    {

        public static int ReputationPoints;

        public static bool canEarnReputation = false;

        public override void OnWorldLoad()
        {
            ReputationPoints = 0;

            canEarnReputation = false;
        }

        public override void PreUpdatePlayers()
        {
            if (Main.LocalPlayer.HasItem(ModContent.ItemType<EmperorsTrust>()))
            {
                canEarnReputation = true;
            }
            else
            {
                canEarnReputation = false;
            }
        }

        public override void PostUpdateWorld()
        {
            if (ReputationPoints > 5000)
            {
                ReputationPoints = 5000;
            }
            if (ReputationPoints < 0)
            {
                ReputationPoints = 0;
            }
        }

        public override void OnWorldUnload()
        {
            ReputationPoints = 0;

            canEarnReputation = false;
        }

        public override void SaveWorldData(TagCompound tag)
        {
            tag["Reputation"] = ReputationPoints;

            if (canEarnReputation)
            {
                tag["earnReputation"] = true;
            }
        }

        public override void LoadWorldData(TagCompound tag)
        {
            ReputationPoints = tag.GetInt("Reputation");

            canEarnReputation = tag.ContainsKey("earnReuptation");
        }

        public override void NetSend(BinaryWriter writer)
        {
            var flags = new BitsByte();

            flags[0] = canEarnReputation;

            writer.Write(flags);
            writer.Write(ReputationPoints);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            canEarnReputation = flags[0];

            ReputationPoints = reader.ReadInt32();
        }
    }
}
