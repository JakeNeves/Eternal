using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Tiles
{
    class Dunestone : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = false;
            drop = ItemType<Items.Placeable.Dunestone>();
            AddMapEntry(new Color(247, 236, 171));
            soundType = SoundID.Tink;
            soundStyle = 1;
            mineResist = 5f;
        }
    }
}
