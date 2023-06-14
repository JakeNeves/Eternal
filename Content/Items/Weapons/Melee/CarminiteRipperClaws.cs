using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class CarminiteRipperClaws : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 20;
            Item.DamageType = DamageClass.Melee;
            Item.rare = ItemRarityID.Orange;
            Item.damage = 20;
            Item.useAnimation = 8;
            Item.useTime = 8;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 2.4f;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;
        }
    }
}
