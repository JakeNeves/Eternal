using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Walls
{
    public class RottenstoneWall : ModWall
    {
        public override void SetStaticDefaults()
        {
            DustType = DustID.GreenBlood;
            Main.wallHouse[Type] = true;
            AddMapEntry(new Color(10, 20, 10));

            WallID.Sets.Conversion.Stone[Type] = true;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}