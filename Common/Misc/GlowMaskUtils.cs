using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Common.Misc
{
    public static class GlowMaskUtils
    {
        /// <summary>
        ///     Applies Glow Masks to NPCs
        /// </summary>
        public static void DrawNPCGlowMask(SpriteBatch spriteBatch, NPC npc, Texture2D texture, Color? color = null)
        {
            var effects = npc.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(
                texture,
                npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY),
                npc.frame,
                color ?? Color.White,
                npc.rotation,
                npc.frame.Size() / 2,
                npc.scale,
                effects,
                0
            );
        }

        /// <summary>
        ///     Extra Details
        /// </summary>
        public static void DrawExtras(SpriteBatch spriteBatch, NPC npc, Texture2D texture)
        {
            var effects = npc.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(
                texture,
                npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY),
                npc.frame,
                new Color(200, 200, 200),
                npc.velocity.X * .1f,
                npc.frame.Size() / 2,
                npc.scale,
                effects,
                0
            );
        }
    }
}
