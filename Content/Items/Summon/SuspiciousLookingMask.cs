using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Content.NPCs.Boss.TheGlare;
using Eternal.Content.Items.Materials;
using Eternal.Common.Systems;
using Terraria.GameContent.Creative;

namespace Eternal.Content.Items.Summon
{
    public class SuspiciousLookingMask : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 30;
            Item.rare = ItemRarityID.Lime;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
        }

        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(ModContent.NPCType<TheGlare>()) && ModContent.GetInstance<ZoneSystem>().zoneGehenna || ModContent.GetInstance<ZoneSystem>().zoneMausoleum;
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                SoundEngine.PlaySound(SoundID.Roar, player.position);

                if (Main.netMode != NetmodeID.MultiplayerClient)
                    NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<TheGlare>());
                else
                    NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: ModContent.NPCType<TheGlare>());
            }

            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.CursedFlame, 8)
                .AddIngredient(ItemID.SoulofNight, 6)
                .AddIngredient(ModContent.ItemType<PsyblightEssence>(), 12)
                .AddTile(TileID.MythrilAnvil)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.Ichor, 8)
                .AddIngredient(ItemID.SoulofNight, 6)
                .AddIngredient(ModContent.ItemType<PsyblightEssence>(), 12)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossSpawners;
        }
    }
}
