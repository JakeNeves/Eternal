using Eternal.Common.Systems;
using Eternal.Content.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Terraria.Audio;
using Eternal.Content.Projectiles.Enemy;
using Eternal.Common.Misc;
using Microsoft.Xna.Framework.Graphics;
using Eternal.Content.Items.Accessories;
using Terraria.GameContent.ItemDropRules;

namespace Eternal.Content.NPCs.DarkMoon
{
    public class Shademan : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.PossessedArmor];

            NPCID.Sets.ImmuneToAllBuffs[Type] = true;
        }

        ref float AttackTimer => ref NPC.ai[1];

        public override void SetDefaults()
        {
            NPC.width = 28;
            NPC.height = 42;
            NPC.damage = 80;
            NPC.defense = 30;
            NPC.lifeMax = 4000;
            NPC.value = Item.sellPrice(platinum: 2);
            NPC.knockBackResist = -1f;
            NPC.aiStyle = 3;
            AnimationType = NPCID.ChaosElemental;
            AIType = NPCID.ChaosElemental;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            SpawnModBiomes = [ ModContent.GetInstance<Biomes.DarkMoon>().Type ];
            NPC.rarity = 4;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([
                new FlavorTextBestiaryInfoElement("Someone got into the dark arts and mastered it, looks like you may have to watch out for the tactics he can pull off!")
            ]);
        }

        public override void OnKill()
        {
            var entitySource = NPC.GetSource_Death();

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                SoundEngine.PlaySound(new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/ShademanTrap")
                {
                    Volume = 0.8f,
                    PitchVariance = Main.rand.NextFloat(0.2f, 0.9f),
                    MaxInstances = 0,
                });

                Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 12f, 0f, ModContent.ProjectileType<ShadeBomb>(), NPC.damage, 0);
                Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 0f, 12f, ModContent.ProjectileType<ShadeBomb>(), NPC.damage, 0);
                Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, -12f, 0f, ModContent.ProjectileType<ShadeBomb>(), NPC.damage, 0);
                Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 0f, -12f, ModContent.ProjectileType<ShadeBomb>(), NPC.damage, 0);
            }
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target];
            NPC.TargetClosest(true);
            if (player.dead || !player.active)
                NPC.TargetClosest(false);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.dedServ)
                return;

            for (int k = 0; k < 10.0; k++)
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, ModContent.DustType<Shade>(), 0, 0, 0, default(Color), 1f);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (DownedBossSystem.downedTrinity && EventSystem.darkMoon)
                return SpawnCondition.OverworldNightMonster.Chance * 0.25f;
            else
                return SpawnCondition.OverworldNightMonster.Chance * 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ShadeLocket>(), 36));
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
            => GlowMaskUtils.DrawNPCGlowMask(spriteBatch, NPC, ModContent.Request<Texture2D>("Eternal/Content/NPCs/DarkMoon/Shademan_Glow").Value);
    }
}
