using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Pets
{
    public class Ark : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			Main.projPet[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.ZephyrFish);
			Projectile.width = 76;
			Projectile.height = 144;

			AIType = ProjectileID.ZephyrFish;
		}

		public override bool PreAI()
		{
			Player player = Main.player[Projectile.owner];

			player.zephyrfish = false;

			return true;
		}

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];

			if (!player.dead && player.HasBuff(ModContent.BuffType<Buffs.Pets.Ark>()))
			{
				Projectile.timeLeft = 2;
			}
		}
	}
}
