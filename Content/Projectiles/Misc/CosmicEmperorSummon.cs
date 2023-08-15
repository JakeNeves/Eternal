using Eternal.Common.Players;
using Eternal.Common.Systems;
using Eternal.Content.Items.Potions;
using Eternal.Content.NPCs.Boss.CosmicEmperor;
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

            Projectile.position.X = player.Center.X;
            Projectile.position.Y = player.Center.Y - 200;

            if (DownedBossSystem.downedCosmicEmperor)
            {
                if (ReputationSystem.ReputationPoints == 5000)
                {
                    switch (Projectile.timeLeft)
                    {
                        case 690:
                            Main.NewText($"I have seen you and your evil schemes, {player.name}!", 150, 36, 120);
                            break;
                        case 450:
                            Main.NewText("You've really pushed my limits!", 150, 36, 120);
                            break;
                        case 290:
                            Main.NewText("With whitty little ants, Scheming behind my back!", 150, 36, 120);
                            break;
                        case 150:
                            Main.NewText("Time to be punished for the greater good!", 150, 36, 120);
                            break;
                    }
                }
                else if (Main.LocalPlayer.HasItem(ModContent.ItemType<CosmicMoonshine>()))
                {
                    switch (Projectile.timeLeft)
                    {
                        case 690:
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
                            Main.NewText("...", 150, 36, 120);
                            break;
                        case 450:
                            Main.NewText("You again?", 150, 36, 120);
                            break;
                        case 290:
                            Main.NewText("If you come here looking for a test dummy, you've came to the wrong place!", 150, 36, 120);
                            break;
                        case 150:
                            Main.NewText("Anywho, let the battle begin.", 150, 36, 120);
                            break;
                    }
                }
            }
            else
            {
                if (ReputationSystem.ReputationPoints >= 100)
                {
                    switch (Projectile.timeLeft)
                    {
                        case 690:
                            Main.NewText("I KNEW IT!", 150, 36, 120);
                            break;
                        case 450:
                            Main.NewText($"You have been following that emperor haven't you {player.name}?", 150, 36, 120);
                            break;
                        case 290:
                            Main.NewText("Do NOT follow his lead, he is a DANGEROUS individual!", 150, 36, 120);
                            break;
                        case 150:
                            Main.NewText("This troublesome resistance of your's won't get through me, this battle will be bloodier within our hands alone, time to repent!", 150, 36, 120);
                            break;
                    }
                }
                else if (Main.LocalPlayer.HasItem(ModContent.ItemType<CosmicMoonshine>()))
                {
                    switch (Projectile.timeLeft)
                    {
                        case 690:
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
                            Main.NewText("To you, who has proven your strength...", 150, 36, 120);
                            break;
                        case 450:
                            Main.NewText("I have been watching you, ever since you have slayed the one, banished to the moon...", 150, 36, 120);
                            break;
                        case 290:
                            Main.NewText($"Now, your journey ends here, you've come along way since I first witnessed you, {player.name}.", 150, 36, 120);
                            break;
                        case 150:
                            Main.NewText("Let the battle begin!", 150, 36, 120);
                            break;
                    }
                }
            }
        }

        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.DD2_EtherianPortalOpen, Projectile.Center);

            var entitySource = Projectile.GetSource_Death();

            if (!Main.LocalPlayer.HasItem(ModContent.ItemType<CosmicMoonshine>()))
                NPC.NewNPC(entitySource, (int)Projectile.position.X, (int)Projectile.position.Y, ModContent.NPCType<CosmicEmperor>());
        }
    }
}
