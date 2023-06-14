using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Weapons.Melee;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class Hyperion : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 54;
            Item.DamageType = DamageClass.Melee;
            Item.rare = ModContent.RarityType<Magenta>();
            Item.damage = 512;
            Item.useAnimation = 12;
            Item.useTime = 12;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 2f;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<HyperionProjectile>();
            Item.shootSpeed = 2.75f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<UnrefinedHeroSword>())
                .AddIngredient(ModContent.ItemType<ThunderblightCrystal>(), 12)
                .AddIngredient(ModContent.ItemType<CometiteBar>(), 24)
                .AddIngredient(ModContent.ItemType<CometiteCrystal>(), 36)
                .AddIngredient(ModContent.ItemType<WeatheredPlating>(), 48)
                .AddTile(ModContent.TileType<AncientForge>())
                .Register();
        }
    }
}
