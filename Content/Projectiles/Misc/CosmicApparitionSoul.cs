using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Misc
{
    public class CosmicApparitionSoul : ModProjectile
    {
        public static LocalizedText CosmicApparitionDefeated1 { get; private set; }
        public static LocalizedText CosmicApparitionDefeated2 { get; private set; }
        public static LocalizedText CosmicApparitionDefeated3 { get; private set; }
        public static LocalizedText CosmicApparitionDefeated4 { get; private set; }
        public static LocalizedText CosmicApparitionDefeated5 { get; private set; }
        public static LocalizedText CosmicApparitionDefeated6 { get; private set; }

        public override void SetStaticDefaults()
        {
            CosmicApparitionDefeated1 = Mod.GetLocalization($"BossDefeatedEvent.{nameof(CosmicApparitionDefeated1)}");
            CosmicApparitionDefeated2 = Mod.GetLocalization($"BossDefeatedEvent.{nameof(CosmicApparitionDefeated2)}");
            CosmicApparitionDefeated3 = Mod.GetLocalization($"BossDefeatedEvent.{nameof(CosmicApparitionDefeated3)}");
            CosmicApparitionDefeated4 = Mod.GetLocalization($"BossDefeatedEvent.{nameof(CosmicApparitionDefeated4)}");
            CosmicApparitionDefeated5 = Mod.GetLocalization($"BossDefeatedEvent.{nameof(CosmicApparitionDefeated5)}");
            CosmicApparitionDefeated6 = Mod.GetLocalization($"BossDefeatedEvent.{nameof(CosmicApparitionDefeated6)}");
        }

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
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                Main.NewText(CosmicApparitionDefeated1.Value, 7, 28, 224);
                Main.NewText(CosmicApparitionDefeated2.Value, 0, 95, 0);
                Main.NewText(CosmicApparitionDefeated3.Value, 220, 0, 210);
                Main.NewText(CosmicApparitionDefeated4.Value, 48, 255, 179);
                Main.NewText(CosmicApparitionDefeated5.Value, 200, 0, 50);
                Main.NewText(CosmicApparitionDefeated6.Value, 100, 0, 100);
            }
            else if (Main.netMode == NetmodeID.Server)
            {
                ChatHelper.BroadcastChatMessage(CosmicApparitionDefeated1.ToNetworkText(), new Color(7, 28, 224));
                ChatHelper.BroadcastChatMessage(CosmicApparitionDefeated2.ToNetworkText(), new Color(0, 95, 0));
                ChatHelper.BroadcastChatMessage(CosmicApparitionDefeated3.ToNetworkText(), new Color(220, 0, 210));
                ChatHelper.BroadcastChatMessage(CosmicApparitionDefeated4.ToNetworkText(), new Color(48, 255, 179));
                ChatHelper.BroadcastChatMessage(CosmicApparitionDefeated5.ToNetworkText(), new Color(200, 0, 50));
                ChatHelper.BroadcastChatMessage(CosmicApparitionDefeated6.ToNetworkText(), new Color(100, 0, 100));
            }
        }
    }
}
