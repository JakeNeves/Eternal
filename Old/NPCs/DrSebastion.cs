using Eternal.Items.Summon;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.NPCs
{
    public class DrSebastion : ModNPC
    {
        int Timer;

        bool dialogue = true;
        int introTimer = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dr. Sebastion Kox");
            Main.npcFrameCount[npc.type] = 2;
        }

        public override void SetDefaults()
        {
            npc.width = 58;
            npc.height = 74;
            npc.aiStyle = -1;
            npc.knockBackResist = -1;
            npc.lifeMax = 500;
            npc.noTileCollide = true;
            npc.noGravity = true;
        }

        public override void AI()
        {
            Player player = Main.player[npc.target];
            npc.TargetClosest(true);
            npc.spriteDirection = npc.direction;
            if (npc.ai[0] == 0f && Main.netMode != NetmodeID.MultiplayerClient)
            {
                npc.TargetClosest(true);
                npc.ai[0] = 1f;
            }
            if (npc.ai[1] != 3f && npc.ai[1] != 2f)
            {
                npc.ai[1] = 2f;
            }
            if (Main.player[npc.target].dead || Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 2000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 2000f)
            {
                npc.TargetClosest(true);
                if (Main.player[npc.target].dead || Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 2000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 2000f)
                {
                    npc.ai[1] = 3f;
                }
            }
            if (npc.ai[1] == 2f)
            {
                if (Vector2.Distance(Main.player[npc.target].Center, npc.Center) > 250)
                {
                    npc.velocity += Vector2.Normalize(Main.player[npc.target].Center - npc.Center) * new Vector2(3.75f, 1.75f);
                }

                npc.velocity *= 0.98f;
                npc.velocity.X = Utils.Clamp(npc.velocity.X, -4, 4);
                npc.velocity.Y = Utils.Clamp(npc.velocity.Y, -2, 2);
            }
            else if (npc.ai[1] == 3f)
            {
                npc.velocity.Y = npc.velocity.Y + 0.1f;
                if (npc.velocity.Y < 0f)
                {
                    npc.velocity.Y = npc.velocity.Y * 0.95f;
                }
                npc.velocity.X = npc.velocity.X * 0.95f;
                if (npc.timeLeft > 50)
                {
                    npc.timeLeft = 50;
                }
            }
            if (dialogue)
            {
                introTimer++;
                npc.dontTakeDamage = true;
                switch (introTimer)
                {
                    case 120:
                        Main.NewText(player.name + "...", 60, 80, 204);
                        break;
                    case 240:
                        Main.NewText("It appears you've progressed so far...", 60, 80, 204);
                        break;
                    case 360:
                        Main.NewText("You have arrived at a moment where you, " + player.name + "...", 60, 80, 204);
                        break;
                    case 480:
                        Main.NewText("Sha'll face the mightiest of mine...", 60, 80, 204);
                        break;
                    case 600:
                        Main.NewText("I have yet to unleash them...", 60, 80, 204);
                        break;
                    case 720:
                        Main.NewText("You sha'll fare against them, my creations.", 60, 80, 204);
                        break;
                    case 840:
                        Main.NewText("Choose wisley, you may choose another afterwards!", 60, 80, 204);
                        break;
                    case 960:
                        Main.NewText("Eash shape of the mechanical cores, can be used at the exo beacon to contact each one of my most powerful creations", 60, 80, 204);
                        break;
                    case 1080:
                        Main.NewText("Good Luck!", 60, 80, 204);
                        break;
                }
                if (introTimer >= 1200)
                {
                    for (int k = 0; k < 5; k++)
                    {
                        Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, DustID.Electric, npc.oldVelocity.X * 0.75f, npc.oldVelocity.Y * 0.75f);
                    }
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, ModContent.ItemType<AtlasCore>());
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, ModContent.ItemType<BorealisCore>());
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, ModContent.ItemType<OrionCore>());
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, ModContent.ItemType<OmnicronCore>());
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, ModContent.ItemType<PolarusCore>());
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, ModContent.ItemType<TripletsCore>());
                    dialogue = false;
                    introTimer = 0;
                    npc.TargetClosest(false);
                    npc.active = false;
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 0.5f;
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int Frame = (int)npc.frameCounter;
            npc.frame.Y = Frame * frameHeight;
        }
    }
}
