using Eternal.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Debug
{
    public class ReputationAdjuster1000 : ModItem
    {
        public override string Texture => "Eternal/Content/Items/Misc/EmperorsTrust";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Reputation Adjuster 1000");
            // Tooltip.SetDefault("[c/FF0000:Debug Item]\nRight Click to change Reputation Points by -1000\nLeft Click to change Reputation Points by +1000");
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
                ReputationSystem.ReputationPoints -= 1000;
                CombatText.NewText(player.Hitbox, Color.OrangeRed, "-1000 Reputation", dramatic: true);
            }
            else
            {
                Item.useStyle = ItemUseStyleID.HoldUp;
                Item.useAnimation = 5;
                Item.useTime = 5;
                ReputationSystem.ReputationPoints += 1000;
                CombatText.NewText(player.Hitbox, Color.OrangeRed, "+1000 Reputation", dramatic: true);
            }
            return base.CanUseItem(player);
        }
    }
}
