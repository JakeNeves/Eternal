﻿using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Placeable.MusicBox
{
    public class DuneGolemMusicBox : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

            ItemID.Sets.CanGetPrefixes[Type] = false;

            MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, "Assets/Music/HauntedTombfromtheDunes"), ModContent.ItemType<DuneGolemMusicBox>(), ModContent.TileType<Tiles.MusicBox.DuneGolemMusicBox>());
        }

        public override void SetDefaults()
        {
            Item.DefaultToMusicBox(ModContent.TileType<Tiles.MusicBox.DuneGolemMusicBox>(), 0);
        }
    }
}