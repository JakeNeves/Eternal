using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Placeable
{
    public class CometiteOre : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("A shard of cosmic debris\n'Pure starpower radiates from this cluster'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 16;
            Item.value = Item.sellPrice(gold: 50);
            Item.rare = ItemRarityID.Red;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 10;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.CometiteOre>());
            Item.maxStack = 9999;
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Lighting.AddLight(Item.Center, 1.20f, 0.50f, 1.90f);
        }
    }
}
