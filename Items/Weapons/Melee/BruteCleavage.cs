using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Weapons.Melee
{
    public class BruteCleavage : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 54;
            item.melee = true;
            item.rare = ItemRarityID.Green;
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
