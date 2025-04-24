using Eternal.Content.Items.Materials;
using Eternal.Content.Items.Weapons.Melee;
using Eternal.Content.Projectiles.Weapons.Hell;
using Eternal.Content.Rarities;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Hell
{
    public class GehennasRemedy : ModItem
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
            Item.rare = ModContent.RarityType<HellMode>();
            Item.damage = 50;
            Item.useAnimation = 12;
            Item.useTime = 12;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 2.5f;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<GehennasRemedyProjectile>();
            Item.shootSpeed = 8f;
            Item.noUseGraphic = true;
            Item.noMelee = true;
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            damage += (player.statDefense + player.endurance) / 4f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<HellHacker>())
                .AddIngredient(ModContent.ItemType<SpiritRites>())
                .Register();
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }
    }
}
