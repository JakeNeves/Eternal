using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Tools
{
    class PickaxeofTheDivineTerraformer : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pickaxe of The Divine Terraformer");
            Tooltip.SetDefault("A pickaxe crafted from Divine Metal and Ancient Crystal, Atomically Constructed... \nA weilder of this pickaxe gains potential power \nto cobble blocks of there desire...");
        }

        public override void SetDefaults()
        {
            item.damage = 200;
            item.melee = true;
            item.width = 60;
            item.height = 60;
            item.useTime = 10;
            item.useAnimation = 15;
            item.pick = 500;
            item.useStyle = 1;
            item.knockBack = 12;
            item.value = Item.buyPrice(gold: 30, silver: 75);
            item.rare = 10;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }
    }
}
