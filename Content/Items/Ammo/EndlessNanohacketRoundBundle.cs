using Eternal.Content.Projectiles.Weapons.Ranged;
using Eternal.Content.Tiles.CraftingStations;
using Eternal.Content.Rarities;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Ammo
{
    public class EndlessNanohacketRoundBundle : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Unlimited Ammo" +
                            "\n'For slicing and dicing!'");

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
                .AddTile(ModContent.TileType<Reconstructatorium>())
                .AddIngredient(ModContent.ItemType<NanohacketRound>(), 3996)
                .Register();
        }
    }
}
