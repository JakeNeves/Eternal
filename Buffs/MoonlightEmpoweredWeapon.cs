using Terraria;
using Terraria.ModLoader;

namespace Eternal.Buffs
{
    public class MoonlightEmpoweredWeapon : ModBuff
    {

        public override void SetDefaults()
        {
            DisplayName.SetDefault("Moonlight Empowered Weapon");
            Description.SetDefault("The Emperor's moonlight blessing empowers your weapons!");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.allDamage += 0.25f;
            player.allDamageMult += 0.15f;
            player.magicCrit += 3;
            player.meleeCrit += 3;
            player.rangedCrit += 3;
        }

    }
}
