using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Tiles.Decorative
{
    public class MausoleumBrick : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            DustType = DustID.Stone;
            AddMapEntry(new Color(20, 10, 20));
            HitSound = SoundID.Tink;
            MineResist = 2f;
        }
    }
}
