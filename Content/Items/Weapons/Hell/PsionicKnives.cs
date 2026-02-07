using Eternal.Content.Items.Materials;
using Eternal.Content.Items.Weapons.Melee;
using Eternal.Content.Projectiles.Weapons.Hell;
using Eternal.Content.Rarities;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Common.Systems;

namespace Eternal.Content.Items.Weapons.Hell
{
    public class PsionicKnives : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.DamageType = DamageClass.Melee;
            Item.rare = ModContent.RarityType<HellMode>();
            Item.damage = 90;
            Item.useAnimation = 12;
            Item.useTime = 12;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 0f;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item39;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<PsionicKnivesProjectile>();
            Item.shootSpeed = 12f;
            Item.channel = true;
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            if (DownedBossSystem.downedArkofImperious)
                damage += 0.25f;
            if (DownedBossSystem.downedTrinity)
                damage += 0.5f;
        }

        public override bool MeleePrefix() => true;

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<ValtoricKnives>())
                .AddIngredient(ModContent.ItemType<SpiritRites>())
                .Register();
        }
    }
}
