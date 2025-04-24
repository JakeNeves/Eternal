using Eternal.Content.Projectiles.Weapons.Magic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Magic
{
    public class BookofTar : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 28;
            Item.damage = 16;
            Item.DamageType = DamageClass.Magic;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.mana = 6;
            Item.knockBack = 6f;
            Item.useAnimation = 16;
            Item.useTime = 16;
            Item.shoot = ModContent.ProjectileType<Tarball>();
            Item.shootSpeed = 8f;
            Item.UseSound = SoundID.Item111;
            Item.rare = ItemRarityID.Orange;
            Item.autoReuse = true;
            Item.noMelee = true;
        }
    }
}
