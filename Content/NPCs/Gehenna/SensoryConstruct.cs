using Eternal.Common.Systems;
using Eternal.Content.Projectiles.Enemy;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.Gehenna
{
    public class SensoryConstruct : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.PossessedArmor];

            NPCID.Sets.ImmuneToAllBuffs[Type] = true;
        }

        public override void SetDefaults()
        {
            NPC.width = 36;
            NPC.height = 54;
            NPC.damage = 10;
            NPC.defense = 30;
            NPC.lifeMax = 1500;
            NPC.value = Item.sellPrice(gold: 15);
            NPC.knockBackResist = -1f;
            NPC.aiStyle = 3;
            AnimationType = NPCID.PossessedArmor;
            NPC.HitSound = SoundID.Tink;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.lavaImmune = true;
            SpawnModBiomes = [ ModContent.GetInstance<Biomes.Gehenna>().Type ];
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([
                new FlavorTextBestiaryInfoElement("These basalt constructs can only react to being provoked, it's best that you should beware of them!")
            ]);
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target];
            NPC.TargetClosest(true);
            if (player.dead || !player.active)
            {
                NPC.TargetClosest(false);
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
                return;

            for (int k = 0; k < 5; k++)
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Torch, 2.5f, -2.5f);

            Player target = Main.player[NPC.target];

            for (int i = 0; i < Main.maxPlayers; i++)
            {
                var player = Main.player[i];
                if (!player.active)
                    continue;

                Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
                direction.Normalize();
                direction.X *= 8.5f;
                direction.Y *= 8.5f;

                float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                float B = (float)Main.rand.Next(-200, 200) * 0.01f;

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    if (Main.rand.NextBool(8))
                    {
                        SoundEngine.PlaySound(SoundID.Item91, NPC.Center);

                        int proj = Projectile.NewProjectile(NPC.GetSource_OnHurt(player), NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<Sanguinebeam>(), NPC.damage, 1, Main.myPlayer, 0, 0);

                        Main.projectile[proj].timeLeft = 100;
                        Main.projectile[proj].tileCollide = false;
                    }
                }
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!DownedBossSystem.downedCosmicApparition && !ModContent.GetInstance<ZoneSystem>().zoneGehenna)
                return SpawnCondition.Underworld.Chance * 0f;
            else
                return SpawnCondition.Underworld.Chance * 0.6f;
        }
    }
}
