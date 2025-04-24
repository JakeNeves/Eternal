using Eternal.Common.Systems;
using Eternal.Content.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.Rift
{
    public class RiftAnomaly : ModNPC
    {
        int attackTimer = 0;

        public override void SetDefaults()
        {
            NPC.lifeMax = 4400;
            NPC.damage = 120;
            NPC.defense = 20;
            NPC.knockBackResist = 0f;
            NPC.width = 50;
            NPC.height = 50;
            NPC.aiStyle = -1;
            NPC.noGravity = true;
            NPC.lavaImmune = true;
            NPC.HitSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCHit/RiftEnemyHit")
            {
                Volume = 0.8f,
                PitchVariance = Main.rand.NextFloat(0.2f, 0.9f),
                MaxInstances = 0,
            };
            NPC.DeathSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCDeath/RiftEnemyDeath");
            NPC.value = Item.sellPrice(gold: 30);
            SpawnModBiomes = [ModContent.GetInstance<Biomes.Rift>().Type ];
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.ShadowFlame] = true;
            NPC.buffImmune[BuffID.CursedInferno] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.Frozen] = true;
            NPC.buffImmune[BuffID.Chilled] = true;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([
                new FlavorTextBestiaryInfoElement("An ominite construct that fires lasers in four directions")
            ]);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (EventSystem.isRiftOpen)
            {
                return SpawnCondition.Overworld.Chance * 1.5f;
            }
            else
            {
                return SpawnCondition.Overworld.Chance * 0f;
            }
        }

        public override void AI()
        {
            Player target = Main.player[NPC.target];
            NPC.TargetClosest(true);
            NPC.spriteDirection = NPC.direction;

            if (!Main.dedServ)
                Lighting.AddLight(NPC.Center, 0.75f, 0f, 0.75f);

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

            if (attackTimer == 200)
            {
                SoundEngine.PlaySound(SoundID.Item8, NPC.position);
                NPC.position.X = targetPosition.X + Main.rand.Next(-400, 400);
                NPC.position.Y = targetPosition.Y + Main.rand.Next(-400, 400);

                for (int k = 0; k < 5; k++)
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.PinkTorch, 0f, -2.5f, 0, default, 1.7f);
            }
            if (attackTimer == 250)
            {
                Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, -12, 0, ProjectileID.EyeLaser, NPC.damage, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 12, 0, ProjectileID.EyeLaser, NPC.damage, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 0, 12, ProjectileID.EyeLaser, NPC.damage, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 0, -12, ProjectileID.EyeLaser, NPC.damage, 0, Main.myPlayer, 0f, 0f);
            }
            if (attackTimer == 400)
            {
                SoundEngine.PlaySound(SoundID.Item8, NPC.position);
                NPC.position.X = targetPosition.X + Main.rand.Next(-400, 400);
                NPC.position.Y = targetPosition.Y + Main.rand.Next(-400, 400);

                for (int k = 0; k < 5; k++)
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.PinkTorch, 0f, -2.5f, 0, default, 1.7f);
            }
            if (attackTimer == 450)
            {
                Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, -12, -12, ProjectileID.EyeLaser, NPC.damage, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 12, -12, ProjectileID.EyeLaser, NPC.damage, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, -12, 12, ProjectileID.EyeLaser, NPC.damage, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 12, 12, ProjectileID.EyeLaser, NPC.damage, 0, Main.myPlayer, 0f, 0f);
            }
            if (attackTimer == 500)
            {
                attackTimer = 0;
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MoteofOminite>(), 1, 2, 6));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ShiftblightShard>(), 2, 2, 4));
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }

            var entitySource = NPC.GetSource_Death();

            int gore1 = Mod.Find<ModGore>("RiftAnomalyShard").Type;

            if (NPC.life <= 0)
            {
                for (int i = 0; i < 4; i++)
                    Gore.NewGore(entitySource, NPC.Center, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore1);

                for (int k = 0; k < 5; k++)
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.PinkTorch, 2.5f, -2.5f, 0, default, 1.7f);
            }
            else
            {
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.PinkTorch, 0, -1f, 0, default(Color), 1f);
            }
        }
    }
}
