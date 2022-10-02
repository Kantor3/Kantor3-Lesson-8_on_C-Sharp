/*
Урок 8. Как не нужно писать код. Часть 2 (Выполнение Домашнего задания)
*/
// Библиотека функций для выполнения вспомогательных операций.
//
// Организация завершения работы с модулем
bool CheckExit(dynamic num)
{
    if (num == 0)
    {
        Console.WriteLine("\nРабота с программой завершена, До встречи!\n");
        return true;
    }
    return false;
}

// Создание 1D-массива заполненных случайно сгенерированными целыми числами
int[] CreatRandom1DArray(int size, int minRnd, int maxRnd)
{
    int[] randNumber = new int[size];
    Random int_rnd = new Random();
    int i;
    for (i = 0; i < size; i++)
        randNumber[i] = int_rnd.Next(minRnd, maxRnd + 1);
    return randNumber;
}

// Создание 2D-массива заполненных случайно сгенерированными числами (целыми или вещественными, на выбор)
dynamic[,] CreatRandom2DArray(bool real = false, bool fill = true, bool zero = false)
{
    Console.Write("Введите число строк: ");
    int rows = Convert.ToInt32(Console.ReadLine());
    Console.Write("Введите число столбцов: ");
    int columns = Convert.ToInt32(Console.ReadLine());

    dynamic[,] newArray = new dynamic[rows, columns];

    int minValue = 0; int maxValue = 0;
    if (fill)
    {
        Console.Write("Введите минимальное значение элемента: ");
        minValue = Convert.ToInt32(Console.ReadLine());
        Console.Write("Введите максимальное значение элемента: ");
        maxValue = Convert.ToInt32(Console.ReadLine());

        for (int i = 0; i < rows; i++)
            for (int j = 0; j < columns; j++)
                newArray[i, j] = !real ? new Random().Next(minValue, maxValue + 1) :
                                        minValue + (maxValue - minValue + 1) * new Random().NextDouble();
    }
    else if (zero)
        for (int i = 0; i < rows; i++) for (int j = 0; j < columns; j++) newArray[i, j] = 0;

    Console.WriteLine("");
    return newArray;
}

// Вывод содержимого 1D-массива
void Show1dArray(dynamic[] array, string text = "")
{
    if (text != string.Empty) Console.WriteLine(text);

    Console.Write("[");
    for (int i = 0; i < array.Length; i++)
        Console.Write($"{array[i]} ");
    Console.WriteLine("]\n");
}

// Вывод содержимого 2D-массива
void Show2dArray(dynamic[,] array, string text = "", string markRow = "", int row = 0)
{
    if (text != string.Empty) Console.WriteLine(text);

    for (int i = 0; i < array.GetLength(0); i++)
    {
        for (int j = 0; j < array.GetLength(1); j++)
            Console.Write($"{array[i, j]} ");
        if (i == row) Console.Write($" {markRow}");
        Console.WriteLine();
    }
    Console.WriteLine();
}

/*-----------------------------------------------------------------
Задача 54: Задайте двумерный массив. Напишите программу, 
которая упорядочит по убыванию элементы каждой строки двумерного массива.
Например, задан массив:
1 4 7 2
5 9 2 3
8 4 2 4
В итоге получается вот такой массив:
7 4 2 1
9 5 3 2
8 4 4 2
-------------------------------------------------------------------
*/
// Реализуем самый простой в исполнении, но не самый эффективный, алгоритм 
// Сортировка пузырьком / Bubble sort
// Сортировка одномерного массива
dynamic[] Array1DSorting(dynamic[] arrayForSort, int directSort = 1)
{
    // Проверка на размер массива
    int sizeArr = arrayForSort.Length;
    if (sizeArr < 2) return arrayForSort;

    // directSort = 1 - по возрастанию; 2 - по убыванию 
    directSort = directSort != 1 & directSort != 2 ? 1 : directSort;
    int signSort = 3 - 2 * directSort;

    // Console.WriteLine($"signSort -> {signSort}");

    int remainder = arrayForSort.Length - 1;

    for (int i = 0; i < remainder; i++)
    {
        // Console.WriteLine($"i -> {i}");
        // Console.WriteLine($"currRemainder -> {remainder-i}");

        for (int j = 0; j < remainder - i; j++)
        {
            // Console.WriteLine($"(i -> {i}, j -> {j})");

            if (signSort * arrayForSort[j] > signSort * arrayForSort[j + 1])
            {
                var temp = arrayForSort[j];
                arrayForSort[j] = arrayForSort[j + 1];
                arrayForSort[j + 1] = temp;
            }
        }
    }
    return arrayForSort;
}

// Сортировка двумерного массива по строкам
dynamic[,] Array2DSorting(dynamic[,] arrayForSort, int directSort = 1)
{
    // Проверка на размер массива
    int size2Arr = arrayForSort.GetLength(1);
    if (size2Arr < 2) return arrayForSort;

    // directSort = 1 - по возрастанию; 2 - по убыванию 
    directSort = directSort != 1 & directSort != 2 ? 1 : directSort;

    int signSort = 3 - 2 * directSort;
    for (int k = 0; k < arrayForSort.GetLength(0); k++)
    {
        int remainder = size2Arr - 1;
        for (int i = 0; i < remainder; i++)
            for (int j = 0; j < remainder - i; j++)
                if (signSort * arrayForSort[k, j] > signSort * arrayForSort[k, j + 1])
                {
                    var temp = arrayForSort[k, j];
                    arrayForSort[k, j] = arrayForSort[k, j + 1];
                    arrayForSort[k, j + 1] = temp;
                }
    }
    return arrayForSort;
}

// Основное тело программы.
Console.WriteLine(@"Задача-54. Сортировка массива по строкам");
Console.WriteLine("---");

while (true)
{
    Console.Write("Продолжить (1- ДА, 0 - НЕТ): ");
    if (CheckExit(Convert.ToInt16(Console.ReadLine()))) break;

    Console.WriteLine("Задайте массив ---------");
    dynamic[,] mayArray = CreatRandom2DArray();
    Show2dArray(mayArray, "Исходный массив:");

    Console.WriteLine($@"Задайте порядок сортировки элементов каждой строки:
                        (1 - по возрастанию; 2 - по убыванию; прочие цифры, по умолчанию - по возрастанию");
    Console.Write("-> "); int directSorting = Convert.ToInt16(Console.ReadLine());
    Console.WriteLine();
    Show2dArray(Array2DSorting(mayArray, directSorting),
                $"Строки массива отсортированы по {(directSorting == 1 ? "возрастанию" : "убыванию")}:");
}
// *** Конец Задачи 54 ***


/*-----------------------------------------------------------------
Задача 56: Задайте прямоугольный двумерный массив. 
Напишите программу, которая будет находить строку с наименьшей суммой элементов.
Например, задан массив:
1 4 7 2
5 9 2 3
8 4 2 4
5 2 6 7
Программа считает сумму элементов в каждой строке и 
выдаёт номер строки с наименьшей суммой элементов: 1 строка
-------------------------------------------------------------------
*/
int FindRowMinArray(dynamic[,] arr)
{
    int SummRowArray(dynamic[,] arr, int row)
    {
        int summ = 0;
        for (int j = 0; j < arr.GetLength(1); j++) summ += arr[row, j];
        return summ;
    }

    int minRow = 1; var sumRow = SummRowArray(arr, 0); var minSum = sumRow;
    for (int i = 1; i < arr.GetLength(0); i++)
    {
        sumRow = SummRowArray(arr, i);
        if (sumRow < minSum)
        {
            sumRow = minSum;
            minRow = i;
        }
    }
    return minRow;
}

// Основное тело программы.
Console.WriteLine(@"Задача-56. Поиск строки массива с наименьшей суммой элементов в строке");
Console.WriteLine("---");

while (true)
{
    Console.Write("Продолжить (1- ДА, 0 - НЕТ): ");
    if (CheckExit(Convert.ToInt16(Console.ReadLine()))) break;

    Console.WriteLine("Задайте массив ---------");
    dynamic[,] mayArray = CreatRandom2DArray();
    Show2dArray(mayArray, "Исходный массив:");

    int rowMin = FindRowMinArray(mayArray);
    Console.WriteLine($"Номер строки с минимальной суммой -> {rowMin} - cм. метку '<-':");
    Show2dArray(mayArray, "", "<-", rowMin - 1);

}
// *** Конец Задачи 56 ***


/*-----------------------------------------------------------------
Задача 58: Задайте две матрицы. Напишите программу, 
которая будет находить произведение двух матриц.
Например, даны 2 матрицы:
2 4 | 3 4
3 2 | 3 3
Результирующая матрица будет:
18 20
15 18
-------------------------------------------------------------------
*/
dynamic[,] MultiArrs(dynamic[,] arr1, dynamic[,] arr2)
{
    dynamic[,] multiArr1Arr2 = new dynamic[arr1.GetLength(0), arr2.GetLength(1)];
    for (int i = 0; i < arr1.GetLength(0); i++)
    {
        for (int j = 0; j < arr2.GetLength(1); j++)
        {
            int sumMulti = 0;
            for (int k = 0; k < arr1.GetLength(1); k++)
                sumMulti += arr1[i, k] * arr2[k, j];
            multiArr1Arr2[i, j] = sumMulti;
        }
    }
    return multiArr1Arr2;
}

// Основное тело программы.
Console.WriteLine(@"Задача-58. Произведение двух матриц");
Console.WriteLine("---");

while (true)
{
    Console.Write("Продолжить (1- YES, 0 - NO): ");
    if (CheckExit(Convert.ToInt16(Console.ReadLine()))) break;

    Console.WriteLine("Задайте матрицу 1 ---------");
    dynamic[,] mayArray1 = CreatRandom2DArray();
    Console.WriteLine("Задайте матрицу 2 (число строк матрицы 2 должно быть = числу колонок матрицы 1) ---------");
    dynamic[,] mayArray2 = CreatRandom2DArray();
    Show2dArray(mayArray1, "Матрица-1:");
    Show2dArray(mayArray2, "Матрица-2:");

    if (mayArray1.GetLength(1) != mayArray2.GetLength(0))
    {
        Console.WriteLine("Для умножения массивы не совместимы. Измените размеры массивов");
        continue;
    }
    Show2dArray(MultiArrs(mayArray1, mayArray2), "Результирующая матрица:");
}
// *** Конец Задачи 58 ***


/*-----------------------------------------------------------------
Задача 60. ...Сформируйте трёхмерный массив из неповторяющихся двузначных чисел. 
Напишите программу, которая будет построчно выводить массив, добавляя индексы каждого элемента.
Массив размером 2 x 2 x 2
66(0,0,0) 25(0,1,0)
34(1,0,0) 41(1,1,0)
27(0,0,1) 90(0,1,1)
26(1,0,1) 55(1,1,1)
-------------------------------------------------------------------
*/
int GetRandomUniq(int[,,] arr, int fromNumber, int toNumber)
{
    while (true)
    {
        int trial = new Random().Next(fromNumber, toNumber);
        foreach (int el in arr) if (el == trial) break;
        return trial;
    }
}

// Основное тело программы.
Console.WriteLine("Задача-60. Формируем и выводим трехмерный массив из уникальных 2-х значных чисел");
Console.WriteLine("---");

while (true)
{
    Console.Write("Продолжить (1- YES, 0 - NO): ");
    if (CheckExit(Convert.ToInt16(Console.ReadLine()))) break;

    Console.Write("Укажите размер массива:");
    int size = Convert.ToInt16(Console.ReadLine());
    int[,,] threeArr = new int[size, size, size];

    for (int i = 0; i < threeArr.GetLength(0); i++)
        for (int j = 0; j < threeArr.GetLength(1); j++)
            for (int k = 0; k < threeArr.GetLength(2); k++)
            {
                threeArr[i, j, k] = GetRandomUniq(threeArr, 10, 99);
                Console.WriteLine($"{threeArr[i, j, k]} ({i},{j},{k})");
            }
}
// *** Конец Задачи 60 ***


/*-----------------------------------------------------------------
Задача 62. Напишите программу, которая заполнит спирально массив 4 на 4.
Например, на выходе получается вот такой массив:
01 02 03 04
12 13 14 05
11 16 15 06
10 09 08 07
-------------------------------------------------------------------
*/

// Основное тело программы.
Console.WriteLine("Задача-62. Заполняем массив NxN по спирали числами из натурального ряда");
Console.WriteLine("---");

while (true)
{
    Console.Write("Продолжить (1- YES, 0 - NO): ");
    if (CheckExit(Convert.ToInt16(Console.ReadLine()))) break;

    Console.WriteLine("Задайте размер массива ---------");
    dynamic[,] spiralArray = CreatRandom2DArray(fill: false);

    int row = 0; int col = 0; int order = 1;
    int sizeRow = spiralArray.GetLength(0);
    int sizeCol = spiralArray.GetLength(1);
    int indent = 0;                         // отступы от краев массива при заполнении спирали

    while (order <= sizeRow * sizeCol)
    {
       spiralArray[row, col] = order;
       if       (row == indent               & col < sizeCol - indent - 1)  ++col;
       else if  (col == sizeCol - indent - 1 & row < sizeRow - indent - 1)  ++row;
       else if  (row == sizeRow - indent - 1 & col > indent)                --col;
       else                                                                 --row;

       if (row == indent + 1 & col == indent & 2*indent != sizeCol - 1) ++indent; 

       ++order;
    }

    Show2dArray(spiralArray, "Спиральная матрица:");
}