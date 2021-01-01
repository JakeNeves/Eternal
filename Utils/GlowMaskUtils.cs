using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal
{
    public static class GlowMaskUtils
    {
        public static void DrawNPCGlowMask(SpriteBatch spriteBatch, NPC npc, Texture2D texture)
        {
            var effects = npc.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(
                texture,
                npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY),
                npc.frame,
                Color.White,
                npc.rotation,
                npc.frame.Size() / 2,
                npc.scale,
                effects,
                0
            );
        }
    }
}
