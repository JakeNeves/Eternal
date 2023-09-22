using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Boss.Incinerius
{
    public class BasaltPrison : ModNPC
    {
        bool isDead = false;
        bool dontKillyet = false;
        bool justSpawned = false;

        int frameNum;
        int DeathTimer;

        public bool SpawnedHeads
        {
            get => NPC.localAI[0] == 1f;
            set => NPC.localAI[0] = value ? 1f : 0f;
        }

        public static int HeadCount()
        {
            int count = 6;

            return count;
        }

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }

        public override void SetDefaults()
        {
            NPC.width = 142;
            NPC.height = 150;
            NPC.aiStyle = -1;
            NPC.damage = 24;
            NPC.defense = 30;
            NPC.lifeMax = 80000;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.Electrified] = true;
            NPC.lavaImmune = true;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.Tink;
            NPC.DeathSound = SoundID.NPCDeath14;
        }

        public Vector2 bossCenter
        {
            get { return NPC.Center; }
            set { NPC.position = value - new Vector2(NPC.width / 2, NPC.height / 2); }
        }

        private void SpawnHeads()
        {
            if (SpawnedHeads)
            {
                return;
            }

            SpawnedHeads = true;

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                return;
            }

            int count = HeadCount();
            var entitySource = NPC.GetSource_FromAI();

            for (int i = 0; i < count; i++)
            {
                int index = NPC.NewNPC(entitySource, (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<BasaltHead>(), NPC.whoAmI);
                NPC minionNPC = Main.npc[index];

                if (minionNPC.ModNPC is BasaltHead minion)
                {
                    minion.ParentIndex = NPC.whoAmI;
                    minion.PositionIndex = i;
                }

                if (Main.netMode == NetmodeID.Server && index < Main.maxNPCs)
                {
                    NetMessage.SendData(MessageID.SyncNPC, number: index);
                }
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }

            if (!dontKillyet)
            {
                if (NPC.life < 0)
                {
                    NPC.life = 1;
                    isDead = true;
                }
            }
        }

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

            if (!justSpawned)
            {
                frameNum = 0;
                justSpawned = true;
            }

            if (NPC.life < NPC.lifeMax / 1.5f)
            {
                Lighting.AddLight(NPC.position, 2.15f, 0.95f, 0f);
                frameNum = 1;
            }
            if (NPC.life < NPC.lifeMax / 2f)
            {
                Lighting.AddLight(NPC.position, 2.15f, 0.95f, 0f);
                frameNum = 2;
            }
            if (NPC.life < NPC.lifeMax / 2.5f)
            {
                Lighting.AddLight(NPC.position, 2.15f, 0.95f, 0f);
                frameNum = 3;
            }

            return true;
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target];

            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            SpawnHeads();

            if (NPC.AnyNPCs(ModContent.NPCType<BasaltHead>()))
            {
                NPC.dontTakeDamage = true;
            }
            else
            {
                NPC.dontTakeDamage = false;
            }

            if (isDead)
            {
                var entitySource = NPC.GetSource_Death();

                DeathTimer++;
                if (DeathTimer > 5)
                {
                    NPC.velocity = new Vector2(Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f));
                    NPC.dontTakeDamage = true;
                }
                if (DeathTimer >= 200)
                {
                    NPC.dontTakeDamage = false;
                    dontKillyet = true;

                    int gore1 = Mod.Find<ModGore>("BasaltPrisonTop").Type;
                    int gore2 = Mod.Find<ModGore>("BasaltPrisonBottom").Type;

                    Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore1);
                    Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore2);

                    Main.NewText("The Basalt Prison has been destroyed!", 175, 75, 255);
                    NPC.NewNPC(entitySource, (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<Incinerius>());

                    player.ApplyDamageToNPC(NPC, 9999, 0, 0, false);
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frame.Y = frameNum * frameHeight;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
    }
}
