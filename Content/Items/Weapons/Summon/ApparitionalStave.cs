using Eternal.Content.Buffs.Minions;
using Eternal.Content.Projectiles.Minions;
using Eternal.Content.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Summon
{
    public class ApparitionalStave : ModItem
    {

        public override void SetStaticDefaults()
        {
            /* Tooltip.SetDefault("Creates a cosmic decoy to haunt your enemies" +
                             "\n'The strangest halucinations you will ever witness...'"); */

            ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true;
            ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;
            Item.damage = 400;
            Item.mana = 60;
            Item.knockBack = 9.5f;
            Item.DamageType = DamageClass.Summon;
            Item.noMelee = true;
            Item.UseSound = SoundID.DD2_DefenseTowerSpawn;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.autoReuse = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = Item.sellPrice(gold: 30);
            Item.rare = ModContent.RarityType<Teal>();
            Item.buffType = ModContent.BuffType<CosmicDecoy>();
            Item.shoot = ModContent.ProjectileType<CosmicDecoyMinion>();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.AddBuff(Item.buffType, 2);

            position = Main.MouseWorld;
            return true;
        }

    }
}
