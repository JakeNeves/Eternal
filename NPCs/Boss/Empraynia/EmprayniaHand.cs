using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.NPCs.Boss.Empraynia
{
    public class EmprayniaHand : ModNPC
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Empraynia");

            NPCID.Sets.TrailCacheLength[npc.type] = 8;
            NPCID.Sets.TrailingMode[npc.type] = 0;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 8000;
            npc.width = 56;
            npc.height = 32;
            npc.damage = 40;
            npc.defense = 40;
            npc.lavaImmune = true;
            npc.noTileCollide = true;
            npc.noGravity = true;
            npc.netAlways = true;
            npc.HitSound = SoundID.NPCHit12;
            npc.DeathSound = null;
            npc.knockBackResist = -1f;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 12000;
            npc.damage = 60;
            if (EternalWorld.hellMode)
            {
                npc.lifeMax = 16000;
                npc.damage = 80;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width * 0.5f, npc.height * 0.5f);
            for (int k = 0; k < npc.oldPos.Length; k++)
            {
                Vector2 drawPos = npc.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, npc.gfxOffY);
                Color color = npc.GetAlpha(lightColor) * ((float)(npc.oldPos.Length - k) / (float)npc.oldPos.Length);
                spriteBatch.Draw(Main.npcTexture[npc.type], drawPos, null, color, npc.rotation, drawOrigin, npc.scale, SpriteEffects.None, 0f);
            }
            return true;
        }

        public override void AI()
        {
            NPC parentNPC = Main.npc[(int)npc.ai[1]];
            Player player = Main.player[parentNPC.target];
            if (!parentNPC.active)
            {
                npc.active = false;
            }

            npc.Center = parentNPC.oldPos[4] + new Vector2(parentNPC.width / 2, parentNPC.height / 2) + new Vector2(50 * npc.ai[0], 40);
            npc.rotation = 0;
            npc.spriteDirection = (int)npc.ai[0];
        }

        public override bool CheckActive()
        {
            return false;
        }

    }
}
