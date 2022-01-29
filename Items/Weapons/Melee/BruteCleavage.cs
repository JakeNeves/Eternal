using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons.Melee
{
    public class BruteCleavage : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 52;
            item.height = 40;
            item.melee = true;
            item.rare = ItemRarityID.Orange;
            item.damage = 24;
            item.useAnimation = 26;
            item.useTime = 26;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 2f;
            item.autoReuse = true;
            item.UseSound = SoundID.Item1;
        }
    }
}
