using Eternal.Common.Systems;
using Eternal.Content.Projectiles.Enemy;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.DarkMoon
{
    public class SanguineSphere : ModNPC
    {
        int attackTimer;
        int frameType = 0;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;

            NPCID.Sets.ImmuneToAllBuffs[Type] = true;
        }

        public override void SetDefaults()
        {
            NPC.width = 38;
            NPC.height = 38;
            NPC.lifeMax = 4000;
            NPC.defense = 60;
            NPC.damage = 48;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = 14;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            SpawnModBiomes = [ ModContent.GetInstance<Biomes.DarkMoon>().Type, ModContent.GetInstance<Biomes.Gehenna>().Type ];
        }

        public override void OnSpawn(IEntitySource source)
        {
            frameType = Main.rand.Next(0, 3);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([
                new FlavorTextBestiaryInfoElement("A much deadlier sphere, compared to a regular Deadly Shpere...")
            ]);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
                return;

            Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.RedTorch);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (DownedBossSystem.downedTrinity && EventSystem.darkMoon)
                return SpawnCondition.OverworldNightMonster.Chance * 0.5f + SpawnCondition.Underworld.Chance * 0f;
            else if (DownedBossSystem.downedTrinity && ModContent.GetInstance<ZoneSystem>().zoneGehenna)
                return SpawnCondition.OverworldNightMonster.Chance * 0f + SpawnCondition.Underworld.Chance * 0.25f;
            else
                return SpawnCondition.OverworldNightMonster.Chance * 0f + SpawnCondition.Underworld.Chance * 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
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
            NPC.rotation += NPC.velocity.X * 0.1f;

            attackTimer++;
            if (attackTimer == 125 || attackTimer == 150 || attackTimer == 175)
            {
                float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<Sanguinebeam2>(), NPC.damage, 1, Main.myPlayer, 0, 0);
            }
            else if (attackTimer == 250)
            {
                attackTimer = 0;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frame.Y = frameType * frameHeight;
        }
    }
}
