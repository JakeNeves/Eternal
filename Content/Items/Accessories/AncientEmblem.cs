using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Accessories
{
    public class AncientEmblem : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Grants 5% increased melee damage\n[c/008060:Ancient Artifact] \nA mysterious emblem, you have never seen this before... \nRumors believed that it was passed down from leader to leader of an unknown group... \nMaybe it was passed down to you because you were going to be the next leader of that group, who knows... \nThis dorminant emblem has something to do with empowering a dormant sword, gifted from the sword god himself.");
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.value = Item.sellPrice(silver: 25, copper: 15);
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
            Item.defense = 1;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Melee) += 0.05f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.StoneBlock, 10)
                .AddIngredient(ItemID.Lens, 3)
                .Register();
        }
    }
}
