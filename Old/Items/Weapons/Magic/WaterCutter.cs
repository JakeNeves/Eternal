using Eternal.Projectiles.Weapons.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Weapons.Magic
{
    public class WaterCutter : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires a Surprisingly Fast Water Cutter\nThis weapon is op early game, so it might ruin your mage playthrough...");
        }

        public override void SetDefaults()
        {
            item.damage = 50;
            item.magic = true;
            item.mana = 15;
            item.width = 28;
            item.height = 32;
            item.useTime = 6;
            item.useAnimation = 6;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 0f;
            item.value = Item.sellPrice(silver: 9);
            item.rare = ItemRarityID.Blue;
            item.autoReuse = true;
            item.shootSpeed = 20f;
            item.shoot = ProjectileType<WaterCutterProjectile>();
            item.UseSound = SoundID.LiquidsWaterLava;
        }

    }
}
