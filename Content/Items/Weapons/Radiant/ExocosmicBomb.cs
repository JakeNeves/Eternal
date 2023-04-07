using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Weapons.Radiant;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Radiant
{
    public class ExocosmicBomb : ModItem
    {
        public override void SetStaticDefaults()
        {
            /* Tooltip.SetDefault("[c/ff0000:WARNING] - Having several bombs detonate at once like this can cause lag spikes!" +
                "\n'The unapologetically \"charming\" super bomb!'"); */

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 1000;
            Item.DamageType = ModContent.GetInstance<DamageClasses.Radiant>();
            Item.width = 50;
            Item.height = 50;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.knockBack = 4f;
            Item.crit = 100;
            Item.value = Item.sellPrice(platinum: 5);
            Item.rare = ModContent.RarityType<Turquoise>();
            Item.shootSpeed = 0f;
            Item.shoot = ModContent.ProjectileType<ExocosmicBombProjectile>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<CosmoniumFragment>())
                .AddIngredient(ModContent.ItemType<StargloomCometiteBar>(), 12)
                .AddIngredient(ModContent.ItemType<CoreofEternal>(), 6)
                .AddTile(ModContent.TileType<Reconstructatorium>())
                .Register();
        }
    }
}
