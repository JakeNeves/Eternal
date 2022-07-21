using Eternal.Content.Projectiles.Weapons.Hell;
using Eternal.Content.Rarities;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Hell
{
    public class DuneEviscrator : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Hell Mode drop");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.DamageType = DamageClass.Melee;
            Item.rare = ModContent.RarityType<HellMode>();
            Item.damage = 80;
            Item.useAnimation = 18;
            Item.useTime = 18;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 2f;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<DuneEviscratorProjectile>();
            Item.shootSpeed = 10.2f;
            Item.noUseGraphic = true;
        }
    }
}
