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
    public class Justifier : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 56;
            Item.height = 56;
            Item.DamageType = DamageClass.Melee;
            Item.rare = ModContent.RarityType<Aquamarine>();
            Item.noMelee = true;
            Item.damage = 1000;
            Item.useAnimation = 12;
            Item.useTime = 12;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<JustifierProjectile>();
            Item.shootSpeed = 12f;
            Item.noUseGraphic = true;
            Item.knockBack = 2f;
        }

        public override bool MeleePrefix() => true;

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<PureGavel>())
                .AddIngredient(ModContent.ItemType<EternalQuartz>(), 4)
                .AddIngredient(ModContent.ItemType<CosmoniumFragment>(), 4)
                .AddIngredient(ModContent.ItemType<InterstellarMetal>(), 6)
                .AddTile(ModContent.TileType<Nanoforge>())
                .Register();
        }
    }
}
