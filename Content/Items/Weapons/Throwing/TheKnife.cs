using Eternal.Common.Configurations;
using Eternal.Common.Systems;
using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Weapons.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Throwing
{
    public class TheKnife : ModItem
    {
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
            Item.shoot = ModContent.ProjectileType<TheKnifeProjectile>();
            Item.shootSpeed = 20f;
            Item.autoReuse = true;
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            if (DownedBossSystem.downedNiades)
                damage += 0.25f;
            if (DownedBossSystem.downedIncinerius)
                damage += 0.5f;
            if (NPC.downedGolemBoss)
                damage += 0.75f;
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
