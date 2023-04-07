using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Weapons.Magic;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Magic
{
    public class StarstaveDrei : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Starstave Drei");
            // Tooltip.SetDefault("Fires a bouncing bolt that explodes into four");
            Item.staff[Item.type] = true;

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 44;
            Item.damage = 600;
            Item.DamageType = DamageClass.Magic;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.mana = 14;
            Item.knockBack = 4f;
            Item.useAnimation = 10;
            Item.useTime = 10;
            Item.shoot = ModContent.ProjectileType<StarstaveProjectileDrei>();
            Item.shootSpeed = 10f;
            Item.UseSound = SoundID.Item8;
            Item.rare = ModContent.RarityType<Magenta>();
            Item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<StarstaveZwei>())
                .AddIngredient(ModContent.ItemType<ApparitionalMatter>(), 12)
                .AddIngredient(ModContent.ItemType<StarmetalBar>(), 16)
                .AddIngredient(ModContent.ItemType<CometiteCrystal>(), 20)
                .AddIngredient(ModContent.ItemType<CometiteBar>(), 20)
                .AddTile(ModContent.TileType<AncientForge>())
                .Register();
        }
    }
}
