using Eternal.NPCs.Miniboss.SebastionsEmergencyDrone;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Summon
{
    public class SebastionsRadioTransceiver : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sebastion's Radio Transceiver");
            Tooltip.SetDefault("Sends a distress signal to Sebastion's Laboratory\nOnly Usable At Night\n'Use with care'");
        }

        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 52;
            item.rare = ItemRarityID.Pink;
            item.useAnimation = 45;
            item.useTime = 45;
            item.consumable = true;
            item.maxStack = 99;
            item.useStyle = ItemUseStyleID.HoldingUp;
        }

        public override bool CanUseItem(Player player)
        {
            return !Main.dayTime && !NPC.AnyNPCs(NPCType<SebastionsEmergencyDrone>());
        }

        public override bool UseItem(Player player)
        {
            Main.NewText("A signal to Sebastion's Laboratory has been transmitted", 46, 20, 82);
            NPC.SpawnOnPlayer(player.whoAmI, NPCType<SebastionsEmergencyDrone>());
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.AddIngredient(ItemID.HellstoneBar, 4);
            recipe.AddIngredient(ItemID.IronBar, 4);
            recipe.AddIngredient(ItemID.Wire, 8);
            recipe.SetResult(this);
            recipe.AddRecipe();

            ModRecipe altrecipe = new ModRecipe(mod);
            altrecipe.AddTile(TileID.MythrilAnvil);
            altrecipe.AddIngredient(ItemID.HellstoneBar, 4);
            altrecipe.AddIngredient(ItemID.LeadBar, 4);
            altrecipe.AddIngredient(ItemID.Wire, 8);
            altrecipe.SetResult(this);
            altrecipe.AddRecipe();
        }

    }
}
