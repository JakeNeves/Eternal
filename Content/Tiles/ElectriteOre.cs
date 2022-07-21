using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Tiles
{
    public class ElectriteOre : ModTile
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
            DustType = DustID.Electric;
            ItemDrop = ModContent.ItemType<Content.Items.Placeable.ElectriteOre>();
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Electrite");
            AddMapEntry(new Color(35, 200, 254), name);
            MinPick = 230;
            HitSound = SoundID.Tink;
            MineResist = 8f;
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 2.08f;
            g = 2.53f;
            b = 2.35f;
        }

    }
}
