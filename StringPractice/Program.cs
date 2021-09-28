using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringPractice
{
    class Program
    {
        static void changeColor(string str, int count)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nTask {count}. {str} \n");
            Console.ResetColor();
        }
        static void PrintArray(string[] arr)
        {
            foreach (var item in arr)
                Console.WriteLine(item);
        }
        static string Reverse(string str)
        {
            char[] charArray = str.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        static bool IsPalindrome(string str, bool ignoreCase = true)
        {
            if (ignoreCase)
                str = str.ToLowerInvariant();
            char[] charArray = str.ToCharArray();
            char[] reverseCharArray = charArray.Reverse().ToArray();
            return charArray.SequenceEqual(reverseCharArray);
        }

        static string[] ReplaceArray(string[] strArray, int wordLength)
        {
            string newString = "";
            for (int i = 0; i < strArray.Length; i++)
            {
                if (strArray[i].Length == wordLength)
                {
                    char[] str = strArray[i].ToCharArray();
                    for (int j = str.Length; j > wordLength-3; j--)
                    {
                        str[j-1] = '$';
                    }
                    newString += new string(str) + " ";
                } else 
                    newString += strArray[i] + " ";
            }

            return newString.Split(' ');
        }

        static void PercentageOfLowerToUpper(string str)
        {
            float lowerLetter = 0;
            float upperLetter = 0;
            char[] charArr = str.ToCharArray();
            for (int i = 0; i < str.Length; i++)
            {
                if (char.IsUpper(charArr[i]))
                    upperLetter++;
                else
                    lowerLetter++;
            }
            Console.WriteLine($"{str} - length {str.Length}");
            Console.WriteLine($"Lower latter - {Math.Round(lowerLetter * 100/str.Length,1)}% ({lowerLetter}) \tUpper Letter - {Math.Round(upperLetter * 100 / str.Length,1)}% ({upperLetter})");
        }

        static void PrintColorMatrix(int x, int y, char[,] matrix, string position, int wordLength)
        {
            int rows = matrix.GetUpperBound(0) + 1;
            int columns = matrix.Length / rows;
            int n = 0;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (i == x & j == y & n < wordLength)
                    {
                        n++;
                        Console.ForegroundColor = ConsoleColor.Green;
                        if (position == "vertical")
                        {
                            x = x + 1; 
                        } else
                        {
                            y = y + 1;
                        }
                    }
                    else Console.ResetColor();
                    Console.Write($"{matrix[i,j]} ");
                }
                Console.WriteLine();
            }
        }


        static void FoundMatrixWord(char[,] matrixArr, string word)
        {

            int rows = matrixArr.GetUpperBound(0) + 1;
            int columns = matrixArr.Length / rows;
            int n = 1;
            int coordinateX = 0;
            int coordinateY = 0;
            string position = "";
            
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (matrixArr[i, j] == word[0])
                    {
                        if(j+1<columns & matrixArr[i,j+1] == word[1])
                        {
                            n = 2;
                            for (int k = j + 2; k < columns; k++)
                            {
                                if (matrixArr[i, k] == word[n] & n < word.Length-1)
                                {
                                    n++;
                                    if (n == word.Length-1)
                                    {
                                        coordinateX = i;
                                        coordinateY = k-n+1;
                                        position = "horizontal";
                                    }
                                }
                                else break;
                            }
                        }
                        else
                        {
                            n = 1;
                            for (int l = i + 1; l < rows; l++)
                            {
                                if (matrixArr[l, j] == word[n] & n < word.Length-1)
                                {
                                    n++;
                                    if (n == word.Length-1)
                                    {
                                        coordinateX = l-n+1;
                                        coordinateY = j;
                                        position = "vertical";
                                    }
                                }
                                else break;
                            }
                        }
                    }

                }
                if (n == word.Length) break;
            }
            Console.WriteLine($"X - {coordinateX} \t Y - {coordinateY} \tposition - {position}");
            PrintColorMatrix(coordinateX, coordinateY, matrixArr, position, word.Length);
        }
        


        static string sumString(string strNumber1, string strNumber2)
        {
            string sumResult = "";
            int carry = 0;

            if (strNumber2.Length > strNumber1.Length)
            {
                string str = strNumber2;
                strNumber2 = strNumber1;
                strNumber1 = str;
            }
            int difLength = strNumber1.Length - strNumber2.Length;

            for (int i = strNumber2.Length - 1; i >= 0; i--)
            {
                int sum = (int)(strNumber1[i + difLength]-'0') + (int)(strNumber2[i]-'0') + carry;
                sumResult += (char)(sum % 10 + '0');
                carry = sum / 10;
            }

            for (int i = difLength - 1; i >= 0; i--)
            {
                int sum = (int)(strNumber1[i] - '0') + carry;
                sumResult += (char)(sum % 10 + '0');
                carry = sum / 10;
            }



            if (carry > 0)
                sumResult += (char)(carry+'0');

            char[] resCachArr = sumResult.ToArray();
            Array.Reverse(resCachArr);
            return new string(resCachArr);
        }


        static void Main(string[] args)
        {
            #region Task1
            //Create some strings array and sort the elements by the first symbol of each string.
            string[] strArray = { "cdfdsf", "beter", "gdfds", "asa", "sdfdsf", "asdfs" };
            changeColor("Create some strings array and sort the elements by the first symbol of each string.", 1);
            Console.WriteLine("Before sort");
            PrintArray(strArray);
            Array.Sort(strArray);
            Console.WriteLine("\nAfter sort");
            PrintArray(strArray);
            #endregion

            #region Task2
            //Enter a string of words.Display the words in reverse order.
            changeColor("Enter a string of words. Display the words in reverse order.", 2);
            string words = "Hello word. How are you. The good day today.";
            Console.WriteLine(words);
            Console.WriteLine(Reverse(words));
            #endregion

            #region Task3
            //A user inputs a string. Remove the spaces between the first and the last dot symbol.
            changeColor("A user inputs a string. Remove the spaces between the first and the last dot symbol.", 3);
            string words2 = "  fsdfsdf.     dsfsdfsd.         uirytiue.";
            Console.WriteLine(words2);
            Console.WriteLine(Regex.Replace(words2, @"\s+", " "));
            #endregion

            #region Task4
            //Define whether the entered string is palindrome or not.Palindrome it’s a word or phrase that read the same in both directions.
            changeColor("Define whether the entered string is palindrome or not.Palindrome it’s a word or phrase that read the same in both directions.", 4);
            string word3 = "madam"; // redivider, radar, level
            Console.WriteLine(word3);
            Console.WriteLine(IsPalindrome(word3));
            #endregion

            #region Task5
            //Find the word that is in a certain order and display its first letter. 
            changeColor("Find the word that is in a certain order and display its first letter.", 5);
            Console.WriteLine(words);
            Console.Write("Enter the word number - ");
            int wordNumber = Int32.Parse(Console.ReadLine());
            string[] arrayWords = words.Split(' ');
            Console.WriteLine(arrayWords[wordNumber - 1][0]);
            #endregion

            #region Task6
            //There is an array of string. Replace the last three symbols with ‘$’ in words with the entered length.
            changeColor("There is an array of string. Replace the last three symbols with ‘$’ in words with the entered length", 6);
            PrintArray(strArray);
            Console.Write("Enter the word length - ");
            int wordLength = Int32.Parse(Console.ReadLine());
            PrintArray(ReplaceArray(strArray, wordLength));
            #endregion

            #region Task7
            //There is some text. Define the percentage of lower and upper letter relative to all characters.
            changeColor("There is some text. Define the percentage of lower and upper letter relative to all characters.", 7);
            string someText = "HeLLo WorD. HoW ARE yoU. The Good Day TOday.";
            PercentageOfLowerToUpper(someText);
            #endregion

            #region Task8
            // There is a square matrix that consists of letters. Find the chain of letters that compose an entered word.
            // The chain may be placed vertically or horizontally.
            //Display the number of the row or column in which the word begins and its position(vertical or horizontal).
            changeColor("There is a square matrix that consists of letters. Find the chain of letters that compose an entered word. \nThe chain may be placed vertically or horizontally. \nDisplay the number of the row or column in which the word begins and its position(vertical or horizontal).", 8);
            char[,] theMatrix =
            {
                { 'd','u','e','h','b','m','r'},
                { 'x','h','q','w','e','r','t'},
                { 'z','e','e','e','i','u','y'},
                { 'z','l','f','r','a','v','b'},
                { 's','l','v','t','l','o','d'},
                { 'a','o','t','c','i','o','l'},
                { 'z','e','e','o','i','u','y'}
            };
            //Console.Write("Enter word - ");
            //string matrixWord = Console.ReadLine();
            string matrixWord = "hello";
            FoundMatrixWord(theMatrix, matrixWord);
            #endregion

            #region Task9
            //There is a string of the words separated by spaces.It might be a several spaces at the beginning, end or in the string.
            //You should replace the string in order to have no spaces on the sides and the words are separated by the single star symbol ‘*’.
            changeColor("There is a string of the words separated by spaces. \nIt might be a several spaces at the beginning, end or in the string. \nYou should replace the string in order to have no spaces on the sides and the words are separated by the single star symbol ‘*’.", 9);
            string wordsTaskNine = "  Hello   word. How are   you. The  good  day   today.      ";
            Console.WriteLine(wordsTaskNine);
            Console.WriteLine(Regex.Replace(wordsTaskNine.Trim(), @"\s+", "*"));
            #endregion

            #region Task10
            //There is a string of the words. Replace all letters ‘a’ with ‘b’ in the largest words of the string.
            changeColor("There is a string of the words. Replace all letters ‘a’ with ‘b’ in the largest words of the string.", 10);
            string wordsTaskTen = "Helldword. Haoawaresdfa you. Thebsdf good day today.";
            Console.WriteLine(wordsTaskTen);
            string[] strArr = wordsTaskTen.Split(' ');
            int wordLenghtIndex = 0;
            int wordLengthTen = 0;
            for (int i = 0; i < strArr.Length; i++)
            {
                if (strArr[i].Length > wordLengthTen)
                {
                    wordLengthTen = strArr[i].Length;
                    wordLenghtIndex = i;
                }
            };
            Console.WriteLine(strArr[wordLenghtIndex].Replace('a', 'b'));
            #endregion

            #region Task11
            //There is a string that consists of words and numbers which are separated by spaces.
            //Create the three strings: one of them must contain only integer numbers, the second contains float numbers, and the third – other words.
            changeColor("There is a string that consists of words and numbers which are separated by spaces. \nCreate the three strings: one of them must contain only integer numbers, the second contains float numbers, \nand the third – other words.", 11);
            string wordsTaskEleven = "Helldword. 3 5 3,5 4,9 Haoawaresdfa 6 sdgfds 8,5";
            Console.WriteLine(wordsTaskEleven);
            string[] strArrEleven = wordsTaskEleven.Split(' ');

            double val = 0;
            string intString = "";
            string stringString = "";
            string doubleString = "";

            for (int i = 0; i < strArrEleven.Length; i++)
            {
                if (Double.TryParse(strArrEleven[i], out val))
                {
                    if (Convert.ToDouble(strArrEleven[i]) % 1 == 0)
                        intString = intString + " " + strArrEleven[i];
                    else
                        doubleString = doubleString + " " + strArrEleven[i];
                }
                else
                {
                    stringString = stringString + " " + strArrEleven[i];
                }
            }
            Console.WriteLine($"Integer string - {intString}");
            Console.WriteLine($"Float string -  {doubleString}");
            Console.WriteLine($"String - {stringString}");
            #endregion

            #region Task12
            //Remove all words from the string which have less than five characters.
            changeColor("Remove all words from the string which have less than five characters.", 12);
            string wordsTaskTwelve = "Hello word How are you The good day today";
            Console.WriteLine(wordsTaskTwelve);
            string[] wordArrTwelve = wordsTaskTwelve.Split(' ');
            var longWords = wordArrTwelve.Where(x => x.Length > 4);
            string outputString = String.Join(" ", longWords.ToArray());
            Console.WriteLine(outputString);
            #endregion

            #region Task13
            //Calculate the sum of two very large numbers. Use the strings to find a solution.
            changeColor("Calculate the sum of two very large numbers. Use the strings to find a solution.", 13);
            string strNumber1 = "923456";
            string strNumber2 = "123456345";
            Console.WriteLine($"Number 1 - {strNumber1}\tNumber 2 - {strNumber2}");
            Console.WriteLine("Sum - " + sumString(strNumber1,strNumber2));
            #endregion

        }
    }
}
