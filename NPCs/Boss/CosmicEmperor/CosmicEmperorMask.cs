using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Eternal.Items.Weapons.Melee;
using Eternal.Items.Weapons.Ranged;
using Eternal.Items.BossBags;
using Microsoft.Xna.Framework.Graphics;
using Eternal.Items.Armor;
using Eternal.Projectiles.Boss;
using Eternal.Items.Materials;
using Eternal.Items.Weapons.Hell;
using Eternal.Projectiles.Enemy;
using Eternal.Projectiles;
using Eternal.Buffs;
using Eternal.Items.Potions;
using Eternal.Items.Tools;

namespace Eternal.NPCs.Boss.CosmicEmperor
{
    [AutoloadBossHead]
    public class CosmicEmperorMask : ModNPC
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cosmic Emperor");
            Main.npcFrameCount[npc.type] = 4;
        }

        bool bossIntro = true;
        int introTimer = 0;

        int attackTimer = 0;

        public override void SetDefaults()
        {
            npc.width = 34;
            npc.height = 50;
            npc.defense = 380;
            npc.damage = 380;
            npc.lifeMax = 6000000;
            npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/EmperorHit");
            npc.DeathSound = null;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/JourneysConquest");
            npc.buffImmune[BuffID.Chilled] = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Suffocation] = true;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.Venom] = true;
            npc.buffImmune[BuffID.BetsysCurse] = true;
            npc.buffImmune[BuffID.Daybreak] = true;
            npc.buffImmune[BuffID.DryadsWardDebuff] = true;
            npc.buffImmune[ModContent.BuffType<EmbericCombustion>()] = true;
            npc.buffImmune[ModContent.BuffType<DoomFire>()] = true;
            if (Eternal.instance.CalamityLoaded)
            {
                npc.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("ExoFreeze")] = true;
                npc.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("GlacialState")] = true;
                npc.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("TemporalSadness")] = true;
                npc.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("SilvaStun")] = true;
                npc.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("TimeSlow")] = true;
                npc.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("PearlAura")] = true;
            }
            if (Eternal.instance.FargowiltasModLoaded)
            {
                npc.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("Lethargic")] = true;
                npc.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("Sadism")] = true;
                npc.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("GodEater")] = true;
                npc.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("ClippedWings")] = true;
                npc.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("MutantNibble")] = true;
                npc.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("OceanicMaul")] = true;
                npc.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("TimeFrozen")] = true;
                npc.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("LightningRod")] = true;
            }
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.boss = true;
            npc.knockBackResist = -1f;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.None;
        }

        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            if (damage > npc.lifeMax / 2)
            {
                Main.PlaySound(SoundID.DD2_BallistaTowerShot, (int)npc.position.X, (int)npc.position.Y);
                if (Main.rand.Next(1) == 0)
                {
                    Main.NewText("No!", 0, 95, 215);
                }
                if (Main.rand.Next(2) == 0)
                {
                    Main.NewText("I shall not tolerate such action...", 0, 95, 215);
                }
                if (Main.rand.Next(3) == 0)
                {
                    Main.NewText("What is wrong with you?", 0, 95, 215);
                }
                if (Main.rand.Next(4) == 0)
                {
                    Main.NewText("You think your black magic can withstand my potental?", 0, 95, 215);
                }
                if (Main.rand.Next(5) == 0)
                {
                    Main.NewText("What an absolute cheater you are.", 0, 95, 215);
                }
                if (Main.rand.Next(6) == 0)
                {
                    Main.NewText("Don't you butcher me with your nonsense!", 0, 95, 215);
                }
                if (Main.rand.Next(7) == 0)
                {
                    Main.NewText("That did not penetrate me...", 0, 95, 215);
                }
                if (Main.rand.Next(8) == 0)
                {
                    Main.NewText("Maybe you should go butcher someone else, not me!", 0, 95, 215);
                }

                damage = 0;
            }
            return false;
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 0.15f;
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int Frame = (int)npc.frameCounter;
            npc.frame.Y = Frame * frameHeight;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 9000000;
            if (EternalWorld.hellMode)
            {
                npc.lifeMax = 12000000;
            }
        }

        int amountOfProjectiles;

        public override void AI()
        {
            Vector2 direction = Main.player[npc.target].Center - npc.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            if (bossIntro)
            {
                introTimer++;
                npc.dontTakeDamage = true;
                switch (introTimer)
                {
                    case 120:
                        Main.NewText("It Appears you've prooven your worth...", 0, 95, 215);
                        break;
                    case 240:
                        Main.NewText("Therefore, You've called upon me with the Cosmic Tablet...", 0, 95, 215);
                        break;
                    case 360:
                        Main.NewText("I sha'll accept, what you've seeked me for...", 0, 95, 215);
                        break;
                    case 480:
                        Main.NewText("Let the battle begin!", 0, 95, 215);
                        break;
                }
                if (introTimer >= 480)
                {
                    npc.dontTakeDamage = false;
                    bossIntro = false;
                    introTimer = 0;
                }
            }
            else
            {
                attackTimer++;

                #region Attacks
                if (attackTimer == 100 || attackTimer == 120 || attackTimer == 140 || attackTimer == 160)
                {
                    Projectile.NewProjectile(npc.Center.X + Main.rand.Next(-200, 200), npc.Center.Y + Main.rand.Next(-200, 200), 0, 0, ModContent.ProjectileType<CosmicRing>(), 12, 0, Main.myPlayer, 0f, 0f);
                }
                if (attackTimer == 200 || attackTimer == 202 || attackTimer == 204 || attackTimer == 206 || attackTimer == 208 || attackTimer == 210)
                {
                    amountOfProjectiles = Main.rand.Next(2, 6);
                    for (int i = 0; i < amountOfProjectiles; ++i)
                    {
                        float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                        float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                        int damage = Main.expertMode ? 15 : 17;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Main.PlaySound(SoundID.DD2_BetsyFireballShot, npc.position);
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<CosmicFireball>(), damage, 1, Main.myPlayer, 0, 0);
                    }
                }
                else if (attackTimer == 215)
                {
                    attackTimer = 0;
                }
                #endregion
            }
        }

        public override bool PreAI()
        {
            if (!bossIntro)
            {
                float speed;
                if (EternalWorld.hellMode)
                {
                    speed = 24f;
                }
                else
                {
                    speed = 16f;
                }
                float acceleration = 0.10f;
                Vector2 vector2 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                float xDir = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector2.X;
                float yDir = (float)(Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - 120) - vector2.Y;
                float length = (float)Math.Sqrt(xDir * xDir + yDir * yDir);
                if (length > 400 && Main.expertMode)
                {
                    ++speed;
                    acceleration += 0.05F;
                    if (length > 600)
                    {
                        ++speed;
                        acceleration += 0.05F;
                        if (length > 800)
                        {
                            ++speed;
                            acceleration += 0.05F;
                        }
                    }
                }
                float num10 = speed / length;
                xDir = xDir * num10;
                yDir = yDir * num10;
                if (npc.velocity.X < xDir)
                {
                    npc.velocity.X = npc.velocity.X + acceleration;
                    if (npc.velocity.X < 0 && xDir > 0)
                        npc.velocity.X = npc.velocity.X + acceleration;
                }
                else if (npc.velocity.X > xDir)
                {
                    npc.velocity.X = npc.velocity.X - acceleration;
                    if (npc.velocity.X > 0 && xDir < 0)
                        npc.velocity.X = npc.velocity.X - acceleration;
                }
                if (npc.velocity.Y < yDir)
                {
                    npc.velocity.Y = npc.velocity.Y + acceleration;
                    if (npc.velocity.Y < 0 && yDir > 0)
                        npc.velocity.Y = npc.velocity.Y + acceleration;
                }
                else if (npc.velocity.Y > yDir)
                {
                    npc.velocity.Y = npc.velocity.Y - acceleration;
                    if (npc.velocity.Y > 0 && yDir < 0)
                        npc.velocity.Y = npc.velocity.Y - acceleration;
                }
            }

            return true;
        }

        public override void PostAI()
        {
            if (npc.life <= 1000)
            {
                npc.boss = false;
                /*Main.NewText("You've done well... I feel like you should be ready for my true power!", 0, 95, 215);
                NPC.NewNPC((int)npc.Center.X - 20, (int)npc.Center.Y, ModContent.NPCType<CosmicEmperor>());
                npc.life = 0;*/
            }
        }

        public override void NPCLoot()
        {
            
            Main.NewText("You've done well... I feel like you should be ready for my true power!", 0, 95, 215);
            NPC.NewNPC((int)npc.Center.X - 20, (int)npc.Center.Y, ModContent.NPCType<CosmicEmperor>());

            /*Main.NewText("You've done well... However, you don't seem worthy enough for my true power. Anyway, here is a gift that you can have for now, until you can proove you can take on my true potential!", 0, 95, 215);
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<InterstellarMetal>(), 99);
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<CosmoniumFragment>(), 99);

            if (Main.rand.Next(1) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<ExosiivaGladiusBlade>());
            }
            if (Main.rand.Next(2) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<TheBigOne>());
            }
            if (Main.rand.Next(3) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Exelodon>());
            }*/
        }
    }
}
