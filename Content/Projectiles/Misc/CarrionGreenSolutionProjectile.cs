using Eternal.Content.Dusts;
using Eternal.Content.Tiles;
using Eternal.Content.Walls;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Misc
{
    public class CarrionGreenSolutionProjectile : ModProjectile
    {
        public static int ConversionType;

        public ref float Progress => ref Projectile.ai[0];
        public bool ShotFromTerraformer => Projectile.ai[1] == 1f;

        public override void SetDefaults()
        {
            Projectile.DefaultToSpray();
            Projectile.aiStyle = 0;
        }

        public override void SetStaticDefaults()
        {
            ConversionType = ModContent.GetInstance<CarrionBiomeConversion>().Type;
        }

        public override bool? CanDamage() => false;

        public override void AI()
        {
            if (Projectile.timeLeft > 133)
                Projectile.timeLeft = 133;

            if (Projectile.owner == Main.myPlayer)
            {
                int size = ShotFromTerraformer ? 3 : 2;
                Point tileCenter = Projectile.Center.ToTileCoordinates();
                WorldGen.Convert(tileCenter.X, tileCenter.Y, ConversionType, size);
            }

            int spawnDustTreshold = 7;
            if (ShotFromTerraformer)
                spawnDustTreshold = 3;

            if (Progress > (float)spawnDustTreshold)
            {
                float dustScale = 1f;
                int dustType = ModContent.DustType<CarrionGreenSolution>();

                if (Progress == spawnDustTreshold + 1)
                    dustScale = 0.2f;
                else if (Progress == spawnDustTreshold + 2)
                    dustScale = 0.4f;
                else if (Progress == spawnDustTreshold + 3)
                    dustScale = 0.6f;
                else if (Progress == spawnDustTreshold + 4)
                    dustScale = 0.8f;

                int dustArea = 0;
                if (ShotFromTerraformer)
                {
                    dustScale *= 1.2f;
                    dustArea = (int)(12f * dustScale);
                }

                Dust sprayDust = Dust.NewDustDirect(new Vector2(Projectile.position.X - dustArea, Projectile.position.Y - dustArea), Projectile.width + dustArea * 2, Projectile.height + dustArea * 2, dustType, Projectile.velocity.X * 0.4f, Projectile.velocity.Y * 0.4f, 100);
                sprayDust.noGravity = true;
                sprayDust.scale *= 1.75f * dustScale;
            }

            Progress++;
            Projectile.rotation += 0.3f * Projectile.direction;
        }
    }

    public class CarrionBiomeConversion : ModBiomeConversion
    {
        public static int WallType;
        public static int UnsafeWallType;
        public static int StoneType;
        public static int SandType;
        public static int GrassType;

        public override void PostSetupContent()
        {
            WallType = ModContent.WallType<RottenstoneWall>();
            UnsafeWallType = ModContent.WallType<RottenstoneWallUnsafe>();
            StoneType = ModContent.TileType<RottenstoneBlock>();
            SandType = ModContent.TileType<RottensandBlock>();
            GrassType = ModContent.TileType<CarrionGrass>();

            for (int i = 0; i < WallLoader.WallCount; i++)
            {
                if (WallID.Sets.Conversion.Stone[i])
                    WallLoader.RegisterConversion(i, Type, ConvertWalls);
            }

            for (int i = 0; i < TileLoader.TileCount; i++)
            {
                if (TileID.Sets.Conversion.Sand[i])
                    TileLoader.RegisterConversion(i, Type, ConvertSand);

                if (TileID.Sets.Conversion.Stone[i])
                    TileLoader.RegisterConversion(i, Type, ConvertStone);

                if (TileID.Sets.Conversion.Grass[i])
                    TileLoader.RegisterConversion(i, Type, ConvertGrass);
            }
        }

        public bool ConvertSand(int i, int j, int type, int conversionType)
        {
            WorldGen.ConvertTile(i, j, SandType, true);
            return false;
        }

        public bool ConvertGrass(int i, int j, int type, int conversionType)
        {

            int tileTypeAbove = -1;
            if (j > 1 && Main.tile[i, j - 1].HasTile)
                tileTypeAbove = Main.tile[i, j - 1].TileType;

            FindAndConvertTree(i, j, tileTypeAbove);

            WorldGen.ConvertTile(i, j, GrassType, true);
            return false;
        }

        public bool ConvertStone(int i, int j, int type, int conversionType)
        {
            WorldGen.ConvertTile(i, j, StoneType, true);
            return false;
        }

        public bool ConvertWalls(int i, int j, int type, int conversionType)
        {

            int wallType = Main.wallHouse[type] ? WallType : UnsafeWallType;
            WorldGen.ConvertWall(i, j, wallType);
            return false;
        }

        public void FindAndConvertTree(int i, int j, int tileTypeAbove)
        {

            if (tileTypeAbove == -1)
                return;

            if (!TileID.Sets.CountsAsGemTree[tileTypeAbove])
                return;

            int treeBottom = j;
            int treeTop = treeBottom - 1;
            int treeCenterX = i;

            int treeFrameX = Main.tile[treeCenterX, treeTop].TileFrameX / 22;
            int treeFrameY = Main.tile[treeCenterX, treeTop].TileFrameY / 22;
            bool isTreeTrunk = (treeFrameX != 1 && treeFrameX != 2) || treeFrameY < 6;

            bool isTreeBranch = (treeFrameX == 3 && treeFrameY < 3) || (treeFrameX == 4 && treeFrameY >= 3 && treeFrameY < 6);
            if (isTreeBranch)
                return;

            if (!isTreeTrunk)
            {
                for (int x = treeCenterX - 1; x < treeCenterX + 2; x += 2)
                {

                    Tile topTile = Main.tile[x, treeTop];
                    if (!topTile.HasTile)
                        continue;

                    treeFrameX = topTile.TileFrameX / 22;
                    treeFrameY = topTile.TileFrameY / 22;
                    isTreeTrunk = (treeFrameX != 1 && treeFrameX != 2) || treeFrameY < 6;

                    if (isTreeTrunk)
                    {
                        treeCenterX = x;
                        break;
                    }
                }
            }

            while (treeTop >= 0 && Main.tile[treeCenterX, treeTop].HasTile)
                treeTop--;

            for (int x = treeCenterX - 1; x < treeCenterX + 2; x++)
            {
                for (int y = treeTop; y < treeBottom; y++)
                {
                    Tile t = Main.tile[x, y];
                    if (t.HasTile)
                        t.TileType = TileID.Trees;
                }
            }

            for (int x = treeCenterX - 1; x < treeCenterX + 2; x++)
            {
                Tile t = Main.tile[x, treeBottom];
                if (t.HasTile && t.TileType == TileID.Grass)
                    t.TileType = (ushort)GrassType;
            }
        }
    }
}
