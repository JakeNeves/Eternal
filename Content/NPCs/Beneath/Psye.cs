using Eternal.Common.Systems;
using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Enemy;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.Beneath
{
    public class Psye : ModNPC
    {
        int attackTimer;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 3;
        }

        public override void SetDefaults()
        {
            NPC.width = 26;
            NPC.height = 28;
            NPC.lifeMax = 100;
            NPC.defense = 15;
            NPC.damage = 12;
            NPC.knockBackResist = -1f;
            NPC.aiStyle = 14;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.Frozen] = true;
            NPC.buffImmune[BuffID.Chilled] = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            SpawnModBiomes = [ ModContent.GetInstance<Biomes.Beneath>().Type ];
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([
                new FlavorTextBestiaryInfoElement("These tiny psychic creatures can be quite tedious to kill on most circumstances, especially if they were to swarm you in a massive group!")
            ]);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }

            var entitySource = NPC.GetSource_Death();

            if (NPC.life <= 0)
            {
                int gore1 = Mod.Find<ModGore>("PsyeTenticle").Type;
                int gore2 = Mod.Find<ModGore>("PsyeEyeball").Type;

                for (int i = 0; i < 2; i++)
                    Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-2, 2), Main.rand.Next(-2, 2)), gore1);

                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-2, 2), Main.rand.Next(-2, 2)), gore2);
            }
            else
            {
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Wraith);
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = Main.player[Main.myPlayer];

            if (ModContent.GetInstance<ZoneSystem>().zoneBeneath)
            {
                return SpawnCondition.Cavern.Chance * 0.5f;
            }
            else
            {
                return SpawnCondition.Cavern.Chance * 0f;
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PsyblightEssence>(), 1, 1, 2));
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target];

            var entitySource = NPC.GetSource_FromAI();

            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            NPC.spriteDirection = NPC.direction;
            NPC.rotation = NPC.velocity.X * 0.02f;

            attackTimer++;
            if (attackTimer == 150 || attackTimer == 300 || attackTimer == 450)
            {
                SoundEngine.PlaySound(new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/Psy")
                {
                    Volume = 0.8f,
                    PitchVariance = Main.rand.NextFloat(0.2f, 0.9f),
                    MaxInstances = 0,
                }, NPC.Center);

                float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<Psyfireball>(), NPC.damage, 1, Main.myPlayer, 0, 0);
            }
            else if (attackTimer == 250)
            {
                attackTimer = 0;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.15f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int Frame = (int)NPC.frameCounter;
            NPC.frame.Y = Frame * frameHeight;
        }
    }
}
