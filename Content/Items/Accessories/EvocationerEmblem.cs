using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Content.DamageClasses;
using Terraria.GameContent.Creative;

namespace Eternal.Content.Items.Accessories
{
    public class EvocationerEmblem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Evocation Emblem");
            Tooltip.SetDefault("15% increased radiant damage");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = ItemRarityID.LightRed;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage<Radiant>() *= 1.15f;
        }
    }
}
