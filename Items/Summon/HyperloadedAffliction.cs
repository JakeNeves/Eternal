using Eternal.NPCs.Boss.Empraynia;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Summon
{
    public class HyperloadedAffliction : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Summons Empraynia, the Dream Eater\n'The sky grows darker...'");
        }

        public override void SetDefaults()
        {
            item.width = 42;
            item.height = 38;
            item.rare = ItemRarityID.Yellow;
            item.useAnimation = 45;
            item.useTime = 45;
            item.consumable = true;
            item.maxStack = 99;
            item.useStyle = ItemUseStyleID.HoldingUp;
        }

        public override bool CanUseItem(Player player)
        {
            Main.NewText("The sky is shifting...", 220, 0, 210);
            return !NPC.AnyNPCs(NPCType<Empraynia>());
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.AddIngredient(ItemID.Ectoplasm, 4);
            recipe.AddIngredient(ItemID.SpectreBar, 8);
            recipe.AddIngredient(ItemID.ShroomiteBar, 8);
            recipe.AddIngredient(ItemID.SoulofNight, 6);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool UseItem(Player player)
        {
            NPC.SpawnOnPlayer(player.whoAmI, NPCType<Empraynia>());
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }
    }
}
