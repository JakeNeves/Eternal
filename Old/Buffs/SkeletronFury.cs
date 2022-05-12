using Terraria;
using Terraria.ModLoader;

namespace Eternal.Buffs
{
    public class SkeletronFury : ModBuff
    {

        public override void SetDefaults()
        {
            DisplayName.SetDefault("Skeletron's Fury");
            Description.SetDefault("Rage fills within your body...");
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 8;
            player.meleeDamage += 1.2f;
            player.maxRunSpeed += 1f;
            player.moveSpeed += 1f;
        }

    }
}
