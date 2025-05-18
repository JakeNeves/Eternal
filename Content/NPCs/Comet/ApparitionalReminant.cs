using Eternal.Common.ItemDropRules.Conditions;
using Eternal.Common.Systems;
using Eternal.Content.Buffs;
using Eternal.Content.Dusts;
using Eternal.Content.Items.Armor;
using Eternal.Content.Items.Materials;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.Comet
{
    public class ApparitionalReminant : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 8;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 2200;
            NPC.damage = 30;
            NPC.defense = 20;
            NPC.knockBackResist = 0f;
            NPC.width = 28;
            NPC.height = 46;
            NPC.aiStyle = -1;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCHit/ApparitionalReminantHit")
            {
                Volume = 0.8f,
                PitchVariance = Main.rand.NextFloat(0.2f, 0.9f),
                MaxInstances = 0,
            };
            //NPC.HitSound = SoundID.NPCHit52;
            NPC.DeathSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCDeath/ApparitionalReminantDeath");
            //NPC.DeathSound = SoundID.NPCDeath55;
            NPC.value = Item.sellPrice(gold: 26, silver: 15);
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.ShadowFlame] = true;
            NPC.buffImmune[BuffID.CursedInferno] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.Frozen] = true;
            NPC.buffImmune[BuffID.Chilled] = true;
            SpawnModBiomes = [ ModContent.GetInstance<Biomes.Comet>().Type ];
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([
                new FlavorTextBestiaryInfoElement("These were once innocent people that have fallen victim to a fallen comet, now they wander this land with the exception of not being able to be seen unless they are nerby the comet.")
            ]);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (DownedBossSystem.downedCosmicApparition && ModContent.GetInstance<ZoneSystem>().zoneComet)
            {
                return SpawnCondition.Overworld.Chance * 1.5f;
            }
            else
            {
                return SpawnCondition.Overworld.Chance * 0f;
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(ModContent.BuffType<ApparitionalWither>(), 1 * 60 * 60, false);
        }

        public override void AI()
        {
            if (!Main.dedServ)
                Lighting.AddLight(NPC.position, 0.75f, 0f, 0.75f);

            NPC.rotation = NPC.velocity.X * 0.03f;
            NPC.TargetClosest(true);
            Player player = Main.player[NPC.target];
            NPC.spriteDirection = NPC.direction = NPC.Center.X < player.Center.X ? -1 : 1;
            if (player.dead || !player.active)
            {
                NPC.TargetClosest(false);
                NPC.active = false;
            }

            if (EventSystem.isRiftOpen)
            {
                if (Main.rand.NextBool(2))
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.PurpleTorch, 0, -2f, 0, default, 1f);
            }

            float speed = 12.5f;
            float acceleration = 0.10f;
            Vector2 vector2 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
            float xDir = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector2.X;
            float yDir = (float)(Main.player[NPC.target].position.Y + (Main.player[NPC.target].height / 2) - 120) - vector2.Y;
            float length = (float)Math.Sqrt(xDir * xDir + yDir * yDir);
            if (length > 400 && Main.expertMode)
            {
                ++speed;
                acceleration += 0.05F;
                if (length > 600)
                {
                    ++speed;
                    acceleration += 0.05F;
                    if (length > 800)
                    {
                        ++speed;
                        acceleration += 0.05F;
                    }
                }
            }
            float num10 = speed / length;
            xDir = xDir * num10;
            yDir = yDir * num10;
            if (NPC.velocity.X < xDir)
            {
                NPC.velocity.X = NPC.velocity.X + acceleration;
                if (NPC.velocity.X < 0 && xDir > 0)
                    NPC.velocity.X = NPC.velocity.X + acceleration;
            }
            else if (NPC.velocity.X > xDir)
            {
                NPC.velocity.X = NPC.velocity.X - acceleration;
                if (NPC.velocity.X > 0 && xDir < 0)
                    NPC.velocity.X = NPC.velocity.X - acceleration;
            }
            if (NPC.velocity.Y < yDir)
            {
                NPC.velocity.Y = NPC.velocity.Y + acceleration;
                if (NPC.velocity.Y < 0 && yDir > 0)
                    NPC.velocity.Y = NPC.velocity.Y + acceleration;
            }
            else if (NPC.velocity.Y > yDir)
            {
                NPC.velocity.Y = NPC.velocity.Y - acceleration;
                if (NPC.velocity.Y > 0 && yDir < 0)
                    NPC.velocity.Y = NPC.velocity.Y - acceleration;
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            PostCosmicApparitionDropCondition postCosmicApparitionDrop = new PostCosmicApparitionDropCondition();

            npcLoot.Add(ItemDropRule.ByCondition(postCosmicApparitionDrop, ModContent.ItemType<ApparitionalMatter>(), 3, 6, 12));
            npcLoot.Add(ItemDropRule.ByCondition(postCosmicApparitionDrop, ModContent.ItemType<Astragel>(), 3, 6, 12));
            npcLoot.Add(ItemDropRule.ByCondition(postCosmicApparitionDrop, ModContent.ItemType<InterstellarScrapMetal>(), 3, 6, 12));
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }

            var entitySource = NPC.GetSource_Death();

            int gore1 = Mod.Find<ModGore>("CosmicApparitionHead").Type;
            int gore2 = Mod.Find<ModGore>("CosmicApparitionBody").Type;
            int gore3 = Mod.Find<ModGore>("CosmicApparitionArm").Type;

            if (NPC.life <= 0)
            {
                Gore.NewGore(entitySource, NPC.Center, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore1);
                Gore.NewGore(entitySource, NPC.Center, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore2);
                for (int i = 0; i < 2; i++)
                    Gore.NewGore(entitySource, NPC.Center, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore3);

                Dust.NewDust(NPC.Center, NPC.width, NPC.height, ModContent.DustType<CosmicSpirit>(), 0, -1f, 0, default(Color), 1f);
            }
            else
            {
                for (int k = 0; k < 25; k++)
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.PurpleTorch, 2.5f, -2.5f, 0, default, 1.7f);
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
