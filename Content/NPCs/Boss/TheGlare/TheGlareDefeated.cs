using Eternal.Common.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Boss.TheGlare
{
    public class TheGlareDefeated : ModNPC
    {
        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);

            NPCID.Sets.ImmuneToAllBuffs[Type] = true;
        }

        public override void SetDefaults()
        {
            NPC.width = 134;
            NPC.height = 134;
            NPC.aiStyle = -1;
            NPC.lifeMax = 1;
            NPC.lavaImmune = true;
            NPC.knockBackResist = -1f;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.dontTakeDamage = true;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color lightColor)
        {
            if (ModContent.GetInstance<ZoneSystem>().zoneGehenna)
            {
                //tf does this supposed to mean
                int num159 = 1;
                float num160 = 0f;
                int num161 = num159;
                SpriteEffects spriteEffects = SpriteEffects.None;
                Microsoft.Xna.Framework.Color color25 = Lighting.GetColor((int)((double)NPC.position.X + (double)NPC.width * 0.5) / 16, (int)(((double)NPC.position.Y + (double)NPC.height * 0.5) / 16.0));
                Texture2D texture2D4 = ModContent.Request<Texture2D>("Eternal/Content/NPCs/Boss/TheGlare/TheGlareGehennaDefeated").Value;
                int num1561 = texture2D4.Height / Main.npcFrameCount[NPC.type];
                int y31 = num1561 * (int)NPC.frameCounter;
                Microsoft.Xna.Framework.Rectangle rectangle2 = new Microsoft.Xna.Framework.Rectangle(0, y31, texture2D4.Width, num1561);
                Vector2 origin3 = rectangle2.Size() / 2f;
                SpriteEffects effects = spriteEffects;
                if (NPC.spriteDirection > 0)
                {
                    effects = SpriteEffects.FlipHorizontally;
                }
                float num165 = NPC.rotation;
                Microsoft.Xna.Framework.Color color29 = NPC.GetAlpha(color25);
                Main.spriteBatch.Draw(texture2D4, NPC.position + NPC.Size / 2f - Main.screenPosition + new Vector2(0f, NPC.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle2), color29, num165 + NPC.rotation * num160 * (float)(num161 - 1) * -(float)spriteEffects.HasFlag(SpriteEffects.FlipHorizontally).ToDirectionInt(), origin3, NPC.scale, effects, 0f);
                return false;
            }

            return true;
        }
    }
}
