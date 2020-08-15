using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ObjectData;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Eternal.Items.Placeable;

namespace Eternal.Tiles
{
    class AncientForge : ModTile
    {

        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Ancient Forge");
            AddMapEntry(new Color(80, 170, 195), name);
            disableSmartCursor = true;
            //The List of Tiles it Functions As
            adjTiles = new int[]
            {
                TileID.WorkBenches,
                TileID.AdamantiteForge,
                TileID.MythrilAnvil,
                TileID.Bottles,
                TileID.AlchemyTable,
                TileID.CookingPots,
                TileID.HeavyWorkBench,
                TileID.Blendomatic,
                TileID.LunarCraftingStation,
                TileID.Sawmill,
                TileID.Loom,
                TileID.Kegs,
                TileType<Starforge>()
            };
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 16, ItemType<Items.Placeable.AncientForge>());
        }

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }
            int height = tile.frameY == 36 ? 18 : 16;
            Main.spriteBatch.Draw(mod.GetTexture("Tiles/AncientForge_Glow"), new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.frameX, tile.frameY, 16, height), Color.Aqua, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

    }
}
