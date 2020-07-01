using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items
{
    class SoulofTwilight : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul of Twilight");
            Tooltip.SetDefault("'The essence of twilight creatures'");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 4));
            ItemID.Sets.AnimatesAsSoul[item.type] = true;
            ItemID.Sets.ItemIconPulse[item.type] = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SetDefaults()
        {
            Item refItem = new Item();
            refItem.SetDefaults(ItemID.SoulofLight);
            item.width = refItem.width;
            item.height = refItem.height;
            item.maxStack = 999;
            item.value = Item.sellPrice(gold: 10, silver: 75);
            item.rare = ItemRarityID.Green;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, Color.Green.ToVector3() * 0.55f * Main.essScale);
        }
    }
}
