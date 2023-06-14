using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Weapons.Melee;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class ArkrystalChakram : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 74;
            Item.height = 74;
            Item.DamageType = DamageClass.Melee;
            Item.rare = ModContent.RarityType<Magenta>();
            Item.noMelee = true;
            Item.damage = 600;
            Item.useAnimation = 16;
            Item.useTime = 16;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<ArkrystalChakramProjectile>();
            Item.shootSpeed = 8f;
            Item.noUseGraphic = true;
            Item.knockBack = 6f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<RefinedArkrystalSheets>(), 16)
                .AddIngredient(ModContent.ItemType<ArkaniumCompoundSheets>(), 12)
                .AddIngredient(ModContent.ItemType<WeatheredPlating>(), 8)
                .AddTile(ModContent.TileType<AncientForge>())
                .Register();
        }
    }
}
