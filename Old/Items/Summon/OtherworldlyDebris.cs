using Eternal.Items.Materials.Elementalblights;
using Eternal.NPCs.Boss.CosmicApparition;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Summon
{
    public class OtherworldlyDebris : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Attracts a wandering soul\n'A mysteriously devious chunk of paranormal essance'");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 28;
            item.rare = ItemRarityID.Red;
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = ItemUseStyleID.HoldingUp;
        }

        public override bool CanUseItem(Player player)
        {
            Main.NewText("Something gazes upon you...", 220, 0, 210);
            return !NPC.AnyNPCs(ModContent.NPCType<SoulCrystal>());
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.AddIngredient(ItemID.Ectoplasm, 16);
            recipe.AddIngredient(ModContent.ItemType<DuskblightCrystal>(), 20);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool UseItem(Player player)
        {
            if (Main.LocalPlayer.GetModPlayer<EternalPlayer>().ZoneCommet)
            {
                NPC.NewNPC((int)player.Center.X, (int)player.Center.Y - 900, ModContent.NPCType<SoulCrystal>());
            }
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }
    }
}
