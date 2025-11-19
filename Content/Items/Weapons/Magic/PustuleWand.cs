using Eternal.Content.Projectiles.Weapons.Magic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Magic
{
    public class PustuleWand : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 28;
            Item.damage = 100;
            Item.DamageType = DamageClass.Magic;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.mana = 12;
            Item.knockBack = 2f;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.shoot = ModContent.ProjectileType<PustuleWandProjectile>();
            Item.shootSpeed = 10f;
            Item.UseSound = SoundID.Item111;
            Item.rare = ItemRarityID.Yellow;
            Item.autoReuse = true;
            Item.noMelee = true;
        }
    }
}
