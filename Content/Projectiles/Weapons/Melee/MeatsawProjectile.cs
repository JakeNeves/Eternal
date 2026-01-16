using Eternal.Content.Buffs.Weapons;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Weapons.Melee
{
    public class MeatsawProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 2;

            ProjectileID.Sets.HeldProjDoesNotUsePlayerGfxOffY[Type] = true;
        }
        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 64;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ownerHitCheck = true;
            Projectile.aiStyle = -1;
            Projectile.hide = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
        }

        public override void AI()
        {
            if (++Projectile.frameCounter >= 3)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.frame = 0;
                }
            }

            Player player = Main.player[Projectile.owner];

            Projectile.timeLeft = 60;

            if (Projectile.soundDelay <= 0)
            {
                SoundEngine.PlaySound(SoundID.Item22, Projectile.Center);
                Projectile.soundDelay = 20;
            }

            Vector2 playerCenter = player.RotatedRelativePoint(player.MountedCenter);
            if (Main.myPlayer == Projectile.owner)
            {
                if (player.channel)
                {
                    float holdoutDistance = player.HeldItem.shootSpeed * Projectile.scale;
                    Vector2 holdoutOffset = holdoutDistance * Vector2.Normalize(Main.MouseWorld - playerCenter);
                    if (holdoutOffset.X != Projectile.velocity.X || holdoutOffset.Y != Projectile.velocity.Y)
                    {
                        Projectile.netUpdate = true;
                    }

                    Projectile.velocity = holdoutOffset;
                }
                else
                {
                    Projectile.Kill();
                }
            }

            if (Projectile.velocity.X > 0f)
            {
                player.ChangeDir(1);
            }
            else if (Projectile.velocity.X < 0f)
            {
                player.ChangeDir(-1);
            }

            Projectile.spriteDirection = Projectile.direction;
            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            player.SetDummyItemTime(2);
            Projectile.Center = playerCenter;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            player.itemRotation = (Projectile.velocity * Projectile.direction).ToRotation();

            Projectile.velocity.X *= 1f + Main.rand.Next(-3, 4) * 0.01f;

            if (Main.rand.NextBool(10))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position + Projectile.velocity * Main.rand.Next(6, 10) * 0.15f, Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 80, Color.White, 1f);
                dust.position.X -= 4f;
                dust.noGravity = true;
                dust.velocity.X *= 0.5f;
                dust.velocity.Y = -Main.rand.Next(3, 8) * 0.1f;
            }

        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            var entitySource = Projectile.GetSource_FromThis();

            if (!Main.dedServ)
            {
                SoundEngine.PlaySound(new SoundStyle("Terraria/Sounds/NPC_Hit_4")
                {
                    Volume = 0.4f,
                    Pitch = Main.rand.NextFloat(-1f, -0.25f),
                    MaxInstances = 0
                }, Projectile.position);

                SoundEngine.PlaySound(new SoundStyle("Terraria/Sounds/NPC_Killed_22")
                {
                    Volume = 0.4f,
                    Pitch = Main.rand.NextFloat(-0.75f, 0f),
                    MaxInstances = 0
                }, Projectile.position);
            }

            if (Main.rand.NextBool(2))
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Blood, Projectile.oldVelocity.X + Main.rand.NextFloat(-1f, 1f), Projectile.oldVelocity.Y + Main.rand.NextFloat(-1f, 1f));

            if (Main.rand.NextBool(4))
                target.AddBuff(ModContent.BuffType<ImmenseArmorFracture>(), 20 * 60);
        }
    }
}
