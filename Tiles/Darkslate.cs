using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Tiles
{
    public class Darkslate : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = false;
            dustType = DustID.Stone;
            drop = ItemType<Items.Placeable.Darkslate>();
            AddMapEntry(new Color(50, 50, 50));
            minPick = 70;
            soundType = SoundID.Tink;
            soundStyle = 1;
            mineResist = 4f;
        }
    }
}
