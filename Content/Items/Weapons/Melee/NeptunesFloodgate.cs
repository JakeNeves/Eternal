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
    public class NeptunesFloodgate : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 72;
            Item.height = 72;
            Item.DamageType = DamageClass.Melee;
            Item.rare = ModContent.RarityType<Azure>();
            Item.value = Item.buyPrice(platinum: 2, gold: 90);
            Item.damage = 330;
            Item.useAnimation = 24;
            Item.useTime = 24;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 0f;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<NeptunesFloodgateProjectile>();
            Item.shootSpeed = 12f;
            Item.noUseGraphic = true;
            Item.noMelee = true;
        }

        public override bool MeleePrefix() => true;

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<ProviditeBar>(), 16)
                .AddIngredient(ModContent.ItemType<CoreofExodus>(), 6)
                .AddIngredient(ModContent.ItemType<MindCrystalCluster>())
                .AddIngredient(ModContent.ItemType<BodyCrystalCluster>())
                .AddIngredient(ModContent.ItemType<SoulCrystalCluster>())
                .AddTile(ModContent.TileType<AncientFoundry>())
                .Register();
        }
    }
}
