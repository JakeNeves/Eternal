using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Weapons.Melee
{
    public class CarmaniteBane : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 30;
            item.melee = true;
            item.rare = ItemRarityID.Green;
            item.damage = 18;
            item.useAnimation = 18;
            item.useTime = 18;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 2.4f;
            item.autoReuse = true;
            item.UseSound = SoundID.Item1;
        }
    }
}
