using Eternal.Content.Items.Materials;
using Eternal.Content.Items.Misc;
using Eternal.Content.Items.Weapons.Throwing;
using Eternal.Content.Projectiles.Boss;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.Surface
{
    public class Level2CarminiteInfestedEye : ModNPC
    {
        int attackTimer = 0;
        int frameType = 0;

        private Player player;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 5;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 100;
            NPC.damage = 25;
            NPC.defense = 30;
            NPC.knockBackResist = 0f;
            NPC.width = 36;
            NPC.height = 22;
            NPC.aiStyle = 2;
            AIType = NPCID.DemonEye;
            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = Item.sellPrice(silver: 15);
            NPC.rarity = 4;
        }

        public override void OnKill()
        {
            var entitySource = NPC.GetSource_Death();

            int gore1 = Mod.Find<ModGore>("Level2CarminiteInfestedEyeFront1").Type;
            int gore2 = Mod.Find<ModGore>("Level2CarminiteInfestedEyeFront2").Type;
            int gore3 = Mod.Find<ModGore>("Level2CarminiteInfestedEyeBack").Type;

            Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-2, 2), Main.rand.Next(-2, 2)), gore1);
            Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-2, 2), Main.rand.Next(-2, 2)), gore2);
            Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-2, 2), Main.rand.Next(-2, 2)), gore3);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,

                new FlavorTextBestiaryInfoElement("A much more infected variant of the Carminite-Infested Eye, they've adopted some unusual traits...")
            ]);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode)
                return SpawnCondition.OverworldNightMonster.Chance * 0.05f;
            else
                return SpawnCondition.OverworldNightMonster.Chance * 0f;
        }

        public override void AI()
        {
            Player target = Main.player[NPC.target];
            NPC.TargetClosest(true);
            NPC.spriteDirection = NPC.direction;

            NPC.rotation = NPC.velocity.ToRotation() + MathHelper.ToRadians(90f);

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

            if (NPC.life < NPC.lifeMax / 2)
                frameType = 1;
            else
                frameType = 0;

            if (attackTimer == 200)
            {
                if (!Main.dedServ)
                    SoundEngine.PlaySound(SoundID.Item8, NPC.position);

                float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<CarminiteSludge>(), NPC.damage, 1, Main.myPlayer, 0, 0);
            }
            if (attackTimer == 210)
            {
                attackTimer = 0;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            int startFrame = 0;
            int finalFrame = 1;

            int frameSpeed = 2;

            if (frameType == 1)
            {
                startFrame = 3;
                finalFrame = Main.npcFrameCount[NPC.type] - 1;

                if (NPC.frame.Y < startFrame * frameHeight)
                {
                    NPC.frame.Y = startFrame * frameHeight;
                }
            }

            NPC.frameCounter += 0.5f;
            NPC.frameCounter += NPC.velocity.Length() / 10f;

            if (NPC.frameCounter > frameSpeed)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;

                if (NPC.frame.Y > finalFrame * frameHeight)
                {
                    NPC.frame.Y = startFrame * frameHeight;
                }
            }
        }

        private void RotateNPCToTarget()
        {
            if (player == null) return;
            Vector2 direction = NPC.Center - player.Center;
            float rotation = (float)Math.Atan2(direction.Y, direction.X);
            NPC.rotation = rotation + ((float)Math.PI * 0.5f);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
                return;

            Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.Blood, 0, -1f, 0, default(Color), 1f);
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CarminacVirus>(), 24));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Carminite>(), minimumDropped: 1, maximumDropped: 3));
            npcLoot.Add(ItemDropRule.Common(ItemID.Lens, minimumDropped: 0, maximumDropped: 1));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Poutine>(), 12));
        }
    }
}
