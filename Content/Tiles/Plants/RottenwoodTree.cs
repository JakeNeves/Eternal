using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace Eternal.Content.Tiles.Plants
{
    public class RottenwoodTree : ModTree
    {
        private Asset<Texture2D> texture;
        private Asset<Texture2D> branchesTexture;
        private Asset<Texture2D> topsTexture;

        public override TreePaintingSettings TreeShaderSettings => new TreePaintingSettings
        {
            UseSpecialGroups = true,
            SpecialGroupMinimalHueValue = 11f / 72f,
            SpecialGroupMaximumHueValue = 0.25f,
            SpecialGroupMinimumSaturationValue = 0.88f,
            SpecialGroupMaximumSaturationValue = 1f
        };

        public override void SetStaticDefaults()
        {
            GrowsOnTileId = [ModContent.TileType<RottenstoneBlock>()];
            texture = ModContent.Request<Texture2D>("Eternal/Content/Tiles/Plants/RottenwoodTree");
            branchesTexture = ModContent.Request<Texture2D>("Eternal/Content/Tiles/Plants/RottenwoodTree_Branches");
            topsTexture = ModContent.Request<Texture2D>("Eternal/Content/Tiles/Plants/RottenwoodTree_Tops");
        }

        public override Asset<Texture2D> GetTexture()
        {
            return texture;
        }

        public override int SaplingGrowthType(ref int style)
        {
            style = 0;
            return ModContent.TileType<RottenwoodSapling>();
        }

        public override void SetTreeFoliageSettings(Tile tile, ref int xoffset, ref int treeFrame, ref int floorY, ref int topTextureFrameWidth, ref int topTextureFrameHeight) { }

        public override Asset<Texture2D> GetBranchTextures() => branchesTexture;

        public override Asset<Texture2D> GetTopTextures() => topsTexture;

        public override int DropWood()
        {
            return ModContent.ItemType<Items.Placeable.Decorative.Rottenwood>();
        }

        /*public override bool Shake(int x, int y, ref bool createLeaves)
        {
            Item.NewItem(WorldGen.GetItemSource_FromTreeShake(x, y), new Vector2(x, y) * 16, ModContent.ItemType<Items.Placeable.ExampleBlock>());
            return false;
        }*/

        /*public override int TreeLeaf()
        {
            return ModContent.GoreType<RottenwoodTreeLeaf>();
        }*/
    }
}
