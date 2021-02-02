using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Weapons.Melee
{
    public class DroxCleaver : ModItem
    {
        public override void SetStaticDefaults()
        {
            EternalGlowmask.AddGlowMask(item.type, "Eternal/Items/Weapons/Melee/DroxCleaver_Glow");
        }

        public override void SetDefaults()
        {
            item.width = 48;
            item.melee = true;
            item.height = 64;
            item.damage = 196;
            item.useTime = 30;
            item.useAnimation = 30;
            item.knockBack = 4.4f;
            item.value = Item.sellPrice(gold: 1, silver: 40);
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.UseSound = SoundID.Item15;
            item.autoReuse = true;
            item.rare = ItemRarityID.Lime;
        }

         public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.AddIngredient(ItemType<DroxPlate>(), 4);
            recipe.AddIngredient(ItemType<DroxCore>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        /*public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.Bleeding, 120);
            target.AddBuff(BuffID.Confused, 120);
        }*/
    }
}
