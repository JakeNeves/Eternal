using Eternal.Items.Materials;
using Eternal.NPCs.Boss.AoI;
using Eternal.Tiles;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Summon
{
    public class RoyalShrineSword : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Summons the Ark of Imperious upon swinging, the god of living swords\n'A pulse of light shines towards the direction of the shrine'");
        }

        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 50;
            item.rare = ItemRarityID.Red;
            item.useAnimation = 15;
            item.useTime = 15;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.shoot = ModContent.ProjectileType<Projectiles.AoISpark>();
            item.shootSpeed = 6f;
            item.UseSound = SoundID.Item71;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = EternalColor.DarkTeal;
                }
            }
        }

        public override bool CanUseItem(Player player)
        {
            return Main.LocalPlayer.GetModPlayer<EternalPlayer>().ZoneLabrynth && !NPC.AnyNPCs(ModContent.NPCType<ArkofImperious>());
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(ModContent.TileType<Starforge>());
            recipe.AddIngredient(ModContent.ItemType<BrokenShrineSword>());
            recipe.AddIngredient(ModContent.ItemType<WeatheredPlating>(), 3);
            recipe.AddIngredient(ModContent.ItemType<CometiteBar>(), 10);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
