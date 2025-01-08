using Eternal.Common.Configurations;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Placeable.MusicBox
{
    public class DarkMoonMusicBox : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ServerConfig.instance.update14;
        }

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

            ItemID.Sets.CanGetPrefixes[Type] = false;

            MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, "Assets/Music/DarkTwistedNightmare"), ModContent.ItemType<DarkMoonMusicBox>(), ModContent.TileType<Tiles.MusicBox.DarkMoonMusicBox>());
        }

        public override void SetDefaults()
        {
            Item.DefaultToMusicBox(ModContent.TileType<Tiles.MusicBox.DarkMoonMusicBox>(), 0);
        }
    }
}
