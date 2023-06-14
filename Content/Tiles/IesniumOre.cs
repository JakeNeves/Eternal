using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Eternal.Content.Tiles
{
    public class IesniumOre : ModTile
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
            DustType = DustID.BlueTorch;
            // ItemDrop = ModContent.ItemType<Content.Items.Placeable.IesniumOre>();
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Iesnium");
            AddMapEntry(new Color(22, 71, 73), name);
            MinPick = 100;
            HitSound = SoundID.Tink;
            MineResist = 2f;
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.58f;
            g = 1.59f;
            b = 1.64f;
        }

	}
}
