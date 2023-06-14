using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class InfernomancerRobe : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void Load()
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }

            EquipLoader.AddEquipTexture(Mod, $"{Texture}_{EquipType.Legs}", EquipType.Legs, this);
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 30;
            Item.value = Item.sellPrice(gold: 5);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 15;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Obsidian, 20)
                .AddIngredient(ItemID.HellstoneBar, 10)
                .AddIngredient(ItemID.Silk, 30)
                .AddTile(TileID.Anvils)
                .Register();
        }
        public override void SetMatch(bool male, ref int equipSlot, ref bool robes)
        {
            robes = true;
            equipSlot = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Legs);
        }
    }
}
