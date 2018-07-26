using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace MGJamSummer2018.Core
{
    class AssetManager
    {
        public static AssetManager Instance { get { return instance ?? (instance = new AssetManager()); } }
        private static AssetManager instance;
        private List<SoundEffect> sfxCache;
        private ContentManager content;
        public AssetManager()
        {
            sfxCache = new List<SoundEffect>();
        }

        public void SetContentManager(ContentManager _content) { content = _content; }
        public ContentManager GetContentManager { get => content; }
        public Texture2D GetSprite(string path) { return content.Load<Texture2D>(path); }
        public SpriteFont GetFont(string path) { return content.Load<SpriteFont>(path); }
        public Effect GetShader(string path) { return content.Load<Effect>(path); }
        public void PlaySound(string path) { SoundEffect snd = content.Load<SoundEffect>(path); snd.Play(); }
        public void PlayMusic(string path, bool repeat) { MediaPlayer.IsRepeating = repeat; MediaPlayer.Play(content.Load<Song>(path)); }
        public void MusicVolume(float percent) { percent = percent / 100f; MediaPlayer.Volume = percent; }

    }
}
