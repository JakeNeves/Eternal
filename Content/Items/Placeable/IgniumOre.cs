using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Content.Rarities;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Eternal.Content.Items.Placeable
{
    public class IgniumOre : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("A fragment of igneous energy\n'Searing to the touch'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 14;
            Item.height = 14;
            Item.value = Item.sellPrice(gold: 50);
            Item.rare = ModContent.RarityType<Teal>();
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 10;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.IgniumOre>());
            Item.maxStack = 9999;
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Lighting.AddLight(Item.Center, 2.50f, 1.11f, 0.66f);
        }
    }
}
