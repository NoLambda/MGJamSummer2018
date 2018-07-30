using System.Collections.Generic;
using System.Xml.Serialization;
using MGJamSummer2018.Entities;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MGJamSummer2018.Core
{
    public class Grid
    {
        /// <summary> Name of the grid (Defined in xml). </summary>
        [XmlElement("Name")] public string Name { get; set; }

        /// <summary> List of layers (Defined in xml). </summary>
        [XmlElement("Layer")] public List<Layer> Layers { get; set; }

        public Grid()
        {
            Layers = new List<Layer>();
        }

        /// <summary>
        /// For all the layers in this grid, load there individual content.
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            for (int i = 0; i < Layers.Count; i++)
            {
                Layers[i].LoadContent(content);
            }
        }

        /// <summary>
        /// For all the layers in this grid, draw there individual content.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Layers.Count; i++)
            {
                Layers[i].Draw(spriteBatch);
            }
        }

        /// <summary>
        /// For all the layers in this grid, pdate there individual content.
        /// </summary>
        /// <param name="player"></param>
        public void Update(Player player)
        {
            for (int i = 0; i < Layers.Count; i++)
            {
                Layers[i].Update(player);
            }
        }
    }
}