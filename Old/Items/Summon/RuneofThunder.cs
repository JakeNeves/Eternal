using Eternal.NPCs.Boss.Dunekeeper;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Summon
{
    class RuneofThunder : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rune of Thunder");
            Tooltip.SetDefault("Summons the Dunekeeper, the Desert thundergen\n'The Spirits of the Dunes Awake'");
        }

        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 54;
            item.rare = ItemRarityID.Orange;
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = ItemUseStyleID.HoldingUp;
        }

        public override bool CanUseItem(Player player)
        {
            return player.ZoneDesert && !NPC.AnyNPCs(NPCType<Dunekeeper>());
        }

        public override bool UseItem(Player player)
        {
            NPC.SpawnOnPlayer(player.whoAmI, NPCType<Dunekeeper>());
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.Anvils);
            recipe.AddIngredient(ItemID.Diamond, 1);
            recipe.AddIngredient(ItemID.SandBlock, 25);
            recipe.AddIngredient(ItemID.SandstoneBrick, 15);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
