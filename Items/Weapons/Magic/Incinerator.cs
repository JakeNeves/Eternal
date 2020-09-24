using Eternal.Projectiles.Weapons.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Weapons.Magic
{
    class Incinerator : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Hot Objects Included'");
        }

        public override void SetDefaults()
        {
            item.damage = 115;
            item.magic = true;
            item.mana = 20;
            item.width = 28;
            item.height = 34;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 0f;
            item.value = Item.sellPrice(silver: 9);
            item.rare = ItemRarityID.Pink;
            item.autoReuse = true;
            item.shootSpeed = 10f;
            item.shoot = ProjectileType<FlameSword>();
            item.UseSound = SoundID.NPCDeath52;
        }

    }
}
