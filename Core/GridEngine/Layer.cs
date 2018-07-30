using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using MGJamSummer2018.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MGJamSummer2018.Core
{
    public class Layer
    {
        /// <summary> Name of the layer (Defined in xml). </summary>
        [XmlElement("Name")] public string Name { get; set; }

        /// <summary> List of row for the current layer (Defined in xml). </summary>
        [XmlElement("Row")] public List<string> Row { get; set; }

        /// <summary> List of textures to use in this layer (Defined in xml). </summary>
        [XmlElement("Import")] public List<string> Import { get; set; }

        /// <summary> List of the cell that should have collision (Defined in xml). </summary>
        [XmlElement("Collideable")] public List<string> Collideable { get; set; }

        /// <summary> Final list of textures. </summary>
        [XmlIgnore] public List<Texture2D> Textures { get; set; }

        /// <summary> Final list of cells. </summary>
        [XmlIgnore] public List<Cell> Cells { get; set; }

        public Layer()
        {
            Textures = new List<Texture2D>();
            Collideable = new List<string>();
            Cells = new List<Cell>();
            Row = new List<string>();
        }

        /// <summary>
        /// Load all the cells textures from (import),
        /// For all the rows of the current layer, create the cell,
        /// if the cell exist in the list "collideable", then add
        /// collision on it.
        /// </summary>
        public void LoadContent(ContentManager content)
        {
            Vector2 offset = new Vector2(0, 0);

            for (int i = 0; i < Import.Count; i++)
            {
                var tempTexture = new Texture2D(GraphicsManager.Instance.GraphicsDeviceManager.GraphicsDevice, 1, 1);
                tempTexture = content.Load<Texture2D>(Import[i]);
                Textures.Add(tempTexture);
            }

            for (int x = 0; x < Row.Count; x++)
            {
                string[] exclude = Row[x].Split("]");

                offset.X = 0;
                offset.Y += 16;

                for (int y = 0; y < exclude.Length; y++)
                {
                    offset.X += 16;
                    if (exclude[y] != String.Empty && !exclude[y].Contains("_"))
                    {
                        exclude[y] = exclude[y].Replace("[", String.Empty);
                        int id = int.Parse(exclude[y].Substring(0, 1));

                        if (Collideable.Count > 1 && Collideable[id].Contains($"[{id}]"))
                            Cells.Add(new Cell(Textures[id], offset, true));
                        else
                            Cells.Add(new Cell(Textures[id], offset, false));
                    }
                }
            }
        }
        
        /// <summary>
        /// For all the cells added into the final list of cells, 
        /// call it's draw method
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Cells.Count; i++)
            {
                spriteBatch.Draw(Cells[i].Texture, Cells[i].Position, Color.White);
            }
        }
    }
}