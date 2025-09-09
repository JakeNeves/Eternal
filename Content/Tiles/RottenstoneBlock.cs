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

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (!fail)
            {
                fail = true;
                Framing.GetTileSafely(i, j).TileType = TileID.Stone;
            }
        }
    }
}
