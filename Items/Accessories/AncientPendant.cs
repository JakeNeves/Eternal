using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Accessories
{
    [AutoloadEquip(EquipType.Neck)]
    class AncientPendant : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Grants 3% increase of melee damage\n[c/008060:Ancient Artifact] \nA mysterious pendant, you have never seen this before... \nRumors believed that it was passed down from leader to leader of an unknown group... \nMaybe it was passed down to you because you were going to be the next leader of that group, who knows... \nThis dorminant pendant has something to do with providing the nessicary needs.");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 20;
            item.value = 0;
            item.rare = ItemRarityID.Blue;
            item.accessory = true;
            item.defense = 3;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.meleeDamage += 0.3f;

            //player.buffImmune[BuffID.Cursed] = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.Anvils);
            recipe.AddIngredient(ItemType<TritalodiumBar>(), 4);
            recipe.AddIngredient(ItemID.Chain, 5);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
