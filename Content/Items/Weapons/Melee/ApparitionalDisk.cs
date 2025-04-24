using Eternal.Content.Projectiles.Weapons.Melee;
using Eternal.Content.Rarities;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class ApparitionalDisk : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 60;
            Item.DamageType = DamageClass.Melee;
            Item.rare = ModContent.RarityType<Teal>();
            Item.noMelee = true;
            Item.damage = 100;
            Item.useAnimation = 16;
            Item.useTime = 16;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<ApparitionalDiskProjectile>();
            Item.shootSpeed = 16f;
            Item.noUseGraphic = true;
            Item.knockBack = 3f;
        }
    }
}
