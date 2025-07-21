using Eternal.Common.Configurations;
using Eternal.Common.Misc;
using Eternal.Common.Systems;
using Eternal.Content.Dusts;
using Eternal.Content.Items.Accessories;
using Eternal.Content.Projectiles.Enemy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.Carrion
{
    public class CadaveraHead : ModNPC
    {
        public override bool IsLoadingEnabled(Mod mod) => ServerConfig.instance.update15;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 3;

            NPCID.Sets.ImmuneToAllBuffs[Type] = true;
        }

        public override void SetDefaults()
        {
            NPC.width = 18;
            NPC.height = 26;
            NPC.lifeMax = 100;
            NPC.defense = 12;
            NPC.damage = 6;
            NPC.knockBackResist = -1f;
            NPC.aiStyle = 14;
            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = new SoundStyle("Terraria/Sounds/NPC_Killed_2")
            {
                Volume = 0.4f,
                Pitch = -0.5f
            };
            SpawnModBiomes = [ ModContent.GetInstance<Biomes.CarrionSurface>().Type ];
        }

        public override void OnKill()
        {
            var entitySource = NPC.GetSource_Death();

            int gore = Mod.Find<ModGore>("CadaveraHeadGore").Type;

            Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-2, 2), Main.rand.Next(-2, 2)), gore);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([
                new FlavorTextBestiaryInfoElement("A floating rotten head that has decayed into nothing but necrotic tissue...")
            ]);
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
                return SpawnCondition.Overworld.Chance * 0.25f;
            else
                return SpawnCondition.Overworld.Chance * 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target];
            NPC.TargetClosest(true);
            if (player.dead || !player.active)
                NPC.TargetClosest(false);

            var entitySource = NPC.GetSource_FromAI();

            if (Main.rand.NextBool(1000))
            {
                SoundEngine.PlaySound(new SoundStyle("Terraria/Sounds/Zombie_8")
                {
                    Volume = 0.4f,
                    Pitch = -0.5f
                }, NPC.position);
            }

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
