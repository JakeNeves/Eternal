using Eternal.Common.Systems;
using Eternal.Content.Items.Materials;
using Eternal.Content.Items.Placeable;
using Eternal.Content.NPCs.Boss.CosmicApparition;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Summon
{
    public class OtherworldlyCosmicDebris : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Red;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
        }

        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(ModContent.NPCType<WanderingSoul>()) && !NPC.AnyNPCs(ModContent.NPCType<CosmicApparition>());
        }

        public override bool? UseItem(Player player)
        {
            var entitySource = player.GetSource_FromThis();

            Main.NewText("Something gazes upon you...", 220, 0, 210);
            NPC.NewNPC(entitySource, (int)player.Center.X, (int)player.Center.Y - 600, ModContent.NPCType<WanderingSoul>());

            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<CometiteOre>(), 8)
                .AddIngredient(ModContent.ItemType<StarquartzCrystalCluster>(), 20)
                .AddIngredient(ItemID.Ectoplasm, 16)
                .AddIngredient(ItemID.LunarBar, 4)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}
