using Eternal.Content.Projectiles.Weapons.Radiant;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Radiant
{
    public class MagicalCastingPestle : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Shoots a pebble that shoots small pebbles");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 10;
            Item.DamageType = ModContent.GetInstance<DamageClasses.Radiant>();
            Item.width = 18;
            Item.height = 18;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 1.5f;
            Item.value = Item.sellPrice(silver: 9);
            Item.rare = ItemRarityID.Blue;
            Item.autoReuse = true;
            Item.shootSpeed = 2f;
            Item.shoot = ModContent.ProjectileType<Pebble>();
            Item.UseSound = SoundID.Item8;
        }

    }
}
