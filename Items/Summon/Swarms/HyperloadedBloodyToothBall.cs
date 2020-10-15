using Eternal.NPCs.Boss.CarmaniteScouter;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Summon.Swarms
{
    public class HyperloadedBloodyToothBall : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Summons Multiple Carmanite Scouters\n'The Scouters are Approaching'");
        }

        public override void SetDefaults()
        {
            item.width = 42;
            item.height = 36;
            item.rare = ItemRarityID.Green;
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = ItemUseStyleID.HoldingUp;
        }

        public override bool CanUseItem(Player player)
        {
            return !Main.dayTime && !NPC.AnyNPCs(NPCType<CarmaniteScouter>());
        }

        public override bool UseItem(Player player)
        {
            Main.NewText("The Carmanite Infested Amagamates are swarming", 175, 75, 255);
            NPC.NewNPC((int)player.Center.X - 812, (int)player.Center.Y, NPCType<CarmaniteScouter>());
            NPC.NewNPC((int)player.Center.X - 824, (int)player.Center.Y, NPCType<CarmaniteScouter>());
            NPC.NewNPC((int)player.Center.X - 836, (int)player.Center.Y, NPCType<CarmaniteScouter>());
            NPC.NewNPC((int)player.Center.X - 848, (int)player.Center.Y, NPCType<CarmaniteScouter>());
            NPC.NewNPC((int)player.Center.X - 860, (int)player.Center.Y, NPCType<CarmaniteScouter>());
            NPC.NewNPC((int)player.Center.X - 872, (int)player.Center.Y, NPCType<CarmaniteScouter>());
            NPC.NewNPC((int)player.Center.X - 884, (int)player.Center.Y, NPCType<CarmaniteScouter>());
            NPC.NewNPC((int)player.Center.X - 908, (int)player.Center.Y, NPCType<CarmaniteScouter>());
            NPC.NewNPC((int)player.Center.X - 920, (int)player.Center.Y, NPCType<CarmaniteScouter>());
            NPC.NewNPC((int)player.Center.X - 932, (int)player.Center.Y, NPCType<CarmaniteScouter>());
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.DemonAltar);
            recipe.AddIngredient(ItemType<SuspiciousLookingDroplet>(), 10);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
