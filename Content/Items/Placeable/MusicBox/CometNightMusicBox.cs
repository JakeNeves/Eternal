using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Placeable.MusicBox
{
    public class CometNightMusicBox : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<ForgottenCometMusicBox>();

            ItemID.Sets.CanGetPrefixes[Type] = false;

            MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, "Assets/Music/AstralDiscoveryRetakeNight"), ModContent.ItemType<CometNightMusicBox>(), ModContent.TileType<Tiles.MusicBox.CometNightMusicBox>());
        }

        public override void SetDefaults()
        {
            Item.DefaultToMusicBox(ModContent.TileType<Tiles.MusicBox.CometNightMusicBox>(), 0);
        }
    }
}
