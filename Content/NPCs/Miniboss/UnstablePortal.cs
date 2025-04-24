using Eternal.Common.Systems;
using Eternal.Content.NPCs.Boss.CosmicApparition;
using Eternal.Content.NPCs.Rift;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.Miniboss
{
    public class UnstablePortal : ModNPC
    {
        int attackTimer = 0;

        private static readonly int[] possibleEnemyTypes = { 
            ModContent.NPCType<UnstableHellwisp>(),
            ModContent.NPCType<Shiftspiral>()
        };

        private int enemyTypeIndex;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 3;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);

            NPCID.Sets.ImmuneToAllBuffs[Type] = true;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 80000;
            NPC.damage = 120;
            NPC.defense = 45;
            NPC.knockBackResist = 0f;
            NPC.width = 54;
            NPC.height = 54;
            NPC.aiStyle = -1;
            NPC.noGravity = true;
            NPC.lavaImmune = true;
            NPC.HitSound = SoundID.NPCHit3;
            NPC.DeathSound = null;
            NPC.noTileCollide = true;
            SpawnModBiomes = [ ModContent.GetInstance<Biomes.Rift>().Type ];
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.ShadowFlame] = true;
            NPC.buffImmune[BuffID.CursedInferno] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.Frozen] = true;
            NPC.buffImmune[BuffID.Chilled] = true;
            NPC.alpha = 0;
        }

        public override bool CheckDead()
        {
            if (NPC.ai[3] == 0f)
            {
                NPC.ai[3] = 1f;
                NPC.damage = 0;
                NPC.life = NPC.lifeMax;
                NPC.dontTakeDamage = true;
                NPC.netUpdate = true;
                return false;
            }
            return true;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (EventSystem.isRiftOpen && !Main.zenithWorld && !NPC.AnyNPCs(ModContent.NPCType<PhantomConstruct>()))
            {
                return SpawnCondition.Sky.Chance * 0.4f;
            }
            else
            {
                return SpawnCondition.Sky.Chance * 0f;
            }
        }

        public override bool PreAI()
        {
            enemyTypeIndex = Main.rand.Next(possibleEnemyTypes.Length);

            return true;
        }

        public override void AI()
        {
            var entitySource = NPC.GetSource_FromAI();

            Player target = Main.player[NPC.target];
            NPC.TargetClosest(true);

            if (!Main.dedServ)
                Lighting.AddLight(NPC.Center, 0.75f, 0f, 0.75f);

            NPC.rotation += 0.05f;

            if (NPC.ai[0] == 0f && Main.netMode != NetmodeID.MultiplayerClient)
            {
                for (int i = 0; i < 15; i++)
                {
                    Vector2 position = NPC.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 15 * i)) * 30;
                    Dust dust = Dust.NewDustPerfect(NPC.Center, DustID.PinkTorch);
                    dust.noGravity = true;
                    dust.velocity = Vector2.Normalize(position - NPC.Center) * 4;
                    dust.noLight = false;
                    dust.fadeIn = 1.25f;
                }

                NPC.TargetClosest(true);
                NPC.ai[0] = 1f;
            }

            attackTimer++;
            Attack();

            if (NPC.ai[3] > 0f)
            {
                entitySource = NPC.GetSource_Death();

                NPC.ai[3] += 1f;
                NPC.dontTakeDamage = true;
                NPC.alpha += 1;

                if (NPC.ai[3] >= 360f)
                {
                    NPC.life = 0;

                    if (!Main.dedServ)
                        SoundEngine.PlaySound(new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/PhantomConstructSpawn"), NPC.position);

                    Main.NewText("A Phantom Construct has appeared!", 175, 75, 255);
                    NPC.NewNPC(entitySource, (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<PhantomConstruct>());
                    NPC.HitEffect(0, 0);
                    NPC.checkDead();
                }

                return;
            }
        }

        private void Attack()
        {
            var entitySource = NPC.GetSource_FromAI();

            Vector2 targetPosition = Main.player[NPC.target].position;
            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;

            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            if (NPC.ai[3] < 1f)
            {
                if (attackTimer == 200)
                {
                    for (int i = 0; i < 1 + Main.rand.Next(3); i++)
                    {
                        NPC.NewNPC(entitySource, (int)NPC.Center.X + Main.rand.Next(-15, 15), (int)NPC.Center.Y + Main.rand.Next(-15, 15), possibleEnemyTypes[enemyTypeIndex]);
                    }
                }
                if (attackTimer == 250)
                {
                    for (int i = 0; i < 1 + Main.rand.Next(3); i++)
                    {
                        NPC.NewNPC(entitySource, (int)NPC.Center.X + Main.rand.Next(-15, 15), (int)NPC.Center.Y + Main.rand.Next(-15, 15), ModContent.NPCType<DetonatingWisp>());
                    }
                }
                if (attackTimer == 400)
                {
                    if (!Main.dedServ)
                        SoundEngine.PlaySound(SoundID.Item8, NPC.position);

                    NPC.position.X = targetPosition.X + Main.rand.Next(-600, 600);
                    NPC.position.Y = targetPosition.Y + Main.rand.Next(-600, 600);
                    attackTimer = 0;
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.15f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int Frame = (int)NPC.frameCounter;
            NPC.frame.Y = Frame * frameHeight;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                for (int k = 0; k < 5; k++)
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.PinkTorch, 2.5f, -2.5f, 0, default, 1.7f);
            }
            else
            {
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.PinkTorch, 0, -1f, 0, default(Color), 1f);
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((byte)enemyTypeIndex);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            enemyTypeIndex = reader.ReadByte();
        }
    }
}
