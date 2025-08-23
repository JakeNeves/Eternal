using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Misc
{
    public class SolStone : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 42;
            Item.rare = ItemRarityID.Pink;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.UseSound = SoundID.Item45;
        }

        public override bool? UseItem(Player player)
        {
            Main.dayTime = true;

            return !Main.dayTime;
        }
    }
}
