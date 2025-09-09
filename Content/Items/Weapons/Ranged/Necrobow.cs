using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Ranged
{
    public class Necrobow : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 50;
            Item.damage = 90;
            Item.knockBack = 2.6f;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.shootSpeed = 8f;
            Item.shoot = AmmoID.Arrow;
            Item.useAmmo = AmmoID.Arrow;
            Item.rare = ItemRarityID.Yellow;
        }
    }
}
