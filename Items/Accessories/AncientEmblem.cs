using Eternal.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Accessories
{
    public class AncientEmblem : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Grants 3% increased melee damage\n[c/008060:Ancient Artifact] \nA mysterious emblem, you have never seen this before... \nRumors believed that it was passed down from leader to leader of an unknown group... \nMaybe it was passed down to you because you were going to be the next leader of that group, who knows... \nThis dorminant pendant has something to do with providing the nessicary needs.");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 26;
            item.value = Item.sellPrice(silver: 25, copper: 15);
            item.rare = ItemRarityID.Blue;
            item.accessory = true;
            item.defense = 3;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.meleeDamage += 0.03f;

            //player.buffImmune[BuffID.Cursed] = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.WorkBenches);
            //recipe.AddIngredient(ItemType<TritalodiumBar>(), 4); old emblem/pendant recipe
            //recipe.AddIngredient(ItemID.Chain, 5);
            recipe.AddIngredient(ItemID.StoneBlock, 10); //new emblem recipe
            recipe.AddIngredient(ItemID.Lens, 3);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
