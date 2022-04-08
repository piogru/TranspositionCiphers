using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSK3_GUI
{
    public static class RailFence
    {
        public static string Encipher(string plainText, int n)
        {
            string cipherText = "";
            int textLength = plainText.Length;
            char[,] array = new char[textLength, n];

            // kierunek poruszania się po tablicy, 1 - w dół, -1 - w górę
            // na początku ustawiony kierunek przeciwny, zostanie poprawiony w pierwszej iteracji
            int diff = -1; 

            int currentChar = 0; // bieżący znak plaintext
            int currentRow = 0; // bieżący rząd tablicy
            while (currentChar < textLength) // pętla przetwarzająca wszystkie znaki w wiadomości
            {
                array[currentChar, currentRow] = plainText[currentChar++]; // wstawienie kolejnego znaku do tablicy
                if (currentRow == n - 1 || currentRow == 0) 
                {
                    diff *= -1; // zmiana kierunku w przypadku, gdy wskaźnik wiersza znajdzie się w 1 lub ostatnim rzędzie
                }

                currentRow += diff; // zmiana wiersza w kierunku określonym przez zmienną diff
            }

            // pętla odczytujaca zaszyfrowaną wiadomość z kolejnych wierszy
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < textLength; j++)
                {
                    if (array[j, i] != '\0')
                    {
                        cipherText += array[j, i];
                    }

                }
            }

            return cipherText;
        }

        public static string Decipher(string cipherText, int n)
        {
            string plainText = "";

            int textLength = cipherText.Length;
            char[,] array = new char[textLength, n];

            // kierunek poruszania się po tablicy, 1 - w dół, -1 - w górę
            // na początku ustawiony kierunek przeciwny, zostanie poprawiony w pierwszej iteracji
            int diff = -1;

            int currentChar = 0; // bieżący znak cipherText
            int currentRow = 0; // bieżący rząd tablicy

            // odtworzenie "płotków", pętla odnajduje pola, w których zostały
            // umieszczone litery szyfrowanej wiadomości i oznacza je znakiem `-`
            while (currentChar < textLength)
            {
                array[currentChar, currentRow] = '-';
                currentChar++;
                if (currentRow == n - 1 || currentRow == 0)
                {
                    diff *= -1; // zmiana kierunku w przypadku, gdy wskaźnik wiersza znajdzie się w 1 lub ostatnim rzędzie
                }

                currentRow += diff;
            }

            // zapis szyfrogramu w miejscach wyznaczonych w poprzedniej pętli
            // znaki szyfrogramu zapisywane są w wyznaczonych miejscach, przechodząc po kolejnych wierszach
            currentChar = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < textLength; j++)
                {
                    if (array[j, i] == '-')
                    {
                        array[j, i] = cipherText[currentChar++];
                        //cipherText += array[j, i];
                    }
                }
            }

            // odczytanie odkodowanej wiadomości w sposób analogiczny do kodowania
            currentChar = 0;
            currentRow = 0;
            while (currentChar < textLength)
            {
                plainText += array[currentChar++, currentRow];
                if (currentRow == n - 1 || currentRow == 0)
                {
                    diff *= -1;
                }

                currentRow += diff;
            }

            return plainText;
        }

    }
}
