using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Weapons.Ranged;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Ammo
{
    public class ArcaneShell : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
        }

        public override void SetDefaults()
        {
            Item.damage = 8;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 10;
            Item.height = 22;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.knockBack = 2f;
            Item.rare = ItemRarityID.Pink;
            Item.shoot = ModContent.ProjectileType<ArcaneShellProjectile>();
            Item.shootSpeed = 18f;
            Item.ammo = AmmoID.Bullet;
        }

        public override void AddRecipes()
        {
            CreateRecipe(333)
                .AddTile(TileID.MythrilAnvil)
                .AddIngredient(ModContent.ItemType<OcculticMatter>())
                .AddIngredient(ModContent.ItemType<PsyblightEssence>())
                .AddIngredient(ModContent.ItemType<CursedAshes>())
                .Register();
        }

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.Ammo;
        }
    }
}
