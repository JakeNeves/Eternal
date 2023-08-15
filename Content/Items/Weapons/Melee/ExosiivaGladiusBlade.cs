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
    public class ExosiivaGladiusBlade : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 102;
            Item.height = 102;
            Item.damage = 1000;
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 18f;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.shootSpeed = 8f;
            Item.UseSound = SoundID.Item71;
            Item.rare = ModContent.RarityType<Turquoise>();
            Item.shoot = ModContent.ProjectileType<ExosiivaGladiusBladeProjectile>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<CosmoniumFragment>())
                .AddIngredient(ModContent.ItemType<StargloomCometiteBar>(), 24)
                .AddIngredient(ModContent.ItemType<RefinedArkrystalSheets>(), 16)
                .AddIngredient(ModContent.ItemType<ApparitionalMatter>(), 16)
                .AddTile(ModContent.TileType<Nanoforge>())
                .Register();
        }
    }
}
