using Eternal.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Debug
{
    public class ReputationAdjuster100 : ModItem
    {
        public override string Texture => "Eternal/Content/Items/Misc/EmperorsTrust";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Reputation Adjuster 100");
            // Tooltip.SetDefault("[c/FF0000:Debug Item]\nRight Click to change Reputation Points by -100\nLeft Click to change Reputation Points by +100");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 30;
            Item.rare = ItemRarityID.Expert;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useAnimation = 5;
            Item.useTime = 5;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useStyle = ItemUseStyleID.HoldUp;
                Item.useAnimation = 5;
                Item.useTime = 5;
                ReputationSystem.ReputationPoints -= 100;
                CombatText.NewText(player.Hitbox, Color.OrangeRed, "-100 Reputation", dramatic: true);
            }
            else
            {
                Item.useStyle = ItemUseStyleID.HoldUp;
                Item.useAnimation = 5;
                Item.useTime = 5;
                ReputationSystem.ReputationPoints += 100;
                CombatText.NewText(player.Hitbox, Color.OrangeRed, "+100 Reputation", dramatic: true);
            }
            return base.CanUseItem(player);
        }
    }
}
