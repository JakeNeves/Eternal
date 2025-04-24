using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Weapons.Magic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Magic
{
    public class Prystaltine : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.LastPrism);
            Item.width = 26;
            Item.height = 30;
            Item.damage = 50;
            Item.mana = 4;
            Item.shoot = ModContent.ProjectileType<PrystaltineHoldout>();
            Item.shootSpeed = 30f;
            Item.rare = ItemRarityID.Lime;
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[ModContent.ProjectileType<PrystaltineHoldout>()] <= 0;
        }
    }
}
