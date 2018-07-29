using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace MGJamSummer2018.Core
{
    public class AssetManager
    {
        private Dictionary<string, SpriteSheet> spriteSheets;
        public static AssetManager Instance { get { return instance ?? (instance = new AssetManager()); } }
        private static AssetManager instance;
        private ContentManager content;

        public AssetManager()
        {
            spriteSheets = new Dictionary<string, SpriteSheet>();
        }

        // General
        public void Init(ContentManager _content) { content = _content; }
        public ContentManager GetContentManager { get => content; }

        public SpriteSheet GetSpriteSheet(string sheetName)
        {
            if (!spriteSheets.ContainsKey(sheetName))
                spriteSheets.Add(sheetName, new SpriteSheet(sheetName));
            return spriteSheets[sheetName];
        }

        // Graphics
        public Texture2D GetSprite(string path) { return content.Load<Texture2D>(path); }
        public SpriteFont GetFont(string path) { return content.Load<SpriteFont>(path); }
        public Effect GetShader(string path) { return content.Load<Effect>(path); }

        // Music & SFX
        public void PlaySound(string path) { SoundEffect snd = content.Load<SoundEffect>(path); snd.Play(); }
        public void PlayMusic(string path, bool repeat) { MediaPlayer.IsRepeating = repeat; MediaPlayer.Play(content.Load<Song>(path)); }
        public void MusicVolume(float percent) { percent = percent / 100f; MediaPlayer.Volume = percent; }

    }
}
