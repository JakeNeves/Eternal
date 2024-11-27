using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Boss.DuneGolem
{
    public class DunePylon : ModNPC
    {

        bool justSpawned = false;

        public override void SetDefaults()
        {
            NPC.width = 34;
            NPC.height = 44;
            NPC.damage = 12;
            NPC.defense = 6;
            NPC.lifeMax = 100;
            NPC.HitSound = SoundID.NPCHit3;
            NPC.DeathSound = SoundID.NPCDeath3;
            NPC.knockBackResist = 0f;
            NPC.noTileCollide = true;
            NPC.noGravity = true;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert,

                new FlavorTextBestiaryInfoElement("When constructed, these pylons will lach onto the Dune Golem, making the idol itself immune to any incoming attacks!")
            });
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            var entitySource = NPC.GetSource_Death();

            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }

            if (NPC.life <= 0)
            {
                int gore1 = Mod.Find<ModGore>("DunePylon1").Type;
                int gore2 = Mod.Find<ModGore>("DunePylon2").Type;
                int gore3 = Mod.Find<ModGore>("DunePylon3").Type;

                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-4, 4), Main.rand.Next(-4, 4)), gore1);
                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-4, 4), Main.rand.Next(-4, 4)), gore2);
                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-4, 4), Main.rand.Next(-4, 4)), gore3);
            }

            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, DustID.DesertTorch, NPC.oldVelocity.X * 0.5f, NPC.oldVelocity.Y * 0.5f);
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(BuffID.Electrified, 360, false);
        }

        public override void AI()
        {
            if (!NPC.AnyNPCs(ModContent.NPCType<DuneGolem>()))
            {
                NPC.active = false;

                for (int i = 0; i < 50; i++)
                {
                    Vector2 position = NPC.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 50 * i)) * 30;
                    Dust dust = Dust.NewDustPerfect(NPC.position, DustID.DesertTorch);
                    dust.noGravity = true;
                    dust.velocity = Vector2.Normalize(position - NPC.Center) * 8;
                    dust.noLight = false;
                    dust.fadeIn = 1f;
                }

                if (!Main.dedServ)
                    SoundEngine.PlaySound(SoundID.DD2_SonicBoomBladeSlash, NPC.position);
            }

            if (!justSpawned)
            {
                for (int i = 0; i < 10; i++)
                {
                    Vector2 position = NPC.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 50 * i)) * 30;
                    Dust dust = Dust.NewDustPerfect(NPC.position, DustID.DesertTorch);
                    dust.noGravity = true;
                    dust.velocity = Vector2.Normalize(position - NPC.Center) * 8;
                    dust.noLight = false;
                    dust.fadeIn = 1f;
                }

                if (!Main.dedServ)
                    SoundEngine.PlaySound(SoundID.DD2_DefenseTowerSpawn, NPC.position);

                justSpawned = true;
            }

            // NPC.velocity.Y += (NPC.velocity.Y * (float)Math.Sin(0.5)) / 20; <- how tf am I supposed to achieve a floating effect?
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color lightColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>("Eternal/Content/NPCs/Boss/DuneGolem/DuneGolem_Chain").Value;
            NPC parentNPC = Main.npc[0];
            for (int i = 0; i < Main.npc.Length; ++i)
            {
                if (Main.npc[i].type == ModContent.NPCType<DuneGolem>())
                {
                    parentNPC = Main.npc[i];
                    break;
                }
            }
            Vector2 position = NPC.Center;
            Vector2 mountedCenter = parentNPC.Center;
            Microsoft.Xna.Framework.Rectangle? sourceRectangle = new Microsoft.Xna.Framework.Rectangle?();
            Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
            float num1 = (float)texture.Height;
            Vector2 vector2_4 = mountedCenter - position;
            float rotation = (float)Math.Atan2((double)vector2_4.Y, (double)vector2_4.X) - 1.57f;
            bool flag = true;
            if (float.IsNaN(position.X) && float.IsNaN(position.Y))
                flag = false;
            if (float.IsNaN(vector2_4.X) && float.IsNaN(vector2_4.Y))
                flag = false;
            while (flag)
            {
                if ((double)vector2_4.Length() < (double)num1 + 1.0)
                {
                    flag = false;
                }
                else
                {
                    Vector2 vector2_1 = vector2_4;
                    vector2_1.Normalize();
                    position += vector2_1 * num1;
                    vector2_4 = mountedCenter - position;
                    Color color2 = Lighting.GetColor((int)position.X / 16, (int)((double)position.Y / 16.0));
                    color2 = NPC.GetAlpha(color2);
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                }
            }

            return true;
        }
    }
}
