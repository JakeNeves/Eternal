using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Tiles
{
    public class HeatslateGrowth : ModTile
    {
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMerge[Type][TileType<Heatslate>()] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = false;
			dustType = DustID.Fire;
			drop = ItemType<Items.Placeable.Heatslate>();
			AddMapEntry(new Color(255, 154, 32));
			minPick = 100;
			soundType = SoundID.Tink;
			soundStyle = 1;
			mineResist = 4.4f;
		}

		public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
			Tile tile = Framing.GetTileSafely(i, j);
			Tile tile2 = Framing.GetTileSafely(i, j - 1);
			if (tile.slope() == 0 && !tile.halfBrick())
			{
				if (!Main.tileSolid[tile2.type] || !tile2.active())
				{
					Color colour = Color.Orange;

					Texture2D glow = GetTexture("Eternal/Tiles/HeatslateGrowth_Glow");
					Vector2 zero = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange, Main.offScreenRange);

					spriteBatch.Draw(glow, new Vector2(i * 16, j * 16) - Main.screenPosition + zero, new Rectangle(tile.frameX, tile.frameY, 16, 16), colour);
				}
			}
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
			Tile tile = Framing.GetTileSafely(i, j - 1);
			if (!Main.tileSolid[tile.type] || !tile.active())
			{
				r = 2.55f;
				g = 1.54f;
				b = 0.32f;
			}
		}
    }
}
