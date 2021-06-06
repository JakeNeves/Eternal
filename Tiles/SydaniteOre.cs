using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Tiles
{
    class SydaniteOre : ModTile
    {

		public override void SetDefaults()
		{
			TileID.Sets.Ore[Type] = true;
			Main.tileSpelunker[Type] = true;
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = false;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = false;
			dustType = DustID.Ice;
			drop = ItemType<Items.Placeable.SydaniteOre>();
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Sydanite");
			AddMapEntry(new Color(25, 230, 250), name);
			minPick = 225;
			soundType = SoundID.Tink;
			soundStyle = 1;
			mineResist = 5f;
		}

	}
}
