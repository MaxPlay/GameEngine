using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameEngine.Core
{
#if DEBUG

    /// <summary>
    /// Class used to draw Gizmos.
    /// </summary>
    /// <remarks>
    /// Can only be used in Debug-Mode.
    /// </remarks>
    public static class Gizmo
    {
        private static Texture2D Pixel;

        static Gizmo()
        {
            Pixel = new Texture2D(Bootstrap.graphics.GraphicsDevice, 1, 1);
            Pixel.SetData<Color>(new Color[] { Color.White });
        }

        /// <summary>
        /// Draws a line using worldcoordinates.
        /// </summary>
        /// <param name="start">Startpoint of the line.</param>
        /// <param name="end">Endpoint of the line.</param>
        public static void DrawLine(Vector2 start, Vector2 end)
        {
            DrawLine(start, end, Color.White);
        }

        /// <summary>
        /// Draws a line using worldcoordinates.
        /// </summary>
        /// <param name="start">Startpoint of the line.</param>
        /// <param name="end">Endpoint of the line.</param>
        /// <param name="color">Color of the line.</param>
        public static void DrawLine(Vector2 start, Vector2 end, Color color)
        {
            Vector2 edge = end - start;
            // calculate angle to rotate line
            float angle = (float)Math.Atan2(edge.Y, edge.X);

            Bootstrap.spriteBatch.Begin();
            Bootstrap.spriteBatch.Draw(Pixel,
                new Rectangle(// rectangle defines shape of line and position of start of line
                    (int)start.X,
                    (int)start.Y,
                    (int)edge.Length(), //sb will strech the texture to fill this rectangle
                    1), //width of line, change this to make thicker line
                null,
                color, //color of line
                angle,     //angle of line (calulated above)
                new Vector2(0, 0), // point in line about which to rotate
                SpriteEffects.None,
                0);
            Bootstrap.spriteBatch.End();
        }

        /// <summary>
        /// Draws a ray using worldcoordinates.
        /// </summary>
        /// <param name="ray">The ray that needs to be drawn.</param>
        public static void DrawRay(Ray2D ray)
        {
            DrawLine(ray.Position, ray.Position + ray.Direction, Color.White);
        }

        /// <summary>
        /// Draws a ray using worldcoordinates.
        /// </summary>
        /// <param name="ray">The ray that needs to be drawn.</param>
        /// <param name="color">Color of the ray.</param>
        public static void DrawRay(Ray2D ray, Color color)
        {
            DrawLine(ray.Position, ray.Position + ray.Direction, color);
        }

        /// <summary>
        /// Draws a box using worldcoordinates.
        /// </summary>
        /// <param name="rect">Rectangle representing the box.</param>
        /// <param name="color">The color of the box.</param>
        public static void DrawBox(Rectangle rect, Color color)
        {
            DrawBox(rect, false, color);
        }

        /// <summary>
        /// Draws a box using worldcoordinates.
        /// </summary>
        /// <param name="rect">Rectangle representing the box.</param>
        /// <param name="filled">Determines wether the rectangle should be filled or lines only.</param>
        public static void DrawBox(Rectangle rect, bool filled)
        {
            DrawBox(rect, filled, Color.White);
        }

        /// <summary>
        /// Draws a box using worldcoordinates.
        /// </summary>
        /// <param name="rect">Rectangle representing the box.</param>
        public static void DrawBox(Rectangle rect)
        {
            DrawBox(rect, false, Color.White);
        }

        /// <summary>
        /// Draws a box using worldcoordinates.
        /// </summary>
        /// <param name="rect">Rectangle representing the box.</param>
        /// <param name="filled">Determines wether the rectangle should be filled or lines only.</param>
        /// <param name="color">The color of the box.</param>
        public static void DrawBox(Rectangle rect, bool filled, Color color)
        {
            if (filled)
            {
                Bootstrap.spriteBatch.Begin();
                Bootstrap.spriteBatch.Draw(Pixel, rect, color);
                Bootstrap.spriteBatch.End();
            }
            else
            {
                Bootstrap.spriteBatch.Begin();
                DrawLine(new Vector2(rect.Left, rect.Top), new Vector2(rect.Right, rect.Top), color);
                DrawLine(new Vector2(rect.Left, rect.Bottom), new Vector2(rect.Right, rect.Bottom), color);
                DrawLine(new Vector2(rect.Left, rect.Top), new Vector2(rect.Left, rect.Bottom), color);
                DrawLine(new Vector2(rect.Right, rect.Top), new Vector2(rect.Right, rect.Bottom), color);
                Bootstrap.spriteBatch.End();
            }
        }

        /// <summary>
        /// Draws a box using worldcoordinates.
        /// </summary>
        /// <param name="x">The upper left x-coordinate of the box.</param>
        /// <param name="y">The upper left y-coordinate of the box.</param>
        /// <param name="width">The width of the box.</param>
        /// <param name="height">The height of the box.</param>
        public static void DrawBox(int x, int y, int width, int height)
        {
            DrawBox(new Rectangle(x, y, width, height), false, Color.White);
        }

        /// <summary>
        /// Draws a box using worldcoordinates.
        /// </summary>
        /// <param name="x">The upper left x-coordinate of the box.</param>
        /// <param name="y">The upper left y-coordinate of the box.</param>
        /// <param name="width">The width of the box.</param>
        /// <param name="height">The height of the box.</param>
        /// <param name="filled">Determines wether the rectangle should be filled or lines only.</param>
        public static void DrawBox(int x, int y, int width, int height, bool filled)
        {
            DrawBox(new Rectangle(x, y, width, height), filled, Color.White);
        }

        /// <summary>
        /// Draws a box using worldcoordinates.
        /// </summary>
        /// <param name="x">The upper left x-coordinate of the box.</param>
        /// <param name="y">The upper left y-coordinate of the box.</param>
        /// <param name="width">The width of the box.</param>
        /// <param name="height">The height of the box.</param>
        /// <param name="filled">Determines wether the box should be filled or lines only.</param>
        /// <param name="color">The color of the box.</param>
        public static void DrawBox(int x, int y, int width, int height, bool filled, Color color)
        {
            DrawBox(new Rectangle(x, y, width, height), filled, color);
        }

        /// <summary>
        /// Draws a box using worldcoordinates.
        /// </summary>
        /// <param name="x">The upper left x-coordinate of the box.</param>
        /// <param name="y">The upper left y-coordinate of the box.</param>
        /// <param name="width">The width of the box.</param>
        /// <param name="height">The height of the box.</param>
        /// <param name="color">The color of the box.</param>
        public static void DrawBox(int x, int y, int width, int height, Color color)
        {
            DrawBox(new Rectangle(x, y, width, height), false, color);
        }

        /// <summary>
        /// Draws a circle using worldcoordinates.
        /// </summary>
        /// <param name="center">Location of the center.</param>
        /// <param name="radius">Radius of the circle.</param>
        public static void DrawCircle(Vector2 center, int radius)
        {
            Bootstrap.spriteBatch.Begin();

            // Work out the minimum step necessary using trigonometry + sine approximation.
            double angleStep = 1f / radius;

            for (double angle = 0; angle < Math.PI * 2; angle += angleStep)
            {
                // Use the parametric definition of a circle: http://en.wikipedia.org/wiki/Circle#Cartesian_coordinates
                int x = (int)Math.Round(radius * Math.Cos(angle));
                int y = (int)Math.Round(radius * Math.Sin(angle));

                Bootstrap.spriteBatch.Draw(Pixel, new Vector2(x, y) + center, Color.White);
            }

            Bootstrap.spriteBatch.End();
        }

        /// <summary>
        /// Draws a circle using worldcoordinates.
        /// </summary>
        /// <param name="x">The x-coordinate of the center.</param>
        /// <param name="y">The y-coordinate of the center.</param>
        /// <param name="radius">Radius of the circle.</param>
        public static void DrawCircle(int x, int y, int radius)
        {
            Bootstrap.spriteBatch.Begin();

            // Work out the minimum step necessary using trigonometry + sine approximation.
            double angleStep = 1f / radius;

            for (double angle = 0; angle < Math.PI * 2; angle += angleStep)
            {
                // Use the parametric definition of a circle: http://en.wikipedia.org/wiki/Circle#Cartesian_coordinates
                int _x = (int)Math.Round(radius * Math.Cos(angle));
                int _y = (int)Math.Round(radius * Math.Sin(angle));

                Bootstrap.spriteBatch.Draw(Pixel, new Vector2(x + _x, y + _y), Color.White);
            }

            Bootstrap.spriteBatch.End();
        }

        /// <summary>
        /// Draws a circle using worldcoordinates.
        /// </summary>
        /// <param name="x">The x-coordinate of the center.</param>
        /// <param name="y">The y-coordinate of the center.</param>
        /// <param name="radius">Radius of the circle.</param>
        /// <param name="filled">Determines wether the circle should be filled or lines only.</param>
        public static void DrawCircle(int x, int y, int radius, bool filled)
        {
            if (!filled)
            {
                DrawCircle(new Vector2(x, y), radius);
                return;
            }

            Bootstrap.spriteBatch.Begin();

            double angleStep = 1f / radius;

            for (double angle = 0; angle < Math.PI; angle += angleStep)
            {
                // Use the parametric definition of a circle: http://en.wikipedia.org/wiki/Circle#Cartesian_coordinates
                int _x = (int)Math.Round(radius * Math.Cos(angle));
                int _y = (int)Math.Round(radius * Math.Sin(angle));

                Bootstrap.spriteBatch.Draw(Pixel, new Rectangle(x + _x, y - _y, 1, _y * 2), Color.White);
            }

            Bootstrap.spriteBatch.End();
        }

        /// <summary>
        /// Draws a circle using worldcoordinates.
        /// </summary>
        /// <param name="center">Location of the center.</param>
        /// <param name="radius">Radius of the circle.</param>
        /// <param name="filled">Determines wether the circle should be filled or lines only.</param>
        public static void DrawCircle(Vector2 center, int radius, bool filled)
        {
            if (!filled)
            {
                DrawCircle(center, radius);
                return;
            }

            Bootstrap.spriteBatch.Begin();

            double angleStep = 1f / radius;

            for (double angle = 0; angle < Math.PI; angle += angleStep)
            {
                // Use the parametric definition of a circle: http://en.wikipedia.org/wiki/Circle#Cartesian_coordinates
                int x = (int)Math.Round(radius * Math.Cos(angle));
                int y = (int)Math.Round(radius * Math.Sin(angle));

                Bootstrap.spriteBatch.Draw(Pixel, new Rectangle((int)center.X + x, (int)center.Y - y, 1, y * 2), Color.White);
            }

            Bootstrap.spriteBatch.End();
        }

        /// <summary>
        /// Draws a circle using worldcoordinates.
        /// </summary>
        /// <param name="center">Location of the center.</param>
        /// <param name="radius">Radius of the circle.</param>
        public static void DrawCircle(Vector2 center, int radius, Color color)
        {
            Bootstrap.spriteBatch.Begin();

            // Work out the minimum step necessary using trigonometry + sine approximation.
            double angleStep = 1f / radius;

            for (double angle = 0; angle < Math.PI * 2; angle += angleStep)
            {
                // Use the parametric definition of a circle: http://en.wikipedia.org/wiki/Circle#Cartesian_coordinates
                int x = (int)Math.Round(radius * Math.Cos(angle));
                int y = (int)Math.Round(radius * Math.Sin(angle));

                Bootstrap.spriteBatch.Draw(Pixel, new Vector2(x, y) + center, color);
            }

            Bootstrap.spriteBatch.End();
        }

        /// <summary>
        /// Draws a circle using worldcoordinates.
        /// </summary>
        /// <param name="x">The x-coordinate of the center.</param>
        /// <param name="y">The y-coordinate of the center.</param>
        /// <param name="radius">Radius of the circle.</param>
        public static void DrawCircle(int x, int y, int radius, Color color)
        {
            Bootstrap.spriteBatch.Begin();

            // Work out the minimum step necessary using trigonometry + sine approximation.
            double angleStep = 1f / radius;

            for (double angle = 0; angle < Math.PI * 2; angle += angleStep)
            {
                // Use the parametric definition of a circle: http://en.wikipedia.org/wiki/Circle#Cartesian_coordinates
                int _x = (int)Math.Round(radius * Math.Cos(angle));
                int _y = (int)Math.Round(radius * Math.Sin(angle));

                Bootstrap.spriteBatch.Draw(Pixel, new Vector2(x + _x, y + _y), color);
            }

            Bootstrap.spriteBatch.End();
        }

        /// <summary>
        /// Draws a circle using worldcoordinates.
        /// </summary>
        /// <param name="x">The x-coordinate of the center.</param>
        /// <param name="y">The y-coordinate of the center.</param>
        /// <param name="radius">Radius of the circle.</param>
        /// <param name="filled">Determines wether the circle should be filled or lines only.</param>
        public static void DrawCircle(int x, int y, int radius, bool filled, Color color)
        {
            if (!filled)
            {
                DrawCircle(new Vector2(x, y), radius);
                return;
            }

            Bootstrap.spriteBatch.Begin();

            double angleStep = 1f / radius;

            for (double angle = 0; angle < Math.PI; angle += angleStep)
            {
                // Use the parametric definition of a circle: http://en.wikipedia.org/wiki/Circle#Cartesian_coordinates
                int _x = (int)Math.Round(radius * Math.Cos(angle));
                int _y = (int)Math.Round(radius * Math.Sin(angle));

                Bootstrap.spriteBatch.Draw(Pixel, new Rectangle(x + _x, y - _y, 1, _y * 2), color);
            }

            Bootstrap.spriteBatch.End();
        }

        /// <summary>
        /// Draws a circle using worldcoordinates.
        /// </summary>
        /// <param name="center">Location of the center.</param>
        /// <param name="radius">Radius of the circle.</param>
        /// <param name="filled">Determines wether the circle should be filled or lines only.</param>
        public static void DrawCircle(Vector2 center, int radius, bool filled, Color color)
        {
            if (!filled)
            {
                DrawCircle(center, radius);
                return;
            }

            Bootstrap.spriteBatch.Begin();

            double angleStep = 1f / radius;

            for (double angle = 0; angle < Math.PI; angle += angleStep)
            {
                // Use the parametric definition of a circle: http://en.wikipedia.org/wiki/Circle#Cartesian_coordinates
                int x = (int)Math.Round(radius * Math.Cos(angle));
                int y = (int)Math.Round(radius * Math.Sin(angle));

                Bootstrap.spriteBatch.Draw(Pixel, new Rectangle((int)center.X + x, (int)center.Y - y, 1, y * 2), color);
            }

            Bootstrap.spriteBatch.End();
        }

        /// <summary>
        /// Draws a Coordinate-Cross at the given position.
        /// </summary>
        /// <param name="location">The center of the Coordinate-Cross.</param>
        public static void DrawCoords(Vector2 location)
        {
            DrawCoords(location.X, location.Y);
        }

        /// <summary>
        /// Draws a Coordinate-Cross at the given position.
        /// </summary>
        /// <param name="x">The x-Coordinate of the Coordinate-Cross</param>
        /// <param name="y">The y-Coordinate of the Coordinate-Cross</param>
        public static void DrawCoords(float x, float y)
        {
            Bootstrap.spriteBatch.Begin();
            DrawLine(new Vector2(x, y - 10), new Vector2(x, y + 10), new Color(0, 255, 0));
            DrawLine(new Vector2(x - 10, y), new Vector2(x + 10, y), new Color(255, 0, 0));
            Bootstrap.spriteBatch.End();
        }
    }

#endif
}