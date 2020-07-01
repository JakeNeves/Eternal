using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons
{
    class ScimitarofTheDunes : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scimitar of The Dunes");
            Tooltip.SetDefault("This handcrafted blade was swung by kahogaus blademasters, their moves resembled the move of a fallen heroine... \nHowever, these blades were given to the most talented men of the kahogaus... \nIt mystery of it's evgravement is unknown!");
        }

        public override void SetDefaults()
        {
            item.damage = 36;
            item.melee = true;
            item.width = 40;
            item.height = 48;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 1;
            item.knockBack = 0;
            item.value = 0;
            item.rare = 3;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }
    }
}
