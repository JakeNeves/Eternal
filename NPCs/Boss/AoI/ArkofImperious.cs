using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using Eternal.Items.Weapons.Ranged;
using Eternal.Items.Ammo;
using Eternal.Items.BossBags;
using Eternal.Items.Weapons.Melee;
using Eternal.Items.Materials;
using Eternal.Projectiles.Boss;
using Eternal.Items.Potions;
using Microsoft.Xna.Framework.Graphics;

namespace Eternal.NPCs.Boss.AoI
{
    [AutoloadBossHead]
    public class ArkofImperious : ModNPC
    {
        private Player player;

        #region Fundimentals
        float speed = 0;
        int Phase = 0;
        int AttackTimer = 0;
        #endregion

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ark of Imperious");
            NPCID.Sets.TrailCacheLength[npc.type] = 12;
            NPCID.Sets.TrailingMode[npc.type] = 0;
        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.width = 68;
            npc.height = 230;
            npc.lifeMax = 1200000;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath3;
            npc.boss = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/BladeofBrutality");
            npc.defense = 70;
            npc.damage = 62;
            npc.lavaImmune = true;
            npc.noTileCollide = true;
            npc.noGravity = true;
            npc.knockBackResist = 0f;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Venom] = true;
            npc.buffImmune[BuffID.ShadowFlame] = true;
            npc.buffImmune[BuffID.CursedInferno] = true;
            npc.buffImmune[BuffID.Frostburn] = true;
            npc.buffImmune[BuffID.Frozen] = true;
            npc.buffImmune[BuffID.Chilled] = true;
            bossBag = ItemType<AoIBag>();
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width * 0.5f, npc.height * 0.5f);
            for (int k = 0; k < npc.oldPos.Length; k++)
            {
                Vector2 drawPos = npc.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, npc.gfxOffY);
                Color color = npc.GetAlpha(lightColor) * ((float)(npc.oldPos.Length - k) / (float)npc.oldPos.Length);
                spriteBatch.Draw(Main.npcTexture[npc.type], drawPos, null, color, npc.rotation, drawOrigin, npc.scale, SpriteEffects.None, 0f);
            }
            return true;
        }

        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffID.Bleeding, 180, false);
            player.AddBuff(BuffID.BrokenArmor, 180, false);
            if (EternalWorld.hellMode)
            {
                player.AddBuff(BuffID.Cursed, 180, false);
            }
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 2400000;
            npc.defense = 72;
            if(EternalWorld.hellMode)
            {
                npc.lifeMax = 3600000;
                npc.defense = 74;
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemType<PristineHealingPotion>();
        }

        public override void AI()
        {
            Vector2 vector2 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
            float xDir = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector2.X;
            float yDir = (float)(Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - 120) - vector2.Y;
            float length = (float)Math.Sqrt(xDir * xDir + yDir * yDir);

            player = Main.player[npc.target];

            Move(new Vector2(0f, 0f));

            DespawnHandler();

            if (npc.life < npc.lifeMax / 2)
            {
                Phase = 1;
            }

            #region close up attack
            float currentXDist = Math.Abs(npc.Center.X - player.Center.X);
            if (npc.Center.X < player.Center.X && npc.ai[2] < 0)
                npc.ai[2] = 0;
            if (npc.Center.X > player.Center.X && npc.ai[2] > 0)
                npc.ai[2] = 0;

            float yDist = player.position.Y - (npc.position.Y + npc.height);
            if (yDist < 0)
                npc.velocity.Y = npc.velocity.Y - 0.2F;
            if (yDist > 150)
                npc.velocity.Y = npc.velocity.Y + 0.2F;
            npc.velocity.Y = MathHelper.Clamp(npc.velocity.Y, -6, 6);
            if (EternalWorld.hellMode)
            {
                npc.rotation = npc.velocity.X * 0.06f;
            }
            else
            {
                npc.rotation = npc.velocity.X * 0.03f;
            }

            if ((currentXDist < 500 || EternalWorld.hellMode) && npc.position.Y < player.position.Y)
            {
                ++npc.ai[3];
                int cooldown = 15;
                if (npc.life < npc.lifeMax * 0.75)
                    cooldown = 154;
                if (npc.life < npc.lifeMax * 0.5)
                    cooldown = 13;
                if (npc.life < npc.lifeMax * 0.25)
                    cooldown = 12;
                cooldown++;
                if (npc.ai[3] > cooldown)
                    npc.ai[3] = -cooldown;

                /*if (npc.ai[3] == 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 position = npc.Center;
                    position.X += npc.velocity.X * 7;

                    float speedX = player.Center.X - npc.Center.X;
                    float speedY = player.Center.Y - npc.Center.Y;
                    float num12 = speed / length;
                    speedX = speedX * num12;
                    speedY = speedY * num12;
                    Projectile.NewProjectile(position.X, position.Y, speedX + 8, speedY + 8, ProjectileType<ArkArrowHostile>(), 28, 0, Main.myPlayer);
                }*/
            }
            #endregion

            AttackTimer++;

            if ((AttackTimer == 100 || AttackTimer == 150 || AttackTimer == 175))
            {
                NPC.NewNPC((int)npc.Center.X - 20, (int)npc.Center.Y, NPCType<Arkling>());

                Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, -12, 0, ProjectileType<ArkArrowHostile>(), 6, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, 12, 0, ProjectileType<ArkArrowHostile>(), 6, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, 0, 12, ProjectileType<ArkArrowHostile>(), 6, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, 0, -12, ProjectileType<ArkArrowHostile>(), 6, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, -8, -8, ProjectileType<ArkArrowHostile>(), 6, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 8, -8, ProjectileType<ArkArrowHostile>(), 6, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, -8, 8, ProjectileType<ArkArrowHostile>(), 6, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 8, 8, ProjectileType<ArkArrowHostile>(), 6, 0, Main.myPlayer, 0f, 0f);
            }
            else if ((AttackTimer == 200 || AttackTimer == 250 || AttackTimer == 275))
            {
                NPC.NewNPC((int)npc.Center.X - 20, (int)npc.Center.Y, NPCType<Arkling>());
                AttackTimer = 0;
            }

        }

        private void Move(Vector2 offset)
        {
            if (Phase == 1)
            {
                speed = 14f;
            }
            else
            {
                speed = 10f;
            }
            npc.rotation = npc.velocity.X * 0.06f;
            Vector2 moveTo = player.Center + offset;
            Vector2 move = moveTo - npc.Center;
            float magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            float turnResistance = 5f;
            move = (npc.velocity * turnResistance + move) / (turnResistance + 1f);
            magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            npc.velocity = move;
        }

        public override void NPCLoot()
        {
            if(!EternalWorld.downedArkOfImperious)
            {
                Main.NewText("The stars are calling upon you...", 0, 90, 216);
                EternalWorld.downedArkOfImperious = true;
            }

            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                if (Main.rand.Next(1) == 0)
                {
                   Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<Arkbow>());
                   Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<ArkArrow>(), Main.rand.Next(30, 90));
                }
                if (Main.rand.Next(2) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<DormantHeroSword>());
                }
                if (Main.rand.Next(3) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<TheImperiousCohort>());
                }
                if (Main.rand.Next(4) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<TheEnigma>());
                }
            }
        }

        private void DespawnHandler()
        {
            if (!player.active || player.dead)
            {
                npc.TargetClosest(false);
                player = Main.player[npc.target];
                if (!player.active || player.dead)
                {
                    npc.velocity = new Vector2(0f, -10f);
                    if (npc.timeLeft > 10)
                    {
                        npc.timeLeft = 10;
                    }
                    return;
                }
            }
        }

        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

    }
}
