using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace MGJamSummer2018.Core
{
    class SpriteSheet
    {
        // Animation variables
        protected float frameTime, time;
        protected bool looping, isAnimated = false;
        protected AnimMetaData currentAnimation;

        // Normal Sprite sheet variables
        protected bool[] isTransparent;
        protected Texture2D sheet;
        protected int frameWidth, frameHeight, spacingHeight;
        protected int frameCounter = 0;
        protected bool mirrored;
        Dictionary<string, AnimMetaData> animData;
        public SpriteSheet(string sheetPath, bool animated = false, float _frameTime = 0.1f)
        {
            isAnimated = animated;
            frameTime = _frameTime;
            sheet = AssetManager.Instance.GetSprite(sheetPath);

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
                int currFrameStart = 0;
                for (int i = 1; i < metaData.Count; i++)
                {
                    line = metaData[i].Split(' ');
                    animData.Add(line[0], new AnimMetaData(int.Parse(line[1]), Height * (i - 1) + spacingHeight * i));
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
            time = 0.0f;
            frameCounter = 0;
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
                    frameCounter++;
                    if (frameCounter >= currentAnimation.AmountOfFrames)
                    {
                        if (looping)
                            frameCounter = 0;
                    }
                   

                }
            }
        }

        public void Draw(Vector2 pos, Vector2 origin, Color color)
        {
            Rectangle currSpriteBox = new Rectangle(frameCounter * frameWidth, currentAnimation.startPosY, frameWidth, frameHeight);
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (mirrored)
                spriteEffects = SpriteEffects.FlipHorizontally;
            GraphicsManager.Instance.SpriteBatch.Draw(sheet, pos, currSpriteBox, color, 0.0f, origin, 1.0f, spriteEffects, 0.0f);
        }

        public Dictionary<string, AnimMetaData> SheetMetaData { get => animData; }
        public Texture2D SprSheet { get => sheet; }
        public bool Mirrored { get => mirrored; set => mirrored = value; }

        public int Width { get => frameWidth; }
        public int Height { get => frameHeight; }

        // Animation properties
        public void ToggleAnimation() => isAnimated = !isAnimated;
        public bool LoopAnimation { get => looping; set => looping = value; }
    }

    struct AnimMetaData
    {
        public int AmountOfFrames, startPosY;
        public AnimMetaData(int frames, int startPixelY)
        {
            AmountOfFrames = frames;
            startPosY = startPixelY;
        }
    }
}
