using Eternal.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Buffs
{
    public class ApparitionalWither : ModBuff
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Apparitional Wither");
            // Description.SetDefault("Your flesh and bones are massivly decaying");
            Main.debuff[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<BrutalHellModeSystem>().hyperthermia = true;
        }
    }
}
