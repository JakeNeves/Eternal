using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Tiles
{
    class Rudanium : ModTile
    {
        public override void SetDefaults()
        {
            TileID.Sets.Ore[Type] = true;
            Main.tileSpelunker[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = false;
            drop = ItemType<Items.Placeable.Rudanium>();
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Rudanium");
            AddMapEntry(new Color(50, 0, 0), name);
            minPick = 150;
            soundType = SoundID.Tink;
            soundStyle = 1;
            mineResist = 5f;
        }
    }
}
