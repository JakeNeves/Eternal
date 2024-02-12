using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class CarminiteBroadsword : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 28;
            Item.DamageType = DamageClass.Melee;
            Item.rare = ItemRarityID.Orange;
            Item.damage = 18;
            Item.useAnimation = 18;
            Item.useTime = 18;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 2.4f;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;
        }
    }
}
