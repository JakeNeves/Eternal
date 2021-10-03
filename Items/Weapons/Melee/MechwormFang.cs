using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons.Melee
{
    public class MechwormFang : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 32;
            item.damage = 2000;
            item.knockBack = 2.75f;
            item.value = Item.sellPrice(platinum: 3);
            item.useTime = 18;
            item.useAnimation = 18;
            item.UseSound = SoundID.Item1;
            item.useStyle = ItemUseStyleID.Stabbing;
            item.rare = ItemRarityID.Red;
            item.autoReuse = true;
            item.melee = true;
        }
    }
}
