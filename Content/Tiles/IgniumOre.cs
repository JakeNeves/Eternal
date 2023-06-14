using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Eternal.Content.Tiles
{
    public class IgniumOre : ModTile
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
            DustType = DustID.Torch;
            // ItemDrop = ModContent.ItemType<Content.Items.Placeable.IgniumOre>();
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Ignium");
            AddMapEntry(new Color(233, 67, 43), name);
            MinPick = 230;
            HitSound = SoundID.Tink;
            MineResist = 8f;
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 2.50f;
            g = 1.11f;
            b = 0.66f;
        }
    }
}
