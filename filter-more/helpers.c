#include "helpers.h"
#include <math.h>

// Convert image to grayscale
void grayscale(int height, int width, RGBTRIPLE image[height][width])
{
    float num_count = 3;
    for (int i = 0; i < height; i++)
    {
        for (int j = 0; j < width; j++)
        {
            int average = (int) round(
                (float) (image[i][j].rgbtRed + image[i][j].rgbtGreen + image[i][j].rgbtBlue) /
                num_count);
            image[i][j].rgbtRed = average;
            image[i][j].rgbtGreen = average;
            image[i][j].rgbtBlue = average;
        }
    }
    return;
}

// Reflect image horizontally
void reflect(int height, int width, RGBTRIPLE image[height][width])
{
    int half_width = width / 2;
    for (int i = 0; i < height; i++)
    {
        for (int j = 0; j < half_width; j++)
        {
            RGBTRIPLE temp = image[i][j];
            image[i][j] = image[i][width - j - 1];
            image[i][width - j - 1] = temp;
        }
    }
    return;
}

// Blur image
void blur(int height, int width, RGBTRIPLE image[height][width])
{
    // Copy image
    RGBTRIPLE original_image[height][width];
    for (int i = 0; i < height; i++)
    {
        for (int j = 0; j < width; j++)
        {
            original_image[i][j] = image[i][j];
        }
    }

    float num_count;
    for (int i = 0; i < height; i++)
    {
        for (int j = 0; j < width; j++)
        {
            int red_sum = 0;
            int green_sum = 0;
            int blue_sum = 0;
            // Middle case
            if (i > 0 && i < height - 1 && j > 0 && j < width - 1)
            {
                num_count = 9;
                for (int row = i - 1; row <= i + 1; row++)
                {
                    for (int column = j - 1; column <= j + 1; column++)
                    {
                        red_sum += original_image[row][column].rgbtRed;
                        green_sum += original_image[row][column].rgbtGreen;
                        blue_sum += original_image[row][column].rgbtBlue;
                    }
                }
            }
            // Top case
            else if (i == 0 && j > 0 && j < width - 1)
            {
                num_count = 6;
                for (int row = i; row <= i + 1; row++)
                {
                    for (int column = j - 1; column <= j + 1; column++)
                    {
                        red_sum += original_image[row][column].rgbtRed;
                        green_sum += original_image[row][column].rgbtGreen;
                        blue_sum += original_image[row][column].rgbtBlue;
                    }
                }
            }
            // Bottom case
            else if (i == height - 1 && j > 0 && j < width - 1)
            {
                num_count = 6;
                for (int row = i - 1; row <= i; row++)
                {
                    for (int column = j - 1; column <= j + 1; column++)
                    {
                        red_sum += original_image[row][column].rgbtRed;
                        green_sum += original_image[row][column].rgbtGreen;
                        blue_sum += original_image[row][column].rgbtBlue;
                    }
                }
            }
            // Left case
            else if (i > 0 && i < height - 1 && j == 0)
            {
                num_count = 6;
                for (int row = i - 1; row <= i + 1; row++)
                {
                    for (int column = j; column <= j + 1; column++)
                    {
                        red_sum += original_image[row][column].rgbtRed;
                        green_sum += original_image[row][column].rgbtGreen;
                        blue_sum += original_image[row][column].rgbtBlue;
                    }
                }
            }
            // Right case
            else if (i > 0 && i < height - 1 && j == width - 1)
            {
                num_count = 6;
                for (int row = i - 1; row <= i + 1; row++)
                {
                    for (int column = j - 1; column <= j; column++)
                    {
                        red_sum += original_image[row][column].rgbtRed;
                        green_sum += original_image[row][column].rgbtGreen;
                        blue_sum += original_image[row][column].rgbtBlue;
                    }
                }
            }
            // Top left case
            else if (i == 0 && j == 0)
            {
                num_count = 4;
                for (int row = i; row <= i + 1; row++)
                {
                    for (int column = j; column <= j + 1; column++)
                    {
                        red_sum += original_image[row][column].rgbtRed;
                        green_sum += original_image[row][column].rgbtGreen;
                        blue_sum += original_image[row][column].rgbtBlue;
                    }
                }
            }
            // Top right case
            else if (i == 0 && j == width - 1)
            {
                num_count = 4;
                for (int row = i; row <= i + 1; row++)
                {
                    for (int column = j - 1; column <= j; column++)
                    {
                        red_sum += original_image[row][column].rgbtRed;
                        green_sum += original_image[row][column].rgbtGreen;
                        blue_sum += original_image[row][column].rgbtBlue;
                    }
                }
            }
            // Bottom left case
            else if (i == height - 1 && j == 0)
            {
                num_count = 4;
                for (int row = i - 1; row <= i; row++)
                {
                    for (int column = j; column <= j + 1; column++)
                    {
                        red_sum += original_image[row][column].rgbtRed;
                        green_sum += original_image[row][column].rgbtGreen;
                        blue_sum += original_image[row][column].rgbtBlue;
                    }
                }
            }
            // Bottom right case
            else if (i == height - 1 && j == width - 1)
            {
                num_count = 4;
                for (int row = i - 1; row <= i; row++)
                {
                    for (int column = j - 1; column <= j; column++)
                    {
                        red_sum += original_image[row][column].rgbtRed;
                        green_sum += original_image[row][column].rgbtGreen;
                        blue_sum += original_image[row][column].rgbtBlue;
                    }
                }
            }
            image[i][j].rgbtRed = (int) round((float) red_sum / num_count);
            image[i][j].rgbtGreen = (int) round((float) green_sum / num_count);
            image[i][j].rgbtBlue = (int) round((float) blue_sum / num_count);
        }
    }
    return;
}

// Detect edges
int xApplyMatrix(int channel, int i, int j, int row, int column, int height, int width);
int yApplyMatrix(int channel, int i, int j, int row, int column, int height, int width);
void edges(int height, int width, RGBTRIPLE image[height][width])
{
    // Copy image
    RGBTRIPLE original_image[height][width];
    for (int i = 0; i < height; i++)
    {
        for (int j = 0; j < width; j++)
        {
            original_image[i][j] = image[i][j];
        }
    }

    // Check for convolutional matrix
    for (int i = 0; i < height; i++)
    {
        for (int j = 0; j < width; j++)
        {
            int red_x = 0;
            int red_y = 0;
            int green_x = 0;
            int green_y = 0;
            int blue_x = 0;
            int blue_y = 0;
            for (int row = i - 1; row <= i + 1; row++)
            {
                for (int column = j - 1; column <= j + 1; column++)
                {
                    red_x += xApplyMatrix(original_image[row][column].rgbtRed, i, j, row, column, height, width);
                    red_y += yApplyMatrix(original_image[row][column].rgbtRed, i, j, row, column, height, width);
                    green_x += xApplyMatrix(original_image[row][column].rgbtGreen, i, j, row, column, height, width);
                    green_y += yApplyMatrix(original_image[row][column].rgbtGreen, i, j, row, column, height, width);
                    blue_x += xApplyMatrix(original_image[row][column].rgbtBlue, i, j, row, column, height, width);
                    blue_y += yApplyMatrix(original_image[row][column].rgbtBlue, i, j, row, column, height, width);
                }
            }
            int red = round(sqrt(red_x * red_x + red_y * red_y));
            if (red > 255)
            {
                red = 255;
            }
            int green = round(sqrt(green_x * green_x + green_y * green_y));
            if (green > 255)
            {
                green = 255;
            }
            int blue = round(sqrt(blue_x * blue_x + blue_y * blue_y));
            if (blue > 255)
            {
                blue = 255;
            }

            image[i][j].rgbtRed = red;
            image[i][j].rgbtGreen = green;
            image[i][j].rgbtBlue = blue;
        }
    }
    return;
}

int xApplyMatrix(int channel, int i, int j, int row, int column, int height, int width)
{
    int sum = 0;
    if (row >= 0 && row < height && column >= 0 && column < width)
    {
        // Add appropriate values for G(x)
        if ((row == i - 1 || row == i + 1) && column == j - 1)
        {
            sum += -1 * channel;
        }
        else if ((row == i - 1 || row == i + 1) && column == j + 1)
        {
            sum += channel;
        }
        else if (row == i && column == j - 1)
        {
            sum += -2 * channel;
        }
        else if (row == i && column == j + 1)
        {
            sum += 2 * channel;
        }
        else if (row == i && column == j)
        {
            sum += 0 * channel;
        }
    }
    return sum;
}

int yApplyMatrix(int channel, int i, int j, int row, int column, int height, int width)
{
    int sum = 0;
    if (row >= 0 && row < height && column >= 0 && column < width)
    {
        // Add appropriate values for G(y)
        if ((column == j - 1 || column == j + 1) && row == i - 1)
        {
            sum += -1 * channel;
        }
        else if ((column == j - 1 || column == j + 1) && row == i + 1)
        {
            sum += channel;
        }
        else if (column == j && row == i - 1)
        {
            sum += -2 * channel;
        }
        else if (column == j && row == i + 1)
        {
            sum += 2 * channel;
        }
        else if (row == i && column == j)
        {
            sum += 0 * channel;
        }
    }
    return sum;
}
