using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BSK3_GUI
{
    public static class MatrixB
    {
        public static string Encipher(string plainText, string key)
        {
            string cipherText = "";

            plainText = Regex.Replace(plainText, @"\s+", ""); // wyrażenie regularne do usunięcia spacji

            int textLength = plainText.Length;
            int colHeight = (textLength - 1) / key.Length + 1; // dzielenie z zaokrągleniem w górę
            char[,] array = new char[key.Length, colHeight];

            char[] order = key.ToCharArray(); // utworzenie tablicy znaków ze słowa-klucza
            Array.Sort(order); // sortowanie tablicy z kluczem

            // stworzenie listy z numerami kolumn tablicy, które jeszcze nie zostały odczytane
            List<int> remainingCols = Enumerable.Range(0, key.Length).ToList(); 

            // zapis wiadmości w tablicy, długość wiersza równa ilości kolumn
            int currentChar = 0;
            for (int i = 0; i < colHeight; i++)
            {
                for (int j = 0; j < key.Length; j++)
                {
                    array[j, i] = plainText[currentChar++];

                    if (currentChar >= textLength)
                    {
                        break;
                    }
                }
            }

            // petla dla kolejnych znaków w posortowanej tablicy z kluczem
            int currentCol = 0;
            foreach (char keyChar in order)
            {
                foreach (int col in remainingCols) // sprawdzane są kolejne nieodczytane kolumny tablicy
                {
                    // jeśli odnaleziona zostanie właściwa kolumna
                    // kolumna jest wpisywana do szyfrogramu, a jej numer usuwany z remainingCols
                    if (keyChar == key[col]) 
                    {
                        for (int i = 0; i < colHeight; i++)
                        {
                            if (array[col, i] != '\0')
                            {
                                cipherText += array[col, i];
                            }
                        }

                        remainingCols.RemoveAt(currentCol);

                        break;
                    }

                    currentCol++; // przejście do kolejnej kolumny
                }

                currentCol = 0; // wyzerowanie wskaźnika kolumny w celu umożliwienia przeszukania wszystkich pozostałych kolumn
            }

            return cipherText;
        }

        public static string Decipher(string cipherText, string key)
        {
            string plainText = "";

            int textLength = cipherText.Length;
            int colHeight = (textLength - 1) / key.Length + 1; // dzielenie z zaokrągleniem w górę
            char[,] array = new char[key.Length, colHeight];

            char[] order = key.ToCharArray(); // utworzenie tablicy znaków ze słowa-klucza
            Array.Sort(order); // sortowanie tablicy z kluczem

            List<int> remainingCols = Enumerable.Range(0, key.Length).ToList();

            int longerCols = cipherText.Length % key.Length;

            int colStart = 0;
            int selectionLength = 0;

            // wpisanie szyfrogramu do tablicy po kolumnach
            int currentCol = 0;
            foreach (char keyChar in order)
            {
                foreach (int col in remainingCols) // analogicznie do szyfrowania, sprawdzane są niewykorzystane kolumny
                {
                    if (keyChar == key[col]) // sprawdzenie czy znak w kolumnie jest identynczy do kolejnego znaku klucza
                    {
                        if (col < longerCols) // korekcja długości kolumny, wg wyliczonej wartości dłuższych kolumn, liczać od lewej strony
                        {
                            selectionLength = colHeight;
                        }
                        else
                        {
                            selectionLength = colHeight - 1;
                        }
                        string split = cipherText.Substring(colStart, selectionLength); // wydzielenie kolumny z dekodowanego ciągu
                        for (int i = 0; i < split.Length; i++)
                        {
                            array[col, i] = (split)[i]; // wpisanie kolejnego ciągu szyfrogramu do wyznaczonej kolumny
                        }

                        colStart += selectionLength; // przesunięcie wskaźnika o wczytaną liczbę znaków
                        remainingCols.RemoveAt(currentCol); // usunięcie odwiedzonej kolumny z remainingCols

                        break;
                    }

                    currentCol++; // przejście do kolejnej kolumny
                }

                currentCol = 0; // wyzerowanie wskaźnika obecnej kolumny w celu umożliwienia przeszukania wszystkich pozostałych kolumn
            }

            // odczytanie wiadomości
            int currentChar = 0;
            for (int i = 0; i < colHeight; i++)
            {
                for (int j = 0; j < key.Length; j++)
                {
                    plainText += array[j, i];

                    if (currentChar >= textLength)
                    {
                        break;
                    }
                }
            }

            return plainText;
        }
    }
}
