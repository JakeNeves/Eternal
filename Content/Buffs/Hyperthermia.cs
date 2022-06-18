using Eternal.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Buffs
{
    public class Hyperthermia : ModBuff
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hyperthermia");
            Description.SetDefault("You are sweating");
            Main.debuff[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<BrutalHellModeSystem>().hyperthermia = true;
        }
    }
}
