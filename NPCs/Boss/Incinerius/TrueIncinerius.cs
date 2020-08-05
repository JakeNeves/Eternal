using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.NPCs.Boss.Incinerius
{
    [AutoloadBossHead]
    public class TrueIncinerius : ModNPC
    {

        private float vel = 5.5f;
        int Timer;
        const float Speed = 5;
        const float Acceleration = 1;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 6;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 920000;
            npc.width = 59;
            npc.height = 61;
            npc.damage = 25;
            npc.defense = 75;
            npc.boss = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/BlazingExtinction");
            npc.lavaImmune = true;
            npc.noTileCollide = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.Venom] = true;
            npc.buffImmune[BuffID.Electrified] = true;
            npc.knockBackResist = 0f;
            npc.noGravity = true;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath42;
            npc.aiStyle = -1;
        }

        /* Vector2 VelocityFPTP(Vector2 pos1, Vector2 pos2, float speed)
        {
            Vector2 move = pos2 - pos1;
            return move * (speed / (float)Math.Sqrt(move.X * move.X + move.Y * move.Y));
        } */

        public override void BossLoot(ref string name, ref int potionType)
        {
            name = "Incinerius' true form";
            potionType = ItemID.SuperHealingPotion;
        }

        public Vector2 bossCenter
        {
            get { return npc.Center; }
            set { npc.position = value - new Vector2(npc.width / 2, npc.height / 2); }
        }

        public override void AI()
        {
            npc.TargetClosest(true);
            npc.spriteDirection = npc.direction;
            Player player = Main.player[npc.target];
            if (player.dead || !player.active)
            {
                npc.TargetClosest(false);
                npc.active = false;
            }
            Timer++;
            npc.TargetClosest(true);
            if(Timer >= 0)
            {
                Vector2 StartPosition = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                float DirectionX = Main.player[npc.target].position.X + Main.player[npc.target].width / 2 - StartPosition.X;
                float DirectionY = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - 120 - StartPosition.Y;
                float Length = (float)Math.Sqrt(DirectionX * DirectionX + DirectionY * DirectionY);
                float Num = Speed / Length;
                DirectionX = DirectionX * Num;
                DirectionY = DirectionY * Num;
                if (npc.velocity.X < DirectionX)
                {
                    npc.velocity.X = npc.velocity.X + Acceleration;
                    if (npc.velocity.X < 0 && DirectionX > 0)
                        npc.velocity.X = npc.velocity.X + Acceleration;
                }
                else if (npc.velocity.X > DirectionX)
                {
                    npc.velocity.X = npc.velocity.X - Acceleration;
                    if (npc.velocity.X > 0 && DirectionX < 0)
                        npc.velocity.X = npc.velocity.X - Acceleration;
                }
                if (npc.velocity.Y < DirectionY)
                {
                    npc.velocity.Y = npc.velocity.Y + Acceleration;
                    if (npc.velocity.Y < 0 && DirectionY > 0)
                        npc.velocity.Y = npc.velocity.Y + Acceleration;
                }
                else if (npc.velocity.Y > DirectionY)
                {
                    npc.velocity.Y = npc.velocity.Y - Acceleration;
                    if (npc.velocity.Y > 0 && DirectionY < 0)
                        npc.velocity.Y = npc.velocity.Y - Acceleration;
                }
            }
            if (Timer == 100)
            {
                Shoot(player);
            }
            if (Timer == 300)
            {
                Shoot(player);
            }
            if (Timer == 500)
            {
                Shoot(player);
            }
            if (Timer == 700)
            {
                Timer = 0;
            }
        }

        private void Shoot(Player player)
        {
            float angle = Main.rand.Next(0, (int)Math.PI * 200) / 100f;
            Vector2 vel = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 3 * Main.rand.Next(5);
            Projectile.NewProjectile(bossCenter.X, bossCenter.Y + 15, vel.X + 15, vel.Y, ProjectileID.CultistBossFireBall, 30, 5f);
            Projectile.NewProjectile(bossCenter.X, bossCenter.Y - 15, vel.X - 15, vel.Y, ProjectileID.CultistBossFireBall, 30, 5f);
            Projectile.NewProjectile(bossCenter.X, bossCenter.Y + 30, vel.X + 30, vel.Y, ProjectileID.CultistBossFireBall, 30, 5f);
            Projectile.NewProjectile(bossCenter.X, bossCenter.Y - 30, vel.X - 30, vel.Y, ProjectileID.CultistBossFireBall, 30, 5f);
            Projectile.NewProjectile(bossCenter.X, bossCenter.Y + 45, vel.X + 45, vel.Y, ProjectileID.CultistBossFireBall, 30, 5f);
            Projectile.NewProjectile(bossCenter.X, bossCenter.Y - 45, vel.X - 45, vel.Y, ProjectileID.CultistBossFireBall, 30, 5f);

            if (EternalWorld.hellMode)
            {
                Projectile.NewProjectile(bossCenter.X, bossCenter.Y + 15, vel.X + 15, vel.Y, ProjectileID.InfernoHostileBlast, 30, 5f);
                Projectile.NewProjectile(bossCenter.X, bossCenter.Y - 15, vel.X - 15, vel.Y, ProjectileID.InfernoHostileBlast, 30, 5f);
                Projectile.NewProjectile(bossCenter.X, bossCenter.Y + 30, vel.X + 30, vel.Y, ProjectileID.InfernoHostileBlast, 30, 5f);
                Projectile.NewProjectile(bossCenter.X, bossCenter.Y - 30, vel.X - 30, vel.Y, ProjectileID.InfernoHostileBlast, 30, 5f);
                Projectile.NewProjectile(bossCenter.X, bossCenter.Y + 45, vel.X + 45, vel.Y, ProjectileID.InfernoHostileBlast, 30, 5f);
                Projectile.NewProjectile(bossCenter.X, bossCenter.Y - 45, vel.X - 45, vel.Y, ProjectileID.InfernoHostileBlast, 30, 5f);
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 0.15f;
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int Frame = (int)npc.frameCounter;
            npc.frame.Y = Frame * frameHeight;
        }
    }
}
