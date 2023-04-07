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
            Projectile.timeLeft = 900;
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
                case 890:
                    Main.NewText("...", 150, 36, 120);
                    break;
                case 750:
                    Main.NewText(player.name + "!", 150, 36, 120);
                    break;
                case 690:
                    Main.NewText("Innocence...", 150, 36, 120);
                    break;
                case 550:
                    Main.NewText("Doesn't...", 150, 36, 120);
                    break;
                case 490:
                    Main.NewText("Get...", 150, 36, 120);
                    break;
                case 350:
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

        public override void Kill(int timeLeft)
        {
            Main.NewText("The tundra crackles restless...", 7, 28, 224);
            Main.NewText("You hear lightning strike the dunes...", 0, 95, 215);
            Main.NewText("The ancient spirits of the underworld starts blazing furiously...", 215, 95, 0);
            Main.NewText("The seal of the cosmic entities has been broken, newfound materials can be gathered from them...", 220, 0, 210);
            Main.NewText("A faint etherial hum can be heard from the shrine...", 48, 255, 179);
        }
    }
}
