using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items
{
    public class Pill : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Minor Improvements to Some Stats" +
                               "\n'ugh...'");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.consumable = true;
            item.maxStack = 30;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.useAnimation = 16;
            item.useTime = 16;
            item.useTurn = true;
            item.rare = ItemRarityID.Blue;
            item.value = Item.sellPrice(silver: 20);
            item.buffType = ModContent.BuffType<Buffs.Pills>();
            item.buffTime = 5200;
            item.UseSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/IFoundPills");
        }
    }
}
