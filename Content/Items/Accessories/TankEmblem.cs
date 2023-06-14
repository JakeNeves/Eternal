using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Content.DamageClasses;
using Terraria.GameContent.Creative;
using Terraria.Localization;

namespace Eternal.Content.Items.Accessories
{
    public class TankEmblem : ModItem
    {
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs("15% increased health");

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.defense = 15;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = ItemRarityID.Lime;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 15;
        }
    }
}
