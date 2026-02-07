using Eternal.Content.Projectiles.Weapons.Magic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Magic
{
    public class Incinerator : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 28;
            Item.damage = 90;
            Item.DamageType = DamageClass.Magic;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.mana = 6;
            Item.knockBack = 0f;
            Item.useAnimation = 24;
            Item.useTime = 24;
            Item.shoot = ModContent.ProjectileType<IncineratorProjectile>();
            Item.shootSpeed = 8f;
            Item.UseSound = SoundID.Item45;
            Item.rare = ItemRarityID.Lime;
            Item.autoReuse = true;
            Item.noMelee = true;
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 2;
        }
    }
}
