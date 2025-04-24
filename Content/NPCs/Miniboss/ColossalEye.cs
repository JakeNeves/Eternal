using Eternal.Common.Systems;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.Miniboss
{
    [AutoloadBossHead]
    public class ColossalEye : ModNPC
    {
        int attackTimer = 0;
        int frameType = 0;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 5;
        }

        public override void SetDefaults()
        {
            NPC.width = 62;
            NPC.height = 42;
            NPC.aiStyle = 5;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.damage = 125;
            NPC.defense = 40;
            NPC.lifeMax = 15000;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            NPC.lavaImmune = true;
            SpawnModBiomes = [ ModContent.GetInstance<Biomes.DarkMoon>().Type ];
            NPC.rarity = 4;
            NPC.npcSlots = 6;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.dedServ)
                return;

            for (int k = 0; k < 2.0; k++)
            {
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.Blood, 0, 0, 0, default(Color), 1f);
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([
                new FlavorTextBestiaryInfoElement("A rather gigantic eye, not one that you would usually expect")
            ]);
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target];

            NPC.TargetClosest();

            if (NPC.life < NPC.lifeMax / 2)
                frameType = 1;
            else
                frameType = 0;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {

        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (DownedBossSystem.downedTrinity && EventSystem.darkMoon)
                return SpawnCondition.OverworldNightMonster.Chance * 0.03f;
            else
                return SpawnCondition.OverworldNightMonster.Chance * 0f;
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

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
    }
}
