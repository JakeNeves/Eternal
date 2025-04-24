using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Tiles.Decorative
{
    public class GehennaBrick : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            DustType = DustID.Stone;
            AddMapEntry(new Color(50, 10, 10));
            HitSound = SoundID.Tink;
            MineResist = 2f;
        }
    }
}
