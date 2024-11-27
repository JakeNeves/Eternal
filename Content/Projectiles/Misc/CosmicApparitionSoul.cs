using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Misc
{
    public class CosmicApparitionSoul : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.alpha = 255;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.timeLeft = 1800;
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

            switch (Projectile.timeLeft)
            {
                case 1600:
                    CombatText.NewText(Projectile.Hitbox, new Color(150, 36, 120), "...", dramatic: true);
                    Main.NewText("...", 150, 36, 120);
                    break;
                case 1400:
                    CombatText.NewText(Projectile.Hitbox, new Color(150, 36, 120), "Tee hee hee hee hee...", dramatic: true);
                    Main.NewText("Tee hee hee hee hee...", 150, 36, 120);
                    break;
                case 1200:
                    CombatText.NewText(Projectile.Hitbox, new Color(150, 36, 120), "Innocence...", dramatic: true);
                    Main.NewText("Innocence...", 150, 36, 120);
                    break;
                case 1000:
                    CombatText.NewText(Projectile.Hitbox, new Color(150, 36, 120), "Doesn't get you far...", dramatic: true);
                    Main.NewText("Doesn't get you far...", 150, 36, 120);
                    break;
                case 800:
                    CombatText.NewText(Projectile.Hitbox, new Color(150, 36, 120), "Remember that!", dramatic: true);
                    Main.NewText("Remember that!", 150, 36, 120);
                    break;
                case 600:
                    CombatText.NewText(Projectile.Hitbox, new Color(150, 36, 120), "Even when the fun...", dramatic: true);
                    Main.NewText("Even when the fun...", 150, 36, 120);
                    break;
                case 400:
                    CombatText.NewText(Projectile.Hitbox, new Color(150, 36, 120), "Is just beginning!", dramatic: true);
                    Main.NewText("Is just beginning!", 150, 36, 120);
                    break;
                case 200:
                    CombatText.NewText(Projectile.Hitbox, new Color(150, 36, 120), "Our emperor from above the stars, watches your every move...", dramatic: true);
                    Main.NewText("Our emperor from above the stars, watches your every move...", 150, 36, 120);
                    break;
            }
        }

        public override void OnKill(int timeLeft)
        {
            Main.NewText("The tundra crackles restless...", 7, 28, 224);
            Main.NewText("You hear lightning strike the sands across the dunes...", 0, 95, 215);
            Main.NewText("The ancient spirits of the underworld starts blazing furiously...", 215, 95, 0);
            Main.NewText("The seal of the cosmic entities has been broken, newfound materials can be gathered from them...", 220, 0, 210);
            Main.NewText("A faint etherial hum can be heard from the shrine...", 48, 255, 179);
        }
    }
}
