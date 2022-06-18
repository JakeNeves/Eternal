using Eternal.Content.Projectiles.Weapons.Melee;
using Eternal.Content.Rarities;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class SolSythe : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Not to be confused with 'Soul Sythe'");
        }

        public override void SetDefaults()
        {
            Item.width = 66;
            Item.height = 62;
            Item.DamageType = DamageClass.Melee;
            Item.rare = ModContent.RarityType<Teal>();
            Item.damage = 400;
            Item.useAnimation = 12;
            Item.useTime = 12;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 2.5f;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<SolSytheProjectile>();
            Item.shootSpeed = 20f;
            Item.noUseGraphic = true;
            Item.noMelee = true;
        }
    }
}
