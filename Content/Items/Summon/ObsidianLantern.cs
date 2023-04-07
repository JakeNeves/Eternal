using Eternal.Common.Systems;
using Eternal.Content.NPCs.Boss.Incinerius;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Summon
{
    public class ObsidianLantern : ModItem
    {
        public override void SetStaticDefaults()
        {
            /* Tooltip.SetDefault("Summons Incinerius, the Blazeborn Construct" +
                             "\n'May cause a prison of basalt to form if used anywhere in the underworld, unless if the prison is destroyed...'"); */

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 26;
            Item.rare = ItemRarityID.Lime;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
        }

        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(ModContent.NPCType<Incinerius>()) && player.ZoneUnderworldHeight;
        }

        public override bool? UseItem(Player player)
        {
            var entitySource = player.GetSource_FromThis();

            if (!DownedBossSystem.downedIncinerius)
            {
                Main.NewText("A Basalt Prison has formed!", 220, 0, 210);
                NPC.NewNPC(entitySource, (int)player.Center.X, (int)player.Center.Y - 150, ModContent.NPCType<BasaltPrison>());
            }
            else
            {
                NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<Incinerius>());
            }
            
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Obsidian, 8)
                .AddIngredient(ItemID.Ectoplasm, 16)
                .AddIngredient(ItemID.HellstoneBar, 4)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
