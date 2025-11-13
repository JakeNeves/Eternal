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
    public class PureGavel : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.DamageType = DamageClass.Melee;
            Item.rare = ModContent.RarityType<Azure>();
            Item.noMelee = true;
            Item.damage = 350;
            Item.useAnimation = 12;
            Item.useTime = 12;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<PureGavelProjectile>();
            Item.shootSpeed = 6f;
            Item.noUseGraphic = true;
            Item.knockBack = 2f;
        }

        public override bool MeleePrefix() => true;

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.PossessedHatchet)
                .AddIngredient(ModContent.ItemType<ProviditeBar>(), 12)
                .AddIngredient(ModContent.ItemType<CoreofExodus>(), 6)
                .AddIngredient(ModContent.ItemType<EternalQuartz>(), 12)
                .AddIngredient(ModContent.ItemType<MindCrystalCluster>())
                .AddIngredient(ModContent.ItemType<BodyCrystalCluster>())
                .AddIngredient(ModContent.ItemType<SoulCrystalCluster>())
                .AddTile(ModContent.TileType<AncientFoundry>())
                .Register();
        }
    }
}
