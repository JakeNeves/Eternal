using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.NPCs.Boss.Empraynia
{
    public class EmprayniaRoller : ModNPC
    {

        bool expert;

        private Player player;
        private float speed;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Empraynia's Rotoblade");
        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 2400;
            npc.damage = 38;
            npc.defense = 72;
            npc.knockBackResist = 0f;
            npc.width = 48;
            npc.height = 48;
            npc.value = Item.buyPrice(copper: 30);
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/GrimstoneHit"); //SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath3;
        }

        public override bool PreAI()
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, DustID.Shadowflame, npc.oldVelocity.X * 0.5f, npc.oldVelocity.Y * 0.5f);
            }

            return true;
        }

        public override void AI()
        {
            Lighting.AddLight(npc.position, 0.8f, 0.4f, 0.8f);
            Target();
            Move(new Vector2(0, 0));
            npc.rotation += npc.velocity.X * 0.1f;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int k = 0; k < damage / npc.lifeMax * 50; k++)
                Dust.NewDust(npc.position, npc.width, npc.height, 27, 2.5f * hitDirection, -2.5f, 0, default, 1.7f);
        }

        private void Target()
        {
            npc.TargetClosest(true);
            player = Main.player[npc.target];
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
        }

        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }

        private void Move(Vector2 offset)
        {
            speed = 4f;
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

    }
}
