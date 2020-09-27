using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework.Graphics;
using Eternal.Projectiles.Enemy;
using Eternal.Items.Weapons.Ranged;
using Eternal.Items.Ammo;
using Eternal.Items.BossBags;

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
            npc.width = 60;
            npc.height = 184;
            npc.lifeMax = 1200000;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath3;
            npc.boss = true;
            music = MusicID.Boss3;
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

        private void Shoot()
        {
            int type = ProjectileType<ArkArrowHostile>();
            Vector2 velocity = player.Center - npc.Center;
            float magnitude = Magnitude(velocity);
            if (magnitude > 0)
            {
                velocity *= 5f / magnitude;
            }
            else
            {
                velocity = new Vector2(0f, 5f);
            }
            Projectile.NewProjectile(npc.Center, velocity, type, npc.damage, 2f);
            npc.ai[1] = 25f;
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
            potionType = ItemID.SuperHealingPotion;
        }

        public override void AI()
        {
            player = Main.player[npc.target];

            Move(new Vector2(0f, 0f));

            RotateNPCToTarget();

            if (npc.life < npc.lifeMax / 2)
            {
                Phase = 1;
            }

            AttackTimer++;

            switch(AttackTimer)
            {
                case 100:
                    Shoot();
                    break;
                case 250:
                    Shoot();
                    if (Main.expertMode)
                    {
                        SpawnArks();
                    }
                    if (EternalWorld.hellMode)
                    {
                        SpawnHellModeArks();
                    }
                    break;
                case 300:
                    Shoot();
                    break;
                case 450:
                    AttackTimer = 0;
                    if (Main.expertMode)
                    {
                        SpawnArks();
                    }
                    if (EternalWorld.hellMode)
                    {
                        SpawnHellModeArks();
                    }
                    break;
            }

        }

        private void SpawnArks()
        {
            NPC.NewNPC((int)npc.Center.X - 20, (int)npc.Center.Y, NPCType<Arkling>());
        }

        private void SpawnHellModeArks()
        {
            NPC.NewNPC((int)npc.Center.X - 20, (int)npc.Center.Y, NPCType<FakeAoI>());
        }

        private void Move(Vector2 offset)
        {
            if (Phase == 1)
            {
                speed = 18f;
            }
            else
            {
                speed = 10f;
            }
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
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                if (Main.rand.Next(1) == 0)
                {
                    player.QuickSpawnItem(ItemType<Arkbow>());
                }

                player.QuickSpawnItem(ItemType<ArkArrow>(), Main.rand.Next(30, 90));
            }
        }

        private void RotateNPCToTarget()
        {
            if (player == null) return;
            Vector2 direction = npc.Center - player.Center;
            float rotation = (float)Math.Atan2(direction.Y, direction.X);
            npc.rotation = rotation + ((float)Math.PI * 0.5f);
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
