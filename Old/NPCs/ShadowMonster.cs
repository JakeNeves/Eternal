using Eternal.Items.Summon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.NPCs
{
    public class ShadowMonster : ModNPC
    {

        private Player player;
        private float speed;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("???");
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.width = 78;
            npc.height = 60;
            npc.lifeMax = 2;
            npc.dontTakeDamage = true;
            npc.aiStyle = -1;
            npc.damage = 999;
            music = MusicID.Boss2;
            npc.noTileCollide = true;
            npc.noGravity = true;
            npc.lavaImmune = true;
        }

        public override void AI()
        {

            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, 54, npc.oldVelocity.X * 0.5f, npc.oldVelocity.Y * 0.5f);
            }

            npc.rotation = npc.velocity.X * 0.03f;

            Target();
            DespawnHandler();
            Move(new Vector2(0, 0));

            player.AddBuff(BuffID.Horrified, 1, false);
            player.AddBuff(BuffID.Blackout, 1, false);

            if (player.ZoneOverworldHeight)
            {
                if (npc.life == 1)
                {
                    Main.PlaySound(SoundID.DD2_EtherianPortalSpawnEnemy, Main.myPlayer);
                    Main.PlaySound(SoundID.Roar, (int)npc.position.X, (int)npc.position.Y, 2);
                    Main.NewText("The mysterious shadow monster fades!", 175, 75, 255);
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, ModContent.ItemType<RuneofThunder>());
                    npc.life = 0;
                }
            }
            else if (player.ZoneDirtLayerHeight)
            {
                npc.life = 1;
            }
        }

        private void DespawnHandler()
        {
            if (!player.active || player.dead)
            {
                npc.TargetClosest(false);
                Main.PlaySound(SoundID.DD2_EtherianPortalOpen, Main.myPlayer);
                npc.life = 0;
            }
        }

        private void Move(Vector2 offset)
        {
            if (npc.life == 1)
            {
                speed = 4.5f;
            }
            else
            {
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

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 0.10f;
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int Frame = (int)npc.frameCounter;
            npc.frame.Y = Frame * frameHeight;
        }

        private void Target()
        {
            npc.TargetClosest(true);
            player = Main.player[npc.target];
        }

        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
            => GlowMaskUtils.DrawNPCGlowMask(spriteBatch, npc, mod.GetTexture("NPCs/ShadowMonster_Glow"));

    }
}
