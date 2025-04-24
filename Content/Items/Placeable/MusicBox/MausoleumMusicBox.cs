using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Placeable.MusicBox
{
    public class MausoleumMusicBox : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

            ItemID.Sets.CanGetPrefixes[Type] = false;

            MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, "Assets/Music/CrucisNihilo"), ModContent.ItemType<MausoleumMusicBox>(), ModContent.TileType<Tiles.MusicBox.MausoleumMusicBox>());
        }

        public override void SetDefaults()
        {
            Item.DefaultToMusicBox(ModContent.TileType<Tiles.MusicBox.MausoleumMusicBox>(), 0);
        }
    }
}
