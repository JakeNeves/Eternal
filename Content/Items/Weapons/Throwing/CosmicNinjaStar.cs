using Eternal.Content.Projectiles.Weapons.Ranged;
using Eternal.Content.Rarities;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Throwing
{
    public class CosmicNinjaStar : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.DamageType = DamageClass.Ranged;
            Item.rare = ModContent.RarityType<Teal>();
            Item.damage = 400;
            Item.useAnimation = 12;
            Item.useTime = 12;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 2f;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<CosmicNinjaStarProjectile>();
            Item.shootSpeed = 24f;
        }
    }
}
