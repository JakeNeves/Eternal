using Eternal.Content.Biomes;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Tiles
{
    public class RottenstoneBlock : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileMerge[Type][TileID.Stone] = true;
            DustType = DustID.GreenBlood;
            AddMapEntry(new Color(41, 51, 26));
            MinPick = 100;
            HitSound = SoundID.Tink;
            MineResist = 4.25f;

            TileID.Sets.Stone[Type] = true;
            TileID.Sets.Conversion.Stone[Type] = true;

            TileID.Sets.CanBeClearedDuringOreRunner[Type] = true;
            TileID.Sets.GeneralPlacementTiles[Type] = false;
            TileID.Sets.ChecksForMerge[Type] = true;
        }

        public override void ChangeWaterfallStyle(ref int style)
        {
            style = ModContent.GetInstance<CarrionWaterfallStyle>().Slot;
        }

        public override void RandomUpdate(int i, int j)
        {
            List<Point> adjacents = OpenAdjacents(i, j, TileID.Stone);
            if (adjacents.Count > 0)
            {
                Point p = adjacents[Main.rand.Next(adjacents.Count)];
                if (HasOpening(p.X, p.Y))
                {
                    Framing.GetTileSafely(p.X, p.Y).TileType = (ushort)ModContent.TileType<RottenstoneBlock>();
                    if (Main.netMode == NetmodeID.Server)
                        NetMessage.SendTileSquare(-1, p.X, p.Y, 1, TileChangeType.None);
                }
            }
        }
        
        public override void Convert(int i, int j, int conversionType)
        {
            switch (conversionType)
            {
                case BiomeConversionID.Purity:
                    WorldGen.ConvertTile(i, j, TileID.Stone);
                    return;
            }
        }

        private List<Point> OpenAdjacents(int i, int j, int type)
        {
            var p = new List<Point>();
            for (int k = -1; k < 2; ++k)
                for (int l = -1; l < 2; ++l)
                    if (!(l == 0 && k == 0) && Framing.GetTileSafely(i + k, j + l).HasTile && Framing.GetTileSafely(i + k, j + l).TileType == type)
                        p.Add(new Point(i + k, j + l));
            return p;
        }

        private bool HasOpening(int i, int j)
        {
            for (int k = -1; k < 2; ++k)
                for (int l = -1; l < 2; ++l)
                    if (!Framing.GetTileSafely(i + k, j + l).HasTile)
                        return true;
            return false;
        }
    }
}
