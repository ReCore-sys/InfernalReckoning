using InfernalReckoning.Items.Placeable;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace InfernalReckoning.Tiles
{
	public class Altar : ModTile
	{
		public override void SetDefaults()
		{
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			Main.tileSolidTop[Type] = false;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileTable[Type] = true;
			Main.tileLavaDeath[Type] = true;
			//TileObjectData.newTile.CoordinateHeights = new[] { 16, 16,};
			//TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.Width = 6;
			TileObjectData.addTile(Type);
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Altar");
			AddMapEntry(new Color(200, 200, 200), name);
			disableSmartCursor = true;
			adjTiles = new int[] {TileID.WorkBenches};
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<AltarItem>());
		}
	}
}