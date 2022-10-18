using Eternal.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Buffs
{
    public class EmperorsPower : ModBuff
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Emperor's Power");
            Description.SetDefault("From a whole loaf of Royal Galadian Bread, you feel your power getting stronger");
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 24;
            player.endurance += 1.26f;
            player.moveSpeed += 1.05f;
        }
    }
}
