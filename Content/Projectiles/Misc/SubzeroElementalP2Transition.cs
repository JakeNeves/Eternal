using Eternal.Content.NPCs.Boss.SubzeroElemental;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Misc
{
    public class SubzeroElementalP2Transition : ModProjectile
    {
        bool justLanded = false;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Subzero Elemental");
        }

        public override void SetDefaults()
        {
            Projectile.width = 90;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.timeLeft = 550;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            Projectile.ai[0] += 1f;

            if (Projectile.ai[0] > 5f)
            {
                Projectile.ai[0] = 10f;
                if (Projectile.velocity.Y == 0f && Projectile.velocity.X != 0f)
                {
                    Projectile.velocity.X = Projectile.velocity.X * 0.97f;
                    {
                        Projectile.velocity.X = Projectile.velocity.X * 0.99f;
                    }
                    if ((double)Projectile.velocity.X > -0.01 && (double)Projectile.velocity.X < 0.01)
                    {
                        Projectile.velocity.X = 0f;
                        Projectile.netUpdate = true;
                    }
                }
                Projectile.velocity.Y = Projectile.velocity.Y + 0.2f;
            }

            switch (Projectile.timeLeft)
            {
                case 400:
                    CombatText.NewText(Projectile.Hitbox, new Color(0, 90, 210), "Oh...", dramatic: true);
                    Main.NewText("Oh...", 0, 90, 210);
                    break;
                case 250:
                    CombatText.NewText(Projectile.Hitbox, new Color(0, 90, 210), "I'm dead...", dramatic: true);
                    Main.NewText("I'm dead...", 0, 90, 210);
                    break;
                case 50:
                    CombatText.NewText(Projectile.Hitbox, new Color(0, 90, 210), "SIKE!", dramatic: true);
                    Main.NewText("SIKE!", 0, 90, 210);
                    Projectile.Kill();
                    break;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (!justLanded)
            {
                SoundEngine.PlaySound(SoundID.Dig, Projectile.Center);
                justLanded = true;
            }
            return false;
        }

        public override void Kill(int timeLeft)
        {
            var entitySource = Projectile.GetSource_Death();

            for (int k = 0; k < 10; k++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Frost, Projectile.oldVelocity.X * 1f, Projectile.oldVelocity.Y * 1f);
            }

            NPC.NewNPC(entitySource, (int)Projectile.Center.X, (int)Projectile.Center.Y, ModContent.NPCType<SubzeroElemental>());
            SoundEngine.PlaySound(SoundID.NPCDeath14, Projectile.Center);
        }
    }
}
