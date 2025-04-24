using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Walls
{
    public class BasaltBrickWall : ModWall
    {
        public override void SetStaticDefaults()
        {
            DustType = DustID.Stone;
            Main.wallHouse[Type] = false;
            AddMapEntry(new Color(150, 150, 150));
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}