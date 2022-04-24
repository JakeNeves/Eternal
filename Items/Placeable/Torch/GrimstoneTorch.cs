using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Placeable.Torch
{
    public class GrimstoneTorch : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 14;
            item.maxStack = 99;
            item.holdStyle = ItemHoldStyleID.HoldingOut;
            item.noWet = true;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.createTile = ModContent.TileType<Tiles.Torch.GrimstoneTorch>();
            item.flame = true;
        }

        public override void HoldItem(Player player)
        {
            if (Main.rand.Next(player.itemAnimation > 0 ? 40 : 80) == 0)
            {
                Dust.NewDust(new Vector2(player.itemLocation.X + 16f * player.direction, player.itemLocation.Y - 14f * player.gravDir), 4, 4, DustID.Electric);
            }
            Vector2 position = player.RotatedRelativePoint(new Vector2(player.itemLocation.X + 12f * player.direction + player.velocity.X, player.itemLocation.Y - 14f + player.velocity.Y), true);
            Lighting.AddLight(position, 0.43f, 2.51f, 2.51f);
        }

        public override void AutoLightSelect(ref bool dryTorch, ref bool wetTorch, ref bool glowstick)
        {
            dryTorch = true;
        }

        public override void PostUpdate()
        {
            if (!item.wet)
            {
                Lighting.AddLight((int)((item.position.X + item.width / 2) / 16f), (int)((item.position.Y + item.height / 2) / 16f), 0.43f, 2.51f, 2.51f);
            }
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Torch);
            recipe.AddIngredient(ModContent.ItemType<Grimstone>());
            recipe.AddIngredient(ModContent.ItemType<CoraliumSludge>());
            recipe.SetResult(this, 3);
            recipe.AddRecipe();
        }
    }
}
