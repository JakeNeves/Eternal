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
            Item.damage = 250;
            Item.useAnimation = 12;
            Item.useTime = 12;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 0f;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item39;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<PsionicKnivesProjectile>();
            Item.shootSpeed = 12f;
            Item.channel = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<ValtoricKnives>())
                .AddIngredient(ModContent.ItemType<SpiritRites>())
                .Register();
        }
    }
}
