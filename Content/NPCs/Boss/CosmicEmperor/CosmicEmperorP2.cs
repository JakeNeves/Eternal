using Eternal.Common.Players;
using Eternal.Common.Systems;
using Eternal.Content.Dusts;
using Eternal.Content.Items.Potions;
using Eternal.Content.Projectiles.Boss;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Boss.CosmicEmperor
{
    [AutoloadBossHead]
    public class CosmicEmperorP2 : ModNPC
    {
        int timer;

        int dialogueTimer = 0;
        int DialogueDeathTimer = 0;
        int deathExplosionTimer = 0;

        bool midFightDialogue = true;
        bool phase2Init = false;
        bool isDead = false;
        bool dontKillyet = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cosmic Emperor");

            var drawModifier = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                CustomTexturePath = "Eternal/Content/NPCs/Boss/CosmicEmperor/CosmicEmperor"
            };

            NPCID.Sets.ShouldBeCountedAsBoss[Type] = true;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

                new FlavorTextBestiaryInfoElement("Hier to the thone, the saviour of the stars. Very well known for his cruel tyranny.")
            });
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 4000000;
            NPC.width = 54;
            NPC.height = 56;
            NPC.damage = 110;
            NPC.defense = 90;
            NPC.knockBackResist = -1f;
            NPC.boss = true;
            NPC.noTileCollide = true;
            Music = MusicID.LunarBoss;
            NPC.noGravity = true;
            NPC.lavaImmune = true;
            NPC.buffImmune[BuffID.Confused] = true;
            NPC.buffImmune[BuffID.CursedInferno] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCDeath/CosmicEmperorDeath");
        }

        public override void OnKill()
        {
            NPC.SetEventFlagCleared(ref DownedBossSystem.downedCosmicEmperor, -1);
        }

        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            if (damage > NPC.lifeMax / 2)
            {
                SoundEngine.PlaySound(SoundID.DD2_BallistaTowerShot);
                if (Main.rand.NextBool(1))
                {
                    Main.NewText("No!", 150, 36, 120);
                }
                if (Main.rand.NextBool(2))
                {
                    Main.NewText("I shall not tolerate such action...", 150, 36, 120);
                }
                if (Main.rand.NextBool(3))
                {
                    Main.NewText("What is wrong with you?", 150, 36, 120);
                }
                if (Main.rand.NextBool(4))
                {
                    Main.NewText("You think your black magic can withstand my potental?", 150, 36, 120);
                }
                if (Main.rand.NextBool(5))
                {
                    Main.NewText("What an absolute cheater you are.", 150, 36, 120);
                }
                if (Main.rand.NextBool(6))
                {
                    Main.NewText("Don't you butcher me with your nonsense!", 150, 36, 120);
                }
                if (Main.rand.NextBool(7))
                {
                    Main.NewText("That did not penetrate me...", 150, 36, 120);
                }
                if (Main.rand.NextBool(8))
                {
                    Main.NewText("Maybe you should go butcher someone else, not me!", 150, 36, 120);
                }

                damage = 0;
            }
            return false;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            if (Main.masterMode)
            {
                NPC.lifeMax = 12000000;
                NPC.defense = 182;
                NPC.damage = 80;
            }
            else if (DifficultySystem.hellMode)
            {
                NPC.lifeMax = 16000000;
                NPC.defense = 184;
                NPC.damage = 86;
            }
            else if (DifficultySystem.hellMode)
            {
                NPC.lifeMax = 20000000;
                NPC.defense = 186;
                NPC.damage = 90;
            }
            else
            {
                NPC.lifeMax = 8000000;
                NPC.defense = 180;
                NPC.damage = 60;
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (!dontKillyet)
            {
                if (NPC.life < 0)
                {
                    NPC.life = 1;
                    isDead = true;
                }
            }

            for (int k = 0; k < damage / NPC.life * 0.25; k++)
            {
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, ModContent.DustType<CosmicSpirit>(), hitDirection, 0, 0, default(Color), 1f);
            }
        }

        public override bool PreAI()
        {
            NPC.netUpdate = true;
            NPC.TargetClosest(true);
            Player player = Main.player[NPC.target];
            if (!player.active || player.dead)
            {
                NPC.TargetClosest(false);
                NPC.velocity.Y = 50;
            }
            NPC.rotation = NPC.velocity.X * 0.04f;
            if (NPC.ai[0] == 0)
            {
                #region Flying Movement
                float speed = 80f;
                float acceleration = 1.15f;
                Vector2 vector2 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
                float xDir = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector2.X;
                float yDir = (float)(Main.player[NPC.target].position.Y + (Main.player[NPC.target].height / 2) - 120) - vector2.Y;
                float length = (float)Math.Sqrt(xDir * xDir + yDir * yDir);
                if (length > 400 && Main.expertMode)
                {
                    ++speed;
                    acceleration += 0.05F;
                    if (length > 600)
                    {
                        ++speed;
                        acceleration += 0.10F;
                        if (length > 800)
                        {
                            ++speed;
                            acceleration += 0.15F;
                        }
                    }
                }
                float num10 = speed / length;
                xDir = xDir * num10;
                yDir = yDir * num10;
                if (NPC.velocity.X < xDir)
                {
                    NPC.velocity.X = NPC.velocity.X + acceleration;
                    if (NPC.velocity.X < 0 && xDir > 0)
                        NPC.velocity.X = NPC.velocity.X + acceleration;
                }
                else if (NPC.velocity.X > xDir)
                {
                    NPC.velocity.X = NPC.velocity.X - acceleration;
                    if (NPC.velocity.X > 0 && xDir < 0)
                        NPC.velocity.X = NPC.velocity.X - acceleration;
                }
                if (NPC.velocity.Y < yDir)
                {
                    NPC.velocity.Y = NPC.velocity.Y + acceleration;
                    if (NPC.velocity.Y < 0 && yDir > 0)
                        NPC.velocity.Y = NPC.velocity.Y + acceleration;
                }
                else if (NPC.velocity.Y > yDir)
                {
                    NPC.velocity.Y = NPC.velocity.Y - acceleration;
                    if (NPC.velocity.Y > 0 && yDir < 0)
                        NPC.velocity.Y = NPC.velocity.Y - acceleration;
                }
                #endregion
            }

            return true;
        }

        public override void AI()
        {
            NPC.netUpdate = true;
            NPC.TargetClosest(true);
            Player player = Main.player[NPC.target];

            var entitySource = NPC.GetSource_FromAI();

            if (NPC.life < NPC.lifeMax / 2)
            {
                if (!phase2Init)
                {
                    for (int i = 0; i < 24; ++i)
                    {
                        NPC.NewNPC(entitySource, (int)NPC.position.X + Main.rand.Next(-120, 120), (int)NPC.position.Y + Main.rand.Next(-120, 120), ModContent.NPCType<CosmicEmperorClone>());
                    }
                    phase2Init = true;
                }

                if (NPC.AnyNPCs(ModContent.NPCType<CosmicEmperorClone>()))
                {
                    NPC.dontTakeDamage = true;
                    NPC.immortal = true;
                    midFightDialogue = false;
                }
                else
                {
                    NPC.dontTakeDamage = false;
                    NPC.immortal = false;
                    midFightDialogue = true;
                }
            }

            #region dialogue
            if (midFightDialogue)
            {
                dialogueTimer++;
                if (NPC.life < NPC.lifeMax / 2)
                {
                    if (dialogueTimer == 1)
                    {
                        Main.NewText("I AM THE EMPEROR!", 150, 36, 120);
                    }
                    if (dialogueTimer == 200)
                    {
                        Main.NewText("MY SPEECH IS THUNDER!", 150, 36, 120);
                    }
                    if (dialogueTimer == 400)
                    {
                        Main.NewText("NO MORE PEACE!", 150, 36, 120);
                    }
                    if (dialogueTimer == 600)
                    {
                        Main.NewText("NO MORE TRANQUILITY!", 150, 36, 120);
                    }
                    if (dialogueTimer == 800)
                    {
                        Main.NewText("EVERYTHING FALLS BEFORE ME!", 150, 36, 120);
                    }
                    if (dialogueTimer == 1000)
                    {
                        Main.NewText("NOBODY CAN BE TOUGHER THAN ME!", 150, 36, 120);
                    }
                    if (dialogueTimer == 1200)
                    {
                        Main.NewText("NO TYRANT CAN OUTPOWER ME!", 150, 36, 120);
                    }
                    if (dialogueTimer == 1400)
                    {
                        Main.NewText("NO MORE RESISTANCE!", 150, 36, 120);
                    }
                    if (dialogueTimer == 1600)
                    {
                        Main.NewText("SAY GOODBYE TO WHAT SOON, WILL CRUMBLE INTO ASHES!", 150, 36, 120);
                    }
                    if (dialogueTimer == 1800)
                    {
                        Main.NewText("I AM THE EMPEROR!", 150, 36, 120);
                    }
                    if (dialogueTimer == 2000)
                    {
                        Main.NewText("SAVIOUR OF THIS WORLD!", 150, 36, 120);
                    }
                    if (dialogueTimer == 2200)
                    {
                        Main.NewText("YOU WILL WATCH AS EVERYTHING AROUND YOU, LIES TO RUIN!", 150, 36, 120);
                    }
                    if (dialogueTimer == 2400)
                    {
                        Main.NewText("YOUR POWERLESS HUSK OF A BODY WILL WITHER WITHIN WHAT WILL SOON, BE DEMOLISHED INTO NOTHINGNESS!", 150, 36, 120);
                    }
                    if (dialogueTimer == 2600)
                    {
                        Main.NewText("YOUR RESISTANCE WILL FALL!", 150, 36, 120);
                    }
                    if (dialogueTimer == 2800)
                    {
                        Main.NewText("MY POWER IS UNMATCHED BY HELPLESS PEASENTS ALONE!", 150, 36, 120);
                        dialogueTimer = 0;
                    }
                }
                else
                {
                    if (dialogueTimer == 1)
                    {
                        Main.NewText("I AM THE EMPEROR!", 150, 36, 120);
                    }
                    if (dialogueTimer == 200)
                    {
                            Main.NewText("I AM POWER!", 150, 36, 120);
                    }
                    if (dialogueTimer == 400)
                    {
                        Main.NewText("MY SKIN IS TOUGHER THAN A THOUSAND ARMORS!", 150, 36, 120);
                    }
                    if (dialogueTimer == 600)
                    {
                        Main.NewText("WARS!", 150, 36, 120);
                    }
                    if (dialogueTimer == 800)
                    {
                        Main.NewText("POLITICS!", 150, 36, 120);
                    }
                    if (dialogueTimer == 1000)
                    {
                        Main.NewText("WORTHLESS!", 150, 36, 120);
                    }
                    if (dialogueTimer == 1200)
                    {
                        Main.NewText("DISTRACTIONS!", 150, 36, 120);
                    }
                    if (dialogueTimer == 1400)
                    {
                        Main.NewText("I AM THE EMPEROR!", 150, 36, 120);
                    }
                    if (dialogueTimer == 1600)
                    {
                        Main.NewText("THE WORLD CRUMBLES BEFORE ME!", 150, 36, 120);
                    }
                    if (dialogueTimer == 1800)
                    {
                        Main.NewText("IT'S DOOM O' CLOCK!", 150, 36, 120);
                    }
                    if (dialogueTimer == 2000)
                    {
                        if (ReputationSystem.ReputationPoints >= 100)
                            Main.NewText("THAT FALSE EMPEROR HAS CORRUPTED YOU!", 150, 36, 120);
                        else
                            Main.NewText("YOUR RESISTANCE MEANS NOTHING!", 150, 36, 120);
                    }
                    if (dialogueTimer == 2200)
                    {
                        Main.NewText("CAN'T YOU SEE!?", 150, 36, 120);
                    }
                    if (dialogueTimer == 2400)
                    {
                        Main.NewText("I AM SUPERIOR!", 150, 36, 120);
                    }
                    if (dialogueTimer == 2600)
                    {
                        Main.NewText("I AM BUILT DIFFERENT!", 150, 36, 120);
                    }
                    if (dialogueTimer == 2800)
                    {
                        Main.NewText("I AM A TRUE TYRANT!", 150, 36, 120);
                    }
                    if (dialogueTimer == 3000)
                    {
                        Main.NewText("TIME TO REPENT!", 150, 36, 120);
                    }
                    if (dialogueTimer == 3200)
                    {
                        Main.NewText("I AM THE EMPEROR!", 150, 36, 120);
                    }
                    if (dialogueTimer == 3400)
                    {
                        Main.NewText("THIS IS HOW AN ERA ENDS!", 150, 36, 120);
                    }
                    if (dialogueTimer == 3600)
                    {
                        Main.NewText("I WILL CRUMBLE THIS WORLD INTO ASHES!", 150, 36, 120);
                    }
                    if (dialogueTimer == 3800)
                    {
                        Main.NewText("EVERYONE WILL BOW TOWARDS ME!", 150, 36, 120);
                    }
                    if (dialogueTimer == 4000)
                    {
                        Main.NewText("YOUR REIGN OF UNSPEAKABLE TERROR ENDS HERE!", 150, 36, 120);
                        dialogueTimer = 0;
                    }
                }
            }
            else
            {
                dialogueTimer = 0;
            }
            #endregion

            timer++;
            if ((timer == 200 || timer == 400 && NPC.life >= (NPC.lifeMax / 2)))
            {
                for (int i = 0; i < 3; i++)
                {
                    Projectile.NewProjectile(entitySource, (int)NPC.position.X + Main.rand.Next(-256, 256), (int)NPC.position.Y + Main.rand.Next(-256, 256), 0, 0, ModContent.ProjectileType<CosmigeddonBomb>(), 0, 0);
                }
            }
            else if ((timer == 600 || timer == 650 || timer == 700 || timer == 800 || timer == 850 || timer == 880))
            {
                NPC.NewNPC(entitySource, (int)NPC.position.X + Main.rand.Next(-256, 256), (int)NPC.position.Y + Main.rand.Next(-256, 256), ModContent.NPCType<GalaxiaStarwisp>());
            }
            else if ((timer == 900 || timer == 950))
            {
            }
            if (timer == 1000)
            {
                timer = 0;
            }

            if (isDead)
            {
                NPC.noTileCollide = false;
                midFightDialogue = false;
                NPC.dontTakeDamage = true;
                timer = 0;

                NPC.velocity.Y += 0.05f;
                NPC.velocity.X = 0;

                DialogueDeathTimer++;
                deathExplosionTimer++;

                entitySource = NPC.GetSource_Death();

                if (deathExplosionTimer == 4)
                {
                    Projectile.NewProjectile(entitySource, (int)NPC.position.X + Main.rand.Next(-64, 64), (int)NPC.position.Y + Main.rand.Next(-64, 64), 0, 0, ModContent.ProjectileType<Projectiles.Explosion.CosmicSpirit>(), 0, 0f);
                    deathExplosionTimer = 0;
                }

                if (!DownedBossSystem.downedCosmicEmperor)
                {
                    
                    switch (DialogueDeathTimer)
                    {
                        case 100:
                            Main.NewText("WHAT!?", 150, 36, 120);
                            break;
                        case 200:
                            Main.NewText("NO!", 150, 36, 120);
                            break;
                        case 300:
                            Main.NewText("HOW DID YOU GAIN SO MUCH POWER!?", 150, 36, 120);
                            break;
                        case 400:
                            Main.NewText("I HAVE BEEN DEFEATED!", 150, 36, 120);
                            break;
                        case 500:
                            Main.NewText("BUT YOU ALSO LOST, " + player.name.ToUpper() + "!", 150, 36, 120);
                            break;
                        case 600:
                            Main.NewText("FATE IS MY OLDEST FRIEND!", 150, 36, 120);
                            break;
                        case 700:
                            Main.NewText("GOODBYE!", 150, 36, 120);
                            break;
                        case 750:
                            for (int k = 0; k < 15; k++)
                            {
                                Dust.NewDust(NPC.Center, NPC.width, NPC.height, ModContent.DustType<CosmicSpirit>(), Main.rand.NextFloat(-1.75f, 1.75f), Main.rand.NextFloat(-1.75f, 1.75f), 0, default(Color), 1f);
                            }
                            NPC.dontTakeDamage = false;
                            DownedBossSystem.downedCosmicEmperor = true;
                            dontKillyet = true;
                            player.ApplyDamageToNPC(NPC, 9999, 0, 0, false);
                            break;
                    }
                }
                else
                {
                    switch (DialogueDeathTimer)
                    {
                        case 100:
                            Main.NewText("WHAT!?", 150, 36, 120);
                            break;
                        case 200:
                            Main.NewText("NO!", 150, 36, 120);
                            break;
                        case 300:
                            Main.NewText("HOW DID YOU GAIN SO MUCH POWER!?", 150, 36, 120);
                            break;
                        case 400:
                            Main.NewText("I HAVE BEEN DEFEATED, AGAIN!", 150, 36, 120);
                            break;
                        case 500:
                            Main.NewText("BUT YOU ALSO LOST, " + player.name.ToUpper() + "!", 150, 36, 120);
                            break;
                        case 600:
                            Main.NewText("FATE IS MY OLDEST FRIEND!", 150, 36, 120);
                            break;
                        case 700:
                            Main.NewText("GOODBYE!", 150, 36, 120);
                            break;
                        case 750:
                            for (int k = 0; k < 15; k++)
                            {
                                Dust.NewDust(NPC.Center, NPC.width, NPC.height, ModContent.DustType<CosmicSpirit>(), Main.rand.NextFloat(-1.75f, 1.75f), Main.rand.NextFloat(-1.75f, 1.75f), 0, default(Color), 1f);
                            }
                            NPC.dontTakeDamage = false;
                            dontKillyet = true;
                            player.ApplyDamageToNPC(NPC, 9999, 0, 0, false);
                            break;
                    }
                }
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            name = "The " + name;
            potionType = ModContent.ItemType<PerfectHealingPotion>();
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color lightColor)
        {
            if (!isDead)
            {
                if (NPC.life < NPC.lifeMax / 2)
                {
                    Main.spriteBatch.End();
                    Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, (Effect)null, Main.GameViewMatrix.ZoomMatrix);
                    MiscShaderData miscShaderData = GameShaders.Misc["HallowBoss"];
                    miscShaderData.UseOpacity(0.25f);
                    miscShaderData.Apply(new DrawData?());
                }
            }
            return true;
        }
    }
}
