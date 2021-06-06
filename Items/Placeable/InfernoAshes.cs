using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using Terraria;

namespace Eternal.Items.Placeable
{
    public class InfernoAshes : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ashes of Inferno");
            Tooltip.SetDefault("Can be Placed" +
                               "\nCan be Placed on Heatslate" + 
                               "\n'Caution, Burns on Any Natural Surface.'");
        }

        public override void SetDefaults()
        {
            item.autoReuse = true;
            item.useTurn = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 15;
            item.rare = ItemRarityID.Orange;
            item.useTime = 15;
            item.maxStack = 99;
            item.placeStyle = 0;
            item.width = 16;
            item.height = 10;
            item.value = Item.sellPrice(gold: 1);
        }

        public override bool UseItem(Player player)
        {
            if (Main.netMode == NetmodeID.Server)
                return false;

            Tile tile = Framing.GetTileSafely(Player.tileTargetX, Player.tileTargetY);
            if (tile.active() && tile.type == TileType<Tiles.Heatslate>() && player.PlacementRange(Player.tileTargetX, Player.tileTargetY))
            {
                WorldGen.PlaceTile(Player.tileTargetX, Player.tileTargetY, TileType<Tiles.HeatslateGrowth>(), forced: true);
                player.inventory[player.selectedItem].stack--;
            }

            return true;
        }

    }
}
