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
        /// <summary>
        ///     Applies Glow Maska to NPCs
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

        /// <summary>
        ///     Applies Glow Masks to Items
        /// </summary>
        public static void DrawItemGlowMask(Texture2D texture, PlayerDrawInfo info)
        {
            Item item = info.drawPlayer.HeldItem;
            if (info.shadow != 0f || info.drawPlayer.frozen || ((info.drawPlayer.itemAnimation <= 0 || item.useStyle == 0) && (item.holdStyle <= 0 || info.drawPlayer.pulley)) || info.drawPlayer.dead || item.noUseGraphic || (info.drawPlayer.wet && item.noWet))
            {
                return;
            }

            Vector2 offset = Vector2.Zero;
            Vector2 origin = Vector2.Zero;
            float rotOffset = 0;

            if (item.useStyle == ItemUseStyleID.HoldingOut)
            {
                if (Item.staff[item.type])
                {
                    rotOffset = 0.785f * info.drawPlayer.direction;
                    if (info.drawPlayer.gravDir == -1f)
                    {
                        rotOffset -= 1.57f * info.drawPlayer.direction;
                        if (info.drawPlayer.gravDir == -1f)
                        {
                            rotOffset -= 1.57f * info.drawPlayer.direction;
                        }

                        origin = new Vector2(texture.Width * 0.5f * (1 - info.drawPlayer.direction), (info.drawPlayer.gravDir == -1f) ? 0 : texture.Height);

                        int oldOriginX = -(int)origin.X;
                        ItemLoader.HoldoutOrigin(info.drawPlayer, ref origin);
                        offset = new Vector2(origin.X + oldOriginX, 0);
                    }
                    else
                    {
                        offset = new Vector2(10, texture.Height / 2);
                        ItemLoader.HoldoutOffset(info.drawPlayer.gravDir, item.type, ref offset);
                        origin = new Vector2(-offset.X, texture.Height / 2);
                        if (info.drawPlayer.direction == -1)
                        {
                            origin.X = texture.Width + offset.X;
                        }

                        offset = new Vector2(texture.Width / 2, offset.Y);
                    }
                }
                else
                {
                    origin = new Vector2(texture.Width * 0.5f * (1 - info.drawPlayer.direction), (info.drawPlayer.gravDir == -1f) ? 0 : texture.Height);
                }

                Main.playerDrawData.Add(new DrawData(
                    texture,
                    info.itemLocation - Main.screenPosition + offset,
                    texture.Bounds,
                    new Color(250, 250, 250, item.alpha),
                    info.drawPlayer.itemRotation + rotOffset,
                    origin,
                    item.scale,
                    info.spriteEffects,
                    0
                ));
            }
        }
    }
}
