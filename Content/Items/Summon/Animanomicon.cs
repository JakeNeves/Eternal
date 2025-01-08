using Eternal.Common.Configurations;
using Eternal.Common.Systems;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Summon
{
    public class Animanomicon : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ServerConfig.instance.update14;
        }

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 28;
            Item.rare = ItemRarityID.Pink;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
        }

        public override bool CanUseItem(Player player)
        {
            return !Main.dayTime && !EventSystem.darkMoon;
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                Main.NewText("The Dark Moon is rising...", 50, 255, 129);
                SoundEngine.PlaySound(SoundID.Roar, player.position);

                EventSystem.darkMoon = true;
            }

            return true;
        }
    }
}
