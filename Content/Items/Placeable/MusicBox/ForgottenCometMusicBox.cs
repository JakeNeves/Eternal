using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Placeable.MusicBox
{
    public class ForgottenCometMusicBox : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<CometNightMusicBox>();

            ItemID.Sets.CanGetPrefixes[Type] = false;

            MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, "Assets/Music/ShatteredStar"), ModContent.ItemType<ForgottenCometMusicBox>(), ModContent.TileType<Tiles.MusicBox.ForgottenCometMusicBox>());
        }

        public override void SetDefaults()
        {
            Item.DefaultToMusicBox(ModContent.TileType<Tiles.MusicBox.ForgottenCometMusicBox>(), 0);
        }
    }
}
