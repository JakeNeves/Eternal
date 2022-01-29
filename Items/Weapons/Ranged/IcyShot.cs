using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons.Ranged
{
    public class IcyShot : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 58;
            item.damage = 110;
            item.knockBack = 2.6f;
            item.noMelee = true;
            item.ranged = true;
            item.useTime = 12;
            item.useAnimation = 12;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.UseSound = SoundID.Item5;
            item.autoReuse = true;
            item.shootSpeed = 36.75f;
            item.shoot = AmmoID.Arrow;
            item.useAmmo = AmmoID.Arrow;
            item.rare = ItemRarityID.Red;
        }
    }
}
