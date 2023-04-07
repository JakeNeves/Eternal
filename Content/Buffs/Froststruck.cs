using Eternal.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Buffs
{
    public class Froststruck : ModBuff
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Froststruck");
            // Description.SetDefault("The freezing tempuratures make you body brittle");
            Main.debuff[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense -= 15;
            player.endurance -= 1.05f;
            player.moveSpeed -= 1.20f;
        }
    }
}
