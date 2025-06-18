using Eternal.Common.Configurations;
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

namespace Eternal.Content.NPCs.Carrion
{
    public class FesteringCarcass : ModNPC
    {
        public override bool IsLoadingEnabled(Mod mod) => ServerConfig.instance.update15;

        public override void SetDefaults()
        {
            NPC.width = 24;
            NPC.height = 38;
            NPC.lifeMax = 200;
            NPC.defense = 48;
            NPC.damage = 18;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = 22;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = new SoundStyle("Terraria/Sounds/NPC_Killed_2")
            {
                Volume = 0.4f,
                Pitch = -0.5f
            };
            SpawnModBiomes = [ ModContent.GetInstance<Biomes.CarrionSurface>().Type ];
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([
                new FlavorTextBestiaryInfoElement("These rotting abominations roam the plains of the dead pasture ahead of them...")
            ]);
        }

        public override void OnKill()
        {
            var entitySource = NPC.GetSource_Death();

            int gore1 = Mod.Find<ModGore>("FesteringCarcassArm").Type;
            int gore2 = Mod.Find<ModGore>("FesteringCarcassHead").Type;

            for (int i = 0; i < 1 + Main.rand.Next(1); i++)
                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-2, 2), Main.rand.Next(-2, 2)), gore1);

            Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-2, 2), Main.rand.Next(-2, 2)), gore2);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
                return;

            Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Blood);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (ModContent.GetInstance<ZoneSystem>().zoneCarrion)
                return SpawnCondition.Overworld.Chance * 1.5f;
	        else
		        return SpawnCondition.Overworld.Chance * 0f;
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target];
            NPC.TargetClosest(true);

            var entitySource = NPC.GetSource_FromAI();

            if (Main.rand.NextBool(6))
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Blood);

            if (Main.rand.NextBool(1000))
            {
                SoundEngine.PlaySound(new SoundStyle("Terraria/Sounds/Zombie_", 0, 3)
                {
                    Volume = 0.4f,
                    Pitch = -0.5f
                }, NPC.position);
            }

            NPC.spriteDirection = NPC.direction;
        }
    }
}
