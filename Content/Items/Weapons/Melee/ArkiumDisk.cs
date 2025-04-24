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
    public class ArkiumDisk : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;
            Item.DamageType = DamageClass.Melee;
            Item.rare = ModContent.RarityType<Magenta>();
            Item.noMelee = true;
            Item.damage = 400;
            Item.useAnimation = 16;
            Item.useTime = 16;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<ArkiumDiskProjectile>();
            Item.shootSpeed = 8f;
            Item.noUseGraphic = true;
            Item.knockBack = 6f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<ArkiumQuartzPlating>(), 16)
                .AddIngredient(ModContent.ItemType<ArkaniumAlloy>(), 12)
                .AddIngredient(ModContent.ItemType<WeatheredPlating>(), 8)
                .AddTile(ModContent.TileType<AncientFoundry>())
                .Register();
        }
    }
}
