using Eternal.Items;
using Eternal.Projectiles;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Weapons
{
    class Permafrost : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Let me cool you off!'");
        }

        public override void SetDefaults()
        {
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.width = 34;
            item.height = 30;
            item.useAnimation = 25;
            item.useTime = 25;
            item.shootSpeed = 60f;
            item.knockBack = 20f;
            item.damage = 1024;
            item.rare = ItemRarityID.Red;

            item.melee = true;
            item.channel = true;
            item.noMelee = true;
            item.noUseGraphic = true;

            item.UseSound = SoundID.Item1;
            item.value = Item.sellPrice(gold: 10);
            item.shoot = ProjectileType<PermafrostProjectile>();
        }

        private static readonly int[] unwantedPrefixes = new int[] { PrefixID.Terrible, PrefixID.Dull, PrefixID.Annoying, PrefixID.Broken, PrefixID.Damaged, PrefixID.Shoddy };

        public override bool AllowPrefix(int pre)
        {
            if (Array.IndexOf(unwantedPrefixes, pre) > -1)
            {
                return false;
            }
            return true;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.Chilled, 120);
            target.AddBuff(BuffID.Frostburn, 120);
            target.AddBuff(BuffID.Frozen, 120);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.AddIngredient(ItemType<SydaniteBar>(), 20);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
