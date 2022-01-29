using Eternal.Items.Materials;
using Eternal.Items.Materials.Elementalblights;
using Eternal.NPCs.Boss.CosmicEmperor;
using Eternal.Tiles;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Summon
{
    public class CosmicTablet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Inscribed with sacred primordial handwritting" +
                                "\nUnleashes the Emperor of the cosmos" +
                                "\n'Only those who have prooven there worth, sha'll be challenged.'");
        }

        public override void SetDefaults()
        {
            item.width = 36;
            item.height = 52;
            item.rare = ItemRarityID.Red;
            item.useTime = 45;
            item.useAnimation = 45;
            item.useStyle = ItemUseStyleID.HoldingUp;
        }

        public override bool UseItem(Player player)
        {
            if (!EternalWorld.downedArkOfImperious)
            {
                Main.NewText("Proove that you can challenge me first...", 0, 95, 215);
                player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " disinigrated into stardust"), 10000, 1, false);
            }
            else
            {
                NPC.NewNPC((int)player.Center.X, (int)player.Center.Y - 900, ModContent.NPCType<CosmicEmperorMask>());
                Main.NewText("To whom, who has challenged greater beings... I have been watching you gain strength ever sinced you've slayed the mighty lord, banished to the moon!", 0, 95, 215);
            }
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(ModContent.TileType<AncientForge>());
            recipe.AddIngredient(ModContent.ItemType<CoreofEternal>(), 12);
            recipe.AddIngredient(ModContent.ItemType<CometiteBar>(), 16);
            recipe.AddIngredient(ModContent.ItemType<DuskblightCrystal>(), 24);
            recipe.AddIngredient(ModContent.ItemType<CometiteCrystal>(), 20);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = EternalColor.Magenta;
                }
            }
        }
    }
}
