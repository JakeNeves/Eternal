using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;


namespace Eternal.Tiles.Interactive
{
    public class ThunderRuneOfferingAltar : ModTile
    {
        private Player player;

        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileObsidianKill[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.addTile(Type);
            disableSmartCursor = true;
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Spirit Tablet Offering Altar");
            AddMapEntry(new Color(247, 236, 171), name);
        }

        public override bool NewRightClick(int i, int j)
        {
            Player player = Main.player[Main.myPlayer];
            Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, ModContent.ItemType<Items.Summon.SpiritTablet>());
            return !Main.LocalPlayer.HasItem(ModContent.ItemType<Items.Summon.SpiritTablet>());
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.player[Main.myPlayer];
            player.noThrow = 2;
            player.showItemIcon = true;
            player.showItemIcon2 = ModContent.ItemType<Items.Summon.SpiritTablet>();
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<Items.Placeable.ThunderRuneOfferingAltar>());
        }
    }
}
