using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Placeable.MusicBox
{
    public class PostTrinityDarkMoonMusicBox : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

            ItemID.Sets.CanGetPrefixes[Type] = false;

            MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, "Assets/Music/AnethemisOccultae"), ModContent.ItemType<PostTrinityDarkMoonMusicBox>(), ModContent.TileType<Tiles.MusicBox.PostTrinityDarkMoonMusicBox>());
        }

        public override void SetDefaults()
        {
            Item.DefaultToMusicBox(ModContent.TileType<Tiles.MusicBox.PostTrinityDarkMoonMusicBox>(), 0);
        }
    }
}
