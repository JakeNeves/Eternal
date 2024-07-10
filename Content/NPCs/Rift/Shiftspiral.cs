using Eternal.Common.Systems;
using Eternal.Content.Buffs;
using Eternal.Content.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.Rift
{
    public class Shiftspiral : ModNPC
    {
        bool justSpawned = false;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;
        }

        public override void SetDefaults()
        {
            NPC.width = 26;
            NPC.height = 44;
            NPC.lifeMax = 32000;
            NPC.defense = 20;
            NPC.damage = 48;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = 22;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.lavaImmune = true;
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.Frozen] = true;
            NPC.buffImmune[BuffID.Chilled] = true;
            NPC.HitSound = SoundID.NPCHit3;
            NPC.DeathSound = SoundID.NPCDeath52;
            SpawnModBiomes = new int[1] { ModContent.GetInstance<Biomes.Rift>().Type };
            NPC.alpha = 100;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(ModContent.BuffType<RiftWithering>(), 1 * 60 * 60, false);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,

                new FlavorTextBestiaryInfoElement("A ghostly figure corrupted with unstable properties of the shifted Underworld")
            });
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.PinkTorch);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = Main.player[Main.myPlayer];

            if (RiftSystem.isRiftOpen && player.ZoneUnderworldHeight)
            {
                return SpawnCondition.Underworld.Chance * 1.5f;
            }
            else
            {
                return SpawnCondition.Underworld.Chance * 0f;
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MoteofOminite>(), 1, 1, 6));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ShiftblightShard>(), 2, 2, 4));
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target];
            NPC.TargetClosest(true);

            var entitySource = NPC.GetSource_FromAI();

            Lighting.AddLight(NPC.Center, 0.75f, 0f, 0.75f);

            if (!justSpawned)
            {
                int amountOfTenticles = Main.rand.Next(2, 6);
                for (int i = 0; i < amountOfTenticles; ++i)
                {
                    NPC.NewNPC(entitySource, (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<ShiftspiralHand>());
                }

                justSpawned = true;
            }

            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            NPC.spriteDirection = NPC.direction;
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
