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
    public class StarcrescentMoondisk : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 42;
            Item.DamageType = DamageClass.Melee;
            Item.rare = ModContent.RarityType<Aquamarine>();
            Item.noMelee = true;
            Item.damage = 1000;
            Item.useAnimation = 16;
            Item.useTime = 16;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<StarcrescentMoondiskProjectile>();
            Item.shootSpeed = 6f;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.knockBack = 2f;
        }

        public override bool MeleePrefix() => true;

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<CosmoniumFragment>(), 3)
                .AddIngredient(ModContent.ItemType<InterstellarMetal>(), 6)
                .AddIngredient(ModContent.ItemType<AwakenedCometiteBar>(), 30)
                .AddIngredient(ModContent.ItemType<ApparitionalDisk>())
                .AddTile(ModContent.TileType<Nanoforge>())
                .Register();
        }
    }
}
