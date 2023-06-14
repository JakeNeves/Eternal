using Eternal.Content.Projectiles.Minions;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Buffs.Minions
{
    public class CosmicDecoy : ModBuff
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cosmic Decoy");
            // Description.SetDefault("The Cosmic Apparition's decoys will haunt your enemies in combat");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<CosmicDecoyMinion>()] > 0)
            {
                player.buffTime[buffIndex] = 18000;
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }

    }
}
