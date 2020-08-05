using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Temporary
{
    public class FAoLSummon : ModItem
    {
        public override void SetStaticDefaults()
        {
            this.Tooltip.SetDefault("Summons the Fake Ark of Imparus");
            this.DisplayName.SetDefault("A Hero's Skull");
        }

        public override void SetDefaults()
        {
            item.useTime = 40;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.width = 24;
            item.height = 24;
            item.useAnimation = 40;
            item.maxStack = 99;
            item.consumable = true;
            item.UseSound = SoundID.Item4;
            item.rare = ItemRarityID.Purple;
        }

        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(NPCType<NPCs.FakeAoI>());
        }

        public override bool UseItem(Player player)
        {
            NPC.SpawnOnPlayer(player.whoAmI, NPCType<NPCs.FakeAoI>());
            return true;

        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DirtBlock, 20);
            recipe.SetResult(this);

        }
    }
}