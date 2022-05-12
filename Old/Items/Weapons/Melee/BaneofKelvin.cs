using Eternal.Projectiles.Weapons.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons.Melee
{
    public class BaneofKelvin : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bane of Kelvin");
            Tooltip.SetDefault("Inflicts Frostburn");
        }

        public override void SetDefaults()
        {
            item.width = 46;
            item.height = 46;
            item.shootSpeed = 12f;
            item.damage = 110;
            item.knockBack = 10.5f;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 26;
            item.useTime = 26;
            item.UseSound = SoundID.Item1;
            item.melee = true;
            item.rare = ItemRarityID.Red;
            item.autoReuse = true;
            item.value = Item.sellPrice(gold: 16);
            item.shoot = ModContent.ProjectileType<FridgedSpikeMelee>();
        }
    }
}
