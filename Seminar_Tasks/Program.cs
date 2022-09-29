/*
Урок 8. Как не нужно писать код. Часть 2 (семинар)
*/
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
dynamic[,] CreatRandom2DArray(bool real = false, int oneSize = 1, int twoSize = 2, bool fill = true)
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
    Console.WriteLine("");
    return newArray;
}

// Вывод содержимого 2D-массива
void Show2dArray(dynamic[,] array, string text = "")
{
    if (text != string.Empty) Console.WriteLine(text);

    for (int i = 0; i < array.GetLength(0); i++)
    {
        for (int j = 0; j < array.GetLength(1); j++)
            Console.Write($"{array[i, j]} ");
        Console.WriteLine();
    }
    Console.WriteLine();
}

/*-----------------------------------------------------------------
Задача 1. Задайте двумерный массив. Напишите программу, которая 
поменяет местами первую и последнюю строку массива 
(унифицировать до замены любых 2-х строк массива)
-------------------------------------------------------------------
*/
void ExchangeStrArray(dynamic[,] mayArray, int row1, int row2)
{
    int sizeRow = mayArray.GetLength(0);
    if (row1 > row2 |
       (row1 < 0 | row1 > sizeRow) |
       (row2 < 0 | row2 > sizeRow))
    {
        Console.WriteLine("Некорректный номер одной или обеих строк!"); return;
    }
    for (int j = 0; j < mayArray.GetLength(1); j++)
    {
        var temp = mayArray[row1, j];
        mayArray[row1, j] = mayArray[row2, j];
        mayArray[row2, j] = temp;
    }
    return;
}

// Основное тело программы.
Console.WriteLine(@"Задача-Б/Н. Меняем местами строки массива");
Console.WriteLine("---");

while (true)
{
    Console.Write("Продолжить (1- ДА, 0 - НЕТ): ");
    if (CheckExit(Convert.ToInt16(Console.ReadLine()))) break;

    Console.WriteLine(@"Задайте массив ---------");
    dynamic[,] mayArray = CreatRandom2DArray();
    Show2dArray(mayArray);

    Console.WriteLine($@"Введите последовательно номера строк (от 1 до {mayArray.GetLength(0)}), 
                         которые надо поменять местами:");
    Console.Write("1 -> "); int row1 = Convert.ToInt16(Console.ReadLine());
    Console.Write("2 -> "); int row2 = Convert.ToInt16(Console.ReadLine());

    ExchangeStrArray(mayArray, row1 - 1, row2 - 1);
    Console.WriteLine("");
    Show2dArray(mayArray, $"Результат замены строки {row1} и строки {row2} местами:");
    Console.WriteLine("");
}
// *** Конец Задачи ... ***


/*-----------------------------------------------------------------------------------------
Задача 2. Задайте двумерный массив. Напишите программу, которая заменяет строки на столбцы. 
В случае, если это невозможно, программа должна вывести сообщение для пользователя.
-------------------------------------------------------------------------------------------
*/
void TranspositionArray(dynamic[,] mayArray2)
{
    if (mayArray2.GetLength(0) != mayArray2.GetLength(1))
    {
        Console.WriteLine(@"Транспонировать могу только квадратную матрицу
                                Массив не транспонирован.
                                Задайте другой размер. Условие - (rows = columns)"); return;
    }
    for (int i = 0; i < mayArray2.GetLength(1); i++)
    {
        for (int j = i + 1; j < mayArray2.GetLength(0); j++)
        {
            var temp = mayArray2[j, i];
            mayArray2[j, i] = mayArray2[i, j];
            mayArray2[i, j] = temp;
        }
    }
    return;
}

// Основное тело программы.
Console.WriteLine(@"Задача-2. Транспонируем 2-мерный массив");
Console.WriteLine("---");

Console.WriteLine(@"Задайте массив ---------");
dynamic[,] mayArray2 = CreatRandom2DArray();
Show2dArray(mayArray2);

while (true)
{
    Console.Write("Продолжить (1- ДА, 0 - НЕТ): ");
    if (CheckExit(Convert.ToInt16(Console.ReadLine()))) break;

    TranspositionArray(mayArray2);
    Show2dArray(mayArray2, $"Результат транспонирования массива:");
    Console.WriteLine("");
}
// *** Конец Задачи 2 ***


/*-------------------------------------------------------------------
Задача 3. Из двумерного массива целых чисел удалить строку и столбец, 
на пересечении которых расположен наименьший элемент.
Вернуть новый результирующий массив.
---------------------------------------------------------------------
*/
dynamic[,] DeleteRowColArray(dynamic[,] mayArray)
{
    int sizeI = mayArray.GetLength(0);
    int sizeJ = mayArray.GetLength(1);
    dynamic[,] abbrevArray = new dynamic[Math.Max(0, sizeI - 1),
                                         Math.Max(0, sizeJ - 1)];
    int findI = 0; int findJ = 0;
    var minElement = mayArray[findI, findJ];
    for (int i = 0; i < sizeI; i++)
    {
        for (int j = 0; j < sizeJ; j++)
        {
            var currElem = Math.Min(minElement, mayArray[i, j]);
            if (currElem < minElement)
            {
                findI = i; findJ = j;
                minElement = currElem;
            }
        }
    }

    int iNew = -1;
    for (int i = 0; i < sizeI; i++)
    {
        int jNew = -1; iNew += i == findI ? 0 : 1;
        for (int j = 0; j < sizeJ; j++)
        {
            jNew += j == findJ ? 0 : 1;
            if (i != findI & j != findJ) abbrevArray[iNew, jNew] = mayArray[i, j];
        }
    }
    return abbrevArray;
}

// Основное тело программы.
// Console.WriteLine(">>>> 1"); - для отладки
Console.WriteLine(@"Задача-3. Удаление строки и колонки на пересечении наименьшего элемента массива");
Console.WriteLine("---");
Console.WriteLine(@"Задайте массив ---------");
dynamic[,] mayArray3 = CreatRandom2DArray();
Show2dArray(mayArray3);

while (true)
{
    Console.Write("Продолжить (1- ДА, 0 - НЕТ): ");
    if (CheckExit(Convert.ToInt16(Console.ReadLine()))) break;

    mayArray3 = DeleteRowColArray(mayArray3);
    Show2dArray(mayArray3,
                $"Результат удаления строки и колонки с наименьшим элементом:");
    Console.WriteLine("");
}

// *** Конец Задачи 3 ***
