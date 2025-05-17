using Eternal.Common.Systems;
using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Enemy;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.DarkMoon
{
    public class PsychoZombie : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Zombie];
        }

        ref float AttackTimer => ref NPC.ai[1];

        public override void SetDefaults()
        {
            NPC.width = 18;
            NPC.height = 40;
            NPC.damage = 28;
            NPC.defense = 6;
            NPC.lifeMax = 400;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath2;
            NPC.value = Item.sellPrice(silver: 12, gold: 4);
            NPC.knockBackResist = 0.5f;
            NPC.aiStyle = 3;
            AnimationType = NPCID.Zombie;
            Banner = Item.NPCtoBanner(NPCID.Zombie);
            BannerItem = Item.BannerToItem(Banner);
            SpawnModBiomes = [ ModContent.GetInstance<Biomes.DarkMoon>().Type ];
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([
                new FlavorTextBestiaryInfoElement("Your simple regular zombies, but this time, they're psychotic and know how to teleport!")
            ]);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient && Main.rand.NextBool(48))
            {
                for (int k = 0; k < 5; k++)
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.MausoleumTorch>(), 0f, -2.5f, 0, default, 1.7f);

                if (!Main.dedServ)
                    SoundEngine.PlaySound(SoundID.Item8, NPC.position);

                int targetTileX = (int)Main.player[NPC.target].Center.X / 16;
                int targetTileY = (int)Main.player[NPC.target].Center.Y / 16;
                Vector2 chosenTile = Vector2.Zero;
                if (NPC.AI_AttemptToFindTeleportSpot(ref chosenTile, targetTileX, targetTileY))
                {
                    NPC.ai[2] = chosenTile.X;
                    NPC.ai[3] = chosenTile.Y;
                }

                NPC.netUpdate = true;
            }
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target];
            NPC.TargetClosest(true);
            if (player.dead || !player.active)
                NPC.TargetClosest(false);

            AttackTimer++;

            if (!Main.dedServ)
                Lighting.AddLight(NPC.Center, 1.50f, 0.25f, 1.50f);

            if (Main.rand.NextBool(1000))
                SoundEngine.PlaySound(SoundID.BloodZombie, NPC.position);

            if (AttackTimer >= 150)
            {
                if (Main.rand.NextBool(2))
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.MausoleumTorch>(), 0f, -2.5f);
            }

            if (AttackTimer == 200 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                for (int k = 0; k < 5; k++)
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.MausoleumTorch>(), 0f, -2.5f, 0, default, 1.7f);

                if (!Main.dedServ)
                    SoundEngine.PlaySound(SoundID.Item8, NPC.position);

                int targetTileX = (int)Main.player[NPC.target].Center.X / 16;
                int targetTileY = (int)Main.player[NPC.target].Center.Y / 16;
                Vector2 chosenTile = Vector2.Zero;
                if (NPC.AI_AttemptToFindTeleportSpot(ref chosenTile, targetTileX, targetTileY))
                {
                    NPC.ai[2] = chosenTile.X;
                    NPC.ai[3] = chosenTile.Y;
                }

                AttackTimer = 0;

                NPC.netUpdate = true;
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (EventSystem.darkMoon)
                return SpawnCondition.OverworldNightMonster.Chance * 0.3f;
            else
                return SpawnCondition.OverworldNightMonster.Chance * 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<OcculticMatter>(), minimumDropped: 4, maximumDropped: 8));
        }
    }
}
