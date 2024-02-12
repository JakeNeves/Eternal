using Eternal.Content.NPCs.Boss.SubzeroElemental;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Summon
{
    public class CrownofTheTundra : ModItem
    {
        public override void SetStaticDefaults()
        {
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
            return !NPC.AnyNPCs(ModContent.NPCType<SubzeroElemental>()) && !NPC.AnyNPCs(ModContent.NPCType<SubzeroElementalRobe>()) && player.ZoneSnow;
        }

        public override bool? UseItem(Player player)
        {
            var entitySource = player.GetSource_FromThis();

            Main.NewText("A Subzero Elemental has awoken!", 220, 0, 210);
            NPC.NewNPC(entitySource, (int)player.Center.X, (int)player.Center.Y - 150, ModContent.NPCType<SubzeroElementalRobe>());
            
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.IceBlock, 8)
                .AddIngredient(ItemID.Ectoplasm, 16)
                .AddIngredient(ItemID.FrostCore, 4)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
