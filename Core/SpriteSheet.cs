using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace MGJamSummer2018.Core
{
    class SpriteSheet
    {
        protected bool[] isTransparent;
        protected Texture2D sheet;
        protected int frameWidth, frameHeight, spacingHeight;
        protected int rows, columns, currIndex;
        protected bool mirrored;
        Dictionary<string, AnimMetaData> animData;
        public SpriteSheet(string sheetPath)
        {
            sheet = AssetManager.Instance.GetSprite(sheetPath);
            //Color[] sheetColors = new Color[sheet.Width * sheet.Height];
            //sheet.GetData(sheetColors);
            //for (int i = 0; i < sheetColors.Length; i++)
            //    isTransparent[i] = sheetColors[i].A != 0;
            List<string> metaData = new List<string>();
            animData = new Dictionary<string, AnimMetaData>();
            string metaDataPath = sheetPath.Replace(".png", ".txt");
            try
            {
                StreamReader sr = new StreamReader(metaDataPath);
                while (sr != null)
                    metaData.Add(sr.ReadLine());
                string[] line = metaData[0].Split(' ');
                frameWidth = int.Parse(line[0]); frameHeight = int.Parse(line[1]); spacingHeight = int.Parse(line[2]);
                int spacingCount = metaData.Count - 1;
                rows = (sheet.Height - spacingCount * spacingHeight) / frameHeight;
                columns = sheet.Width / frameWidth;
                int currFrameStart = 0;
                for(int i = 1; i < metaData.Count; i++)
                {
                    line = metaData[0].Split(' ');
                    animData.Add(line[0], new AnimMetaData(currFrameStart, currFrameStart + int.Parse(line[1])));
                    currFrameStart += int.Parse(line[1]);
                }
            }
            catch
            {
                throw new IOException("Error parsing metadata for sprite with path" + sheetPath + 
                    "/n check if .txt is named the same as the sprite and if the file conventions are correct!");
            }
        }

        public void Draw(Vector2 pos, Vector2 origin, Color color)
        {
            int currRow = currIndex / columns;
            int currCol = currIndex % rows;
            Rectangle currSpriteBox = new Rectangle(currCol * frameWidth, currRow * frameHeight, frameWidth, frameHeight);
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (mirrored)
                spriteEffects = SpriteEffects.FlipHorizontally;
            GraphicsManager.Instance.SpriteBatch.Draw(sheet, pos, currSpriteBox, color, 0.0f, origin, 1.0f, spriteEffects, 0.0f);
        }
        //public bool isOccupied(int x, int y)
        //{
        //    int currRow = currIndex / columns;
        //    int currCol = currIndex % rows;

        //    return isTransparent[currCol * frameWidth + x + (currRow * frameHeight + y) * sheet.Width];
        //}

        public Dictionary<string, AnimMetaData> SheetMetaData { get => animData; }
        public Texture2D SprSheet { get => sheet; }
        public bool Mirrored { get => mirrored; set => mirrored = value; }

        public int Width { get => frameWidth; }
        public int Height { get => frameHeight; }
        public int SheetIndex { get => currIndex; set { if (value < columns * rows && value >= 0) { currIndex = value; } } }

    }



    struct AnimMetaData
    {
        public int FrameStart, FrameEnd;
        public AnimMetaData(int Fstart, int Fend)
        {
            FrameStart = Fstart;
            FrameEnd = Fend;
        }
    }
}
