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
    public class RiftsDivinity : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 86;
            Item.height = 86;
            Item.damage = 240;
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 18f;
            Item.useAnimation = 18;
            Item.useTime = 18;
            Item.shootSpeed = 6f;
            Item.UseSound = SoundID.Item71;
            Item.rare = ModContent.RarityType<Magenta>();
            Item.shoot = ModContent.ProjectileType<RiftsDivinityProjectile>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AspectofTheShiftedWarlock>())
                .AddIngredient(ModContent.ItemType<WeaponsGradeNaquadahAlloy>(), 20)
                .AddIngredient(ModContent.ItemType<OminaquaditeBar>(), 20)
                .AddTile(ModContent.TileType<AncientFoundry>())
                .Register();
        }
    }
}
