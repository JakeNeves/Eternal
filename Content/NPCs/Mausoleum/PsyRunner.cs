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
using Eternal.Content.Items.Summon;
using Eternal.Common.Misc;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.ItemDropRules;

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
            NPC.damage = 20;
            NPC.defense = 25;
            NPC.lifeMax = 250;
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
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Animanomicon>(), 12));
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color lightColor)
        {
            Main.instance.LoadNPC(NPC.type);
            Texture2D texture = ModContent.Request<Texture2D>("Eternal/Content/NPCs/Mausoleum/PsyRunner_Shadow").Value;

            Vector2 drawOrigin = new Vector2(texture.Width, NPC.height);
            for (int k = 0; k < NPC.oldPos.Length; k++)
            {
                Vector2 drawPos = (NPC.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, NPC.gfxOffY);
                Color color = NPC.GetAlpha(lightColor) * ((NPC.oldPos.Length - k) / (float)NPC.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, null, color, NPC.rotation, drawOrigin, NPC.scale, SpriteEffects.None, 0);
            }

            return true;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
            => GlowMaskUtils.DrawNPCGlowMask(spriteBatch, NPC, ModContent.Request<Texture2D>("Eternal/Content/NPCs/Mausoleum/PsyRunner_Glow").Value);
    }
}
