using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons.Ranged
{
    public class CarmaniteDeadshot : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Feels fleshy...'");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 34;
            item.damage = 22;
            item.knockBack = 2.6f;
            item.noMelee = true;
            item.ranged = true;
            item.useTime = 16;
            item.useAnimation = 16;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.UseSound = SoundID.Item5;
            item.autoReuse = true;
            item.shootSpeed = 6f;
            item.shoot = AmmoID.Arrow;
            item.useAmmo = AmmoID.Arrow;
            item.rare = ItemRarityID.Green;
        }
    }
}
