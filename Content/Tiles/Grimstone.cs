using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Eternal.Content.Tiles
{
    public class Grimstone : ModTile
    {

        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            DustType = DustID.Stone;
            ItemDrop = ModContent.ItemType<Items.Placeable.Grimstone>();
            ModTranslation name = CreateMapEntryName();
            AddMapEntry(new Color(24, 24, 24));
            MinPick = 50;
            HitSound = SoundID.DD2_CrystalCartImpact;
            MineResist = 6f;
        }
    }
}
