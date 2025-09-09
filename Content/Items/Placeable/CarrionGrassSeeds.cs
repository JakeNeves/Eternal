using Eternal.Content.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Placeable
{
    public class CarrionGrassSeeds : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 16;
            Item.value = Item.sellPrice(gold: 50);
            Item.rare = ItemRarityID.White;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 10;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.maxStack = 9999;
        }

        public override bool? UseItem(Player player)
        {
            if (Main.netMode == NetmodeID.Server)
                return false;

            Tile tile = Framing.GetTileSafely(Player.tileTargetX, Player.tileTargetY);
            if (tile.HasTile && tile.TileType == TileID.Dirt && player.WithinRange(new Vector2(Player.tileTargetX, Player.tileTargetY), player.blockRange))
            {
                WorldGen.PlaceTile(Player.tileTargetX, Player.tileTargetY, ModContent.TileType<CarrionGrass>(), forced: true);
                player.inventory[player.selectedItem].stack--;
            }

            return null;
        }
    }
}
