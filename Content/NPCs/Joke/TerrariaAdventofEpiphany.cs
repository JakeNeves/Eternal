using Eternal.Common.Systems;
using Eternal.Content.Items.Materials;
using Eternal.Content.NPCs.Boss.AoI;
using Eternal.Content.NPCs.Boss.CarminiteAmalgamation;
using Eternal.Content.NPCs.Boss.CosmicApparition;
using Eternal.Content.NPCs.Comet;
using Eternal.Content.NPCs.DarkMoon;
using Eternal.Content.NPCs.Miniboss;
using Eternal.Content.NPCs.Underworld;
using Eternal.Content.Projectiles.Boss;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Joke
{
    public class TerrariaAdventofEpiphany : ModNPC
    {
        static int aiTAOEShotRateMax = 5;
        int aiTAOEShotRate = aiTAOEShotRateMax;

        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }

        private static readonly int[] possibleEnemyTypes = { 
            ModContent.NPCType<CarminiteAmalgamation>(),
            ModContent.NPCType<CosmicApparition>(),
            ModContent.NPCType<ArkofImperious>(),
            ModContent.NPCType<Yog>(),
            ModContent.NPCType<PhantomConstruct>(),
            ModContent.NPCType<UnstablePortal>(),
            ModContent.NPCType<Shademan>(),
            ModContent.NPCType<BloodSlurper>(),
            ModContent.NPCType<CosmicApex>(),
            ModContent.NPCType<AstroidSmasher>()
        };

        private int enemyTypeIndex;

        ref float AttackTimer => ref NPC.ai[1];

        public override void SetDefaults()
        {
            NPC.width = 328;
            NPC.height = 209;
            NPC.lifeMax = 2500;
            NPC.defense = 20;
            NPC.damage = 20;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = 14;
            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
        }

        public override bool PreAI()
        {
            enemyTypeIndex = Main.rand.Next(possibleEnemyTypes.Length);

            return true;
        }

        public override void AI()
        {
            AttackTimer++;

            Vector2 targetPosition = Main.player[NPC.target].position;
            Player player = Main.player[NPC.target];
            NPC.TargetClosest(true);

            var entitySource = NPC.GetSource_FromAI();

            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            NPC.spriteDirection = NPC.direction;

            if (AttackTimer > 0)
            {
                aiTAOEShotRate--;

                if (aiTAOEShotRate <= 0)
                {
                    if (!Main.dedServ)
                    {
                        Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Blood);
                        Projectile.NewProjectile(entitySource, NPC.Center, direction, ModContent.ProjectileType<CarminiteSludge>(), 0, 0f, Main.myPlayer);
                        SoundEngine.PlaySound(SoundID.NPCDeath22, NPC.Center);
                    }

                    aiTAOEShotRate = aiTAOEShotRateMax;
                }
            }

            if (AttackTimer > 100)
            {
                AttackTimer = 0;

                if (!Main.dedServ)
                {
                    int npc = NPC.NewNPC(entitySource, (int)NPC.Center.X, (int)NPC.Center.Y, possibleEnemyTypes[enemyTypeIndex]);

                    Main.npc[npc].boss = false;
                    Main.npc[npc].life = 500;
                    Main.npc[npc].lifeMax = 500;
                    Main.npc[npc].defense = 0;
                }

                 NPC.position.X = targetPosition.X + Main.rand.Next(-500, 500);
                 NPC.position.Y = targetPosition.Y + Main.rand.Next(-500, 500);
            }
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
