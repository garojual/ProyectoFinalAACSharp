using System.Net.NetworkInformation;

namespace AlgortimosCSharp.ControlMatrices;

using System;
using System.IO;
using System.Text;
public class ControlMatrices
{
     public static void CrearCsv()
        {
            // Ruta del archivo CSV
            string csvFilePath = "C:/Users/Juan Alejandro/RiderProjects/PruebaGrafica/PruebaGrafica/CSV/matrices.csv";

            // Verificar si el archivo ya existe
            if (File.Exists(csvFilePath))
            {
                Console.WriteLine($"El archivo {csvFilePath} ya existe. No se realizarán cambios.");
                return;
            }

            // Lista para almacenar los tamaños de las matrices
            List<long> matrixSizes = new List<long> { 4, 8, 16, 32, 64, 128, 256, 512};

            // Generador de números aleatorios
            Random random = new Random();

            // Crear el archivo CSV
            using (StreamWriter writer = new StreamWriter(csvFilePath, false, Encoding.UTF8))
            {
                // Iterar sobre los pares de matrices
                for (int i = 0; i < 8; i++)
                {
                    long size1 = matrixSizes[i];
                    long size2 = matrixSizes[i];

                    // Generar y escribir la primera matriz
                    WriteMatrixToCsv(writer, GenerateMatrix(size1, random));

                    // Generar y escribir la segunda matriz
                    WriteMatrixToCsv(writer, GenerateMatrix(size2, random));
                }
            }

            Console.WriteLine($"Archivo CSV creado correctamente: {csvFilePath}");
        }

        // Función para generar una matriz cuadrada con números aleatorios
        static long[,] GenerateMatrix(long size, Random random)
        {
            long[,] matrix = new long[size, size];
            for (long i = 0; i < size; i++)
            {
                for (long j = 0; j < size; j++)
                {
                    // Generar número aleatorio de al menos 6 dígitos
                    matrix[i, j] = random.Next(100000, 1000000);
                }
            }
            return matrix;
        }

        // Función para escribir una matriz en el archivo CSV
        static void WriteMatrixToCsv(StreamWriter writer, long[,] matrix)
        {
            //writer.WriteLine(matrix.GetLength(0));
            for (long i = 0; i < matrix.GetLength(0); i++)
            {
                for (long j = 0; j < matrix.GetLength(1); j++)
                {
                    writer.Write(matrix[i, j]);
                    if (j < matrix.GetLength(1) - 1)
                    {
                        writer.Write(",");
                    }
                }
                writer.WriteLine();
            }
            writer.WriteLine();
        } 
        
        static long[,] ReadMatrixFromCsv(StreamReader reader)
        {
            List<long> values = new List<long>();
            string line;
            while ((line = reader.ReadLine()) != string.Empty)
            {
                values.AddRange(line.Split(',').Select(long.Parse));
            }

            long size = (long)Math.Sqrt(values.Count);
            long[,] matrix = new long[size, size];
            int index = 0;
            for (long i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = values[index++];
                }
            }
            return matrix;
        }

        static bool TryFindMatricesOfSize(string csvFilePath, long n, out long[,] matrix1, out long[,] matrix2)
        {
            matrix1 = null;
            matrix2 = null;

            using (StreamReader reader = new StreamReader(csvFilePath))
            {
                while (!reader.EndOfStream)
                {
                    long[,] currentMatrix = ReadMatrixFromCsv(reader);
                    long size = currentMatrix.GetLength(0);

                    if (size == n)
                    {
                        matrix1 = currentMatrix;
                        matrix2 = ReadMatrixFromCsv(reader);
                        return true;
                    }
                }
            }

            return false;
        }

        public static long[][,] LeerCsv(long desiredSize)
        {
            long[][,] paresMatrices = new long[2][,];
            string csvFilePath = "C:/Users/Juan Alejandro/RiderProjects/PruebaGrafica/PruebaGrafica/CSV/matrices.csv";

            if (TryFindMatricesOfSize(csvFilePath, desiredSize, out long[,] matrixA, out long[,] matrixB))
            {
                
                Console.WriteLine("Matriz A {0}x{0} creada", desiredSize);
                Console.WriteLine("Matriz B {0}x{0} creada", desiredSize);
                
                paresMatrices[0] = matrixA;
                paresMatrices[1] = matrixB;
                
                return paresMatrices;
            }
            else
            {
                Console.WriteLine("No se encontraron matrices del tamaño especificado.");
                return null;
            }
        }
        
    // Método para imprimir una matriz
    public static void PrlongMatrix(long[,] matrix, long n)
    {
        for (long i = 0; i < n; i++)
        {
            for (long j = 0; j < n; j++)
            {
                Console.Write(matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }
}