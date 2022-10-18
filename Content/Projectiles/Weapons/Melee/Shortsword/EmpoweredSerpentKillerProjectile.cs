using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Weapons.Melee.Shortsword
{
    public class EmpoweredSerpentKillerProjectile : ModProjectile
    {
		public const int FadeInDuration = 7;
		public const int FadeOutDuration = 4;

		public const int TotalDuration = 16;

		public float CollisionWidth => 10f * Projectile.scale;

		public int Timer
		{
			get => (int)Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Empowered Serpent Killer");
		}

		public override void SetDefaults()
		{
			Projectile.Size = new Vector2(18);
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			Projectile.scale = 1f;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.ownerHitCheck = true;
			Projectile.extraUpdates = 1;
			Projectile.timeLeft = 360;
			Projectile.hide = true;
		}

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];

			var entitySource = Projectile.GetSource_FromAI();

			Timer += 1;
			if (Timer >= TotalDuration)
			{
				for (int i = 0; i < 3; i++)
				{
					Projectile.NewProjectile(entitySource, player.Center.X + Main.rand.Next(-48, 48), player.Center.Y + Main.rand.Next(-48, 48), Projectile.velocity.X, Projectile.velocity.Y, ModContent.ProjectileType<EmpoweredSerpentKillerOrb>(), Projectile.damage, 0, Main.myPlayer, 0f, 0f);
				}
				Projectile.Kill();
				return;
			}
			else
			{
				player.heldProj = Projectile.whoAmI;
			}

			Projectile.Opacity = Utils.GetLerpValue(0f, FadeInDuration, Timer, clamped: true) * Utils.GetLerpValue(TotalDuration, TotalDuration - FadeOutDuration, Timer, clamped: true);

			Vector2 playerCenter = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: false, addGfxOffY: false);
			Projectile.Center = playerCenter + Projectile.velocity * (Timer - 1f);

			Projectile.spriteDirection = (Vector2.Dot(Projectile.velocity, Vector2.UnitX) >= 0f).ToDirectionInt();

			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 - MathHelper.PiOver4 * Projectile.spriteDirection;

			SetVisualOffsets();
		}

		private void SetVisualOffsets()
		{
			const int HalfSpriteWidth = 44 / 2;
			const int HalfSpriteHeight = 44 / 2;

			int HalfProjWidth = Projectile.width / 2;
			int HalfProjHeight = Projectile.height / 2;

			DrawOriginOffsetX = 0;
			DrawOffsetX = -(HalfSpriteWidth - HalfProjWidth);
			DrawOriginOffsetY = -(HalfSpriteHeight - HalfProjHeight);

		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			SoundEngine.PlaySound(SoundID.NPCDeath14, Projectile.position);

			for (int i = 0; i < 50; i++)
			{
				Vector2 position = Projectile.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 50f * i)) * 60;
				Dust dust = Dust.NewDustPerfect(Projectile.position, DustID.PinkTorch);
				dust.noGravity = true;
				dust.velocity = Vector2.Normalize(position - Projectile.Center) * 8;
				dust.noLight = false;
				dust.fadeIn = 1f;
			}

			for (int i = 0; i < 25; i++)
			{
				Vector2 position = Projectile.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 25f * i)) * 30;
				Dust dust = Dust.NewDustPerfect(Projectile.position, DustID.PurpleTorch);
				dust.noGravity = true;
				dust.velocity = Vector2.Normalize(position - Projectile.Center) * 4;
				dust.noLight = false;
				dust.fadeIn = 1f;
			}
		}

        public override bool ShouldUpdatePosition()
		{
			return false;
		}

		public override void CutTiles()
		{
			DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
			Vector2 start = Projectile.Center;
			Vector2 end = start + Projectile.velocity.SafeNormalize(-Vector2.UnitY) * 10f;
			Utils.PlotTileLine(start, end, CollisionWidth, DelegateMethods.CutTiles);
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			Vector2 start = Projectile.Center;
			Vector2 end = start + Projectile.velocity * 6f;
			float collisionPoint = 0f;
			return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), start, end, CollisionWidth, ref collisionPoint);
		}
	}
}
