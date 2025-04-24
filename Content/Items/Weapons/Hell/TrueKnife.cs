using Eternal.Content.Items.Materials;
using Eternal.Content.Items.Weapons.Throwing;
using Eternal.Content.Projectiles.Weapons.Hell;
using Eternal.Content.Rarities;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Hell
{
    public class TrueKnife : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 200;
            Item.DamageType = DamageClass.Ranged;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.UseSound = SoundID.Item1;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.width = 34;
            Item.height = 36;
            Item.knockBack = 2.5f;
            Item.rare = ModContent.RarityType<HellMode>();
            Item.shoot = ModContent.ProjectileType<TrueKnifeProjectile>();
            Item.shootSpeed = 20f;
            Item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<TheKnife>())
                .AddIngredient(ModContent.ItemType<SpiritRites>())
                .Register();
        }
    }
}
