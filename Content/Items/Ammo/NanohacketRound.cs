using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Weapons.Ranged;
using Eternal.Content.Tiles.CraftingStations;
using Eternal.Content.Rarities;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Ammo
{
    public class NanohacketRound : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'For slicing and dicing!'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
        }

        public override void SetDefaults()
        {
            Item.damage = 500;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 18;
            Item.height = 18;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.knockBack = 2f;
            Item.rare = ModContent.RarityType<Turquoise>();
            Item.shoot = ModContent.ProjectileType<NanohacketRoundProjectile>();
            Item.shootSpeed = 24f;
            Item.ammo = AmmoID.Bullet;
        }

        public override void AddRecipes()
        {
            CreateRecipe(333)
                .AddTile(ModContent.TileType<Reconstructatorium>())
                .AddIngredient(ModContent.ItemType<ConcintratedHardStone>())
                .Register();
        }
    }
}
