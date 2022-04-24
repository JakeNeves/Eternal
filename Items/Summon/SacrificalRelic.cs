using Eternal.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Summon
{
    public class SacrificalRelic : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Summons the fleshy amalgam\n'At least you won't need the voodoo doll'");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 22;
            item.rare = ItemRarityID.Orange;
            item.useAnimation = 45;
            item.useTime = 45;
            item.consumable = true;
            item.maxStack = 99;
            item.useStyle = ItemUseStyleID.HoldingUp;
        }

        public override bool CanUseItem(Player player)
        {
            return player.ZoneUnderworldHeight && !NPC.AnyNPCs(NPCID.WallofFlesh);
        }

        public override bool UseItem(Player player)
        {
            Main.NewText("You feel unsettled...", 220, 30, 28);
            NPC.SpawnOnPlayer(player.whoAmI, NPCID.WallofFlesh);
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.WorkBenches);
            recipe.AddIngredient(ItemID.HellstoneBar, 3);
            recipe.AddIngredient(ItemType<Carmanite>(), 15);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
