using Eternal.Common.Systems;
using Eternal.Content.Buffs;
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
    public class UnstableOminiteTippedNaquadahSword : ModNPC
    {
        int teleportTimer;

        public override void SetDefaults()
        {
            NPC.lifeMax = 96000;
            NPC.damage = 100;
            NPC.defense = 30;
            NPC.knockBackResist = 0f;
            NPC.width = 32;
            NPC.height = 32;
            NPC.aiStyle = 23;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCHit/RiftEnemyHitNaquadah")
            {
                Volume = 0.8f,
                PitchVariance = Main.rand.NextFloat(0.2f, 0.9f),
                MaxInstances = 0,
            };
            NPC.DeathSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCDeath/RiftEnemyDeath");
            NPC.value = Item.sellPrice(gold: 26, silver: 15);
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.ShadowFlame] = true;
            NPC.buffImmune[BuffID.CursedInferno] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.Frozen] = true;
            NPC.buffImmune[BuffID.Chilled] = true;
            SpawnModBiomes = new int[1] { ModContent.GetInstance<Biomes.Rift>().Type };
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(ModContent.BuffType<RiftWithering>(), 1 * 60 * 60, false);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

                new FlavorTextBestiaryInfoElement("A sword tipped with Crystalized Ominite. Forged in Naquadah, these swords are quite durable.")
            });
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (EventSystem.isRiftOpen && DownedBossSystem.downedRiftArkofImperious)
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
            Vector2 targetPosition = Main.player[NPC.target].position;

            teleportTimer++;

            if (teleportTimer > 200)
            {
                SoundEngine.PlaySound(SoundID.Item8, NPC.position);
                NPC.position.X = targetPosition.X + Main.rand.Next(-400, 400);
                NPC.position.Y = targetPosition.Y + Main.rand.Next(-400, 400);
                for (int k = 0; k < 10; k++)
                {
                    Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, DustID.DemonTorch, NPC.oldVelocity.X * 0.5f, NPC.oldVelocity.Y * 0.5f);
                }
                teleportTimer = 0;
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RawNaquadah>(), 1, 3, 12));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CrystalizedOminite>(), 2, 1, 2));
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.Wraith, 0, -1f, 0, default(Color), 1f);
            }
            else
            {
                for (int k = 0; k < 2; k++)
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.PinkTorch, 2.5f, -2.5f, 0, default, 1.7f);
            }
        }
    }
}
