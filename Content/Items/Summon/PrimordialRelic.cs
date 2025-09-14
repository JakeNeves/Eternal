using Eternal.Content.Items.Materials;
using Eternal.Content.NPCs.Boss.Trinity;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Summon
{
    public class PrimordialRelic : ModItem
    {

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 22;
            Item.rare = ItemRarityID.Red;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
        }

       public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(ModContent.NPCType<Thunderius>()) && !NPC.AnyNPCs(ModContent.NPCType<Infernito>()) && !NPC.AnyNPCs(ModContent.NPCType<Cryota>());
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                var entitySource = player.GetSource_FromThis();

                Main.NewText("The Trinity has awoken!", 175, 75, 255);
                SoundEngine.PlaySound(new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/PrimordialRelicUse"), player.position);

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.NewNPC(entitySource, (int)player.Center.X - 600, (int)player.Center.Y - 300, ModContent.NPCType<Thunderius>());
                    NPC.NewNPC(entitySource, (int)player.Center.X + 600, (int)player.Center.Y - 300, ModContent.NPCType<Infernito>());
                    NPC.NewNPC(entitySource, (int)player.Center.X, (int)player.Center.Y - 300, ModContent.NPCType<Cryota>());
                }
            }

            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<ElectriteBar>(), 20)
                .AddIngredient(ModContent.ItemType<IgniumBar>(), 20)
                .AddIngredient(ModContent.ItemType<GalaciteBar>(), 20)
                .AddIngredient(ModContent.ItemType<ArkaniumAlloy>(), 40)
                .AddIngredient(ModContent.ItemType<CrystallizedOminite>(), 40)
                .AddIngredient(ModContent.ItemType<NaquadahBar>(), 40)
                .AddTile(ModContent.TileType<AncientFoundry>())
                .Register();
        }

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossSpawners;
        }
    }
}
