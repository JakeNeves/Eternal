using Eternal.Content.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Accessories
{
    public class HotEnvironmentBreathingPack : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("[c/20E354:Environmental Protection Pack (EPP)]" +
                             "\nProvides Immunity to Hyperthermia in Brutal Hell Mode");
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 30;
            Item.value = Item.sellPrice(gold: 30);
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[ModContent.BuffType<Hyperthermia>()] = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(TileID.Anvils)
                .AddIngredient(ItemID.Silk, 12)
                .AddIngredient(ItemID.HellstoneBar, 16)
                .Register();
        }
    }
}
