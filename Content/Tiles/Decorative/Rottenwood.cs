using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Tiles.Decorative
{
    public class Rottenwood : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            DustType = DustID.GreenBlood;
            AddMapEntry(new Color(50, 200, 50));
            HitSound = SoundID.Dig;
            MineResist = 2f;
        }
    }
}
