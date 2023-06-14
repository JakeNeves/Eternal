using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Content.NPCs.Boss.Duneworm;
using Terraria.GameContent.Creative;

namespace Eternal.Content.Items.Summon
{
    public class SandFood : ModItem
    {
        public override void SetStaticDefaults()
        {
            /* Tooltip.SetDefault("Attracts a giant sandworm" +
                               "\n'Not to be confused with Worm Food'"); */

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 9999;
            Item.rare = ItemRarityID.LightPurple;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = true;
        }

        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(ModContent.NPCType<DunewormHead>()) && player.ZoneDesert;
        }

        public override bool? UseItem(Player player)
        {
            NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<DunewormHead>());
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.SandBlock, 10)
                .AddIngredient(ItemID.SoulofLight, 6)
                .AddIngredient(ItemID.SoulofNight, 6)
                .AddIngredient(ItemID.Bone, 12)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}
