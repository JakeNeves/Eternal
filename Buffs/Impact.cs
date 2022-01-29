using Terraria;
using Terraria.ModLoader;

namespace Eternal.Buffs
{
    public class Impact : ModBuff
    {

        public override void SetDefaults()
        {
            DisplayName.SetDefault("Impact");
            Description.SetDefault("Your attacks deal 25% extra damage");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.allDamage += 0.25f;
        }

    }
}
