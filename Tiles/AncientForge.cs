using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ObjectData;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

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
                TileID.Kegs
            };
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 16, ItemType<Items.Placeable.AncientForge>());
        }

    }
}
