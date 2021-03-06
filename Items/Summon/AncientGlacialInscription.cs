﻿using Eternal.Items.Placeable;
using Eternal.NPCs.Boss;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Summon
{
    class AncientGlacialInscription : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Kelvin Inscription");
            Tooltip.SetDefault("Summons a Subzero Elemental\n'The frost in the tundra is spreading...'");
            ItemID.Sets.SortingPriorityBossSpawns[item.type] = 16;
        }

        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 52;
            item.rare = ItemRarityID.Yellow;
            item.useAnimation = 45;
            item.useTime = 45;
            item.consumable = true;
            item.maxStack = 99;
            item.useStyle = ItemUseStyleID.HoldingUp;
        }

        public override bool CanUseItem(Player player)
        {
            return player.ZoneSnow && !NPC.AnyNPCs(NPCType<NPCs.Boss.SubzeroElemental.SubzeroElemental>());
        }

        public override bool UseItem(Player player)
        {
            Main.NewText("Something cold is gathering around you...", 0, 95, 215);
            NPC.SpawnOnPlayer(player.whoAmI, NPCType<NPCs.Boss.SubzeroElemental.SubzeroElemental>());
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.AddIngredient(ItemID.FrostCore, 3);
            recipe.AddIngredient(ItemID.Ectoplasm, 15);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
