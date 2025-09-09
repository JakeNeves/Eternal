using Eternal.Common.Misc;
using Eternal.Common.Systems;
using Eternal.Content.Items.Accessories;
using Eternal.Content.Items.Materials;
using Eternal.Content.Items.Weapons.Magic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.Miniboss
{
    [AutoloadBossHead]
    public class ShadeSlime : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.RainbowSlime];
        }

        ref float TeleportTimer => ref NPC.ai[1];

        public override void SetDefaults()
        {
            NPC.width = 74;
            NPC.height = 48;
            NPC.aiStyle = 1;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            AIType = NPCID.BlueSlime;
            NPC.damage = 100;
            NPC.defense = 30;
            NPC.lifeMax = 20000;
            AnimationType = NPCID.RainbowSlime;
            NPC.knockBackResist = 0f;
            NPC.lavaImmune = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Suffocation] = true;
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.BetsysCurse] = true;
            NPC.buffImmune[BuffID.Daybreak] = true;
            NPC.buffImmune[BuffID.DryadsWardDebuff] = true;
            SpawnModBiomes = [ ModContent.GetInstance<Biomes.DarkMoon>().Type ];
            NPC.rarity = 4;
            NPC.npcSlots = 6;
        }

        public override void OnKill()
        {
            var entitySource = NPC.GetSource_Death();

            if (!Main.dedServ)
                for (int i = 0; i < Main.rand.Next(4, 6); i++)
                    NPC.NewNPC(entitySource, (int)NPC.Center.X + Main.rand.Next(-2, 2), (int)NPC.Center.Y + Main.rand.Next(-2, 2), ModContent.NPCType<Shadeling>(), NPC.whoAmI);

            NPC.SetEventFlagCleared(ref DownedMinibossSystem.downedShadeSlime, -1);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.dedServ)
                return;

            for (int k = 0; k < 2.0; k++)
            {
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, ModContent.DustType<Dusts.Shade>(), 0, 0, 0, default(Color), 1f);
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([
                new FlavorTextBestiaryInfoElement("A rather unusual kind of slime, you didn't expect them to get their hands on the dark arts, did you?")
            ]);
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target];

            NPC.TargetClosest();

            TeleportTimer++;

            if (TeleportTimer >= 250)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    SoundEngine.PlaySound(SoundID.Item8, NPC.position);

                    int targetTileX = (int)Main.player[NPC.target].Center.X / 16;
                    int targetTileY = (int)Main.player[NPC.target].Center.Y / 16;
                    Vector2 chosenTile = Vector2.Zero;
                    if (NPC.AI_AttemptToFindTeleportSpot(ref chosenTile, targetTileX, targetTileY))
                    {
                        NPC.ai[1] = 30f;
                        NPC.ai[2] = chosenTile.X;
                        NPC.ai[3] = chosenTile.Y;
                    }
                    NPC.netUpdate = true;
                }

                TeleportTimer = 0f;
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ShadeMatter>(), 1, 2, 6));

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ShadeLocket>(), 24));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DarkArts>(), 8));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (DownedBossSystem.downedTrinity && EventSystem.darkMoon)
                return SpawnCondition.OverworldNightMonster.Chance * 0.02f;
            else
                return SpawnCondition.OverworldNightMonster.Chance * 0f;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
            => GlowMaskUtils.DrawNPCGlowMask(spriteBatch, NPC, ModContent.Request<Texture2D>("Eternal/Content/NPCs/Miniboss/ShadeSlime_Glow").Value);
    }
}
