using Eternal.Common.ItemDropRules.Conditions;
using Eternal.Common.Systems;
using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Enemy;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.DarkMoon
{
    public class Occultist : ModNPC
    {
        int frameType = 0;

        ref float AttackTimer => ref NPC.ai[1];

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 2;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 300;
            NPC.damage = 20;
            NPC.defense = 30;
            NPC.knockBackResist = 0f;
            NPC.width = 26;
            NPC.height = 48;
            NPC.aiStyle = -1;
            NPC.HitSound = SoundID.NPCHit2;
            NPC.DeathSound = SoundID.NPCDeath2;
            NPC.value = Item.sellPrice(gold: 30);
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.Frozen] = true;
            NPC.buffImmune[BuffID.Chilled] = true;
            NPC.value = 10f;
            SpawnModBiomes = [ ModContent.GetInstance<Biomes.DarkMoon>().Type, ModContent.GetInstance<Biomes.Mausoleum>().Type ];
        }

        public override void SetBestiary(BestiaryDatabase dataNPC, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([
                new FlavorTextBestiaryInfoElement("Dreadful casters that inhabit the Mausoleum, occasionally strikes when the glow of the darkened moon shines across the horizon...")
            ]);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (EventSystem.darkMoon)
                return SpawnCondition.OverworldNightMonster.Chance * 0.5f + SpawnCondition.Underground.Chance * 0f;
            else if (ModContent.GetInstance<ZoneSystem>().zoneMausoleum && Main.hardMode)
                return SpawnCondition.OverworldNightMonster.Chance * 0f + SpawnCondition.Underground.Chance * 0.25f;
            else
                return SpawnCondition.OverworldNightMonster.Chance * 0f + SpawnCondition.Underground.Chance * 0f;
        }

        public override void AI()
        {
            if (!Main.dedServ)
                Lighting.AddLight(NPC.Center, 1.50f, 0.25f, 1.50f);

            NPC.spriteDirection = NPC.direction;

            // Taken from the vanilla source code, I edited it for the sake of reworking the Occultist, why did I even do this in the first place?
            NPC.TargetClosest();
            NPC.velocity.X *= 0.93f;
            if ((double)NPC.velocity.X > -0.1 && (double)NPC.velocity.X < 0.1)
            {
                NPC.velocity.X = 0f;
            }
            if (NPC.ai[0] == 0f)
            {
                NPC.ai[0] = 500f;
            }
            if (NPC.ai[2] != 0f && NPC.ai[3] != 0f)
            {
                NPC.position += NPC.netOffset;
                SoundEngine.PlaySound(in SoundID.Item8, NPC.position);
                for (int num70 = 0; num70 < 50; num70++)
                {
                    int num78 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, ModContent.DustType<Dusts.MausoleumTorch>(), 0f, 0f, 100, default(Color), 2.5f);
                    Dust dust9 = Main.dust[num78];
                    Dust dust2 = dust9;
                    dust2.velocity *= 3f;
                    Main.dust[num78].noGravity = true;
                }
                NPC.position -= NPC.netOffset;
                NPC.position.X = NPC.ai[2] * 16f - (float)(NPC.width / 2) + 8f;
                NPC.position.Y = NPC.ai[3] * 16f - (float)NPC.height;
                NPC.netOffset *= 0f;
                NPC.velocity.X = 0f;
                NPC.velocity.Y = 0f;
                NPC.ai[2] = 0f;
                NPC.ai[3] = 0f;
                SoundEngine.PlaySound(in SoundID.Item8, NPC.position);
                for (int num79 = 0; num79 < 50; num79++)
                {
                    int num87 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, ModContent.DustType<Dusts.MausoleumTorch>(), 0f, 0f, 100, default(Color), 2.5f);
                    Dust dust17 = Main.dust[num87];
                    Dust dust2 = dust17;
                    dust2.velocity *= 3f;
                    Main.dust[num87].noGravity = true;
                }
            }
            NPC.ai[0] += 1f;
            if (NPC.ai[0] == 100f || NPC.ai[0] == 150f || NPC.ai[0] == 200f)
            {
                frameType = 1;

                NPC.ai[1] = 30f;
                NPC.netUpdate = true;
            }
            else
                frameType = 0;
            if (NPC.ai[0] >= 650f && Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.ai[0] = 1f;
                int targetTileX = (int)Main.player[NPC.target].Center.X / 16;
                int targetTileY = (int)Main.player[NPC.target].Center.Y / 16;
                Vector2 chosenTile = Vector2.Zero;
                if (NPC.AI_AttemptToFindTeleportSpot(ref chosenTile, targetTileX, targetTileY))
                {
                    NPC.ai[1] = 20f;
                    NPC.ai[2] = chosenTile.X;
                    NPC.ai[3] = chosenTile.Y;
                }
                NPC.netUpdate = true;
            }
            if (NPC.ai[1] > 0f)
            {
                NPC.ai[1] -= 1f;
                if (NPC.ai[1] == 25f)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        float num102 = 10f;
                        Vector2 vector14 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
                        float num103 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector14.X + (float)Main.rand.Next(-10, 11);
                        float num104 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - vector14.Y + (float)Main.rand.Next(-10, 11);
                        float num105 = (float)Math.Sqrt(num103 * num103 + num104 * num104);
                        num105 = num102 / num105;
                        num103 *= num105;
                        num104 *= num105;
                        int num106 = 40;
                        int num108 = Projectile.NewProjectile(NPC.GetSource_FromAI(), vector14.X, vector14.Y, num103, num104, ModContent.ProjectileType<Psyfireball>(), num106, 0f, Main.myPlayer);
                        Main.projectile[num108].timeLeft = 300;
                        NPC.localAI[0] = 0f;
                    }
                }
            }
            NPC.position += NPC.netOffset;
            if (Main.rand.NextBool(2))
            {
                int num117 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y + 2f), NPC.width, NPC.height, ModContent.DustType<Dusts.MausoleumTorch>(), NPC.velocity.X * 0.2f, NPC.velocity.Y * 0.2f, 100, default(Color), 2f);
                Main.dust[num117].noGravity = true;
                Main.dust[num117].velocity.X *= 1f;
                Main.dust[num117].velocity.Y *= 1f;
            }
            NPC.position -= NPC.netOffset;
            return;
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frame.Y = frameType * frameHeight;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
                return;

            if (NPC.life <= 0)
            {
                for (int k = 0; k < 5; k++)
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.PsycheFire>(), 2.5f, -2.5f, 0, default, 1.7f);
            }
            else
            {
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, ModContent.DustType<Dusts.PsycheFire>(), 0, -1f, 0, default(Color), 1f);
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            PostNiadesDropCondition postNiadesDrop = new PostNiadesDropCondition();

            npcLoot.Add(ItemDropRule.ByCondition(postNiadesDrop, ModContent.ItemType<CursedAshes>(), 2, 2, 6));

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<OcculticMatter>(), minimumDropped: 8, maximumDropped: 12));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PsychicAshes>(), minimumDropped: 4, maximumDropped: 12));
        }
    }
}
