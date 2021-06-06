using Eternal.Items.Materials;
using Eternal.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Accessories
{
    public class CometGauntlet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Increased Melee Damage 60%" +
                               "\n'The comets are now in the palm of your hand'");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 30;
            item.accessory = true;
            item.value = Item.sellPrice(gold: 30);
            item.rare = ItemRarityID.Red;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            EternalGlobalProjectile.cometGauntlet = true;

            player.meleeDamage += 0.6f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileType<Starforge>());
            recipe.AddIngredient(ItemType<GalaxianPlating>(), 20);
            recipe.AddIngredient(ItemType<StarmetalBar>(), 40);
            recipe.AddIngredient(ItemType<InterstellarSingularity>(), 10);
            recipe.AddIngredient(ItemID.FireGauntlet);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
