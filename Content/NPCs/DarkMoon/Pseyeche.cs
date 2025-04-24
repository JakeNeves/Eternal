using Eternal.Common.Systems;
using Eternal.Content.Items.Materials;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.DarkMoon
{
    public class Pseyeche : ModNPC
    {
        private Player player;

        int attackTimer;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;
        }

        public override void SetDefaults()
        {
            NPC.width = 26;
            NPC.height = 26;
            NPC.damage = 6;
            NPC.defense = 4;
            NPC.lifeMax = 250;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.Frozen] = true;
            NPC.buffImmune[BuffID.Chilled] = true;
            NPC.value = 15f;
            NPC.aiStyle = 14;
            NPC.scale = Main.rand.NextFloat(1f, 1.5f);
            SpawnModBiomes = [ ModContent.GetInstance<Biomes.DarkMoon>().Type ];
        }

        public override void AI()
        {
            if (!Main.dedServ)
            {
                Lighting.AddLight(NPC.Center, 1.50f, 0.25f, 1.50f);
            }

            player = Main.player[NPC.target];

            var entitySource = NPC.GetSource_FromAI();

            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            attackTimer++;
            if (attackTimer >= 200)
            {
                float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ProjectileID.EyeLaser, NPC.damage, 1, Main.myPlayer, 0, 0);

                attackTimer = 0;
            }

            RotateNPCToTarget();
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int k = 0; k < 5.0; k++)
            {
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, ModContent.DustType<Dusts.OcculticMatter>(), 0, 0, 0, default(Color), 1f);
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([
                new FlavorTextBestiaryInfoElement("A condensed ball of matter, made from pure occultic energy, they can easily phase through solid surfaces and fire twisted dark energy!")
            ]);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (EventSystem.darkMoon)
            {
                return SpawnCondition.OverworldNightMonster.Chance * 0.2f;
            }
            else
            {
                return SpawnCondition.OverworldNightMonster.Chance * 0f;
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<OcculticMatter>(), minimumDropped: 4, maximumDropped: 8));
        }

        private void RotateNPCToTarget()
        {
            if (player == null) return;
            Vector2 direction = NPC.Center - player.Center;
            float rotation = (float)Math.Atan2(direction.Y, direction.X);
            NPC.rotation = rotation + ((float)Math.PI * 0.5f);
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
