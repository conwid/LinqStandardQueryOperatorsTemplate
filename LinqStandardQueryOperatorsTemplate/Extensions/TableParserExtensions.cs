using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace $safeprojectname$.Extensions
{
    // based on https://github.com/Robert-McGinley/TableParser
    public static class TableParserExtensions
{
    private static string ToStringTable<T>(this IEnumerable<T> values, string[] columnHeaders, Delegate[] valueSelectors)
    {
        var arrValues = new string[values.Count() + 1, valueSelectors.Length];

        // Fill headers
        for (int colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
        {
            arrValues[0, colIndex] = columnHeaders[colIndex];
        }

        // Fill table rows
        for (int rowIndex = 1; rowIndex < arrValues.GetLength(0); rowIndex++)
        {
            for (int colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
            {
                object value = valueSelectors[colIndex].DynamicInvoke(values.ElementAt(rowIndex - 1));

                arrValues[rowIndex, colIndex] = value != null ? value.ToString() : "null";
            }
        }

        return ToStringTable(arrValues);
    }

    private static string ToStringTable(string[,] arrValues)
    {
        int[] columnsWidth = GetMaxColumnsWidth(arrValues);
        var headerSpliter = new string('-', columnsWidth.Sum(i => i + 3) - 1);

        var sb = new StringBuilder();
        for (int rowIndex = 0; rowIndex < arrValues.GetLength(0); rowIndex++)
        {
            for (int colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
            {
                // Print cell
                string cell = arrValues[rowIndex, colIndex];
                if (cell.Length <= columnsWidth[colIndex])
                {
                    cell = cell.PadRight(columnsWidth[colIndex]);
                }
                else
                {
                    cell = (cell.Substring(0, columnsWidth[colIndex] - 3) + "...").PadRight(columnsWidth[colIndex]);
                }
                sb.Append(" | ");
                sb.Append(cell);
            }

            // Print end of line
            sb.Append(" | ");
            sb.AppendLine();

            // Print splitter
            if (rowIndex == 0)
            {
                sb.AppendFormat(" |{0}| ", headerSpliter);
                sb.AppendLine();
            }
        }

        return sb.ToString();
    }

    private static int[] GetMaxColumnsWidth(string[,] arrValues)
    {

        var columnCount = arrValues.GetLength(1);
        var rowLength = Console.WindowWidth - columnCount * 3 - 1 - 3;

        var maxColumnsWidth = new int[columnCount];

        for (int colIndex = 0; colIndex < columnCount; colIndex++)
        {
            for (int rowIndex = 0; rowIndex < arrValues.GetLength(0); rowIndex++)
            {
                int newLength = arrValues[rowIndex, colIndex].Length;
                int oldLength = maxColumnsWidth[colIndex];

                if (newLength > oldLength)
                {
                    maxColumnsWidth[colIndex] = newLength;
                }
            }
        }

        var columnsWidth = new int[columnCount];
        while (rowLength > 0 && Enumerable.Range(0, columnCount).Any(i => columnsWidth[i] < maxColumnsWidth[i]))
        {
            for (int i = 0; i < columnCount && rowLength > 0; i++)
            {
                if (columnsWidth[i] < maxColumnsWidth[i])
                {
                    columnsWidth[i]++;
                    rowLength--;
                }
            }
        }
        return columnsWidth;
    }

    public static string ToStringTable<T>(this IEnumerable<T> values)
    {
        if (typeof(T).IsAtomic())
        {
            return string.Join(Environment.NewLine, values);
        }
        var headers = typeof(T).GetProperties().Where(p => p.CanRead && (p.PropertyType.IsAtomic() || p.PropertyType.HasToString())).Select(p => p.Name).ToArray();
        var selectors = typeof(T).GetProperties()
                        .Where(p => p.CanRead && (p.PropertyType.IsAtomic() || p.PropertyType.HasToString()))
                        .Select(p => Delegate.CreateDelegate(typeof(Func<,>).MakeGenericType(new[] { typeof(T), p.PropertyType }), p.GetMethod))
                        .ToArray();

        return ToStringTable(values, headers, selectors);
    }


}
}
