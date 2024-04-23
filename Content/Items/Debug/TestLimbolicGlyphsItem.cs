using Eternal.Common.Configurations;
using Eternal.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Debug
{
    public class TestLimbolicGlyphsItem : ModItem
    {
        public override string Texture => "Eternal/Content/Items/Misc/LetterofRecommendation";

        public override bool IsLoadingEnabled(Mod mod)
        {
            return ServerConfig.instance.limbolicGlyphs;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 26;
        }
    }
}
