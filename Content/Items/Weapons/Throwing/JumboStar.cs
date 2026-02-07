using Eternal.Common.Systems;
using Eternal.Content.Projectiles.Weapons.Ranged;
using Eternal.Content.Rarities;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Throwing
{
    public class JumboStar : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 74;
            Item.height = 74;
            Item.DamageType = DamageClass.Ranged;
            Item.rare = ModContent.RarityType<Teal>();
            Item.damage = 80;
            Item.useAnimation = 12;
            Item.useTime = 12;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 0f;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<JumboStarProjectile>();
            Item.shootSpeed = 12f;
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            if (DownedBossSystem.downedArkofImperious)
                damage += 0.25f;
            if (DownedBossSystem.downedTrinity)
                damage += 0.5f;
        }
    }
}
