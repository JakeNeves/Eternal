using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Tiles
{
    class TritalodiumOre : ModTile
    {
        public override void SetDefaults()
        {
			TileID.Sets.Ore[Type] = true;
			Main.tileSpelunker[Type] = true;
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = false;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = false;
			drop = ItemType<Items.Placeable.TritalodiumOre>();
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Tritalodium");
			AddMapEntry(new Color(0, 25, 50), name);
			minPick = 55;
			soundType = SoundID.Tink;
			soundStyle = 1;
			mineResist = 5f;
		}
    }
}
