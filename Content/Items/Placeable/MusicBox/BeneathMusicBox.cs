using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Placeable.MusicBox
{
    public class BeneathMusicBox : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

            ItemID.Sets.CanGetPrefixes[Type] = false;

            MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, "Assets/Music/PsionicEchoes"), ModContent.ItemType<BeneathMusicBox>(), ModContent.TileType<Tiles.MusicBox.BeneathMusicBox>());
        }

        public override void SetDefaults()
        {
            Item.DefaultToMusicBox(ModContent.TileType<Tiles.MusicBox.CarminiteAmalgamationMusicBox>(), 0);
        }
    }
}
