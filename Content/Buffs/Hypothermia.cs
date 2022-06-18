using Eternal.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Buffs
{
    public class Hypothermia : ModBuff
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hypothermia");
            Description.SetDefault("You are shivering");
            Main.debuff[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<BrutalHellModeSystem>().hypothermia = true;
        }
    }
}
