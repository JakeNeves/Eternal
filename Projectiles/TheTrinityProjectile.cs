using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Projectiles
{
    public class TheTrinityProjectile : ModProjectile
    {
        public bool yoyosSpawned = false;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] = -1f;
            ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 575f;
            ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 25f;
        }

        public override void SetDefaults()
        {
            projectile.extraUpdates = 1;
            projectile.width = 10;
            projectile.height = 10;
            projectile.aiStyle = 99;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.melee = true;
            projectile.scale = 1f;
            
        }

        public override void AI()
        {
            if(!yoyosSpawned)
            {
                int maxYoyos = 3;
                for (int i = 0; i < maxYoyos; i++)
                {
                    float radians = 360f / (float)maxYoyos * i * (float)(MathHelper.Pi / 180);
                    Projectile yoyoFire = Projectile.NewProjectileDirect(projectile.Center, Vector2.Zero, ProjectileType<FlameBall>(), projectile.damage, projectile.knockBack, projectile.owner, 3, radians);
                    yoyoFire.localAI[0] = projectile.whoAmI;
                    Projectile yoyoIce = Projectile.NewProjectileDirect(projectile.Center, Vector2.Zero, ProjectileID.BallofFrost, projectile.damage, projectile.knockBack, projectile.owner, 3, radians);
                    yoyoIce.localAI[0] = projectile.whoAmI;
                    Projectile yoyoThunder = Projectile.NewProjectileDirect(projectile.Center, Vector2.Zero, ProjectileID.Electrosphere, projectile.damage, projectile.knockBack, projectile.owner, 3, radians);
                    yoyoThunder.localAI[0] = projectile.whoAmI;
                }
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Frostburn, 120);
            target.AddBuff(BuffID.OnFire, 120);
            target.AddBuff(BuffID.Electrified, 120);
        } 

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}


    }
}
