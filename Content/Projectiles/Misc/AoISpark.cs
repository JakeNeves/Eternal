using Eternal.Content.NPCs.Boss.AoI;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Misc
{
    public class AoISpark : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.alpha = 255;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.timeLeft = 100;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            float dustScale = 1f;
            if (Projectile.ai[0] == 0f)
                dustScale = 0.25f;
            else if (Projectile.ai[0] == 1f)
                dustScale = 0.5f;
            else if (Projectile.ai[0] == 2f)
                dustScale = 0.75f;

            if (Main.rand.NextBool(2))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.GreenTorch, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100);
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
            Projectile.ai[0] += 1f;

            if (Projectile.timeLeft <= 50)
            {
                Projectile.velocity.Y--;
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
            var entitySource = Projectile.GetSource_FromAI();

            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GreenTorch, Projectile.oldVelocity.X * 1f, Projectile.oldVelocity.Y * 1f);
            }
            SoundEngine.PlaySound(SoundID.DD2_EtherianPortalOpen, Projectile.position);
            NPC.NewNPC(entitySource, (int)Projectile.Center.X - 20, (int)Projectile.Center.Y, ModContent.NPCType<ArkofImperious>());
            Main.NewText("Ark of Imperious has awoken!", 175, 75, 255);
        }

    }
}
