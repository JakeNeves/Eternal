using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Misc
{
    public class CosmicApparitionSoul : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul of the Cosmic Apparition");
        }

        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.alpha = 255;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.timeLeft = 1600;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            for (int k = 0; k < 5; k++)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Shadowflame, Projectile.oldVelocity.X * 1f, Projectile.oldVelocity.Y * 1f);
                Main.dust[dust].noGravity = true;
            }

            Player player = Main.player[Projectile.owner];

            switch (Projectile.timeLeft)
            {
                case 1490:
                    Main.NewText("...", 150, 36, 120);
                    break;
                case 1250:
                    Main.NewText(player.name + "!", 150, 36, 120);
                    break;
                case 1090:
                    Main.NewText("Innocence...", 150, 36, 120);
                    break;
                case 850:
                    Main.NewText("Doesn't...", 150, 36, 120);
                    break;
                case 690:
                    Main.NewText("Get...", 150, 36, 120);
                    break;
                case 450:
                    Main.NewText("You...", 150, 36, 120);
                    break;
                case 290:
                    Main.NewText("Far!", 150, 36, 120);
                    break;
                case 150:
                    Main.NewText("Remember that.", 150, 36, 120);
                    break;
            }
        }
    }
}
