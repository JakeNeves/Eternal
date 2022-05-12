using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Tiles
{
    public class CometiteOre : ModTile
    {

        public override void SetStaticDefaults()
        {
            TileID.Sets.Ore[Type] = true;
            Main.tileSpelunker[Type] = true;
            Main.tileOreFinderPriority[Type] = 500;
            Main.tileShine2[Type] = true;
            Main.tileShine[Type] = 800;
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            DustType = DustID.PurpleTorch;
            ItemDrop = ModContent.ItemType<Content.Items.Placeable.CometiteOre>();
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Cometite");
            AddMapEntry(new Color(30, 20, 40), name);
            MinPick = 225;
            SoundType = SoundID.Tink;
            SoundStyle = 1;
            MineResist = 5f;
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 1.25f;
            g = 0.75f;
            b = 1.5f;
        }

    }
}
