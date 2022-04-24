using Terraria;

namespace Eternal
{
    public static class GeneralUtils
    {

        public static bool PlacementRange(this Player player, int x, int y) =>
            player.position.X / 16f - Player.tileRangeX - player.inventory[player.selectedItem].tileBoost - player.blockRange <= x
            && (player.position.X + player.width) / 16f + Player.tileRangeX + player.inventory[player.selectedItem].tileBoost - 1f + player.blockRange >= x
            && player.position.Y / 16f - Player.tileRangeY - player.inventory[player.selectedItem].tileBoost - player.blockRange <= y
            && (player.position.Y + player.height) / 16f + Player.tileRangeY + player.inventory[player.selectedItem].tileBoost - 2f + player.blockRange >= y;

    }
}
