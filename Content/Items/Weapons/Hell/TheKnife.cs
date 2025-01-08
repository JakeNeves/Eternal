using Eternal.Common.Configurations;
using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Weapons.Hell;
using Eternal.Content.Rarities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Hell
{
    public class TheKnife : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return !ServerConfig.instance.update14;
        }

        public override void SetDefaults()
        {
            Item.damage = 60;
            Item.DamageType = DamageClass.Ranged;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.UseSound = SoundID.Item1;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.width = 26;
            Item.height = 46;
            Item.knockBack = 2.5f;
            Item.rare = ModContent.RarityType<HellMode>();
            Item.shoot = ModContent.ProjectileType<TheKnifeProjectile>();
            Item.shootSpeed = 20f;
            Item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<KnifeHandle>())
                .AddIngredient(ModContent.ItemType<KnifeBlade>())
                .Register();
        }
    }
}
