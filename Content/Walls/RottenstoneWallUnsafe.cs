using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Walls
{
    public class RottenstoneWallUnsafe : ModWall
    {
        public override void SetStaticDefaults()
        {
            DustType = DustID.GreenBlood;
            AddMapEntry(new Color(10, 20, 10));

            WallID.Sets.Conversion.Stone[Type] = true;

            RegisterItemDrop(ModContent.ItemType<Items.Placeable.RottenstoneWall>());
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}