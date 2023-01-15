using Eternal.Content.Projectiles.Weapons.Melee;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class BifrostPrisimdisk : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("\n'I know, it looks bland but it's really cool and colorful when you throw it, trust me!'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 54;
            Item.DamageType = DamageClass.Melee;
            Item.rare = ItemRarityID.Pink;
            Item.damage = 90;
            Item.useAnimation = 12;
            Item.useTime = 12;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 4f;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<BifrostPrisimdiskProjectile>();
            Item.shootSpeed = 6f;
            Item.noUseGraphic = true;
            Item.noMelee = true;
        }
    }
}
