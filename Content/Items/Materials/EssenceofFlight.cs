using Eternal.Content.Rarities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Materials
{
    public class EssenceofFlight : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemNoGravity[Item.type] = true;

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 32;
            Item.value = Item.sellPrice(silver: 80);
            Item.rare = ItemRarityID.Yellow;
            Item.maxStack = 9999;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.LightSkyBlue.ToVector3() * 0.55f * Main.essScale);
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255, 50);
        }
    }
}
