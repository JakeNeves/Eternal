using Eternal.Common.Configurations;
using Eternal.Content.Projectiles.Weapons.Melee;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class HellHacker : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 72;
            Item.height = 72;
            Item.DamageType = DamageClass.Melee;
            Item.rare = ItemRarityID.Cyan;
            Item.damage = 120;
            Item.useAnimation = 12;
            Item.useTime = 12;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 2.5f;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<HellHackerProjectile>();
            Item.shootSpeed = 8f;
            Item.noUseGraphic = true;
            Item.noMelee = true;
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }
    }
}
