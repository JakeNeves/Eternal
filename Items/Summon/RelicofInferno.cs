using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Summon
{
    class RelicofInferno : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Relic of Inferno");
            Tooltip.SetDefault("Summons Incinerius, the flaming golem\n'The Flames of The Underworld are Blazing Furiously...'");
            ItemID.Sets.SortingPriorityBossSpawns[item.type] = 15;
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 60;
            item.rare = ItemRarityID.Pink;
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = ItemUseStyleID.HoldingUp;
        }

        public override bool CanUseItem(Player player)
        {
            return player.ZoneUnderworldHeight && !NPC.AnyNPCs(NPCType<NPCs.Boss.Incinerius.Incinerius>());
        }

        public override bool UseItem(Player player)
        {
            Main.NewText("Let's see how long you last before you burn to the ground...", 215, 95, 0);
            NPC.SpawnOnPlayer(player.whoAmI, NPCType<NPCs.Boss.Incinerius.Incinerius>());
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.Anvils);
            recipe.AddIngredient(ItemID.HellstoneBar, 6);
            recipe.AddIngredient(ItemID.SoulofLight, 5);
            recipe.AddIngredient(ItemID.SoulofNight, 5);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
