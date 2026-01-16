using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Weapons.Melee;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class Meatsaw : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

            ItemID.Sets.IsDrill[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 22;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.rare = ItemRarityID.Yellow;
            Item.damage = 100;
            Item.useAnimation = 16;
            Item.useTime = 4;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 0.75f;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item23;
            Item.shoot = ModContent.ProjectileType<MeatsawProjectile>();
            Item.shootSpeed = 32f;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.channel = true;
        }
    }
}
