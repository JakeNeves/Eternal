using Eternal.Common.Systems;
using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Misc;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.Underworld
{
    public class Yog : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.RainbowSlime];
        }

        public override void SetDefaults()
        {
            NPC.width = 74;
            NPC.height = 48;
            NPC.aiStyle = 1;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = null;
            AIType = NPCID.LavaSlime;
            NPC.damage = 150;
            NPC.defense = 15;
            NPC.lifeMax = 36000;
            AnimationType = NPCID.RainbowSlime;
            NPC.value = Item.sellPrice(platinum: 3, gold: 12);
            NPC.knockBackResist = -1f;
            NPC.lavaImmune = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Suffocation] = true;
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.BetsysCurse] = true;
            NPC.buffImmune[BuffID.Daybreak] = true;
            NPC.buffImmune[BuffID.DryadsWardDebuff] = true;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            var entitySource = NPC.GetSource_Death();

            for (int k = 0; k < 15.0; k++)
            {
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.Torch, 0, 0, 0, default(Color), 1f);
            }
            if (NPC.life < 0)
            {
                Projectile.NewProjectile(entitySource, NPC.position, NPC.velocity, ModContent.ProjectileType<YogExplode>(), NPC.damage, 0);
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,

                new FlavorTextBestiaryInfoElement("Giant milenia-aged slimes that have roamed the underworld since the creation of this world.")
            });
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<YoggieSpotch>(), minimumDropped: 1, maximumDropped: 3));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (DownedBossSystem.downedCosmicApparition)
                return SpawnCondition.Underworld.Chance * 0.25f;
            else
                return SpawnCondition.Underworld.Chance * 0f;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
    }
}
