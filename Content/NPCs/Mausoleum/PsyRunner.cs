using Eternal.Common.Misc;
using Eternal.Common.Systems;
using Eternal.Content.Dusts;
using Eternal.Content.Items.Materials;
using Eternal.Content.Items.Misc;
using Eternal.Content.Items.Summon;
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

namespace Eternal.Content.NPCs.Mausoleum
{
    public class PsyRunner : ModNPC
    {
        public override void SetStaticDefaults()
        {
            NPCID.Sets.TrailCacheLength[NPC.type] = 10;
            NPCID.Sets.TrailingMode[NPC.type] = 0;

            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.PossessedArmor];

            NPCID.Sets.ImmuneToAllBuffs[Type] = true;
        }

        ref float AttackTimer => ref NPC.ai[1];

        public override void SetDefaults()
        {
            NPC.width = 28;
            NPC.height = 42;
            if (Main.hardMode)
            {
                NPC.damage = 20;
                NPC.defense = 30;
                NPC.lifeMax = 300;
            }
            else
            {
                NPC.damage = 15;
                NPC.defense = 10;
                NPC.lifeMax = 100;
            }
            NPC.value = Item.sellPrice(silver: 60);
            NPC.knockBackResist = -1f;
            NPC.aiStyle = 3;
            AnimationType = NPCID.ChaosElemental;
            AIType = NPCID.ChaosElemental;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            SpawnModBiomes = [ ModContent.GetInstance<Biomes.Mausoleum>().Type ];
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([
                new FlavorTextBestiaryInfoElement("They were once zealots from the Dungeon, but they've since then turned their devotion towards the Mausoleum...")
            ]);
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target];
            NPC.TargetClosest(true);
            if (player.dead || !player.active)
                NPC.TargetClosest(false);

            AttackTimer++;
            if (AttackTimer >= 250)
            {
                if (Main.rand.NextBool(2))
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<MausoleumTorch>(), 0f, -2.5f);
            }

            if (AttackTimer == 300 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                for (int k = 0; k < 5; k++)
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<MausoleumTorch>(), 0f, -2.5f, 0, default, 1.7f);

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

                int[] i =
                {
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, 0f, 4f, ModContent.ProjectileType<Psyfireball>(), NPC.damage, 0),
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, 0f, -4f, ModContent.ProjectileType<Psyfireball>(), NPC.damage, 0),
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, -4f, 0f, ModContent.ProjectileType<Psyfireball>(), NPC.damage, 0),
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, 4f, 0f, ModContent.ProjectileType<Psyfireball>(), NPC.damage, 0)
                };

                for (int j = 0; j < i.Length; j++)
                {
                    Main.projectile[i[j]].timeLeft = 100;
                    Main.projectile[i[j]].tileCollide = false;
                }

                AttackTimer = 0;

                NPC.netUpdate = true;
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.dedServ)
                return;

            for (int k = 0; k < 10.0; k++)
            {
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, ModContent.DustType<MausoleumTorch>(), 0, 0, 0, default(Color), 1f);
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (ModContent.GetInstance<ZoneSystem>().zoneMausoleum)
                return SpawnCondition.Cavern.Chance * 0.5f;
            else
                return SpawnCondition.Cavern.Chance * 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            LeadingConditionRule isHardmodeRule = new(new Conditions.IsHardmode());

            isHardmodeRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Animanomicon>(), 12));

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Poutine>(), 12));
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
            => GlowMaskUtils.DrawNPCGlowMask(spriteBatch, NPC, ModContent.Request<Texture2D>("Eternal/Content/NPCs/Mausoleum/PsyRunner_Glow").Value);
    }
}
