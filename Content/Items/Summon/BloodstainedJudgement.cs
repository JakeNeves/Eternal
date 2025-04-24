using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Content.NPCs.Boss.Niades;
using Eternal.Content.Items.Materials;
using Eternal.Common.Systems;
using Terraria.GameContent.Creative;

namespace Eternal.Content.Items.Summon
{
    public class BloodstainedJudgement : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 10;
            Item.rare = ItemRarityID.Pink;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
        }

        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(ModContent.NPCType<Niades>()) && EventSystem.darkMoon;
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                SoundEngine.PlaySound(SoundID.Roar, player.position);

                if (Main.netMode != NetmodeID.MultiplayerClient)
                    NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<Niades>());
                else
                    NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: ModContent.NPCType<Niades>());
            }

            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<OcculticMatter>(), 12)
                .AddIngredient(ItemID.SoulofNight, 6)
                .AddIngredient(ModContent.ItemType<PsychicAshes>(), 12)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
