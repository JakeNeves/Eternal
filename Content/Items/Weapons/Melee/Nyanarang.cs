using Eternal.Content.Projectiles.Weapons.Melee;
using Eternal.Content.Rarities;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class Nyanarang : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.DamageType = DamageClass.Melee;
            Item.rare = ModContent.RarityType<Teal>();
            Item.noMelee = true;
            Item.damage = 400;
            Item.useAnimation = 16;
            Item.useTime = 16;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<NyanarangProjectile>();
            Item.shootSpeed = 16f;
            Item.noUseGraphic = true;
            Item.knockBack = 3f;
        }
    }
}
