using Eternal.Common.Configurations;
using Eternal.Common.Misc;
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
using Terraria.Localization;
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
        bool doAttacks = true;

        public override LocalizedText DisplayName => base.DisplayName.WithFormatArgs("Cosmic Emperor");

        public override void SetStaticDefaults()
        {
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

        /*public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
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
        }*/

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * balance * bossAdjustment);
            NPC.damage = (int)(NPC.damage * balance * bossAdjustment);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (!dontKillyet)
            {
                if (NPC.life < 0)
                {
                    NPC.life = 1;
                    isDead = true;
                }
            }

            for (int k = 0; k < 0.25; k++)
            {
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, ModContent.DustType<CosmicSpirit>(), 0, 0, 0, default(Color), 1f);
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
            if (ClientConfig.instance.bossBarExtras)
            {
                if (!EternalBossBarOverlay.visible && Main.netMode != NetmodeID.Server)
                {
                    EternalBossBarOverlay.SetTracked("Master of the Cosmic Power, ", NPC, ModContent.Request<Texture2D>("Eternal/Assets/Textures/UI/EternalBossBar", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value);
                    EternalBossBarOverlay.visible = true;
                }
            }

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

            if (doAttacks)
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

            if (NPC.ai[3] > 0f)
            {
                NPC.ai[3] += 1f;
                NPC.dontTakeDamage = true;

                NPC.velocity = new Vector2(Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f));

                if (Main.rand.NextBool(5) && NPC.ai[3] < 180f)
                {
                    for (int dustNumber = 0; dustNumber < 3; dustNumber++)
                    {
                        Dust dust = Main.dust[Dust.NewDust(NPC.Left, NPC.width, NPC.height / 2, DustID.PinkTorch, 0f, 0f, 0, default(Color), 1f)];
                        dust.position = NPC.Center + Vector2.UnitY.RotatedByRandom(4.1887903213500977) * new Vector2(NPC.width * 1.5f, NPC.height * 1.1f) * 0.8f * (0.8f + Main.rand.NextFloat() * 0.2f);
                        dust.velocity.X = 0f;
                        dust.velocity.Y = -Math.Abs(dust.velocity.Y - (float)dustNumber + NPC.velocity.Y - 4f) * 3f;
                        dust.noGravity = true;
                        dust.fadeIn = 1f;
                        dust.scale = 1f + Main.rand.NextFloat() + (float)dustNumber * 0.3f;
                    }
                }

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
                        Main.NewText("MY POWER IS DEPLETING", 150, 36, 120);
                        break;
                    case 600:
                        Main.NewText("DEATH IS MY OLDEST FRIEND!", 150, 36, 120);
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
                        break;
                }

                if (NPC.ai[3] >= 750f)
                {
                    NPC.life = 0;
                    if (!DownedBossSystem.downedCosmicEmperor)
                    {
                        DownedBossSystem.downedCosmicEmperor = true;
                    }

                    NPC.HitEffect(0, 0);
                    NPC.checkDead();
                }
            }
        }

        public override bool CheckDead()
        {
            if (NPC.ai[3] == 0f)
            {
                NPC.ai[3] = 1f;
                NPC.damage = 0;
                NPC.life = NPC.lifeMax;
                NPC.dontTakeDamage = true;
                NPC.netUpdate = true;
                return false;
            }
            return true;
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
