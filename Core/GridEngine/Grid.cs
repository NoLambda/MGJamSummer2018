using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MGJamSummer2018.Core
{
    public class Grid
    {
        [XmlElement("Name")] public string Name { get; set; }
        [XmlElement("Layer")] public List<Layer> Layers { get; set; }

        public Grid()
        {
            Layers = new List<Layer>();
        }

        public void LoadContent(ContentManager content)
        {
            for (int i = 0; i < Layers.Count; i++)
            {
                Layers[i].LoadContent(content);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Layers.Count; i++)
            {
                Layers[i].Draw(spriteBatch);
            }
        }
    }
}