using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using Eternal.Items;
using Eternal.Items.Weapons.Melee;
using Eternal.Items.Weapons.Ranged;
using Eternal.Items.BossBags;
using Microsoft.Xna.Framework.Graphics;

namespace Eternal.NPCs.Boss.Dunekeeper
{
    [AutoloadBossHead]
    public class Dunekeeper : ModNPC
    {
        private Player player;

        #region Fundimentals
        bool overhead;
        float speed;
        int attackTimer;
        int Phase;
        #endregion

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 9000;
            npc.damage = 5;
            npc.defense = 15;
            npc.knockBackResist = 0f;
            npc.width = 58;
            npc.height = 58;
            npc.value = Item.buyPrice(gold: 30);
            npc.lavaImmune = true;
            npc.boss = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.buffImmune[BuffID.Electrified] = true;
            npc.HitSound = SoundID.NPCHit3;
            npc.DeathSound = SoundID.NPCDeath3;
            bossBag = ItemType<DunekeeperBag>();
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/DunesWrath");
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/DunekeeperEye"), 1f);
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/DunekeeperLeftHalf"), 1f);
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/DunekeeperRightHalf"), 1f);
            }
        }

        public override void NPCLoot()
        {
            Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, -8, 0, ProjectileID.MartianTurretBolt, 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 8, 0, ProjectileID.MartianTurretBolt, 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 0, 8, ProjectileID.MartianTurretBolt, 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 0, -8, ProjectileID.MartianTurretBolt, 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, -8, -8, ProjectileID.MartianTurretBolt, 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 8, -8, ProjectileID.MartianTurretBolt, 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, -8, 8, ProjectileID.MartianTurretBolt, 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 8, 8, ProjectileID.MartianTurretBolt, 6, 0, Main.myPlayer, 0f, 0f);

            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                if (Main.rand.Next(1) == 0)
                {
                    player.QuickSpawnItem(ItemType<Wasteland>());
                }

                if (Main.rand.Next(2) == 0)
                {
                    player.QuickSpawnItem(ItemType<StormBeholder>());
                }

                if (Main.rand.Next(3) == 0)
                {
                    player.QuickSpawnItem(ItemType<Items.Armor.ThunderduneHeadgear>());
                }

                player.QuickSpawnItem(ItemType<DuneCore>(), Main.rand.Next(25, 75));
            }
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
            => GlowMaskUtils.DrawNPCGlowMask(spriteBatch, npc, mod.GetTexture("NPCs/Boss/Dunekeeper/Dunekeeper_Glow"));

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 12000;
            npc.damage = (int)(npc.damage + 1f);
            npc.defense = (int)(npc.defense + numPlayers);
            if (EternalWorld.hellMode)
            {
                npc.lifeMax = 24000;
            }
        }

        private void Move(Vector2 offset)
        {
            if(Phase == 1)
            {
                speed = 4.4f;
            }
            else if (!player.ZoneDesert)
            {
                speed = 6f;
            }
            else {
                speed = 4f;
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

        private void Shoot(Player player)
        {
            int type = ProjectileID.MartianTurretBolt;
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

        public override void AI()
        {
            attackTimer++;
            if (NPC.AnyNPCs(NPCType<DunekeeperHandL>()) || NPC.AnyNPCs(NPCType<DunekeeperHandR>()))
            {
                npc.dontTakeDamage = true;
            }
            else
            {
                npc.dontTakeDamage = false;
            }

            if (npc.life < npc.lifeMax / 2)
            {
                Phase = 1;
            }

            if (Phase == 1)
            {
                switch(attackTimer)
                {
                    case 100:
                        Shoot(player);
                        break;
                    case 200:
                        Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, -8, 0, ProjectileID.MartianTurretBolt, 6, 0, Main.myPlayer, 0f, 0f);
                        Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 8, 0, ProjectileID.MartianTurretBolt, 6, 0, Main.myPlayer, 0f, 0f);
                        Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 0, 8, ProjectileID.MartianTurretBolt, 6, 0, Main.myPlayer, 0f, 0f);
                        Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 0, -8, ProjectileID.MartianTurretBolt, 6, 0, Main.myPlayer, 0f, 0f);
                        break;
                    case 300:
                        attackTimer = 0;
                        break;
                }
            }
            else
            {
                switch(attackTimer)
                {
                    case 100:
                        Shoot(player);
                        break;
                    case 200:
                        Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, -8, -8, ProjectileID.MartianTurretBolt, 6, 0, Main.myPlayer, 0f, 0f);
                        Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 8, -8, ProjectileID.MartianTurretBolt, 6, 0, Main.myPlayer, 0f, 0f);
                        Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, -8, 8, ProjectileID.MartianTurretBolt, 6, 0, Main.myPlayer, 0f, 0f);
                        Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 8, 8, ProjectileID.MartianTurretBolt, 6, 0, Main.myPlayer, 0f, 0f);
                        break;
                    case 300:
                        attackTimer = 0;
                        break;
                }
            }

            #region movement
            player = Main.player[npc.target];

            Move(new Vector2(0f, 0f));

            DespawnHandler();

            npc.spriteDirection = npc.direction;
            #endregion

        }

    }

        

}

