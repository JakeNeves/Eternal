using Eternal.NPCs.Boss;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Summon
{
    class SuspiciousLookingDroplet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Summons a Carmanite Scouter, The blood-feasting abomination");
            ItemID.Sets.SortingPriorityBossSpawns[item.type] = 13;
        }

        public override void SetDefaults()
        {
            item.width = 36;
            item.height = 56;
            item.maxStack = 99;
            item.rare = ItemRarityID.Green;
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.consumable = true;
        }

        public override bool UseItem(Player player)
        {
            NPC.SpawnOnPlayer(player.whoAmI, NPCType<NPCs.Boss.CarmaniteScouter.CarmaniteScouter>());
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.Bottles);
            recipe.AddIngredient(ItemID.VilePowder, 5);
            recipe.AddIngredient(ItemID.RottenChunk);
            recipe.AddIngredient(ItemID.VileMushroom);
            recipe.SetResult(this);
            recipe.AddRecipe();

            ModRecipe recipealt = new ModRecipe(mod);
            recipealt.AddTile(TileID.Bottles);
            recipealt.AddIngredient(ItemID.ViciousPowder, 5);
            recipealt.AddIngredient(ItemID.Vertebrae);
            recipealt.AddIngredient(ItemID.ViciousMushroom);
            recipealt.SetResult(this);
            recipealt.AddRecipe();
        }
    }
}
