using Eternal.Content.Projectiles.Weapons.Melee;
using Eternal.Content.Rarities;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class SolSythe : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Not to be confused with 'Soul Sythe'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 66;
            Item.height = 62;
            Item.DamageType = DamageClass.Melee;
            Item.rare = ModContent.RarityType<Teal>();
            Item.damage = 300;
            Item.useAnimation = 12;
            Item.useTime = 12;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 2.5f;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<SolSytheProjectile>();
            Item.shootSpeed = 14f;
            Item.noUseGraphic = true;
            Item.noMelee = true;
        }
    }
}
