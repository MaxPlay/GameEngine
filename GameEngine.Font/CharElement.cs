using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using Point = System.Drawing.Point;

namespace GameEngine.Font
{
    /// <summary>
    /// A struct containing a character and a position on a texture, as well as the dimension of the character on the texture.
    /// </summary>
    public struct CharElement
    {
        /// <summary>
        /// The "name" of the charelement, represented by a char
        /// </summary>
        public char Name;
        /// <summary>
        /// The position on the source image
        /// </summary>
        public Point position;
        /// <summary>
        /// The dimension of the character in pixel
        /// </summary>
        public Point dimension;

        /// <summary>
        /// Initializes a new Instance of the CharElement with its required values. The default char ' ' will be used.
        /// </summary>
        /// <param name="x">The X value of the location</param>
        /// <param name="y">The Y value of the location</param>
        /// <param name="width">The width of the char</param>
        /// <param name="height">The height of the char</param>
        public CharElement(int x, int y, int width, int height)
        {
            Name = ' ';
            position = new Point(x, y);
            dimension = new Point(width, height);
        }

        /// <summary>
        /// Initializes a new Instance of the CharElement with its required values.
        /// </summary>
        /// <param name="name">The char that is represented by the element</param>
        /// <param name="x">The X value of the location</param>
        /// <param name="y">The Y value of the location</param>
        /// <param name="width">The width of the char</param>
        /// <param name="height">The height of the char</param>
        public CharElement(char name, int x, int y, int width, int height)
        {
            Name = name;
            position = new Point(x, y);
            dimension = new Point(width, height);
        }

        /// <summary>
        /// This method is used to create a System.Windows.Rect Object out of the position and dimension points.
        /// </summary>
        /// <returns></returns>
        public Rect ToRect()
        {
            return new Rect(position.X, position.Y, dimension.X, dimension.Y);
        }

        /// <summary>
        /// This method is used to create a relative System.Windows.Rect Object out of the position and dimension points. It is relative to the given height and width which are the max-values of the source rectangle.
        /// </summary>
        /// <example>position.X = 1.25 and Width = 2.5 will return Rect.X = 0.5</example>
        /// <param name="Width">The width of the source-Rectangle</param>
        /// <param name="Height">The height of the source-Rectangle</param>
        /// <returns>Relative rectangle containing the % values of the positon and dimension</returns>
        public Rect ToRect(double Width, double Height)
        {
            return new Rect(position.X / Width, position.Y / Height, dimension.X / Width, dimension.Y / Height);
        }

        /// <summary>
        /// Returns a string representation of the CharElement.
        /// </summary>
        /// <returns>A string representing the values of the class</returns>
        public override string ToString()
        {
            return "Character: " + this.Name;
        }

        /// <summary>
        /// This method is used to create a System.Drawing.Rectangle Object out of the position and dimension points.
        /// </summary>
        /// <returns></returns>
        public Rectangle ToRectangle()
        {
            return new Rectangle(position.X, position.Y, dimension.X, dimension.Y);
        }
    }
}
