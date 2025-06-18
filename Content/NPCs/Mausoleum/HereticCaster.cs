using Eternal.Common.Systems;
using Eternal.Content.Items.Materials;
using Eternal.Content.Items.Summon;
using Eternal.Content.Items.Weapons.Throwing;
using Eternal.Content.Projectiles.Enemy;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.Mausoleum
{
    public class HereticCaster : ModNPC
    {
        int attackTimer = 0;
        int frameNum;

        static int attackDelayMax = 12;
        int attackDelay = attackDelayMax;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 2;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 300;
            NPC.damage = 10;
            NPC.defense = 60;
            NPC.knockBackResist = -1f;
            NPC.width = 28;
            NPC.height = 46;
            NPC.aiStyle = -1;
            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit2;
            NPC.DeathSound = SoundID.NPCDeath2;
            NPC.value = Item.sellPrice(gold: 15);
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.ShadowFlame] = true;
            NPC.buffImmune[BuffID.CursedInferno] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.Frozen] = true;
            NPC.buffImmune[BuffID.Chilled] = true;
            NPC.lavaImmune = true;
            SpawnModBiomes = [ ModContent.GetInstance<Biomes.Mausoleum>().Type ];
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([
                new FlavorTextBestiaryInfoElement("They can be very deadly with their 4-way attacks...")
            ]);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!ModContent.GetInstance<ZoneSystem>().zoneMausoleum && Main.hardMode)
                return SpawnCondition.Cavern.Chance * 0f;
            else
                return SpawnCondition.Cavern.Chance * 0.32f;
        }

        public override void AI()
        {
            Player target = Main.player[NPC.target];
            NPC.TargetClosest(true);
            NPC.spriteDirection = NPC.direction;

            Lighting.AddLight(NPC.position, 1f, 0f, 15f);

            attackTimer++;
            Attack();
        }

        private void Attack()
        {
            var entitySource = NPC.GetSource_FromAI();

            Vector2 targetPosition = Main.player[NPC.target].position;
            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;

            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            float A = (float)Main.rand.Next(-200, 200) * 0.01f;
            float B = (float)Main.rand.Next(-200, 200) * 0.01f;

            if (attackTimer == 100 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                for (int k = 0; k < 5; k++)
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.PurpleTorch, 0f, -2.5f, 0, default, 1.7f);

                if (!Main.dedServ)
                    SoundEngine.PlaySound(SoundID.Item8, NPC.position);

                int targetTileX = (int)Main.player[NPC.target].Center.X / 16;
                int targetTileY = (int)Main.player[NPC.target].Center.Y / 16;
                Vector2 chosenTile = Vector2.Zero;
                if (NPC.AI_AttemptToFindTeleportSpot(ref chosenTile, targetTileX, targetTileY))
                {
                    NPC.ai[2] = chosenTile.X;
                    NPC.ai[3] = chosenTile.Y;
                }
                NPC.netUpdate = true;
            }

            if (attackTimer >= 200 && attackTimer < 400)
            {
                frameNum = 1;

                attackDelay--;

                if (attackDelay < 0)
                {
                    if (!Main.dedServ)
                        SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot, NPC.position);

                    if (!Main.dedServ)
                    {
                        int[] i =
                        {
                            Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 12f, 12f, ModContent.ProjectileType<Psyfireball>(), NPC.damage, 0),
                            Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, -12f, 12f, ModContent.ProjectileType<Psyfireball>(), NPC.damage, 0),
                            Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 12f, -12f, ModContent.ProjectileType<Psyfireball>(), NPC.damage, 0),
                            Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, -12f, -12f, ModContent.ProjectileType<Psyfireball>(), NPC.damage, 0)
                        };

                        for (int j = 0; j < i.Length; j++)
                        {
                            Main.projectile[i[j]].timeLeft = 300;
                        }
                    }

                    attackDelay = attackDelayMax;
                }
            }

            if (attackTimer == 400)
                frameNum = 0;

            if (attackTimer == 500)
            {
                for (int k = 0; k < 5; k++)
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.PurpleTorch, 0f, -2.5f, 0, default, 1.7f);

                if (!Main.dedServ)
                    SoundEngine.PlaySound(SoundID.Item8, NPC.position);

                NPC.position.X = targetPosition.X + Main.rand.Next(-400, 400);
                NPC.position.Y = targetPosition.Y + Main.rand.Next(-400, 400);

                for (int k = 0; k < 5; k++)
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.PurpleTorch, 0f, -2.5f, 0, default, 1.7f);
            }

            if (attackTimer >= 600 && attackTimer < 800)
            {
                frameNum = 1;

                attackDelay--;

                if (attackDelay < 0)
                {
                    if (!Main.dedServ)
                        SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot, NPC.position);

                    if (!Main.dedServ)
                    {
                        int proj = Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<Psyfireball>(), NPC.damage / 4, 1, Main.myPlayer, 0, 0);

                        Main.projectile[proj].timeLeft = 300;
                    }

                    attackDelay = attackDelayMax;
                }
            }

            if (attackTimer > 800)
            {
                frameNum = 0;

                attackTimer = 0;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frame.Y = frameNum * frameHeight;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Animanomicon>(), 4));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PsyblightEssence>(), 4, 2, 6));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MausoleumKeyFragment2>(), 12));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CombatGavel>(), 24));
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
                return;

            if (NPC.life <= 0)
            {
                for (int k = 0; k < 5; k++)
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.PurpleTorch, 2.5f, -2.5f, 0, default, 1.7f);
            }
            else
            {
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.PurpleTorch, 0, -1f, 0, default(Color), 1f);
            }
        }
    }
}
