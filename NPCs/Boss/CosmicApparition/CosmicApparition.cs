using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Terraria.Graphics.Effects;

namespace Eternal.NPCs.Boss.CosmicApparition
{
    public class CosmicApparition : ModNPC
    {
        int teleportTimer;
        int attackTimer;
        int moveTimer;

        const float acceleration = 0.2f;
        const float speed = 14f;

        bool teleport = true;
        bool expert = Main.expertMode;

        public override void SetDefaults()
        {
            npc.width = 64;
            npc.height = 50;
            npc.lifeMax = 256000;
            npc.damage = 100;
            npc.defense = 20;
            npc.knockBackResist = -1f;
            npc.boss = true;
            music = MusicID.Boss2;
            npc.HitSound = SoundID.NPCHit52;
            npc.DeathSound = SoundID.NPCDeath55;
            npc.noTileCollide = true;
            npc.noGravity = true;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Venom] = true;
            npc.buffImmune[BuffID.ShadowFlame] = true;
            npc.buffImmune[BuffID.CursedInferno] = true;
            npc.buffImmune[BuffID.Frostburn] = true;
            npc.buffImmune[BuffID.Frozen] = true;
            npc.buffImmune[BuffID.Chilled] = true;
        }

        public override void AI()
        {
            Vector2 targetPosition = Main.player[npc.target].position;

            #region Flying
            npc.TargetClosest(true);
            npc.spriteDirection = npc.direction;
            Player player = Main.player[npc.target];
            if (player.dead || !player.active)
            {
                npc.TargetClosest(false);
                npc.active = false;
            }
            moveTimer++;
            if (moveTimer >= 0)
            {
                Vector2 StartPosition = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                float DirectionX = Main.player[npc.target].position.X + Main.player[npc.target].width / 2 - StartPosition.X;
                float DirectionY = Main.player[npc.target].position.Y + Main.player[npc.target].height / 2 - StartPosition.Y;
                float Length = (float)Math.Sqrt(DirectionX * DirectionX + DirectionY * DirectionY);
                float Num = speed / Length;
                DirectionX = DirectionX * Num;
                DirectionY = DirectionY * Num;
                if (npc.velocity.X < DirectionX)
                {
                    npc.velocity.X = npc.velocity.X + acceleration;
                    if (npc.velocity.X < 0 && DirectionX > 0)
                        npc.velocity.X = npc.velocity.X + acceleration;
                }
                else if (npc.velocity.X > DirectionX)
                {
                    npc.velocity.X = npc.velocity.X - acceleration;
                    if (npc.velocity.X > 0 && DirectionX < 0)
                        npc.velocity.X = npc.velocity.X - acceleration;
                }
                if (npc.velocity.Y < DirectionY)
                {
                    npc.velocity.Y = npc.velocity.Y + acceleration;
                    if (npc.velocity.Y < 0 && DirectionY > 0)
                        npc.velocity.Y = npc.velocity.Y + acceleration;
                }
                else if (npc.velocity.Y > DirectionY)
                {
                    npc.velocity.Y = npc.velocity.Y - acceleration;
                    if (npc.velocity.Y > 0 && DirectionY < 0)
                        npc.velocity.Y = npc.velocity.Y - acceleration;
                }
                if (Main.rand.Next(36) == 1)
                {
                    Vector2 StartPosition2 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height / 2));
                    float BossRotation = (float)Math.Atan2(StartPosition2.Y - (Main.player[npc.target].position.Y + (Main.player[npc.target].height * 0.5f)), StartPosition2.X - (Main.player[npc.target].position.X + (Main.player[npc.target].width * 0.5f)));
                    npc.velocity.X = (float)(Math.Cos(BossRotation) * 9) * -1;
                    npc.velocity.Y = (float)(Math.Sin(BossRotation) * 9) * -1;
                    npc.netUpdate = true;
                }
            }
            #endregion

            player.AddBuff(BuffID.Horrified, 1, false);

            if (teleport)
            {
                teleportTimer++;
            }
            if (teleportTimer == 250)
            {
                Main.PlaySound(SoundID.Item8, Main.myPlayer);
                npc.position.X = targetPosition.X + Main.rand.Next(-400, 400);
                for (int k = 0; k < 5; k++)
                {
                    Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, DustID.Shadowflame, npc.oldVelocity.X * 0.5f, npc.oldVelocity.Y * 0.5f);
                }
                teleportTimer = 0;

                Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, -12, 0, ProjectileID.DD2DrakinShot, 6, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, 12, 0, ProjectileID.DD2DrakinShot, 6, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, 0, 12, ProjectileID.DD2DrakinShot, 6, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, 0, -12, ProjectileID.DD2DrakinShot, 6, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, -8, -8, ProjectileID.DD2DrakinShot, 6, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 8, -8, ProjectileID.DD2DrakinShot, 6, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, -8, 8, ProjectileID.DD2DrakinShot, 6, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 8, 8, ProjectileID.DD2DrakinShot, 6, 0, Main.myPlayer, 0f, 0f);
            }

            if(!player.active || player.dead)
            {
                teleport = false;
                npc.TargetClosest(false);
                npc.direction = 1;
                npc.velocity.Y = npc.velocity.Y - 0.1f;
                SkyManager.Instance.Deactivate("Eternal:Empraynia");
                if (npc.timeLeft > 5)
                {
                    npc.timeLeft = 5;
                    return;
                }
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int k = 0; k < damage / npc.lifeMax * 50; k++)
                Dust.NewDust(npc.position, npc.width, npc.height, 27, 2.5f * hitDirection, -2.5f, 0, default, 1.7f);
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.SuperHealingPotion;
        }

        public override void NPCLoot()
        {

            Main.PlayTrackedSound(SoundID.DD2_EtherianPortalSpawnEnemy, npc.Center);
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 direction = Main.player[npc.target].Center - npc.Center;
                direction.Normalize();
                int damage = expert ? 15 : 19;
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X * 14f, direction.Y * 14f, ProjectileID.DD2DrakinShot, damage, 1, Main.myPlayer, 0, 0);
            }

            Main.NewText("The sky resumes back to it's tranquil state...", 220, 0, 210);
            SkyManager.Instance.Deactivate("Eternal:Empraynia");
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
    }
}
