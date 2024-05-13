#include <stdio.h>
#include <stdlib.h>
#include <stdint.h>

uint8_t buffer[4];

int main(int argc, char *argv[])
{
    if (argc != 2)
    {
        printf("Usage: ./recover card.raw\n");
        return 1;
    }

    FILE *card = fopen(argv[1], "r");
    if (card == NULL)
    {
        printf("Could not open file.\n");
        return 1;
    }

    char filename[8];
    int jpeg_count = 0;

    FILE *img = NULL;
    while (fread(&buffer, 512, 1, card) == 1)
    {
        if (buffer[0] == 0xff && buffer[1] == 0xd8 && buffer[2] == 0xff &&
            (buffer[3] & 0xf0) == 0xe0)
        {
            if (jpeg_count != 0)
            {
                fclose(img);
            }
            sprintf(filename, "%03i.jpg", jpeg_count);
            img = fopen(filename, "w");
            jpeg_count += 1;
        }
        if (img != NULL)
        {
            fwrite(&buffer, 512, 1, img);
        }
    }
    fclose(img);
    fclose(card);
}
