using Eternal.NPCs.Boss.AoI;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Projectiles
{
    public class AoISpark : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spark of Imperious"); 
		}

		public override void SetDefaults()
		{
			projectile.width = 6;
			projectile.height = 6;
			projectile.alpha = 255;
			projectile.friendly = false;
			projectile.hostile = false;
			projectile.timeLeft = 100;
			projectile.ignoreWater = false;
			projectile.tileCollide = false;
		}

		public override void AI()
		{

			float dustScale = 1f;
			if (projectile.ai[0] == 0f)
				dustScale = 0.25f;
			else if (projectile.ai[0] == 1f)
				dustScale = 0.5f;
			else if (projectile.ai[0] == 2f)
				dustScale = 0.75f;

			if (Main.rand.Next(2) == 0)
			{
				Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.GreenTorch, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100);
				if (Main.rand.NextBool(3))
				{
					dust.noGravity = true;
					dust.scale *= 3f;
					dust.velocity.X *= 2f;
					dust.velocity.Y *= 2f;
				}

				dust.scale *= 1.5f;
				dust.velocity *= 1.2f;
				dust.scale *= dustScale;
			}
			projectile.ai[0] += 1f;

			if (projectile.timeLeft <= 50)
            {
                projectile.velocity.Y--;
            }
		}

		public override void ModifyDamageHitbox(ref Rectangle hitbox)
		{
			int size = 30;
			hitbox.X -= size;
			hitbox.Y -= size;
			hitbox.Width += size * 2;
			hitbox.Height += size * 2;
		}

		public override void Kill(int timeLeft)
        {
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.GreenTorch, projectile.oldVelocity.X * 1f, projectile.oldVelocity.Y * 1f);
			}
            Main.PlaySound(SoundID.DD2_EtherianPortalOpen, projectile.position);
			NPC.NewNPC((int)projectile.Center.X - 20, (int)projectile.Center.Y, ModContent.NPCType<ArkofImperious>());
			Main.NewText("Ark of Imperious has awoken!", 175, 75, 255);
		}

	}
}
