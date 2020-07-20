using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items
{
    class CreatorsMessage : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Creator's Message");
            Tooltip.SetDefault("[c/3C3C3C:Message]\nPlease note that this mod is not finished yet, you can play around with this mod\nas long as you either have the HERO's or Cheat Sheet mod!\nThank you, enjoy the mod.\n-JakeTEM, Mod Creator");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
        }
    }
}
