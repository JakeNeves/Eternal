using Eternal.Invasion;
using Eternal.Items.Materials;
using Eternal.NPCs.Boss.DroxOverlord;
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

        public override bool CanUseItem(Player player)
        {
            DroxClanPlayer modPlayer = Main.player[Main.myPlayer].GetModPlayer<DroxClanPlayer>();
            if (DroxClanWorld.DClan)
                return false;
            return true;
        }

        public override bool UseItem(Player player)
        {
            NPC.SpawnOnPlayer(player.whoAmI, NPCType<DroxOverlord>());
            DroxClanPlayer modPlayer = Main.player[Main.myPlayer].GetModPlayer<DroxClanPlayer>();
            DroxClanWorld.DClan = true;
            Main.LocalPlayer.GetModPlayer<EternalPlayer>().droxEvent = true;
            Main.NewText("The Drox Clan is striking!", 175, 75, 255);
            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/DroxClan"), player.position);
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.AddIngredient(ItemID.Wire, 6);
            recipe.AddIngredient(ItemID.IronBar, 12);
            recipe.AddIngredient(ItemID.HallowedBar, 12);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 12);
            recipe.AddIngredient(ItemID.ShroomiteBar, 6);
            recipe.SetResult(this);
            recipe.AddRecipe();

            ModRecipe altrecipe = new ModRecipe(mod);
            altrecipe.AddTile(TileID.MythrilAnvil);
            altrecipe.AddIngredient(ItemID.Wire, 6);
            altrecipe.AddIngredient(ItemID.LeadBar, 12);
            altrecipe.AddIngredient(ItemID.HallowedBar, 12);
            altrecipe.AddIngredient(ItemID.ChlorophyteBar, 12);
            altrecipe.AddIngredient(ItemID.ShroomiteBar, 6);
            altrecipe.SetResult(this);
            altrecipe.AddRecipe();
        }
    }
}
