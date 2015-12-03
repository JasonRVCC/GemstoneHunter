using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using System.IO;
using Tile_Engine;

namespace Level_Editor
{
    public partial class MapEditor : Form
    {
        public Game1 game;

        public MapEditor()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game.Exit();
            Application.Exit();
        }

        private void LoadImageList()
        {
            string filepath = Application.StartupPath + @"\Content\Textures\PlatformTiles.png";
            Bitmap tileSheet = new Bitmap(filepath);
            int tilecount = 0;

            for (int y = 0; y < tileSheet.Height / TileMap.TileHeight; y++)
            {
                for (int x = 0; x < tileSheet.Width / TileMap.TileWidth; x++)
                {
                    Bitmap newBitmap = tileSheet.Clone(new System.Drawing.Rectangle(x * TileMap.TileWidth,
                        y * TileMap.TileHeight, TileMap.TileWidth, TileMap.TileHeight),
                        System.Drawing.Imaging.PixelFormat.DontCare);
                    imgListTiles.Images.Add(newBitmap);

                    string itemName = "";
                    if (tilecount == 0)
                    { itemName = "Empty"; }
                    if (tilecount == 1)
                    { itemName = "White"; }
                    listTiles.Items.Add(new ListViewItem(itemName, tilecount++));
                }
            }
        }

        private void MapEditor_Load(object sender, EventArgs e)
        {
            LoadImageList();
            FixScrollBarScales();
            FixScrollBarScales();

            cboCodeValues.Items.Clear();
            cboCodeValues.Items.Add("Gemstone");
            cboCodeValues.Items.Add("Enemy");
            cboCodeValues.Items.Add("Lethal");
            cboCodeValues.Items.Add("EnemyBlocking");
            cboCodeValues.Items.Add("Start");
            cboCodeValues.Items.Add("Clear");
            cboCodeValues.Items.Add("Custom");

            for (int x = 0; x < 100; x++)
            { cboMapNumber.Items.Add(x.ToString().PadLeft(3, '0')); }

            cboMapNumber.SelectedIndex = 0;
            TileMap.EditorMode = true;
        }

        private void FixScrollBarScales()
        {
            Camera.ViewPortWidth = pctSurface.Width;
            Camera.ViewPortHeight = pctSurface.Height;

            Camera.Move(Vector2.Zero);

            vScrollBar1.Minimum = 0;
            vScrollBar1.Maximum = Camera.WorldRectangle.Height - Camera.ViewPortHeight;

            hScrollBar1.Minimum = 0;
            hScrollBar1.Maximum = Camera.WorldRectangle.Width - Camera.ViewPortWidth;
        }

        private void MapEditor_Resize(object sender, EventArgs e)
        {
            FixScrollBarScales();
        }

        private void cboCodeValues_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNewCode.Enabled = false;
            switch (cboCodeValues.Items[cboCodeValues.SelectedIndex].ToString())
            {
                case "Gemstone":
                    txtNewCode.Text = "GEM";
                    break;
                case "Enemy":
                    txtNewCode.Text = "ENEMY";
                    break;
                case "Lethal":
                    txtNewCode.Text = "DEAD";
                    break;
                case "EnemyBlocking":
                    txtNewCode.Text = "BLOCK";
                    break;
                case "Start":
                    txtNewCode.Text = "START";
                    break;
                case "Clear": 
                    txtNewCode.Text = "";
                    break;
                case "Custom":
                    txtNewCode.Text = "";
                    txtNewCode.Enabled = true;
                    break;
            }
        }
    }
}
