using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Placeable
{
    public class MausoleumTorch : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;

            ItemID.Sets.ShimmerTransformToItem[Type] = ItemID.ShimmerTorch;
            ItemID.Sets.SingleUseInGamepad[Type] = true;
            ItemID.Sets.Torches[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.DefaultToTorch(ModContent.TileType<Tiles.MausoleumTorch>(), 0, false);
        }

        public override void HoldItem(Player player)
        {
            if (player.wet)
            {
                return;
            }

            if (Main.rand.NextBool(player.itemAnimation > 0 ? 7 : 30))
            {
                Dust dust = Dust.NewDustDirect(new Vector2(player.itemLocation.X + (player.direction == -1 ? -16f : 6f), player.itemLocation.Y - 14f * player.gravDir), 4, 4, ModContent.DustType<Dusts.MausoleumTorch>(), 0f, 0f, 100);
                if (!Main.rand.NextBool(3))
                {
                    dust.noGravity = true;
                }

                dust.velocity *= 0.3f;
                dust.velocity.Y -= 1.5f;
                dust.position = player.RotatedRelativePoint(dust.position);
            }

            Vector2 position = player.RotatedRelativePoint(new Vector2(player.itemLocation.X + 12f * player.direction + player.velocity.X, player.itemLocation.Y - 14f + player.velocity.Y), true);

            Lighting.AddLight(position, 1f, 0f, 1f);
        }
    }
}
