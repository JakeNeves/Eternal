using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Tiles.Decorative
{
    public class CometiteBrick : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = false;
            dustType = DustID.Shadowflame;
            drop = ItemType<Items.Placeable.CometiteBrick>();
            AddMapEntry(new Color(60, 40, 80));
            soundType = SoundID.Tink;
            soundStyle = 1;
            mineResist = 1.2f;
        }
    }
}
