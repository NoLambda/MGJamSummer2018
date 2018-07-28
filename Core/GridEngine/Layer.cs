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
        [XmlElement("Name")] public string Name { get; set; }
        [XmlElement("Row")] public List<string> Row { get; set; }
        [XmlElement("Import")] public List<string> Import { get; set; }
        [XmlElement("Collideable")] public List<string> Collideable { get; set; }

        [XmlIgnore] public List<Texture2D> Textures { get; set; }
        [XmlIgnore] public List<Cell> Cells { get; set; }

        public Layer()
        {
            Textures = new List<Texture2D>();
            Collideable = new List<string>();
            Cells = new List<Cell>();
            Row = new List<string>();
        }

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

        public void Update(Player player)
        {
            for (int i = 0; i < Cells.Count; i++)
            {
                Cells[i].Update(player);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Cells.Count; i++)
            {
                spriteBatch.Draw(Cells[i].Texture, Cells[i].Position, Color.White);
            }
        }
    }
}