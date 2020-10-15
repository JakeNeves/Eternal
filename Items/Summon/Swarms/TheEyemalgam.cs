using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Summon.Swarms
{
    public class TheEyemalgam : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Summons Multiple Eyes of Cthulhu\n'Multiple Presences are watching...'");
        }

        public override void SetDefaults()
        {
            item.width = 68;
            item.height = 48;
            item.rare = ItemRarityID.White;
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = ItemUseStyleID.HoldingUp;
        }

        public override bool CanUseItem(Player player)
        {
            return !Main.dayTime && !NPC.AnyNPCs(NPCID.EyeofCthulhu);
        }

        public override bool UseItem(Player player)
        {
            Main.NewText("The Eyes of Cthulhu are Approaching", 175, 75, 255);
            NPC.NewNPC((int)player.Center.X - 812, (int)player.Center.Y, NPCID.EyeofCthulhu);
            NPC.NewNPC((int)player.Center.X - 824, (int)player.Center.Y, NPCID.EyeofCthulhu);
            NPC.NewNPC((int)player.Center.X - 836, (int)player.Center.Y, NPCID.EyeofCthulhu);
            NPC.NewNPC((int)player.Center.X - 848, (int)player.Center.Y, NPCID.EyeofCthulhu);
            NPC.NewNPC((int)player.Center.X - 860, (int)player.Center.Y, NPCID.EyeofCthulhu);
            NPC.NewNPC((int)player.Center.X - 872, (int)player.Center.Y, NPCID.EyeofCthulhu);
            NPC.NewNPC((int)player.Center.X - 884, (int)player.Center.Y, NPCID.EyeofCthulhu);
            NPC.NewNPC((int)player.Center.X - 908, (int)player.Center.Y, NPCID.EyeofCthulhu);
            NPC.NewNPC((int)player.Center.X - 920, (int)player.Center.Y, NPCID.EyeofCthulhu);
            NPC.NewNPC((int)player.Center.X - 932, (int)player.Center.Y, NPCID.EyeofCthulhu);
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.DemonAltar);
            recipe.AddIngredient(ItemID.SuspiciousLookingEye, 10);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
