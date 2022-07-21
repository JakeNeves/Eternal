using Eternal.Content.Projectiles.Weapons.Melee.Shortsword;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class CarminitePurgatory : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.DamageType = DamageClass.Melee;
            Item.rare = ItemRarityID.Orange;
            Item.damage = 24;
            Item.useAnimation = 18;
            Item.useTime = 18;
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.knockBack = 2.4f;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<CarminitePurgatoryProjectile>();
            Item.shootSpeed = 2.1f;
            Item.noUseGraphic = true;
            Item.noMelee = true;
        }
    }
}
