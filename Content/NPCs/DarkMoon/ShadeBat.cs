using Eternal.Common.Misc;
using Eternal.Common.Systems;
using Eternal.Content.Dusts;
using Eternal.Content.Items.Accessories;
using Eternal.Content.Items.Materials;
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

namespace Eternal.Content.NPCs.DarkMoon
{
    public class ShadeBat : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.GiantBat];

            NPCID.Sets.ImmuneToAllBuffs[Type] = true;
        }

        public override void SetDefaults()
        {
            NPC.width = 40;
            NPC.height = 22;
            NPC.lifeMax = 4000;
            NPC.defense = 50;
            NPC.damage = 24;
            NPC.knockBackResist = -1f;
            NPC.aiStyle = 14;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            SpawnModBiomes = [ ModContent.GetInstance<Biomes.DarkMoon>().Type ];
            AnimationType = NPCID.GiantBat;
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

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([
                new FlavorTextBestiaryInfoElement("A much deadlier variant of your typical kind of bat, usually annoying, but this one has a little twist that becomes unleashed when killed!")
            ]);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.dedServ)
                return;

            for (int k = 0; k < 10.0; k++)
            {
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, ModContent.DustType<Shade>(), 0, 0, 0, default(Color), 1f);
            }
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
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ShadeMatter>(), 1, 1, 4));

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ShadeLocket>(), 36));
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target];
            NPC.TargetClosest(true);
            if (player.dead || !player.active)
                NPC.TargetClosest(false);

            var entitySource = NPC.GetSource_FromAI();
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
            => GlowMaskUtils.DrawNPCGlowMask(spriteBatch, NPC, ModContent.Request<Texture2D>("Eternal/Content/NPCs/DarkMoon/ShadeBat_Glow").Value);
    }
}
