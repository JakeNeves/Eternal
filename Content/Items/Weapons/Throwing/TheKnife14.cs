using Eternal.Common.Configurations;
using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Weapons.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Throwing
{
    public class TheKnife14 : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ServerConfig.instance.update14;
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
            Item.width = 30;
            Item.height = 32;
            Item.knockBack = 2.5f;
            Item.rare = ItemRarityID.Lime;
            Item.shoot = ModContent.ProjectileType<TheKnife14Projectile>();
            Item.shootSpeed = 20f;
            Item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<KnifeHandle14>())
                .AddIngredient(ModContent.ItemType<KnifeBlade14>())
                .Register();
        }
    }
}
