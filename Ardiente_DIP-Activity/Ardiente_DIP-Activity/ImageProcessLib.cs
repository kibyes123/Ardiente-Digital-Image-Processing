/* 
KIBYES IMAGE PROCESSING METHODS REPOSITORY
Copyright (C) 2022 Project - All Rights Reserved

PAUL JUSTINE ARDIENTE
justineardiente@gmail.com
*/

using System;
using System.Drawing;

namespace Ardiente_DIP_Activity
{
        public class ImageProcessLib
        {
            /// <summary>
            /// Lets you copy the loaded image, and pastes it on the processed image variable
            /// </summary>
            /// <param name="loaded"> Original Image </param>
            /// <param name="processed"> Processed Image </param>
            public static void CopyImage(ref Bitmap loaded, ref Bitmap processed)
            {
                processed = new Bitmap(loaded.Width, loaded.Height);

                for (int i = 0; i < loaded.Width; i++)
                {
                    for (int j = 0; j < loaded.Height; j++)
                    {
                        Color pixel = loaded.GetPixel(i, j);

                        // Set pixel from the pixel we got in pixel variable
                        processed.SetPixel(i, j, pixel);
                    }
                }
            }

            /// <summary>
            /// Lets you copy the loaded image, makes it greyscale, and pastes it on the processed image variable
            /// </summary>
            /// <param name="loaded"> Original Image </param>
            /// <param name="processed"> Processed Image</param>
            public static void Greyscale(ref Bitmap loaded, ref Bitmap processed)
            {
                processed = new Bitmap(loaded.Width, loaded.Height);

                for (int i = 0; i < loaded.Width; i++)
                {
                    for (int j = 0; j < loaded.Height; j++)
                    {
                        Color pixel = loaded.GetPixel(i, j);

                        // Calculate formula for greyscale
                        int grey = (pixel.R + pixel.G + pixel.B) / 3;

                        // Set pixel to the calculated one
                        Color greyscale = Color.FromArgb(grey, grey, grey);
                        processed.SetPixel(i, j, greyscale);
                    }
                }
            }

            /// <summary>
            /// Lets you copy the loaded image, makes it a color inversion, and pastes it on the processed image variable
            /// </summary>
            /// <param name="loaded"> Original Image </param>
            /// <param name="processed"> Processed Image </param>
            public static void Inversion(ref Bitmap loaded, ref Bitmap processed)
            {
                processed = new Bitmap(loaded.Width, loaded.Height);

                for (int i = 0; i < loaded.Width; i++)
                {
                    for (int j = 0; j < loaded.Height; j++)
                    {
                        Color pixel = loaded.GetPixel(i, j);

                        // Calculate formula for inversion
                        int iR = 255 - pixel.R;
                        int iG = 255 - pixel.G;
                        int iB = 255 - pixel.B;

                        // Set pixel to the calculated one
                        Color inversion = Color.FromArgb(iR, iG, iB);
                        processed.SetPixel(i, j, inversion);
                    }
                }
            }

            /// <summary>
            /// Lets you copy the loaded image, makes it sepia, and pastes it on the processed image variable
            /// </summary>
            /// <param name="loaded"> Original Image </param>
            /// <param name="processed"> Processed Image </param>
            public static void Sepia(ref Bitmap loaded, ref Bitmap processed)
            {
                processed = new Bitmap(loaded.Width, loaded.Height);

                for (int i = 0; i < loaded.Width; i++)
                {
                    for (int j = 0; j < loaded.Height; j++)
                    {
                        Color pixel = loaded.GetPixel(i, j);

                        // Calculate formula for sepia
                        int sR = (int)((0.393 * pixel.R) + (0.769 * pixel.G) + (0.189 * pixel.B));
                        int sG = (int)((0.349 * pixel.R) + (0.686 * pixel.G) + (0.168 * pixel.B));
                        int sB = (int)((0.272 * pixel.R) + (0.534 * pixel.G) + (0.131 * pixel.B));

                        // Check conditions
                        if (sR > 255)
                            sR = 255;
                        if (sG > 255)
                            sG = 255;
                        if (sB > 255)
                            sB = 255;

                        // Set pixel to the calculated one
                        Color sepia = Color.FromArgb(sR, sG, sB);
                        processed.SetPixel(i, j, sepia);
                    }
                }
            }

            /// <summary>
            /// Lets you make a histogram for a specific image (original image)
            /// </summary>
            /// <param name="original"> Original Image </param>
            /// <param name="processed"> Histogram Image</param>
            public static void Histogram(ref Bitmap original, ref Bitmap processed)
            {
                Color temp, gray;
                Byte graydata;

                // Convert original image to greyscale
                for (int i = 0; i < original.Width; i++)
                {
                    for (int j = 0; j < original.Height; j++)
                    {
                        temp = original.GetPixel(i, j);
                        graydata = (byte)((temp.R + temp.G + temp.B) / 3);
                        gray = Color.FromArgb(graydata, graydata, graydata);
                        original.SetPixel(i, j, gray);
                    }
                }

                // Histogram 1D data
                int[] histogramData = new int[256];
                for (int i = 0; i < original.Width; i++)
                {
                    for (int j = 0; j < original.Height; j++)
                    {
                        temp = original.GetPixel(i, j);
                        histogramData[temp.R]++;
                    }
                }

                // Bitmap Graph Generate
                // Setting empty bitmap with BG color
                processed = new Bitmap(256, 800);
                for (int i = 0; i < 256; i++)
                {
                    for (int j = 0; j < 800; j++)
                    {
                        processed.SetPixel(i, j, Color.White);
                    }
                }

                // Map or Plot points based from histogramData
                for (int i = 0; i < 256; i++)
                {
                    for (int j = 0; j < Math.Min(histogramData[i] / 5, processed.Height - 1); j++)
                    {
                        processed.SetPixel(i, (processed.Height - 1) - j, Color.Black);
                    }
                }
            }

            /// <summary>
            /// Lets you copy the loaded image and background image, subtracts it, and pastes it on the processed image variable
            /// </summary>
            /// <param name="image"> Original Image with greenscreen </param>
            /// <param name="background"> Background Image </param>
            /// <param name="processed"> Processed Image </param>
            public static void Subtract(ref Bitmap image, ref Bitmap background, ref Bitmap processed)
            {
                processed = new Bitmap(image.Width, image.Height);

                Color myGreen = Color.FromArgb(0, 0, 255);
                int greyGreen = (myGreen.R + myGreen.G + myGreen.B) / 3;
                int threshold = 5;

                for (int i = 0; i < image.Width; i++)
                {
                    for (int j = 0; j < image.Height; j++)
                    {
                        Color pixel = image.GetPixel(i, j);
                        Color backPixel = background.GetPixel(i, j);

                        // Calculate formula for sepia
                        int grey = (pixel.R + pixel.G + pixel.B) / 3;
                        int subtractValue = Math.Abs(grey - greyGreen);

                        // Check conditions & Set pixel to the calculated one
                        if (subtractValue > threshold)
                        {
                            processed.SetPixel(i, j, pixel);
                        }
                        else
                        {
                            processed.SetPixel(i, j, backPixel);
                        }
                    }
                }
            }
        }
}
