using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Eternal.Content.Tiles
{
    public class GalaciteOre : ModTile
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
            DustType = DustID.Ice;
            ItemDrop = ModContent.ItemType<Content.Items.Placeable.GalaciteOre>();
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Galacite");
            AddMapEntry(new Color(107, 160, 255), name);
            MinPick = 230;
            HitSound = SoundID.Tink;
            MineResist = 8f;
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 1.07f;
            g = 2.24f;
            b = 2.55f;
        }

    }
}
