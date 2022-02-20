using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Eternal.Items.Potions;
using Eternal.Projectiles.Boss;

namespace Eternal.NPCs.Boss.BionicBosses.Omnicron
{
    //[AutoloadBossHead]
    public class Omnicron : ModNPC
    {
        private Player player;

        float vectX = 0f;
        float vectY = 0f;

        bool isSpinning = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("XM-2024 Omicron-X8");
            Main.npcFrameCount[npc.type] = 3;
        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 620000;
            npc.damage = 60;
            npc.defense = 30;
            npc.knockBackResist = -1f;
            npc.width = 146;
            npc.height = 166;
            npc.value = Item.buyPrice(platinum: 3, gold: 20);
            npc.lavaImmune = true;
            npc.boss = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/ExoMenace");
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Venom] = true;
            npc.buffImmune[BuffID.ShadowFlame] = true;
            npc.buffImmune[BuffID.CursedInferno] = true;
            npc.buffImmune[BuffID.Frostburn] = true;
            npc.buffImmune[BuffID.Frozen] = true;
            npc.buffImmune[BuffID.Chilled] = true;
            //bossBag = ItemType<CarmaniteScouterBag>();
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 1240000;
            npc.damage = 120;
            npc.defense = 60;

            if (EternalWorld.hellMode)
            {
                npc.lifeMax = 2530000;
                npc.damage = 240;
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                //Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/AtlasHead"), 1f);
                //Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/AtlasJaw"), 1f);
                //Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/AtlasBody"), 1f);
            }
            else
            {

                for (int k = 0; k < damage / npc.lifeMax * 20.0; k++)
                {
                    Dust.NewDust(npc.Center, npc.width, npc.height, DustID.Electric, hitDirection, -2f, 0, default(Color), 1f);
                    Dust.NewDust(npc.Center, npc.width, npc.height, DustID.Fire, hitDirection, -1f, 0, default(Color), 1f);
                }
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ModContent.ItemType<PristineHealingPotion>();
        }

        public override bool PreAI()
        {
            Lighting.AddLight(npc.Center, 1.90f, 0.22f, 0.22f);

            player = Main.player[npc.target];
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
                    return true;
                }
            }

            return true;
        }

        public override void AI()
        {
            if (isSpinning)
                npc.rotation += npc.velocity.X * 0.1f;
            else
                npc.rotation = npc.velocity.X * 0.06f;

            player = Main.player[npc.target];
            npc.TargetClosest(true);
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                int num728 = 6000;
                if (Math.Abs(npc.Center.X - Main.player[npc.target].Center.X) + Math.Abs(npc.Center.Y - Main.player[npc.target].Center.Y) > (float)num728)
                {
                    npc.active = false;
                    npc.life = 0;
                    if (Main.netMode == NetmodeID.Server)
                    {
                        NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npc.whoAmI, 0f, 0f, 0f, 0, 0, 0);
                    }
                }
            }
            if (npc.localAI[0] == 0f && Main.netMode != NetmodeID.MultiplayerClient)
            {
                npc.localAI[0] = 1f;
                int omniHook = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<OmnicronHook>(), npc.whoAmI, 0f, 0f, 0f, 0f, 255);
                omniHook = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<OmnicronHook>(), npc.whoAmI, 0f, 0f, 0f, 0f, 255);
                omniHook = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<OmnicronHook>(), npc.whoAmI, 0f, 0f, 0f, 0f, 255);
            }

            #region Plantera-Like Movement
            int[] array2 = new int[3];
            float num730 = 0f;
            float num731 = 0f;
            int num732 = 0;
            for (int i = 0; i < Main.npc.Length; ++i)
            {
                if (Main.npc[i].active && Main.npc[i].type == ModContent.NPCType<OmnicronHook>())
                {
                    num730 += Main.npc[i].Center.X;
                    num731 += Main.npc[i].Center.Y;
                    array2[num732] = i;
                    num732++;
                    if (num732 > 2)
                    {
                        break;
                    }
                }
            }
            num730 /= (float)num732;
            num731 /= (float)num732;
            float num734 = 2.5f;
            float speed = 0.10f;
            float speedMultiplier = 4f;
            if (npc.life < npc.lifeMax / 2)
            {
                num734 = 5f;
                speed = 1.05f;
            }
            if (Main.expertMode)
            {
                num734 += 1f;
                num734 *= 1.1f;
            }
            if (EternalWorld.hellMode)
            {
                num734 += 1.1f;
                num734 *= 1.2f;
            }
            Vector2 vector91 = new Vector2(num730, num731);
            float targetX = Main.player[npc.target].Center.X - vector91.X;
            float targetY = Main.player[npc.target].Center.Y - vector91.Y;
            if (!player.active)
            {
                targetY *= -1f;
                targetX *= -1f;
                num734 += 8f;
            }
            float num738 = (float)Math.Sqrt((double)(targetX * targetX + targetY * targetY));
            int num739 = 250;
            if (Main.expertMode)
            {
                num739 += 150;
            }
            if (EternalWorld.hellMode)
            {
                num739 += 200;
            }
            if (num738 >= (float)num739)
            {
                num738 = (float)num739 / num738;
                targetX *= num738;
                targetY *= num738;
            }
            num730 += targetX;
            num731 += targetY;
            vector91 = new Vector2(npc.Center.X, npc.Center.Y);
            targetX = num730 - vector91.X;
            targetY = num731 - vector91.Y;
            num738 = (float)Math.Sqrt((double)(targetX * targetX + targetY * targetY));
            if (isSpinning)
            {
                speedMultiplier += 2f;
            }
            if (num738 < num734)
            {
                targetX = npc.velocity.X;
                targetY = npc.velocity.Y;
            }
            else
            {
                num738 = num734 / num738;
                targetX *= num738 * speedMultiplier;
                targetY *= num738 * speedMultiplier;
            }
            if (npc.velocity.X < targetX)
            {
                npc.velocity.X = npc.velocity.X + speed;
                if (npc.velocity.X < 0f && targetX > 0f)
                {
                    npc.velocity.X = npc.velocity.X + speed * 2f;
                }
            }
            else if (npc.velocity.X > targetX)
            {
                npc.velocity.X = npc.velocity.X - speed;
                if (npc.velocity.X > 0f && targetX < 0f)
                {
                    npc.velocity.X = npc.velocity.X - speed * 2f;
                }
            }
            if (npc.velocity.Y < targetY)
            {
                npc.velocity.Y = npc.velocity.Y + speed;
                if (npc.velocity.Y < 0f && targetY > 0f)
                {
                    npc.velocity.Y = npc.velocity.Y + speed * 2f;
                }
            }
            else if (npc.velocity.Y > targetY)
            {
                npc.velocity.Y = npc.velocity.Y - speed;
                if (npc.velocity.Y > 0f && targetY < 0f)
                {
                    npc.velocity.Y = npc.velocity.Y - speed * 2f;
                }
            }
            Vector2 vector92 = new Vector2(npc.Center.X, npc.Center.Y);
            float num740 = Main.player[npc.target].Center.X - vector92.X;
            float num741 = Main.player[npc.target].Center.Y - vector92.Y;
            npc.rotation = (float)Math.Atan2((double)num741, (double)num740) + 1.57f;
            #endregion

        }

        public override void FindFrame(int frameHeight)
        {
            //npc.frame.Y = frameNum * frameHeight;

            npc.frameCounter += 0.15f;
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int Frame = (int)npc.frameCounter;
            npc.frame.Y = Frame * frameHeight;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

    }
}
