using Eternal.Common.Systems;
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
            Tooltip.SetDefault("Attracts a wandering soul" +
                             "\n'A mysteriously devious chunk of paranormal essance'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 26;
            Item.rare = ItemRarityID.Red;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
        }

        public override bool CanUseItem(Player player)
        {
            Main.NewText("Something gazes upon you...", 220, 0, 210);
            return !NPC.AnyNPCs(ModContent.NPCType<WanderingSoul>()) && !NPC.AnyNPCs(ModContent.NPCType<CosmicApparition>()) && ModContent.GetInstance<ZoneSystem>().zoneComet;
        }

        public override bool? UseItem(Player player)
        {
            var entitySource = player.GetSource_FromThis();

            NPC.NewNPC(entitySource, (int)player.Center.X, (int)player.Center.Y - 900, ModContent.NPCType<WanderingSoul>());
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<CometiteOre>(), 8)
                .AddIngredient(ItemID.Ectoplasm, 16)
                .AddIngredient(ItemID.LunarBar, 4)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}
