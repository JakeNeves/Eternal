using Eternal.Common.Configurations;
using Eternal.Common.Misc;
using Eternal.Common.Systems;
using Eternal.Content.Items.BossBags;
using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Boss;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Boss.Incinerius
{
    [AutoloadBossHead]
    public class Incinerius : ModNPC
    {
        int attackTimer = 0;

        Vector2 CircleDirc = new Vector2(0.0f, 16f);

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;

            NPCID.Sets.ShouldBeCountedAsBoss[Type] = true;

            NPCID.Sets.TrailCacheLength[NPC.type] = 14;
            NPCID.Sets.TrailingMode[NPC.type] = 0;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,

                new FlavorTextBestiaryInfoElement("An infernal construct capable of burning anything to ashes with no warning, it has been said, he was the original subzero elemental before melting and incimatizing himself to the heat of the underworld, never to be seen again!")
            });
        }

        public override void SetDefaults()
        {
            NPC.width = 100;
            NPC.height = 116;
            NPC.boss = true;
            Music = MusicID.Boss3;
            NPC.aiStyle = -1;
            NPC.damage = 12;
            NPC.defense = 20;
            NPC.lifeMax = 48000;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.Electrified] = true;
            NPC.lavaImmune = true;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.Tink;
            NPC.DeathSound = SoundID.NPCDeath42;
        }

        public Vector2 bossCenter
        {
            get { return NPC.Center; }
            set { NPC.position = value - new Vector2(NPC.width / 2, NPC.height / 2); }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * balance * bossAdjustment);
            NPC.damage = (int)(NPC.damage * balance * bossAdjustment);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {

            }
        }

        private Player player;

        public override bool PreAI()
        {
            NPC.spriteDirection = NPC.direction;

            NPC.TargetClosest(true);
            NPC.spriteDirection = NPC.direction;
            Player player = Main.player[NPC.target];
            if (player.dead || !player.active)
            {
                NPC.TargetClosest(false);
                NPC.active = false;
            }

            NPC.rotation = NPC.velocity.X * 0.01f;

            float speed = 18.5f;
            float acceleration = 0.20f;
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

            return true;
        }

        public override void AI()
        {
            if (ClientConfig.instance.bossBarExtras)
            {
                if (!EternalBossBarOverlay.visible && Main.netMode != NetmodeID.Server)
                {
                    EternalBossBarOverlay.SetTracked("The Underworld's Infernal Sabotuer, ", NPC, ModContent.Request<Texture2D>("Eternal/Assets/Textures/UI/EternalBossBarEternalBossBar", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value);
                    EternalBossBarOverlay.visible = true;
                }
            }

            Player player = Main.player[NPC.target];

            Lighting.AddLight(NPC.position, 2.15f, 0.95f, 0f);

            var entitySource = NPC.GetSource_FromAI();

            #region Death Dialogue
            if (player.dead || !player.active)
            {
                switch (Main.rand.Next(6))
                {
                    case 0:
                        CombatText.NewText(NPC.Hitbox, new Color(215, 95, 0), "Hey guess what, you just got burnt to crisp!", dramatic: true);
                        Main.NewText("Hey guess what, you just got burnt to crisp!", 215, 95, 0);
                        break;
                    case 1:
                        CombatText.NewText(NPC.Hitbox, new Color(215, 95, 0), "Did you like the improvements I've made? I did what I can to show them off.", dramatic: true);
                        Main.NewText("Did you like the improvements I've made? I did what I can to show them off.", 215, 95, 0);
                        break;
                    case 2:
                        CombatText.NewText(NPC.Hitbox, new Color(215, 95, 0), "I just can't wait to see if this is going to get persistent!", dramatic: true);
                        Main.NewText("I just can't wait to see if this is going to get persistent!", 215, 95, 0);
                        break;
                    case 3:
                        CombatText.NewText(NPC.Hitbox, new Color(215, 95, 0), "There's plenty of mistakes to where that came from!", dramatic: true);
                        Main.NewText("There's plenty of mistakes to where that came from!", 215, 95, 0);
                        break;
                    case 4:
                        CombatText.NewText(NPC.Hitbox, new Color(215, 95, 0), "That was terrible...", dramatic: true);
                        Main.NewText("That was terrible...", 215, 95, 0);
                        break;
                    case 5:
                        CombatText.NewText(NPC.Hitbox, new Color(215, 95, 0), "Maybe if you practiced enough, life would have been much easier than you think...", dramatic: true);
                        Main.NewText("Maybe if you practiced enough, life would have been much easier than you think...", 215, 95, 0);
                        break;
                }
                NPC.active = false;
            }
            #endregion

            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            attackTimer++;

            if (attackTimer == 100 || attackTimer == 175)
            {
                
            }
            if (attackTimer > 250 && attackTimer < 275)
            {
                CircleDirc = Utils.RotatedBy(CircleDirc, 0.10000000149011612, new Vector2());
                int index5 = Projectile.NewProjectile(entitySource, NPC.Center, CircleDirc, ProjectileID.InfernoHostileBlast, NPC.damage, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                Main.projectile[index5].tileCollide = false;
                Main.projectile[index5].timeLeft = 300;
            }

            if (attackTimer > 1000)
            {
                attackTimer = 0;
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());

            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<IncineriusBag>()));

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ApparitionalMatter>(), minimumDropped: 6, maximumDropped: 8));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<StarmetalBar>(), minimumDropped: 12, maximumDropped: 16));
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.15f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int Frame = (int)NPC.frameCounter;
            NPC.frame.Y = Frame * frameHeight;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color lightColor)
        {
            Main.instance.LoadNPC(NPC.type);
            Texture2D texture = ModContent.Request<Texture2D>("Eternal/Content/NPCs/Boss/Incinerius/Incinerius_Shadow").Value;

            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, NPC.height * 0.5f);
            for (int k = 0; k < NPC.oldPos.Length; k++)
            {
                Vector2 drawPos = (NPC.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, NPC.gfxOffY);
                Color color = NPC.GetAlpha(lightColor) * ((NPC.oldPos.Length - k) / (float)NPC.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, null, color, NPC.rotation, drawOrigin, NPC.scale, SpriteEffects.None, 0);
            }

            return true;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
    }
}
