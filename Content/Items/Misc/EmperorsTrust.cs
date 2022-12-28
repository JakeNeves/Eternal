using Eternal.Common.Players;
using Eternal.Content.Rarities;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace Eternal.Content.Items.Misc
{
    public class EmperorsTrust : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Emperor's Trust");
            Tooltip.SetDefault("Allows the emperor's Emissaries to settle in your town" +
                "\nAllows you to earn Reputation which allows you to buy items from the Emissary" +
                "\nSome weapons receive special buffs when in your inventory" +
                "\n'In the eyes of the emperor, as long as you have his trust within your reach, he will let his emissaries settle on your land.'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 30;
            Item.rare = ModContent.RarityType<Teal>();
            Item.value = Item.sellPrice(gold: 30);
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useAnimation = 5;
            Item.useTime = 5;
        }

        public override bool? UseItem(Player player)
        {
            if (ReputationSystem.ReputationPoints == 5000)
                CombatText.NewText(player.Hitbox, Color.Gold, $"You have max Reputation!", dramatic: true);
            else
                CombatText.NewText(player.Hitbox, Color.Gold, $"You have {ReputationSystem.ReputationPoints} Reputation!", dramatic: true);

            return true;
        }
    }
}
