using Eternal.Common.Configurations;
using Eternal.Content.Items.Misc;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Eternal.Common.Players
{
    public class LifeMotePlayer : ModPlayer
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ServerConfig.instance.lifeMotes;
        }

        public int lifeMotes;

        public override void ModifyMaxStats(out StatModifier health, out StatModifier mana)
        {
            health = StatModifier.Default;
            health.Base = lifeMotes * LifeMote.LifePerMote;
            mana = StatModifier.Default;
        }

        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = Mod.GetPacket();
            packet.Write((byte)Eternal.MessageType.StatIncreaseSync);
            packet.Write((byte)Player.whoAmI);
            packet.Write((byte)lifeMotes);
            packet.Send(toWho, fromWho);
        }
        public void ReceivePlayerSync(BinaryReader reader)
        {
            lifeMotes = reader.ReadByte();
        }

        public override void CopyClientState(ModPlayer targetCopy)
        {
            LifeMotePlayer clone = (LifeMotePlayer)targetCopy;
            clone.lifeMotes = lifeMotes;
        }

        public override void SendClientChanges(ModPlayer clientPlayer)
        {
            LifeMotePlayer clone = (LifeMotePlayer)clientPlayer;

            if (lifeMotes != clone.lifeMotes)
                SyncPlayer(toWho: -1, fromWho: Main.myPlayer, newPlayer: false);
        }

        public override void SaveData(TagCompound tag)
        {
            tag["lifeMotes"] = lifeMotes;
        }

        public override void LoadData(TagCompound tag)
        {
            lifeMotes = tag.GetInt("lifeMotes");
        }
    }
}
