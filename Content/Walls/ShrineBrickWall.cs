using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Walls
{
    public class ShrineBrickWall : ModWall
    {
        public override void SetStaticDefaults()
        {
            DustType = DustID.Stone;
            Main.wallHouse[Type] = false;
            ItemDrop = ModContent.ItemType<Items.Placeable.ShrineBrickWall>();
            AddMapEntry(new Color(16, 20, 26));
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}