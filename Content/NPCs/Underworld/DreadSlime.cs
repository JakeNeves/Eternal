using Eternal.Common;
using Eternal.Common.Systems;
using Eternal.Content.Items.Materials;
using Eternal.Content.Items.Placeable;
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
    public class DreadSlime : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.BlueSlime];
        }

        public override void SetDefaults()
        {
            NPC.width = 56;
            NPC.height = 36;
            NPC.aiStyle = 1;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            AIType = NPCID.LavaSlime;
            NPC.damage = 80;
            NPC.defense = 20;
            NPC.lifeMax = 6000;
            AnimationType = NPCID.BlueSlime;
            NPC.value = Item.sellPrice(platinum: 1, gold: 2);
            NPC.knockBackResist = 0f;
            NPC.lavaImmune = true;
            NPC.color = EternalCommonUtils.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, [Color.Red, Color.Orange, Color.Red]);
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Suffocation] = true;
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.BetsysCurse] = true;
            NPC.buffImmune[BuffID.Daybreak] = true;
            NPC.buffImmune[BuffID.DryadsWardDebuff] = true;
        }

        public override void OnKill()
        {
            var entitySource = NPC.GetSource_Death();

            if (!Main.dedServ)
            {
                int[] i =
                {
                    Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 12f, 12f, ProjectileID.BallofFire, NPC.damage / 2, 0),
                    Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 12f, -12f, ProjectileID.BallofFire, NPC.damage / 2, 0),
                    Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, -12f, -12f, ProjectileID.BallofFire, NPC.damage / 2, 0),
                    Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, -12f, 12f, ProjectileID.BallofFire, NPC.damage / 2, 0)
                };

                for (int j = 0; j < i.Length; j++)
                {
                    Main.projectile[i[j]].tileCollide = false;
                    Main.projectile[i[j]].friendly = false;
                    Main.projectile[i[j]].hostile = true;
                    Main.projectile[i[j]].timeLeft = 250;
                }
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            var entitySource = NPC.GetSource_Death();

            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.Torch, 0, 0);
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,

                new FlavorTextBestiaryInfoElement("Not to be confused with Lava Slimes")
            });
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Yoggie>(), 4, 1, 3));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (DownedBossSystem.downedCosmicApparition)
                return SpawnCondition.Underworld.Chance * 0.15f;
            else
                return SpawnCondition.Underworld.Chance * 0f;
        }
    }
}
