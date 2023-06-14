using Eternal.Content.Projectiles.Weapons.Magic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Magic
{
    public class StarstaveEin : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Starstave Ein");
            // Tooltip.SetDefault("'Stave? Staff? You decide...'");
            Item.staff[Item.type] = true;

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 44;
            Item.damage = 200;
            Item.DamageType = DamageClass.Magic;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.mana = 10;
            Item.knockBack = 4f;
            Item.useAnimation = 12;
            Item.useTime = 12;
            Item.shoot = ModContent.ProjectileType<StarstaveProjectile>();
            Item.shootSpeed = 6f;
            Item.UseSound = SoundID.Item8;
            Item.rare = ItemRarityID.Red;
            Item.autoReuse = true;
        }
    }
}
