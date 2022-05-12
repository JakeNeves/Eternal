using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Debug
{
    public class ReverseLifeContainer : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("[c/FF0000:Debug Item]" +
                "\nRemoves 20 HP from the Players Health" +
                "\nDoes not work with other mods" +
                "\nConsume Life Crystals and/or Life Fruit to Regain the Effects");
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 22;
            Item.rare = ItemRarityID.White;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useAnimation = 16;
            Item.useTime = 16;
            Item.UseSound = SoundID.Item4;
        }

        public override bool ConsumeItem(Player player)
        {
            if (player.statLifeMax <= 20)
                return false;

            return true;
        }

        public override bool? UseItem(Player player)
        {
            if (player.itemAnimation > 0 && player.statLifeMax > 20 && player.itemTime == 0)
            {
                player.statLifeMax -= 20;
                player.statLifeMax2 -= 20;
                player.statLife -= 20;
                if (Main.myPlayer == player.whoAmI)
                {
                    player.HealEffect(-20, true);
                }
            }

            return true;
        }
    }
}
