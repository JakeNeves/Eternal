using Eternal.Content.Projectiles.Weapons.Whips;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Summon
{
    public class Fleshsnapper : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 24;
            Item.damage = 60;
            Item.DamageType = DamageClass.SummonMeleeSpeed;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 3f;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<FleshsnapperProjectile>();
            Item.shootSpeed = 4;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item152;
            Item.autoReuse = false;
        }
    }
}
