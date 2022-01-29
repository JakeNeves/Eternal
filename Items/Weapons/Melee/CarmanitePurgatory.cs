using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons.Melee
{
    public class CarmanitePurgatory : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.melee = true;
            item.rare = ItemRarityID.Orange;
            item.damage = 24;
            item.useAnimation = 18;
            item.useTime = 18;
            item.useStyle = ItemUseStyleID.Stabbing;
            item.knockBack = 2.4f;
            item.autoReuse = true;
            item.UseSound = SoundID.Item1;
        }
    }
}
