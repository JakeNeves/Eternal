using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Content.NPCs.Boss.Igneopede;
using Terraria.GameContent.Creative;

namespace Eternal.Content.Items.Summon
{
    public class MoltenBait : ModItem
    {
        public override void SetStaticDefaults()
        {
            /* Tooltip.SetDefault("Attracts a giant lava centipede" +
                               "\n'This is not food'"); */

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 10;
            Item.maxStack = 9999;
            Item.rare = ItemRarityID.Pink;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = true;
        }

        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(ModContent.NPCType<IgneopedeHead>()) && player.ZoneUnderworldHeight;
        }

        public override bool? UseItem(Player player)
        {
            NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<IgneopedeHead>());
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Obsidian, 10)
                .AddIngredient(ItemID.Bone, 6)
                .AddIngredient(ItemID.HellstoneBar, 12)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}
