using Eternal.Items.Materials;
using Eternal.Projectiles.Weapons.Throwing;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Weapons.Throwing
{
    public class DroxCybercutter : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("[c/FCA5033:Throwing Item]\nThrows a disk that follows your cursor\n'Caution, Cybersharp! Use with Care'");
        }

        public override void SetDefaults()
        {
            item.damage = 168;
            item.width = 64;
            item.height = 64;
            item.ranged = true;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.channel = true;
            item.knockBack = 8.3f;
            item.value = Item.sellPrice(gold: 3, silver: 18);
            item.rare = ItemRarityID.Lime;
            item.UseSound = SoundID.Item1;
            item.shoot = ProjectileType<DroxCybercutterProj>();
            item.shootSpeed = 10f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<DroxCore>());
            recipe.AddIngredient(ItemType<DroxPlate>(), 14);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
