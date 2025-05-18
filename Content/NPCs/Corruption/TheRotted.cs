using Eternal.Common.ItemDropRules.Conditions;
using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Enemy;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.Corruption
{
    public class TheRotted : ModNPC
    {
        ref float AttackTimer => ref NPC.ai[1];

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 2;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 500;
            NPC.damage = 86;
            NPC.defense = 20;
            NPC.knockBackResist = 0f;
            NPC.width = 24;
            NPC.height = 38;
            NPC.aiStyle = 14;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCHit/RottedHit")
            {
                Volume = 0.8f,
                PitchVariance = Main.rand.NextFloat(0.2f, 0.9f),
                MaxInstances = 0,
            };
            NPC.DeathSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCDeath/RottedDeath");
            NPC.value = Item.sellPrice(gold: 5);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption,

                new FlavorTextBestiaryInfoElement("A posthumous monstrosity that has lurked amongst the Corruption, who knows what this thing is capable of...")
            });
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            PostCosmicApparitionDropCondition postCosmicApparitionDrop = new PostCosmicApparitionDropCondition();

            npcLoot.Add(ItemDropRule.ByCondition(postCosmicApparitionDrop, ModContent.ItemType<DeadMeat>(), 4, 4, 8));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!NPC.downedMoonlord)
                return SpawnCondition.Corruption.Chance * 0f;
            else
                return SpawnCondition.Corruption.Chance * 0.15f;
        }

        public override void AI()
        {
            Player target = Main.player[NPC.target];
            NPC.TargetClosest(true);

            NPC.spriteDirection = NPC.direction;

            if (Main.rand.NextBool(2))
            {
                for (int k = 0; k < 5; k++)
                    Dust.NewDust(NPC.position + new Vector2(0f, 8f), NPC.width / 2, 0, DustID.GreenBlood, 1.5f, -1.5f, 0, default, Main.rand.NextFloat(0.75f, 1.25f));
            }

            AttackTimer++;
            Attack();
        }

        private void Attack()
        {
            var entitySource = NPC.GetSource_FromAI();

            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            if (AttackTimer == 200 || AttackTimer == 300 || AttackTimer == 400)
            {
                if (!Main.dedServ)
                {
                    SoundEngine.PlaySound(SoundID.Item167, NPC.Center);
                    SoundEngine.PlaySound(SoundID.NPCDeath22, NPC.Center);
                }

                float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int proj = Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<Sanguinebeam>(), NPC.damage, 1, Main.myPlayer, 0, 0);
                    Main.projectile[proj].timeLeft = 250;
                }
            }

            if (AttackTimer > 410)
            {
                AttackTimer = 0;
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                for (int k = 0; k < 5; k++)
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.GreenBlood, 2.5f, -2.5f, 0, default, 1.7f);
            }
            else
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.GreenBlood, 0, -1f, 0, default(Color), 1f);
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.05f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int Frame = (int)NPC.frameCounter;
            NPC.frame.Y = Frame * frameHeight;
        }
    }
}
