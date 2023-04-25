using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Content.Rarities;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Eternal.Content.Items.Placeable
{
    public class ElectriteOre : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("A fragment of electrifying energy\n'Shocking to the touch'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 14;
            Item.height = 16;
            Item.value = Item.sellPrice(gold: 50);
            Item.rare = ModContent.RarityType<Teal>();
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 10;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<Tiles.ElectriteOre>();
            Item.maxStack = 9999;
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Lighting.AddLight(Item.Center, 2.08f, 2.53f, 2.35f);
        }
    }
}
