using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Eternal.Common.Configurations;

namespace Eternal.Content.Items.Glyphs
{
    public class LimbolicGlyphZ : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ServerConfig.instance.limbolicGlyphs;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
        }
    }
}
