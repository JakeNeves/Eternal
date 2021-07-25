using Eternal.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Summon
{
    public class DroxBeacon : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Calls upon the Drox Clan");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 32;
            item.rare = ItemRarityID.Lime;
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = ItemUseStyleID.HoldingUp;
        }

        /*public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(NPCType<Empraynia>());
        }*/

        public override bool UseItem(Player player)
        {
            //NPC.SpawnOnPlayer(player.whoAmI, NPCType<Empraynia>());
            Main.NewText("The Drox Clan is striking!", 175, 75, 255);
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.AddIngredient(ItemID.Wire, 6);
            recipe.AddIngredient(ItemType<DroxCore>());
            recipe.AddIngredient(ItemType<DroxPlate>(), 2);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
