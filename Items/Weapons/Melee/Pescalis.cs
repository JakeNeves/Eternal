using Eternal.Items.Accessories;
using Eternal.Items.Materials;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Weapons.Melee
{
    public class Pescalis : ModItem
    {
        public override void SetStaticDefaults()
        {
            //This is a post-Moon Lord Weapon
            Tooltip.SetDefault("[c/008060:Ancient Artifact]\nThis blade was once weilded by a cosmic champion, until he met his fate with doom,something had killed the champion in one attack! \nHowever, the campion's soul quickly reincarnate with it's brittle body and the champion eventually found a way to cheat death, but the strategy remains unknown...");
        }

        public override void SetDefaults()
        {
            //Things here may change...
            item.width = 62;
            item.height = 62;
            item.damage = 11000;
            item.knockBack = 92;
            item.value = Item.buyPrice(platinum: 1, gold: 3);
            item.useTime = 15;
            item.useAnimation = 15;
            item.UseSound = SoundID.Item1;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.rare = 
            item.rare = ItemRarityID.Red;
            item.autoReuse = true;
            item.melee = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = EternalColor.Teal;
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<DormantHeroSword>());
            recipe.AddIngredient(ItemType<AncientPendant>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
