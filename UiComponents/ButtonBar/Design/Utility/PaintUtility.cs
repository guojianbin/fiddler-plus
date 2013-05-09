﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Windows.Forms;
using ButtonBarsControl.Design.Enums;
using ButtonBarsControl.Design.Layout;

namespace ButtonBarsControl.Design.Utility
{
    internal static class PaintUtility
    {
        internal static string ThemeName = string.Empty;
        internal static string xpThemeName = string.Empty;

        internal static Color ChangeColor(Color startColor, Color endColor, int level, bool swapColors)
        {
            if (swapColors)
            {
                var color = startColor;
                startColor = endColor;
                endColor = color;
            }
            if (level == -1)
            {
                return endColor;
            }
            int r = startColor.R;
            int g = startColor.G;
            int b = startColor.B;
            if (r < endColor.R)
            {
                if ((r + level) < endColor.R)
                {
                    r += level;
                }
                else
                {
                    r = endColor.R;
                }
            }
            else if ((r - level) > endColor.R)
            {
                r -= level;
            }
            else
            {
                r = endColor.R;
            }
            if (r < endColor.G)
            {
                if ((g + level) < endColor.G)
                {
                    g += level;
                }
                else
                {
                    g = endColor.G;
                }
            }
            else if ((g - level) > endColor.G)
            {
                g -= level;
            }
            else
            {
                g = endColor.G;
            }
            if (b < endColor.B)
            {
                if ((b + level) < endColor.B)
                {
                    b += level;
                }
                else
                {
                    b = endColor.B;
                }
            }
            else if ((b - level) > endColor.B)
            {
                b -= level;
            }
            else
            {
                b = endColor.B;
            }
            return Color.FromArgb(0xff, r, g, b);
        }

        internal static void DrawBackground(Graphics g, RectangleF rect, Brush fillBrush, CornerShape bShape,
                                            int cornerRadius, Region excRegion)
        {
            if (excRegion != null)
            {
                g.ExcludeClip(excRegion);
            }
            var path = GetDrawingPath(rect, bShape, cornerRadius);
            g.FillPath(fillBrush, path);
            fillBrush.Dispose();
            path.Dispose();
        }

        internal static void DrawBorder(Graphics g, RectangleF rect, CornerShape bShape,
                                        ToolStripStatusLabelBorderSides bVisibility, DashStyle bLineStyle,
                                        int cornerRadius, Brush borderBrush, Region excRegion)
        {
            DrawBorder(g, rect, bShape, bVisibility, bLineStyle, cornerRadius, Color.Empty, borderBrush, excRegion);
        }

        internal static void DrawBorder(Graphics g, RectangleF rect, CornerShape bShape,
                                        ToolStripStatusLabelBorderSides bVisibility, DashStyle bLineStyle,
                                        int cornerRadius, Color borderColor, Brush borderBrush, Region excRegion)
        {
            if (bVisibility == ToolStripStatusLabelBorderSides.None)
            {
                return;
            }
            if (excRegion != null)
            {
                g.ExcludeClip(excRegion);
            }
            var pen = borderBrush != null ? new Pen(borderBrush, 1f) : new Pen(borderColor, 1f);
            var smoothingMode = g.SmoothingMode;
            pen.DashStyle = bLineStyle;
            var num = 2*cornerRadius;
            if (bVisibility == ToolStripStatusLabelBorderSides.All)
            {
                var path = GetDrawingPath(rect, bShape, cornerRadius);
                g.DrawPath(pen, path);
                path.Dispose();
                g.SmoothingMode = smoothingMode;
                pen.Dispose();
                return;
            }
            if ((bVisibility & ToolStripStatusLabelBorderSides.Left) > ToolStripStatusLabelBorderSides.None)
            {
                g.DrawLine(pen, rect.X, rect.Y + cornerRadius, rect.X, (rect.Y + rect.Height) - cornerRadius);
            }
            if ((bVisibility & ToolStripStatusLabelBorderSides.Top) > ToolStripStatusLabelBorderSides.None)
            {
                g.DrawLine(pen, rect.X + cornerRadius, rect.Y, (rect.X + rect.Width) - cornerRadius, rect.Y);
            }
            if ((bVisibility & ToolStripStatusLabelBorderSides.Right) > ToolStripStatusLabelBorderSides.None)
            {
                g.DrawLine(pen, rect.X + rect.Width, rect.Y + cornerRadius, rect.X + rect.Width,
                           (rect.Y + rect.Height) - cornerRadius);
            }
            if ((bVisibility & ToolStripStatusLabelBorderSides.Bottom) > ToolStripStatusLabelBorderSides.None)
            {
                g.DrawLine(pen, rect.X + cornerRadius, rect.Y + rect.Height, (rect.X + rect.Width) - cornerRadius,
                           rect.Y + rect.Height);
            }
            if (((bVisibility & ToolStripStatusLabelBorderSides.Left) > ToolStripStatusLabelBorderSides.None) ||
                ((bVisibility & ToolStripStatusLabelBorderSides.Top) > ToolStripStatusLabelBorderSides.None))
            {
                switch (bShape.TopLeft)
                {
                    case CornerType.Sliced:
                        g.DrawLine(pen, rect.X, rect.Y + cornerRadius, rect.X + cornerRadius, rect.Y);
                        break;

                    case CornerType.Square:
                        if (((bVisibility & ToolStripStatusLabelBorderSides.Left) <=
                             ToolStripStatusLabelBorderSides.None) ||
                            ((bVisibility & ToolStripStatusLabelBorderSides.Top) > ToolStripStatusLabelBorderSides.None))
                        {
                            if (((bVisibility & ToolStripStatusLabelBorderSides.Left) <=
                                 ToolStripStatusLabelBorderSides.None) &&
                                ((bVisibility & ToolStripStatusLabelBorderSides.Top) >
                                 ToolStripStatusLabelBorderSides.None))
                            {
                                g.DrawLine(pen, rect.X, rect.Y, rect.X + cornerRadius, rect.Y);
                            }
                            else
                            {
                                g.DrawLine(pen, rect.X, rect.Y + cornerRadius, rect.X, rect.Y);
                                g.DrawLine(pen, rect.X, rect.Y, rect.X + cornerRadius, rect.Y);
                            }
                        }
                        else
                        {
                            g.DrawLine(pen, rect.X, rect.Y + cornerRadius, rect.X, rect.Y);
                        }
                        break;
                }
                if (((bVisibility & ToolStripStatusLabelBorderSides.Top) > ToolStripStatusLabelBorderSides.None) ||
                    ((bVisibility & ToolStripStatusLabelBorderSides.Right) > ToolStripStatusLabelBorderSides.None))
                {
                    switch (bShape.TopRight)
                    {
                        case CornerType.Sliced:
                            g.DrawLine(pen, (rect.X + rect.Width) - cornerRadius, rect.Y, rect.X + rect.Width,
                                       rect.Y + cornerRadius);
                            break;
                        case CornerType.Square:
                            if (((bVisibility & ToolStripStatusLabelBorderSides.Right) >
                                 ToolStripStatusLabelBorderSides.None) ||
                                ((bVisibility & ToolStripStatusLabelBorderSides.Top) <=
                                 ToolStripStatusLabelBorderSides.None))
                            {
                                if (((bVisibility & ToolStripStatusLabelBorderSides.Right) >
                                     ToolStripStatusLabelBorderSides.None) &&
                                    ((bVisibility & ToolStripStatusLabelBorderSides.Top) <=
                                     ToolStripStatusLabelBorderSides.None))
                                {
                                    g.DrawLine(pen, rect.X + rect.Width, rect.Y, rect.X + rect.Width,
                                               rect.Y + cornerRadius);
                                }
                                else
                                {
                                    g.DrawLine(pen, (rect.X + rect.Width) - cornerRadius, rect.Y, rect.X + rect.Width,
                                               rect.Y);
                                    g.DrawLine(pen, rect.X + rect.Width, rect.Y, rect.X + rect.Width,
                                               rect.Y + cornerRadius);
                                }
                            }
                            else
                            {
                                g.DrawLine(pen, (rect.X + rect.Width) - cornerRadius, rect.Y, rect.X + rect.Width,
                                           rect.Y);
                            }
                            break;
                    }
                    if (((bVisibility & ToolStripStatusLabelBorderSides.Right) > ToolStripStatusLabelBorderSides.None) ||
                        ((bVisibility & ToolStripStatusLabelBorderSides.Bottom) > ToolStripStatusLabelBorderSides.None))
                    {
                        switch (bShape.BottomRight)
                        {
                            case CornerType.Sliced:
                                g.DrawLine(pen, rect.X + rect.Width, (rect.Y + rect.Height) - cornerRadius,
                                           (rect.X + rect.Width) - cornerRadius, rect.Y + rect.Height);
                                break;

                            case CornerType.Square:
                                if (((bVisibility & ToolStripStatusLabelBorderSides.Right) <=
                                     ToolStripStatusLabelBorderSides.None) ||
                                    ((bVisibility & ToolStripStatusLabelBorderSides.Bottom) >
                                     ToolStripStatusLabelBorderSides.None))
                                {
                                    if (((bVisibility & ToolStripStatusLabelBorderSides.Right) <=
                                         ToolStripStatusLabelBorderSides.None) &&
                                        ((bVisibility & ToolStripStatusLabelBorderSides.Bottom) >
                                         ToolStripStatusLabelBorderSides.None))
                                    {
                                        g.DrawLine(pen, (rect.X + rect.Width), (rect.Y + rect.Height),
                                                   (rect.X + rect.Width) - cornerRadius, (rect.Y + rect.Height));
                                    }
                                    else
                                    {
                                        g.DrawLine(pen, (rect.X + rect.Width), ((rect.Y + rect.Height) - cornerRadius),
                                                   rect.X + rect.Width, (rect.Y + rect.Height));
                                        g.DrawLine(pen, (rect.X + rect.Width), (rect.Y + rect.Height),
                                                   (rect.X + rect.Width) - cornerRadius, (rect.Y + rect.Height));
                                    }
                                }
                                else
                                {
                                    g.DrawLine(pen, rect.X + rect.Width, (rect.Y + rect.Height) - cornerRadius,
                                               rect.X + rect.Width, rect.Y + rect.Height);
                                }
                                break;
                        }
                        if (((bVisibility & ToolStripStatusLabelBorderSides.Bottom) >
                             ToolStripStatusLabelBorderSides.None) ||
                            ((bVisibility & ToolStripStatusLabelBorderSides.Left) > ToolStripStatusLabelBorderSides.None))
                        {
                            switch (bShape.BottomLeft)
                            {
                                case CornerType.Sliced:
                                    g.DrawLine(pen, rect.X + cornerRadius, rect.Y + rect.Height, rect.X,
                                               (rect.Y + rect.Height) - cornerRadius);
                                    g.SmoothingMode = smoothingMode;
                                    pen.Dispose();
                                    return;

                                case CornerType.Square:
                                    if (((bVisibility & ToolStripStatusLabelBorderSides.Left) >
                                         ToolStripStatusLabelBorderSides.None) ||
                                        ((bVisibility & ToolStripStatusLabelBorderSides.Bottom) <=
                                         ToolStripStatusLabelBorderSides.None))
                                    {
                                        if (((bVisibility & ToolStripStatusLabelBorderSides.Left) >
                                             ToolStripStatusLabelBorderSides.None) &&
                                            ((bVisibility & ToolStripStatusLabelBorderSides.Bottom) <=
                                             ToolStripStatusLabelBorderSides.None))
                                        {
                                            g.DrawLine(pen, rect.X, rect.Y + rect.Height, rect.X,
                                                       (rect.Y + rect.Height) - cornerRadius);
                                        }
                                        else
                                        {
                                            g.DrawLine(pen, rect.X + cornerRadius, rect.Y + rect.Height, rect.X,
                                                       rect.Y + rect.Height);
                                            g.DrawLine(pen, rect.X, rect.Y + rect.Height, rect.X,
                                                       (rect.Y + rect.Height) - cornerRadius);
                                        }
                                    }
                                    else
                                    {
                                        g.DrawLine(pen, rect.X + cornerRadius, rect.Y + rect.Height, rect.X,
                                                   rect.Y + rect.Height);
                                    }
                                    g.SmoothingMode = smoothingMode;
                                    pen.Dispose();
                                    return;
                            }
                            g.DrawArc(pen, rect.X, (rect.Y + rect.Height) - num, num, num, 90f, 90f);
                        }
                        g.DrawArc(pen, (rect.X + rect.Width) - num, (rect.Y + rect.Height) - num, num, num, 0f, 90f);
                    }
                    g.DrawArc(pen, (rect.X + rect.Width) - num, rect.Y, num, num, 270f, 90f);
                }
                g.DrawArc(pen, rect.X, rect.Y, num, num, 180f, 90f);
            }
        }

        internal static void DrawImage(Graphics g, Rectangle rect, Image img, int alpha)
        {
            if (img != null)
            {
                if (alpha == 0xff)
                {
                    g.DrawImage(img, rect);
                }
                else
                {
                    var imageAttr = new ImageAttributes();
                    imageAttr.SetColorMatrix(MakeTransparentImage(alpha));
                    g.DrawImage(img, rect, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imageAttr);
                    imageAttr.Dispose();
                }
            }
        }

        internal static GraphicsPath GetDrawingPath(RectangleF rect, CornerShape bShape, int cornerRadius)
        {
            var path = new GraphicsPath();
            int num = 2*cornerRadius;
            if (bShape.TopLeft == CornerType.Square)
            {
                if (bShape.TopRight == CornerType.Square)
                {
                    path.AddLine(rect.X, rect.Y, rect.X + rect.Width, rect.Y);
                }
                else
                {
                    path.AddLine(rect.X, rect.Y, (rect.X + rect.Width) - cornerRadius, rect.Y);
                    if (bShape.TopRight == CornerType.Round && cornerRadius > 0)
                    {
                        path.AddArc((rect.X + rect.Width) - num, rect.Y, num, num, 270f, 90f);
                    }
                    else
                    {
                        path.AddLine((rect.X + rect.Width) - cornerRadius, rect.Y, rect.X + rect.Width,
                                     rect.Y + cornerRadius);
                    }
                }
            }
            else if (bShape.TopRight == CornerType.Square)
            {
                path.AddLine(rect.X + cornerRadius, rect.Y, rect.X + rect.Width, rect.Y);
            }
            else
            {
                path.AddLine(rect.X + cornerRadius, rect.Y, (rect.X + rect.Width) - cornerRadius, rect.Y);
                if (bShape.TopRight == CornerType.Round && cornerRadius > 0)
                {
                    path.AddArc((rect.X + rect.Width) - num, rect.Y, num, num, 270f, 90f);
                }
                else
                {
                    path.AddLine((rect.X + rect.Width) - cornerRadius, rect.Y, rect.X + rect.Width,
                                 rect.Y + cornerRadius);
                }
            }
            if (bShape.TopRight == CornerType.Square)
            {
                if (bShape.BottomRight == CornerType.Square)
                {
                    path.AddLine(rect.X + rect.Width, rect.Y, rect.X + rect.Width, rect.Y + rect.Height);
                }
                else
                {
                    path.AddLine(rect.X + rect.Width, rect.Y, rect.X + rect.Width, (rect.Y + rect.Height) - cornerRadius);
                    if (bShape.BottomRight == CornerType.Round && cornerRadius > 0)
                    {
                        path.AddArc((rect.X + rect.Width) - num, (rect.Y + rect.Height) - num, num, num, 0f, 90f);
                    }
                    else
                    {
                        path.AddLine(rect.X + rect.Width, (rect.Y + rect.Height) - cornerRadius,
                                     (rect.X + rect.Width) - cornerRadius, rect.Y + rect.Height);
                    }
                }
            }
            else if (bShape.BottomRight == CornerType.Square)
            {
                path.AddLine(rect.X + rect.Width, rect.Y + cornerRadius, (rect.X + rect.Width), (rect.Y + rect.Height));
            }
            else
            {
                path.AddLine(rect.X + rect.Width, rect.Y + cornerRadius, (rect.X + rect.Width),
                             ((rect.Y + rect.Height) - cornerRadius));
                if (bShape.BottomRight == CornerType.Round && cornerRadius > 0)
                {
                    path.AddArc((rect.X + rect.Width) - num, (rect.Y + rect.Height) - num, num, num, 0f, 90f);
                }
                else
                {
                    path.AddLine(rect.X + rect.Width, (rect.Y + rect.Height) - cornerRadius,
                                 (rect.X + rect.Width) - cornerRadius, rect.Y + rect.Height);
                }
            }
            if (bShape.BottomRight == CornerType.Square)
            {
                if (bShape.BottomLeft == CornerType.Square)
                {
                    path.AddLine(rect.X + rect.Width, rect.Y + rect.Height, rect.X, rect.Y + rect.Height);
                }
                else
                {
                    path.AddLine(rect.X + rect.Width, rect.Y + rect.Height, rect.X + cornerRadius, rect.Y + rect.Height);
                    if (bShape.BottomLeft == CornerType.Round && cornerRadius > 0)
                    {
                        path.AddArc(rect.X, (rect.Y + rect.Height) - num, num, num, 90f, 90f);
                    }
                    else
                    {
                        path.AddLine(rect.X + cornerRadius, rect.Y + rect.Height, rect.X,
                                     (rect.Y + rect.Height) - cornerRadius);
                    }
                }
            }
            else if (bShape.BottomLeft == CornerType.Square)
            {
                path.AddLine((rect.X + rect.Width) - cornerRadius, rect.Y + rect.Height, rect.X, rect.Y + rect.Height);
            }
            else
            {
                path.AddLine((rect.X + rect.Width) - cornerRadius, rect.Y + rect.Height, rect.X + cornerRadius,
                             rect.Y + rect.Height);
                if (bShape.BottomLeft == CornerType.Round && cornerRadius > 0)
                {
                    path.AddArc(rect.X, (rect.Y + rect.Height) - num, num, num, 90f, 90f);
                }
                else
                {
                    path.AddLine(rect.X + cornerRadius, rect.Y + rect.Height, rect.X,
                                 (rect.Y + rect.Height) - cornerRadius);
                }
            }
            if (bShape.BottomLeft == CornerType.Square)
            {
                if (bShape.TopLeft == CornerType.Square)
                {
                    path.AddLine(rect.X, rect.Y + rect.Height, rect.X, rect.Y);
                    return path;
                }
                path.AddLine(rect.X, rect.Y + rect.Height, rect.X, rect.Y + cornerRadius);
                if (bShape.TopLeft == CornerType.Round && cornerRadius > 0)
                {
                    path.AddArc(rect.X, rect.Y, num, num, 180f, 90f);
                    return path;
                }
                path.AddLine(rect.X, rect.Y + cornerRadius, rect.X + cornerRadius, rect.Y);
                return path;
            }
            if (bShape.TopLeft == CornerType.Square)
            {
                path.AddLine(rect.X, (rect.Y + rect.Height) - cornerRadius, rect.X, rect.Y);
                return path;
            }
            path.AddLine(rect.X, (rect.Y + rect.Height) - cornerRadius, rect.X, rect.Y + cornerRadius);
            if (bShape.TopLeft == CornerType.Round && cornerRadius > 0)
            {
                path.AddArc(rect.X, rect.Y, num, num, 180f, 90f);
                return path;
            }
            path.AddLine(rect.X, rect.Y + cornerRadius, rect.X + cornerRadius, rect.Y);
            return path;
        }

        internal static ColorMatrix MakeTransparentImage(int alpha)
        {
            var matrix = new ColorMatrix
                             {
                                 Matrix00 = 1f,
                                 Matrix11 = 1f,
                                 Matrix22 = 1f,
                                 Matrix33 = alpha/255f,
                                 Matrix44 = 1f
                             };
            return matrix;
        }

        internal static RectangleF PaintGradientRectangle(Graphics g, RectangleF rect, ColorPair pair)
        {
            var brush1 = new LinearGradientBrush(rect, pair.BackColor1, pair.BackColor2, pair.Gradient);
            g.FillRectangle(brush1, rect);
            return rect;
        }

        internal static RectangleF PaintGradientRectangle(Graphics g, int aPointX, int aPointY, int aWidth, int aHeight,
                                                          ColorPair pair)
        {
            var ef2 = XRectangleF(aPointX, aPointY, aWidth, aHeight);
            var brush1 = new LinearGradientBrush(ef2, pair.BackColor1, pair.BackColor2, pair.Gradient);
            g.FillRectangle(brush1, ef2);
            return ef2;
        }

        internal static void PaintBorder(Graphics g, Rectangle rect, Color aColor)
        {
            var pen1 = new Pen(new SolidBrush(aColor), 1f);
            g.DrawRectangle(pen1, XRectangle(rect.X, rect.Y, rect.Width - 1, rect.Height - 1));
        }

        internal static void PaintBorder(Graphics g, RectangleF rect, Color aColor)
        {
            var pen1 = new Pen(new SolidBrush(aColor), 1f);
            g.DrawRectangle(pen1,
                            XRectangle((int) rect.X, (int) rect.Y, (int) (rect.Width - 1), (int) (rect.Height - 1)));
        }

        internal static Rectangle XRectangle(int x, int y, int width, int height)
        {
            if (width < 1)
            {
                width = 1;
            }
            if (height < 1)
            {
                height = 1;
            }
            return new Rectangle(x, y, width, height);
        }

        internal static RectangleF XRectangleF(float x, float y, float width, float height)
        {
            if (width < 1f)
            {
                width = 1f;
            }
            if (height < 1f)
            {
                height = 1f;
            }
            return new RectangleF(x, y, width, height);
        }

        internal static void DrawString(Graphics graphics, Rectangle rectangle, string text, AppearenceText app,
                                        bool useMnemonic, Color textColor)
        {
            if (rectangle.IsEmpty)
                return;
            var format = new StringFormat();
            format.Trimming = app.Trimming;
            format.Alignment = app.Alignment;
            format.LineAlignment = app.LineAlignment;
            if (useMnemonic)
            {
                format.HotkeyPrefix = HotkeyPrefix.Show;
            }
            rectangle.X += (int) app.Xshift;
            rectangle.Y += (int) app.Yshift;
            graphics.DrawString(text, app.Font, new SolidBrush(ChangeColor(textColor, Color.White, 100, false)),
                                rectangle, format);
            rectangle.X -= (int) app.Xshift;
            rectangle.Y -= (int) app.Yshift;
            graphics.DrawString(text, app.Font, new SolidBrush(textColor), rectangle, format);
        }

        internal static Color GetColor(string c)
        {
            if (c.IndexOf(',') > 0)
            {
                var parts = c.Split(',');
                return Color.FromArgb(Convert.ToInt32(parts[0].Trim()), Convert.ToInt32(parts[1].Trim()),
                                      Convert.ToInt32(parts[2].Trim()),
                                      Convert.ToInt32(parts[3].Trim()));
            }
            if (string.IsNullOrEmpty(c))
                return Color.Empty;
            return Color.FromName(c);
        }

        internal static string GetString(Color c)
        {
            if (c.IsNamedColor || c.IsKnownColor || c.IsSystemColor)
                return c.Name;
            if (c.IsEmpty)
                return string.Empty;
            return c.A + ", " + c.R + ", " + c.G + ", " + c.B;
        }
    }
}