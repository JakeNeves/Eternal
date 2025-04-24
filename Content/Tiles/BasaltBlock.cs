using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Tiles
{
    public class BasaltBlock : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            DustType = DustID.Stone;
            AddMapEntry(new Color(10, 10, 10));
            MinPick = 100;
            HitSound = SoundID.Tink;
            MineResist = 2f;
        }
    }
}
