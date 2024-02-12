using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Ranged
{
    public class CarminiteShortbow : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 32;
            Item.damage = 22;
            Item.knockBack = 2.6f;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.shootSpeed = 6f;
            Item.shoot = AmmoID.Arrow;
            Item.useAmmo = AmmoID.Arrow;
            Item.rare = ItemRarityID.Orange;
        }
    }
}
