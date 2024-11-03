using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Eternal.Content.Tiles.CraftingStations
{
    public class AncientFoundry : ModTile
    {

        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
            TileObjectData.newTile.Width = 7;
            TileObjectData.newTile.Height = 4;
            TileObjectData.newTile.Origin = new Point16(6, 3);
            TileObjectData.newTile.CoordinateHeights = [ 16, 16, 16, 16 ];
            TileObjectData.newTile.StyleWrapLimit = 2;
            TileObjectData.newTile.StyleMultiplier = 2;
            TileObjectData.addTile(Type);
            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Color(80, 170, 195), name);
            AdjTiles = new int[]
            {
                TileID.WorkBenches,
                TileID.AdamantiteForge,
                TileID.MythrilAnvil,
                TileID.Anvils,
                TileID.Bottles,
                TileID.AlchemyTable,
                TileID.CookingPots,
                TileID.HeavyWorkBench,
                TileID.Blendomatic,
                TileID.LunarCraftingStation,
                TileID.Sawmill,
                TileID.Loom,
                TileID.Kegs,
                ModContent.TileType<Starforge>()
            };
        }

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }
            int height = tile.TileFrameY == 36 ? 18 : 16;
            Main.spriteBatch.Draw(ModContent.Request<Texture2D>("Eternal/Content/Tiles/CraftingStations/AncientFoundry_Glow").Value, new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, height), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

    }
}
