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
			Projectile.CloneDefaults(ProjectileID.DD2PetGato);
			Projectile.width = 46;
			Projectile.height = 92;

			AIType = ProjectileID.DD2PetGato;
		}

		public override bool PreAI()
		{
			Player player = Main.player[Projectile.owner];

			player.petFlagDD2Gato = false;

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
