using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Weapons.Melee.Shortsword;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class ArkrystalGladius : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'The Arkrystal Blade's short counterpart'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 44;
            Item.DamageType = DamageClass.Melee;
            Item.rare = ModContent.RarityType<Magenta>();
            Item.damage = 500;
            Item.useAnimation = 16;
            Item.useTime = 16;
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.knockBack = 2.4f;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<ArkrystalGladiusProjectile>();
            Item.shootSpeed = 2f;
            Item.noUseGraphic = true;
            Item.noMelee = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<RefinedArkrystalSheets>(), 24)
                .AddIngredient(ModContent.ItemType<ArkaniumCompoundSheets>(), 15)
                .AddIngredient(ModContent.ItemType<WeatheredPlating>(), 2)
                .AddTile(ModContent.TileType<AncientForge>())
                .Register();
        }
    }
}
