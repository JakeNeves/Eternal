using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Content.Rarities;

namespace Eternal.Content.Items.Materials
{
    public class CosmicEmperorsInterstellarAlloy : ModItem
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cosmic Emperor's Interstellar Alloy");
            /* Tooltip.SetDefault("'Precious, vaulable, otherworldly and a one of a kind material'" +
                              "\n[c/008060:Ancient Artifact]\nThe Cosmic Emperor has explored the stars that linger across the night sky..." +
                              "\nOne day, after hours of exploration, he returned to his palace with this incredibly rare and one of a kind alloy!"); */
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 30;
            Item.rare = ModContent.RarityType<Teal>();
        }
    }
}
