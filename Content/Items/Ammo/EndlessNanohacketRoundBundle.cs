using Eternal.Content.Projectiles.Weapons.Ranged;
using Eternal.Content.Tiles.CraftingStations;
using Eternal.Content.Rarities;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace Eternal.Content.Items.Ammo
{
    public class EndlessNanohacketRoundBundle : ModItem
    {
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs("Unlimited Ammo" +
                                                                            "\n'For slicing and dicing!'");

        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 500;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 26;
            Item.height = 22;
            Item.knockBack = 2f;
            Item.rare = ModContent.RarityType<Turquoise>();
            Item.shoot = ModContent.ProjectileType<NanohacketRoundProjectile>();
            Item.shootSpeed = 24f;
            Item.ammo = AmmoID.Bullet;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(ModContent.TileType<Nanoforge>())
                .AddIngredient(ModContent.ItemType<NanohacketRound>(), 3996)
                .Register();
        }

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.Ammo;
        }
    }
}
