using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Projectiles.Minions;
using Eternal.Buffs.Minions;

namespace Eternal.Items.Weapons.Summon
{
    public class ShatteredShrineSword : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Calls upon the spirit of a formal-living ark" +
                             "\n'Not to be confused with the Terraprisma'");

            ItemID.Sets.GamepadWholeScreenUseRange[item.type] = true;
            ItemID.Sets.LockOnIgnoresCollision[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 34;
            item.damage = 4600;
            item.mana = 30;
            item.knockBack = 8f;
            item.summon = true;
            item.noMelee = true;
            item.UseSound = SoundID.Item44;
            item.useTime = 16;
            item.useAnimation = 16;
            item.autoReuse = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.value = Item.sellPrice(gold: 30);
            item.rare = ItemRarityID.Red;
            item.buffType = ModContent.BuffType<SpiritArkBuff>();
            item.shoot = ModContent.ProjectileType<SpiritArk>();
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

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            player.AddBuff(item.buffType, 2);

            position = Main.MouseWorld;
            return true;
        }

    }
}
