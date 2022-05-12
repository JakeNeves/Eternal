using Eternal.Buffs.Minions;
using Eternal.Items.Materials;
using Eternal.Projectiles.Minions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons.Summon
{
    public class CryonicStave : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Creates an ancient Cryonic Construct" +
                             "\n'Ice to see that the frosty immeterializer got an upgrade.'");

            ItemID.Sets.GamepadWholeScreenUseRange[item.type] = true;
            ItemID.Sets.LockOnIgnoresCollision[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 40;
            item.damage = 110;
            item.mana = 60;
            item.knockBack = 9.5f;
            item.summon = true;
            item.noMelee = true;
            item.UseSound = SoundID.DD2_DefenseTowerSpawn;
            item.useTime = 20;
            item.useAnimation = 20;
            item.autoReuse = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.value = Item.sellPrice(gold: 30);
            item.rare = ItemRarityID.Red;
            item.buffType = ModContent.BuffType<CryonicRollerBuff>();
            item.shoot = ModContent.ProjectileType<CryonicRoller>();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.AddIngredient(ItemID.LunarBar, 12);
            recipe.AddIngredient(ItemID.FragmentStardust, 4);
            recipe.AddIngredient(ModContent.ItemType<SydaniteBar>(), 8);
            recipe.AddIngredient(ModContent.ItemType<FrostyImmaterializer>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            player.AddBuff(item.buffType, 2);

            position = Main.MouseWorld;
            return true;
        }

    }
}
