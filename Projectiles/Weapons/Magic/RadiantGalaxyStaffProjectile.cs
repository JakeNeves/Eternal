using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace Eternal.Projectiles.Weapons.Magic
{
    public class RadiantGalaxyStaffProjectile : ModProjectile
    {
        private const float MAX_CHARGE = 100f;

        private const float MOVE_DISTANCE = 60f;

        public float Distance
        {
            get => projectile.localAI[0];
            set => projectile.localAI[0] = value;
        }

        public float Charge
        {
            get => projectile.localAI[0];
            set => projectile.localAI[0] = value;
        }

        public bool IsAtMaxCharge => Charge == MAX_CHARGE;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Radiant Galaxy Laser");
        }

        public override void SetDefaults()
        {
            projectile.width = 26;
            projectile.height = 26;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.magic = true;
            projectile.hide = true;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            if (IsAtMaxCharge)
            {
                DrawLaser(spriteBatch, Main.projectileTexture[projectile.type], Main.player[projectile.owner].Center, projectile.velocity, 10, projectile.damage, -1.27f, 1f, 1000f, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), (int)MOVE_DISTANCE);
            }
            return false;
        }

        public void DrawLaser(SpriteBatch spriteBatch, Texture2D texture, Vector2 start, Vector2 unit, float step, int damage, float rotation = 0f, float scale = 1f, float maxDist = 2000f, Color color = default(Color), int transDist = 50)
        {
            float r = unit.ToRotation() + rotation;

            for (float i = transDist; i <= Distance; i += step)
            {
                Color c = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
                var origin = start + i * unit;
                spriteBatch.Draw(texture, origin - Main.screenPosition, new Rectangle(0, 26, 28, 26), i < transDist ? Color.Transparent : c, r, new Vector2(28 * .5f, 26 * .5f), scale, 0, 0);
            }

            spriteBatch.Draw(texture, start + unit * (transDist - step) - Main.screenPosition, new Rectangle(0, 0, 28, 26), new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), r, new Vector2(28 * .5f, 26 * .5f), scale, 0, 0);

            spriteBatch.Draw(texture, start + (transDist - step) * unit - Main.screenPosition, new Rectangle(0, 52, 28, 26), new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), r, new Vector2(28 * .5f, 26 * .5f), scale, 0, 0);
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (!IsAtMaxCharge) return false;

            Player player = Main.player[projectile.owner];
            Vector2 unit = projectile.velocity;
            float point = 0f;

            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), player.Center, player.Center + unit * Distance, 22, ref point);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 5;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            projectile.position = player.Center + projectile.velocity * MOVE_DISTANCE;
            projectile.timeLeft = 2;

            if (Charge < MAX_CHARGE) return;

            SetLaserPosition(player);

            ChargeLaser(player);
            UpdatePlayer(player);

            CastLights();
        }

        private void SetLaserPosition(Player player)
        {
            for (Distance = MOVE_DISTANCE; Distance <= 2200f; Distance += 5f)
            {
                var start = player.Center + projectile.velocity * Distance;
                if(!Collision.CanHit(player.Center, 1, 1, start, 1, 1))
                {
                    Distance -= 5f;
                    break;
                }
            }
        }

        private void ChargeLaser(Player player)
        {
            if (!player.channel)
            {
                projectile.Kill();
            }
            else
            {
                if (Main.time % 10 < 1 && !player.CheckMana(player.inventory[player.selectedItem].mana, true))
                {
                    projectile.Kill();
                }
                Vector2 offset = projectile.velocity;
                offset *= MOVE_DISTANCE - 20;
                Vector2 pos = player.Center + offset - new Vector2(10, 10);
                if (Charge < MAX_CHARGE)
                {
                    Charge++;
                }
                
            }
        }

        private void UpdatePlayer(Player player)
        {
            if (projectile.owner == Main.myPlayer)
            {
                Vector2 diff = Main.MouseWorld - player.Center;
                diff.Normalize();
                projectile.velocity = diff;
                projectile.direction = Main.MouseWorld.X > player.position.X ? 1 : -1;
                projectile.netUpdate = true;
            }
            int dir = projectile.direction;
            player.ChangeDir(dir);
            player.heldProj = projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = (float)Math.Atan2(projectile.velocity.Y * dir, projectile.velocity.X * dir);
        }

        private void CastLights()
        {
            DelegateMethods.v3_1 = new Vector3(0.8f, 0.8f, 1f);
            Utils.PlotTileLine(projectile.Center, projectile.Center + projectile.velocity * (Distance - MOVE_DISTANCE), 26, DelegateMethods.CastLight);
        }

        public override bool ShouldUpdatePosition() => false;

        public override void CutTiles()
        {
            DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
            Vector2 unit = projectile.velocity;
            Utils.PlotTileLine(projectile.Center, projectile.Center + unit * Distance, (projectile.width + 16) * projectile.scale, DelegateMethods.CutTiles);
        }

    }
}
