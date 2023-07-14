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
            /* Tooltip.SetDefault("'Now where have I seen this before?'" +
                             "\n[c/FC036B:Developer Item]" +
                             "\nDedicated to [c/038CFC:JakeTEM]"); */

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 102;
            Item.height = 102;
            Item.damage = 1000;
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6.5f;
            Item.useAnimation = 26;
            Item.useTime = 26;
            Item.shootSpeed = 6f;
            Item.UseSound = SoundID.Item71;
            Item.rare = ModContent.RarityType<Turquoise>();
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
