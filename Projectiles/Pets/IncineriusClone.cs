using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Projectiles.Pets
{
    public class IncineriusClone : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 6;
            Main.projPet[projectile.type] = true;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
            ProjectileID.Sets.LightPet[projectile.type] = true;
        }

        public override void SetDefaults()
        {
            projectile.width = 55;
            projectile.height = 61;
            projectile.penetrate = -1;
            projectile.netImportant = true;
            projectile.timeLeft *= 5;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.scale = 0.8f;
            projectile.tileCollide = false;
            aiType = ProjectileID.PetLizard;
			projectile.light = 0.75f;
        }

		//ripped from the example mod because i'm lazy
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			SpriteEffects spriteEffects = SpriteEffects.None;
			if (projectile.spriteDirection == -1)
			{
				spriteEffects = SpriteEffects.FlipHorizontally;
			}
			Texture2D texture = Main.projectileTexture[projectile.type];
			int frameHeight = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
			int startY = frameHeight * projectile.frame;
			Rectangle sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);
			Vector2 origin = sourceRectangle.Size() / 2f;
			origin.X = (float)(projectile.spriteDirection == 1 ? sourceRectangle.Width - 20 : 20);

			Color drawColor = projectile.GetAlpha(lightColor);
			Main.spriteBatch.Draw(texture,
				projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY),
				sourceRectangle, drawColor, projectile.rotation, origin, projectile.scale, spriteEffects, 0f);

			return false;
		}


		public override bool PreAI()
		{
			Player player = Main.player[projectile.owner];
			player.lizard = false;
			return true;
		}

		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			EternalPlayer modPlayer = player.GetModPlayer<EternalPlayer>();
			if (player.dead)
			{
				modPlayer.incineriusPet = false;
			}
			if (modPlayer.incineriusPet)
			{
				projectile.timeLeft = 2;
			}
		}
	}
}
