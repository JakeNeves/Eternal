using Eternal.Common.Players;
using Eternal.Common.Systems;
using Eternal.Content.Items.Potions;
using Eternal.Content.NPCs.Boss.CosmicEmperor;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Misc
{
    public class CosmicEmperorSummon : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.alpha = 255;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.timeLeft = 700;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            for (int k = 0; k < 5; k++)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.DemonTorch, Projectile.oldVelocity.X * 1f, Projectile.oldVelocity.Y * 1f);
                Main.dust[dust].noGravity = true;
            }

            Player player = Main.player[Projectile.owner];

            if (DownedBossSystem.downedCosmicEmperor)
            {
                if (Main.LocalPlayer.HasItem(ModContent.ItemType<CosmicMoonshine>()))
                {
                    switch (Projectile.timeLeft)
                    {
                        case 690:
                            CombatText.NewText(Projectile.Hitbox, new Color(150, 36, 120), "No.", dramatic: true);
                            Main.NewText("No.", 150, 36, 120);
                            break;
                        case 450:
                            Projectile.Kill();
                            break;
                    }
                }
                else
                {
                    switch (Projectile.timeLeft)
                    {
                        case 690:
                            CombatText.NewText(Projectile.Hitbox, new Color(150, 36, 120), "...", dramatic: true);
                            Main.NewText("...", 150, 36, 120);
                            break;
                        case 450:
                            CombatText.NewText(Projectile.Hitbox, new Color(150, 36, 120), "You again?", dramatic: true);
                            Main.NewText("You again?", 150, 36, 120);
                            break;
                        case 290:
                            CombatText.NewText(Projectile.Hitbox, new Color(150, 36, 120), "If you came here looking for a test dummy, you've come to the wrong place!", dramatic: true);
                            Main.NewText("If you came here looking for a test dummy, you've come to the wrong place!", 150, 36, 120);
                            break;
                        case 150:
                            CombatText.NewText(Projectile.Hitbox, new Color(150, 36, 120), "Anywho, let the battle begin.", dramatic: true);
                            Main.NewText("Anywho, let the battle begin.", 150, 36, 120);
                            break;
                    }
                }
            }
            else
            {
                if (Main.LocalPlayer.HasItem(ModContent.ItemType<CosmicMoonshine>()))
                {
                    switch (Projectile.timeLeft)
                    {
                        case 690:
                            CombatText.NewText(Projectile.Hitbox, new Color(150, 36, 120), "No.", dramatic: true);
                            Main.NewText("No.", 150, 36, 120);
                            break;
                        case 450:
                            Projectile.Kill();
                            break;
                    }
                }
                else
                {
                    switch (Projectile.timeLeft)
                    {
                        case 690:
                            CombatText.NewText(Projectile.Hitbox, new Color(150, 36, 120), "To you, who has proven your strength...", dramatic: true);
                            Main.NewText("To you, who has proven your strength...", 150, 36, 120);
                            break;
                        case 450:
                            CombatText.NewText(Projectile.Hitbox, new Color(150, 36, 120), "I have been watching you, ever since you have slayed the one, banished to the moon...", dramatic: true);
                            Main.NewText("I have been watching you, ever since you have slayed the one, banished to the moon...", 150, 36, 120);
                            break;
                        case 290:
                            CombatText.NewText(Projectile.Hitbox, new Color(150, 36, 120), "...And now, your journey ends here, you've come along way since I first witnessed you!", dramatic: true);
                            Main.NewText("...And now, your journey ends here, you've come along way since I first witnessed you!", 150, 36, 120);
                            break;
                        case 150:
                            CombatText.NewText(Projectile.Hitbox, new Color(150, 36, 120), "Let the battle begin!", dramatic: true);
                            Main.NewText("Let the battle begin!", 150, 36, 120);
                            break;
                    }
                }
            }
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.DD2_EtherianPortalOpen, Projectile.Center);

            var entitySource = Projectile.GetSource_Death();

            if (!Main.LocalPlayer.HasItem(ModContent.ItemType<CosmicMoonshine>()))
                NPC.NewNPC(entitySource, (int)Projectile.position.X, (int)Projectile.position.Y, ModContent.NPCType<CosmicEmperor>());
        }
    }
}
