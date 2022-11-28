using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace MSA.FW.Utilities.Image
{
    public class ImageHelper
    {
        public static void ResizeImage(string imageFilePath, int width, int height,
           double mBytes)
        {
            if (width > 0 || height > 0)
            {
                ResizeImage(imageFilePath, width, height);
            }

            if (mBytes > 0)
            {
                DownSizeImage(imageFilePath, mBytes);
            }
        }
        public static void ResizeImage(string imageFilePath, int width, int height)
        {
            Bitmap open = new Bitmap(imageFilePath);

            try
            {
                if (width <= 0)
                {
                    width = open.Width;
                }
                if (height <= 0)
                {
                    height = open.Height;
                }

                if (HasTransparency(open))
                {
                    open.Dispose();
                }
                else if (width < open.Width || height < open.Height)
                {
                    if (width > 0 && width < open.Width && height > 0 && height < open.Height)
                    {
                        if (width / open.Width < height / open.Height)
                        {
                            height = (int)Math.Round((double)open.Height * width / open.Width);
                        }
                        else if (width / open.Width > height / open.Height)
                        {
                            width = (int)Math.Round((double)open.Width * height / open.Height);
                        }
                    }
                    else if (width > 0 && width < open.Width)
                    {
                        height = (int)Math.Round((double)open.Height * width / open.Width);
                        if (height > open.Height)
                        {
                            height = open.Height;
                            width = (int)Math.Round((double)open.Width * height / open.Height);
                        }
                    }
                    else if (height > 0 && height < open.Height)
                    {
                        width = (int)Math.Round((double)open.Width * height / open.Height);
                        if (width > open.Width)
                        {
                            width = open.Width;
                            height = (int)Math.Round((double)open.Height * width / open.Width);
                        }
                    }

                    open.Dispose();

                    using (var result = new Bitmap(width, height))
                    {
                        using (var input = new Bitmap(imageFilePath))
                        {
                            using (Graphics g = Graphics.FromImage(result))
                            {
                                g.DrawImage(input, 0, 0, width, height);
                                g.Dispose();
                            }
                            input.Dispose();
                        }

                        var ici = ImageCodecInfo.GetImageEncoders().FirstOrDefault();
                        var eps = new EncoderParameters(1);
                        eps.Param[0] = new EncoderParameter(Encoder.Quality, 100L);
                        result.Save(imageFilePath, ici, eps);
                        result.Dispose();
                    }
                }
                else
                {
                    open.Dispose();
                }
            }
            catch (Exception ex)
            {
                open.Dispose();
                throw ex;
            }
        }

        public static void ResizeImageWidth(string imageFilePath, int widthSize)
        {
            Bitmap open = new Bitmap(imageFilePath);
            int width = open.Width;
            if (width > open.Height)
            {
                width = open.Height;
            }

            if (HasTransparency(open))
            {
                open.Dispose();
            }
            else if (widthSize < width)
            {
                float rate = (float)widthSize / width;
                int _width = (int)(open.Width * rate);
                int _height = (int)(open.Height * rate);
                open.Dispose();

                using (var result = new Bitmap(_width, _height))
                {
                    using (var input = new Bitmap(imageFilePath))
                    {
                        using (Graphics g = Graphics.FromImage(result))
                        {
                            g.DrawImage(input, 0, 0, _width, _height);
                        }
                    }

                    var ici = ImageCodecInfo.GetImageEncoders().FirstOrDefault();
                    var eps = new EncoderParameters(1);
                    eps.Param[0] = new EncoderParameter(Encoder.Quality, 100L);
                    result.Save(imageFilePath, ici, eps);
                    result.Dispose();
                }
            }
            open.Dispose();
        }

        public static void DownSizeImage(string imageFilePath, double mByte)
        {
            Bitmap open = new Bitmap(imageFilePath);

            try
            {
                open = new Bitmap(imageFilePath);
                if (HasTransparency(open))
                {
                    open.Dispose();
                }
                else
                {
                    mByte = mByte * 1024 * 1024;
                    int currentSize = open.Width * open.Height * 4;
                    if (currentSize > mByte)
                    {
                        double rate = (double)currentSize / mByte;
                        int width = (int)(open.Width / rate);
                        int height = (int)(open.Height / rate);
                        open.Dispose();

                        using (var result = new Bitmap(width, height))
                        {
                            using (var input = new Bitmap(imageFilePath))
                            {
                                using (Graphics g = Graphics.FromImage(result))
                                {
                                    g.DrawImage(input, 0, 0, width, height);
                                    g.Dispose();
                                }
                                input.Dispose();
                            }

                            var ici = ImageCodecInfo.GetImageEncoders().FirstOrDefault();
                            var eps = new EncoderParameters(1);
                            eps.Param[0] = new EncoderParameter(Encoder.Quality, 100L);
                            result.Save(imageFilePath, ici, eps);
                            result.Dispose();
                        }
                    }
                    else
                    {
                        open.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                open.Dispose();
                throw ex;
            }
        }

        public static bool HasTransparency(Bitmap image)
        {
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    var pixel = image.GetPixel(i, j);
                    if (pixel.A != 255)
                        return true;
                }
            }

            return false;
        }
    }
}
