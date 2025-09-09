using Eternal.Common.Systems;
using Eternal.Content.Items.Accessories;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Eternal.Common.Misc;
using Microsoft.Xna.Framework.Graphics;
using Eternal.Content.Items.Materials;
using Eternal.Common.ItemDropRules.Conditions;

namespace Eternal.Content.NPCs.Mausoleum
{
    public class CursedSludgeLarge : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.BlueSlime];
        }

        ref float TeleportTimer => ref NPC.ai[1];

        public override void SetDefaults()
        {
            NPC.width = 56;
            NPC.height = 36;
            NPC.aiStyle = 1;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            AIType = NPCID.BlueSlime;
            NPC.damage = 60;
            NPC.defense = 10;
            NPC.lifeMax = 500;
            AnimationType = NPCID.BlueSlime;
            NPC.knockBackResist = 0f;
            NPC.lavaImmune = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Suffocation] = true;
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.BetsysCurse] = true;
            NPC.buffImmune[BuffID.Daybreak] = true;
            NPC.buffImmune[BuffID.DryadsWardDebuff] = true;
            SpawnModBiomes = [ ModContent.GetInstance<Biomes.Mausoleum>().Type ];
            NPC.npcSlots = 6;
        }

        public override void OnKill()
        {
            var entitySource = NPC.GetSource_Death();

            if (!Main.dedServ)
                for (int i = 0; i < Main.rand.Next(2, 4); i++)
                    NPC.NewNPC(entitySource, (int)NPC.Center.X + Main.rand.Next(-2, 2), (int)NPC.Center.Y + Main.rand.Next(-2, 2), ModContent.NPCType<CursedSludgeSmall>(), NPC.whoAmI);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.dedServ)
                return;

            for (int k = 0; k < 2.0; k++)
            {
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, ModContent.DustType<Dusts.MausoleumTorch>(), 0, 0, 0, default(Color), 1f);
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                new FlavorTextBestiaryInfoElement("This is not your tipical slime that you run into, this one seems to be cursed by the immense occultism that has imbued the mausoleum with...")
            });
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target];

            NPC.TargetClosest();

            TeleportTimer++;

            if (TeleportTimer >= 300)
            {
                NPC.Teleport(new Vector2(player.Center.X, player.position.Y - 300), 2);

                TeleportTimer = 0f;
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            PostNiadesDropCondition postNiadesDrop = new PostNiadesDropCondition();

            npcLoot.Add(ItemDropRule.ByCondition(postNiadesDrop, ModContent.ItemType<CursedAshes>(), 1, 2, 4));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (DownedBossSystem.downedNiades && Main.hardMode && ModContent.GetInstance<ZoneSystem>().zoneMausoleum)
                return SpawnCondition.Cavern.Chance * 0.09f;
            else
                return SpawnCondition.Cavern.Chance * 0f;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
    }
}
