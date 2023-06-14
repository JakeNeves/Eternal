using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Content.Projectiles.Weapons.Melee;
using Eternal.Content.Rarities;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class StarofNihilion : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Star of Nihilion");
            // Tooltip.SetDefault("'Named after the silent nihiliophane demon, Nihilion'");

            ItemID.Sets.ToolTipDamageMultiplier[Type] = 2f;
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 310;
            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true;
            Item.scale = 1f;
            Item.noUseGraphic = true;
            Item.width = 26;
            Item.height = 28;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 6.5f;
            Item.rare = ModContent.RarityType<Magenta>();
            Item.value = Item.sellPrice(gold: 60);
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
            Item.channel = true;
            Item.shoot = ModContent.ProjectileType<StarofNihilionProjectile>();
            Item.shootSpeed = 32f;
        }
    }
}
