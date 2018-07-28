using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace MGJamSummer2018.Core
{
    public class SpriteSheet
    {
        // Animation variables
        protected float frameTime, time;
        protected bool looping, isAnimated = false;
        protected AnimMetaData currentAnimation;

        // Normal Sprite sheet variables
        protected bool[] isTransparent;
        protected Texture2D sheet;
        protected int frameWidth, frameHeight, spacingHeight;
        protected int rows, columns, currIndex;
        protected bool mirrored;
        Dictionary<string, AnimMetaData> animData;
        public SpriteSheet(string sheetPath, int sheetIndex, bool animated = false, float _frameTime = 0.1f)
        {
            isAnimated = animated;
            frameTime = _frameTime;
            currIndex = sheetIndex;
            sheet = AssetManager.Instance.GetSprite(sheetPath);
            //Color[] sheetColors = new Color[sheet.Width * sheet.Height];
            //sheet.GetData(sheetColors);
            //for (int i = 0; i < sheetColors.Length; i++)
            //    isTransparent[i] = sheetColors[i].A != 0;
            List<string> metaData = new List<string>();
            animData = new Dictionary<string, AnimMetaData>();
            string metaDataPath = sheetPath + ".txt";
            metaDataPath = metaDataPath.Insert(0, "Content/");
            try
            {
                StreamReader sr = new StreamReader(metaDataPath);
                string mLine = sr.ReadLine();
                while (mLine != null)
                {
                    metaData.Add(mLine);
                    mLine = sr.ReadLine();
                }

                string[] line = metaData[0].Split(' ');
                frameWidth = int.Parse(line[0]); frameHeight = int.Parse(line[1]); spacingHeight = int.Parse(line[2]);
                int spacingCount = metaData.Count - 1;
                rows = (sheet.Height - spacingCount * spacingHeight) / frameHeight;
                columns = sheet.Width / frameWidth;
                int currFrameStart = 0;
                for (int i = 1; i < metaData.Count; i++)
                {
                    line = metaData[i].Split(' ');
                    animData.Add(line[0], new AnimMetaData(currFrameStart, int.Parse(line[1])));
                    currFrameStart = int.Parse(line[1]);
                }
            }
            catch
            {
                throw new IOException("Error parsing metadata for sprite with path" + sheetPath +
                    "/n check if .txt is named the same as the sprite and if the file conventions are correct!");
            }
        }

        public void PlayAnimation(string name)
        {
            if(!isAnimated) { throw new Exception("Tried to call Playanimation on a non animated spritesheet"); }
            if(!animData.ContainsKey(name)) { throw new ArgumentException(name + " was not found within the loaded animation MetaData!"); }
            currentAnimation = animData[name];
            SheetIndex = currentAnimation.FrameStart;
            time = 0.0f;
        }

        public void Update(GameTime gTime)
        {
            if(!isAnimated) { return; }
            else
            {
                time += (float)gTime.ElapsedGameTime.TotalSeconds;
                while (time > frameTime)
                {
                    time -= frameTime;
                    currIndex++;
                    if ((currIndex - currentAnimation.FrameStart) > currentAnimation.AmountOfFrames)
                    {
                        currIndex = currentAnimation.FrameStart;
                        if (!looping)
                            isAnimated = false;
                    }
                    
                }
            }
        }

        public void Draw(Vector2 pos, Vector2 origin, Color color)
        {
            //int currRow = currIndex / columns;
            int currRow = currIndex / (columns % rows);
            int currCol = currIndex % columns;
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

        // Animation properties
        public void ToggleAnimation() => isAnimated = !isAnimated;
        public bool LoopAnimation { get => looping; set => looping = value; }
    }



    public struct AnimMetaData
    {
        public int FrameStart, AmountOfFrames;
        public AnimMetaData(int Fstart, int Fend)
        {
            FrameStart = Fstart;
            AmountOfFrames = Fend;
        }
    }
}
