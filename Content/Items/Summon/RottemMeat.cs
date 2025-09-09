using Eternal.Common.Systems;
using Eternal.Content.Items.Materials;
using Eternal.Content.Items.Placeable;
using Eternal.Content.NPCs.Boss.CosmicApparition;
using Eternal.Content.NPCs.Boss.Niades;
using Eternal.Content.NPCs.Boss.TheChimera;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Summon
{
    public class RottenMeat : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Lime;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
        }

        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(ModContent.NPCType<TheChimera>()) && !NPC.AnyNPCs(ModContent.NPCType<TheHeap>()) && !NPC.AnyNPCs(ModContent.NPCType<TheHive>());
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                SoundEngine.PlaySound(SoundID.Roar, player.position);

                if (Main.netMode != NetmodeID.MultiplayerClient)
                    NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<TheChimera>());
                else
                    NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: ModContent.NPCType<TheChimera>());
            }

            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<NecroticTissue>(), 12)
                .AddIngredient(ModContent.ItemType<PolypChunk>(), 12)
                .AddIngredient(ItemID.Bone, 6)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}
