using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Tiles
{
    class ThunderiteOre : ModTile
    {

		public override void SetDefaults()
		{
			TileID.Sets.Ore[Type] = true;
			Main.tileSpelunker[Type] = true;
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = false;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = false;
			drop = ItemType<Items.Placeable.ThunderiteOre>();
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Thunderite");
			AddMapEntry(new Color(245, 225, 20), name);
			minPick = 225;
			soundType = SoundID.Tink;
			soundStyle = 1;
			mineResist = 5f;
		}

	}
}
