using Eternal.Common.Players;
using Eternal.Content.Rarities;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.ID;

namespace Eternal.Content.Items.Debug
{
    public class ReverseLifeMote : ModItem
    {
        public static readonly int LifeMoteMax = 20;
        public static readonly int LifePerMote = 5;

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 10;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 30;
            Item.rare = ModContent.RarityType<Teal>();
            Item.value = Item.sellPrice(gold: 30);
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item4;
            Item.useAnimation = 10;
            Item.useTime = 10;
        }

        public override bool CanUseItem(Player player)
        {
            return player.ConsumedLifeCrystals == Player.LifeCrystalMax && player.ConsumedLifeFruit == Player.LifeFruitMax;
        }

        public override bool? UseItem(Player player)
        {
            if (player.GetModPlayer<LifeMotePlayer>().lifeMotes >= LifeMoteMax)
            {
                return null;
            }

            player.UseHealthMaxIncreasingItem(LifePerMote);

            player.GetModPlayer<LifeMotePlayer>().lifeMotes--;

            return true;
        }
    }
}
