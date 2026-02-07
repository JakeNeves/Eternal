using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Weapons.Ranged;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Ammo
{
    public class EmberArrow : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
        }

        public override void SetDefaults()
        {
            Item.damage = 76;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 14;
            Item.height = 34;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.knockBack = 4f;
            Item.rare = ItemRarityID.Lime;
            Item.shoot = ModContent.ProjectileType<EmberArrowProjectile>();
            Item.shootSpeed = 20.5f;
            Item.ammo = AmmoID.Arrow;
        }

        public override void AddRecipes()
        {
            CreateRecipe(333)
                .AddTile(TileID.MythrilAnvil)
                .AddIngredient(ModContent.ItemType<MagmaticAlloy>(), 6)
                .AddIngredient(ModContent.ItemType<InfernalAshes>(), 3)
                .Register();
        }

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.Ammo;
        }
    }
}
