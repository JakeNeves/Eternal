using Eternal.Common.Configurations;
using Eternal.Content.Projectiles.Misc;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Tiles
{
    public class RottensandBlock : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBrick[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;

            Main.tileSand[Type] = true;
            TileID.Sets.Conversion.Sand[Type] = true;
            TileID.Sets.ForAdvancedCollision.ForSandshark[Type] = true;
            TileID.Sets.CanBeDugByShovel[Type] = true;
            TileID.Sets.Falling[Type] = true;
            TileID.Sets.Suffocate[Type] = true;
            TileID.Sets.FallingBlockProjectile[Type] = new TileID.Sets.FallingBlockProjectileInfo(ModContent.ProjectileType<RottensandBallFallingProjectile>(), 10);

            TileID.Sets.CanBeClearedDuringOreRunner[Type] = true;
            TileID.Sets.GeneralPlacementTiles[Type] = false;
            TileID.Sets.ChecksForMerge[Type] = true;

            TileID.Sets.Conversion.Sand[Type] = true;

            DustType = DustID.GreenBlood;
            AddMapEntry(new Color(41, 51, 26));
            HitSound = SoundID.Dig;
            MineResist = 0.5f;
        }

        public override void RandomUpdate(int i, int j)
        {
            List<Point> adjacents = OpenAdjacents(i, j, TileID.Sand);
            if (adjacents.Count > 0)
            {
                Point p = adjacents[Main.rand.Next(adjacents.Count)];
                if (HasOpening(p.X, p.Y))
                {
                    Framing.GetTileSafely(p.X, p.Y).TileType = (ushort)ModContent.TileType<RottensandBlock>();
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
                Framing.GetTileSafely(i, j).TileType = TileID.Sand;
            }
        }

        public override bool HasWalkDust()
        {
            return Main.rand.NextBool(3);
        }

        public override void WalkDust(ref int dustType, ref bool makeDust, ref Color color)
        {
            dustType = DustID.GreenBlood;
        }
    }
}
