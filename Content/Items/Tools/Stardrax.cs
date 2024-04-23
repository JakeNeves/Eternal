using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Misc;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Tools
{
    public class Stardrax : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

            ItemID.Sets.IsDrill[Type] = true;
            ItemID.Sets.IsChainsaw[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.tileBoost = 12;
            Item.damage = 200;
            Item.DamageType = DamageClass.Melee;
            Item.width = 72;
            Item.height = 30;
            Item.useTime = 4;
            Item.useAnimation = 15;
            Item.axe = 50;
            Item.pick = 230;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 12;
            Item.value = Item.buyPrice(gold: 30, silver: 75);
            Item.rare = ModContent.RarityType<Teal>();
            Item.UseSound = SoundID.Item23;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<StardraxProjectile>();
            Item.shootSpeed = 32f;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.channel = true;
        }

        // Holding off the recipe, the Stardrax needs some more work to be finished.
        /*
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<CometiteBar>(), 4)
                .AddIngredient(ModContent.ItemType<StarmetalBar>(), 5)
                .AddIngredient(ModContent.ItemType<Astragel>(), 10)
                .AddTile(ModContent.TileType<Starforge>())
                .Register();
        }
        */
    }
}
