using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BSK3_GUI
{
    public class MatrixA
    {
        public static string Encipher(string plainText, int[] key)
        {
            string cipherText = "";

            plainText = Regex.Replace(plainText, @"\s+", ""); // wyrażenie regularne do usunięcia spacji
            int d = key.Length;
            int textLength = plainText.Length;
            int colHeight = (textLength - 1) / d + 1; // dzielenie z zaokrągleniem w górę
            char[,] array = new char[d, colHeight]; // tablica o szerokości równej długości kodowanego ciągu, wysokości równej dł. klucza

            //zapis wiadomości w tablicy, wiersz po wierszu
            int currentChar = 0;
            for (int i = 0; i < colHeight; i++)
            {
                for (int j = 0; j < d; j++)
                {
                    array[j, i] = plainText[currentChar++];

                    if (currentChar >= textLength)
                    {
                        break; //zakończenie pętli, gdy wpisana zostanie cała wiadomość
                    }
                }
            }

            // odczytanie szyfrogramu
            // dla każdego wiersza tablicy odczytywane są znaki wg kolejności wyznaczonej przez klucz
            for (int i = 0; i < colHeight; i++) 
            {
                foreach (int col in key) // pętla dla kolumn, zmienna col zawiera kolejne nr kolumn z klucza
                {
                    if (array[col - 1, i] != '\0')
                    {
                        cipherText += array[col - 1, i]; // wpisanie znaku do określonej kolumny
                    }
                }
            }

            return cipherText;
        }

        public static string Decipher(string cipherText, int[] key)
        {
            string plainText = "";

            int d = key.Length;
            int textLength = cipherText.Length;
            int colHeight = (textLength - 1) / d + 1; // dzielenie z zaokrągleniem w górę
            char[,] array = new char[d, colHeight];

            int currentChar = 0;

            // wpisanie szyfrogramu do tablicy wiersz po wierszu, według kolejności ustalonej w kluczu
            for (int i = 0; i < colHeight; i++) // pętla dla wierszy tablicy
            {
                foreach (int col in key) // pętla dla kolumn, zmienna col zawiera kolejne nr kolumn z klucza
                {
                    //zapisanie znaku w odpowiedniej kolumnie obecnie przetwarzanego wiersza
                    array[col - 1, i] = cipherText[currentChar++];
                    if (currentChar == textLength)
                    {
                        break; // przerwanie pętli, jeśli obecny znak jest ostatnim w przetwarzanym ciągu
                    }
                }
            }

            // odczytanie wiadomości, wiersz po wierszu
            for (int i = 0; i < colHeight; i++)
            {
                for (int j = 0; j < d; j++)
                {
                    if (array[j, i] != '\0')
                    {
                        plainText += array[j, i];
                    }
                }
            }

            return plainText;
        }
    }
}
