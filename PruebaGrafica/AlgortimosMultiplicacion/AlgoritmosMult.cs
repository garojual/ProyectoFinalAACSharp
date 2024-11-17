namespace AlgortimosCSharp.AlgortimosMultiplicacion;

using System;

public class AlgoritmosMultiplicacion
{
    //Multiplicacion de dos matrices n * n usando NaivOnArray
    public static long[,] MultiplicacionNOA(long[,] matrizA, long[,] matrizB, int n)
    {
        //Resultado es igual una matriz de N*N
        long[,] resultado = new long[n, n];

        for (long i = 0; i < n; i++) //Iteracion de filas en matrizA
        {
            for (long j = 0; j < n; j++) //Iteracion en columnas matrizB
            {
                resultado[i, j] = 0;
                for (long k = 0; k < n; k++) //Columnas de A y filas de B
                {
                    resultado[i,j] += matrizA[i, k] * matrizB[k, j];
                }
            }
        }
        return resultado;
    }
    
    // Función para multiplicar dos matrices n*n usando NaivLoopUnrollingTwo
    public static long[,] MultiplicacionNLUT(long[,] A, long[,] B, int n)
    {
        long[,] C = new long[n,n];
        // Inicializa la matriz C en ceros
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                C[i, j] = 0;
            }
        }

        // Multiplicación de matrices con Naiv Loop Unrolling Four
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j += 2) // Desenrollado en bloques de 2
            {
                long sum1 = 0, sum2 = 0;

                // Sumar los productos de A y B para las columnas y filas correspondientes
                for (int k = 0; k < n; k++)
                {
                    sum1 += A[i, k] * B[k, j]; // Primer elemento de la columna j
                    sum2 += A[i, k] * B[k, j + 1]; // Segundo elemento de la columna j+1 (si existe)
                }

                // Asignar los resultados a la matriz C
                C[i, j] = sum1;
                if (j + 1 < n)
                    C[i, j + 1] = sum2;
            }
        }
        return C;
    }
    
    public static long[,] MultiplicacionNLUF(long[,] A, long[,] B, int n)
    {
        long[,] C = new long[n, n];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                long sum = 0;

                // Unroll the loop to process 4 elements at a time
                for (int k = 0; k < n; k += 4)
                {
                    sum += A[i, k] * B[k, j];
                    sum += A[i, k + 1] * B[k + 1, j];
                    sum += A[i, k + 2] * B[k + 2, j];
                    sum += A[i, k + 3] * B[k + 3, j];
                }

                C[i, j] = sum;
            }
        }
        return C;
    }
    
    public static long[,] MultiplicacionWinograd(long[,] A, long[,] B, int n)
    {
        long[,] C = new long[n, n];
        // Arrays auxiliares para las sumas y productos intermedios
        long[] rowFactor = new long[n];
        long[] colFactor = new long[n];

        // Paso 1: Calcular los factores para las filas de A
        for (int i = 0; i < n; i++)
        {
            rowFactor[i] = 0;
            for (int j = 0; j < n - 1; j += 2)
            {
                rowFactor[i] += A[i, j] * A[i, j + 1];
            }
        }

        // Paso 2: Calcular los factores para las columnas de B
        for (int j = 0; j < n; j++)
        {
            colFactor[j] = 0;
            for (int i = 0; i < n - 1; i += 2)
            {
                colFactor[j] += B[i, j] * B[i + 1, j];
            }
        }

        // Paso 3: Multiplicación de matrices
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                C[i, j] = -rowFactor[i] - colFactor[j];

                for (int k = 0; k < n - 1; k += 2)
                {
                    C[i, j] += (A[i, k] + B[k + 1, j]) * (A[i, k + 1] + B[k, j]);
                }

                if (n % 2 == 1)
                {
                    C[i, j] += A[i, n - 1] * B[n - 1, j];
                }
            }
        }

        return C;
    }

    public static long[,] MultiplicacionWinogradScaled(long[,] A, long[,] B, int n)
    {

        long[,] result = new long[n, n];

        // Arrays intermedios
        long[] rowFactors = new long[n]; // Factores para las filas de A
        long[] colFactors = new long[n]; // Factores para las columnas de B

        // Precalcular los factores para las filas de A
        for (int i = 0; i < n; i++)
        {
            rowFactors[i] = 0;
            for (int k = 0; k < n - 1; k += 2)
            {
                rowFactors[i] += A[i, k] * A[i, k + 1];
            }
        }

        // Precalcular los factores para las columnas de B
        for (int j = 0; j < n; j++)
        {
            colFactors[j] = 0;
            for (int k = 0; k < n - 1; k += 2)
            {
                colFactors[j] += B[k, j] * B[k + 1, j];
            }
        }

        // Realizar la multiplicación
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                result[i, j] = -rowFactors[i] - colFactors[j];

                for (int k = 0; k < n - 1; k += 2)
                {
                    result[i, j] += (A[i, k] + B[k + 1, j]) * (A[i, k + 1] + B[k, j]);
                }
            }
        }
        return result;
    }
    
    public static long[,] MultiplicacionStrassen(long[,] A, long[,] B)
    {
        int n = A.GetLength(0);
        if (n == 1)
        {
            // Caso base: multiplicación de matrices 1x1
            long[,] result = new long[1, 1];
            result[0, 0] = A[0, 0] * B[0, 0];
            return result;
        }

        // A y B se dividen en 4 submatrices
        int mid = n / 2;

        long[,] A11 = new long[mid, mid];
        long[,] A12 = new long[mid, mid];
        long[,] A21 = new long[mid, mid];
        long[,] A22 = new long[mid, mid];
        
        long[,] B11 = new long[mid, mid];
        long[,] B12 = new long[mid, mid];
        long[,] B21 = new long[mid, mid];
        long[,] B22 = new long[mid, mid];

        // Rellenar las submatrices A11, A12, A21, A22
        for (int i = 0; i < mid; i++)
        {
            for (int j = 0; j < mid; j++)
            {
                A11[i, j] = A[i, j];
                A12[i, j] = A[i, j + mid];
                A21[i, j] = A[i + mid, j];
                A22[i, j] = A[i + mid, j + mid];

                B11[i, j] = B[i, j];
                B12[i, j] = B[i, j + mid];
                B21[i, j] = B[i + mid, j];
                B22[i, j] = B[i + mid, j + mid];
            }
        }

        // Calcular los 7 productos de Strassen
        long[,] P1 = MultiplicacionStrassen(Add(A11, A22), Add(B11, B22));
        long[,] P2 = MultiplicacionStrassen(Add(A21, A22), B11);
        long[,] P3 = MultiplicacionStrassen(A11, Subtract(B12, B22));
        long[,] P4 = MultiplicacionStrassen(A22, Subtract(B21, B11));
        long[,] P5 = MultiplicacionStrassen(Add(A11, A12), B22);
        long[,] P6 = MultiplicacionStrassen(Subtract(A21, A11), Add(B11, B12));
        long[,] P7 = MultiplicacionStrassen(Subtract(A12, A22), Add(B21, B22));

        // Calcular las submatrices C11, C12, C21, C22
        long[,] C11 = Add(Subtract(Add(P1, P4), P5), P7);
        long[,] C12 = Add(P3, P5);
        long[,] C21 = Add(P2, P4);
        long[,] C22 = Add(Subtract(Add(P1, P3), P2), P6);

        // Unir las submatrices en la matriz final C
        long[,] C = new long[n, n];
        for (int i = 0; i < mid; i++)
        {
            for (int j = 0; j < mid; j++)
            {
                C[i, j] = C11[i, j];
                C[i, j + mid] = C12[i, j];
                C[i + mid, j] = C21[i, j];
                C[i + mid, j + mid] = C22[i, j];
            }
        }

        return C;
    }

    // Métodos auxiliares para la suma y resta de matrices (tipo long)
    public static long[,] Add(long[,] A, long[,] B)
    {
        int n = A.GetLength(0);
        long[,] result = new long[n, n];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                result[i, j] = A[i, j] + B[i, j];
            }
        }
        return result;
    }

    public static long[,] Subtract(long[,] A, long[,] B)
    {
        int n = A.GetLength(0);
        long[,] result = new long[n, n];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                result[i, j] = A[i, j] - B[i, j];
            }
        }
        return result;
    }
    
    public static long[,] MultiplicaionStrassenWinograd(long[,] A, long[,] B)
    {
        int n = A.GetLength(0);

        // Caso base: matrices de tamaño 1x1
        if (n == 1)
        {
            return new long[,] { { A[0, 0] * B[0, 0] } };
        }

        // Dividir las matrices en submatrices
        int halfSize = n / 2;
        long[,] A11 = SubMatrix(A, 0, 0, halfSize);
        long[,] A12 = SubMatrix(A, 0, halfSize, halfSize);
        long[,] A21 = SubMatrix(A, halfSize, 0, halfSize);
        long[,] A22 = SubMatrix(A, halfSize, halfSize, halfSize);
        long[,] B11 = SubMatrix(B, 0, 0, halfSize);
        long[,] B12 = SubMatrix(B, 0, halfSize, halfSize);
        long[,] B21 = SubMatrix(B, halfSize, 0, halfSize);
        long[,] B22 = SubMatrix(B, halfSize, halfSize, halfSize);

        // Calcular los productos de Strassen-Winograd
        long[,] P1 = MultiplicaionStrassenWinograd(Add(A11, A22), Add(B11, B22));       // P1 = (A11 + A22)(B11 + B22)
        long[,] P2 = MultiplicaionStrassenWinograd(Add(A21, A22), B11);                // P2 = (A21 + A22)B11
        long[,] P3 = MultiplicaionStrassenWinograd(A11, Subtract(B12, B22));           // P3 = A11(B12 - B22)
        long[,] P4 = MultiplicaionStrassenWinograd(A22, Subtract(B21, B11));           // P4 = A22(B21 - B11)
        long[,] P5 = MultiplicaionStrassenWinograd(Add(A11, A12), B22);                // P5 = (A11 + A12)B22
        long[,] P6 = MultiplicaionStrassenWinograd(Subtract(A21, A11), Add(B11, B12)); // P6 = (A21 - A11)(B11 + B12)
        long[,] P7 = MultiplicaionStrassenWinograd(Subtract(A12, A22), Add(B21, B22)); // P7 = (A12 - A22)(B21 + B22)

        // Calcular los elementos de la matriz resultante
        long[,] C11 = AddSW(Subtract(Add(P1, P4), P5), P7); // C11 = P1 + P4 - P5 + P7
        long[,] C12 = AddSW(P3, P5);                       // C12 = P3 + P5
        long[,] C21 = AddSW(P2, P4);                       // C21 = P2 + P4
        long[,] C22 = AddSW(SubtractSW(Add(P1, P3), P2), P6); // C22 = P1 + P3 - P2 + P6

        // Combinar submatrices en la matriz resultante
        return Combine(C11, C12, C21, C22);
    }

    private static long[,] SubMatrix(long[,] matrix, int rowStart, int colStart, int size)
    {
        long[,] result = new long[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                result[i, j] = matrix[rowStart + i, colStart + j];
            }
        }
        return result;
    }

    private static long[,] AddSW(long[,] A, long[,] B)
    {
        int n = A.GetLength(0);
        long[,] result = new long[n, n];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                result[i, j] = A[i, j] + B[i, j];
            }
        }
        return result;
    }

    private static long[,] SubtractSW(long[,] A, long[,] B)
    {
        int n = A.GetLength(0);
        long[,] result = new long[n, n];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                result[i, j] = A[i, j] - B[i, j];
            }
        }
        return result;
    }

    private static long[,] Combine(long[,] C11, long[,] C12, long[,] C21, long[,] C22)
    {
        int halfSize = C11.GetLength(0);
        int n = halfSize * 2;
        long[,] result = new long[n, n];

        for (int i = 0; i < halfSize; i++)
        {
            for (int j = 0; j < halfSize; j++)
            {
                result[i, j] = C11[i, j];
                result[i, j + halfSize] = C12[i, j];
                result[i + halfSize, j] = C21[i, j];
                result[i + halfSize, j + halfSize] = C22[i, j];
            }
        }
        return result;
    }
    
    public static long[,] MultiplicacionSequentialBlock(long[,] A, long[,] B, int n)
    {
        long[,] C = new long[n, n];
        int blockSize = (int)Math.Sqrt(n); // Tamaño de cada bloque

        for (int ii = 0; ii < n; ii += blockSize)
        {
            for (int jj = 0; jj < n; jj += blockSize)
            {
                for (int kk = 0; kk < n; kk += blockSize)
                {
                    // Multiplicar los bloques (submatrices)
                    for (int i = ii; i < Math.Min(ii + blockSize, n); i++)
                    {
                        for (int j = jj; j < Math.Min(jj + blockSize, n); j++)
                        {
                            for (int k = kk; k < Math.Min(kk + blockSize, n); k++)
                            {
                                C[i, j] += A[i, k] * B[k, j];
                            }
                        }
                    }
                }
            }
        }
        return C;
    }
    
    public static long[,] MultiplicacionParallelBlock(long[,] A, long[,] B)
    {
        int n = A.GetLength(0); // Asumimos que A y B son cuadradas y de tamaño n x n
        if (n % 2 != 0 || n == 0) throw new ArgumentException("El tamaño de la matriz debe ser potencia de 2.");

        long[,] C = new long[n, n];

        ParallelBlockMultiply(A, B, C, 0, 0, 0, 0, 0, 0, n);
        return C;
    }

    private static void ParallelBlockMultiply(long[,] A, long[,] B, long[,] C, 
        int rowA, int colA, int rowB, int colB, int rowC, int colC, int size)
    {
        if (size == 1)
        {
            C[rowC, colC] += A[rowA, colA] * B[rowB, colB];
            return;
        }

        int newSize = size / 2;

        Parallel.Invoke(
            () => ParallelBlockMultiply(A, B, C, rowA, colA, rowB, colB, rowC, colC, newSize),
            () => ParallelBlockMultiply(A, B, C, rowA, colA, rowB, colB + newSize, rowC, colC + newSize, newSize),
            () => ParallelBlockMultiply(A, B, C, rowA, colA + newSize, rowB + newSize, colB, rowC, colC, newSize),
            () => ParallelBlockMultiply(A, B, C, rowA, colA + newSize, rowB + newSize, colB + newSize, rowC, colC + newSize, newSize),
            () => ParallelBlockMultiply(A, B, C, rowA + newSize, colA, rowB, colB, rowC + newSize, colC, newSize),
            () => ParallelBlockMultiply(A, B, C, rowA + newSize, colA, rowB, colB + newSize, rowC + newSize, colC + newSize, newSize),
            () => ParallelBlockMultiply(A, B, C, rowA + newSize, colA + newSize, rowB + newSize, colB, rowC + newSize, colC, newSize),
            () => ParallelBlockMultiply(A, B, C, rowA + newSize, colA + newSize, rowB + newSize, colB + newSize, rowC + newSize, colC + newSize, newSize)
        );
    }
    
    public static long[,] MultiplicacionEnhancedParallelBlock(long[,] A, long[,] B, int n)
    {
        long[,] C = new long[n, n];
        int blockSize = n / 2; // Tamaño del bloque

        Parallel.For(0, 2, i =>
        {
            for (int j = 0; j < 2; j++)
            {
                for (int k = 0; k < 2; k++)
                {
                    MultiplyBlock(A, B, C, i, j, k, blockSize);
                }
            }
        });
        return C;
    }

    static void MultiplyBlock(long[,] A, long[,] B, long[,] C, int blockRow, int blockCol, int commonBlock, int blockSize)
    {
        for (int i = 0; i < blockSize; i++)
        {
            for (int j = 0; j < blockSize; j++)
            {
                for (int k = 0; k < blockSize; k++)
                {
                    C[blockRow * blockSize + i, blockCol * blockSize + j] += 
                        A[blockRow * blockSize + i, commonBlock * blockSize + k] *
                        B[commonBlock * blockSize + k, blockCol * blockSize + j];
                }
            }
        }
    }

    
}