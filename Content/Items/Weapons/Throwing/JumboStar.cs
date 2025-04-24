using Eternal.Content.Projectiles.Weapons.Ranged;
using Eternal.Content.Rarities;
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
            Item.damage = 200;
            Item.useAnimation = 12;
            Item.useTime = 12;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 0f;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<JumboStarProjectile>();
            Item.shootSpeed = 12f;
        }
    }
}
