﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Pets
{
    public class Ark : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ark");

			Main.projPet[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.ZephyrFish);
			Projectile.width = 38;
			Projectile.height = 72;

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