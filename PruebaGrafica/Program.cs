// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Globalization;
using AlgortimosCSharp.AlgortimosMultiplicacion;
using AlgortimosCSharp.ControlMatrices;
using System.Text;

class Program
{

    static void Main()
    {
        //Escribir CSV si no existe
        ControlMatrices.CrearCsv();

        //Inicializar Todas las matrices

        long[][,] parMatriz4x4 = new long[2][,];
        parMatriz4x4 = ControlMatrices.LeerCsv(4);

        long[][,] parMatriz8x8 = new long[2][,];
        parMatriz8x8 = ControlMatrices.LeerCsv(8);

        long[][,] parMatriz16x16 = new long[2][,];
        parMatriz16x16 = ControlMatrices.LeerCsv(16);

        long[][,] parMatriz32x32 = new long[2][,];
        parMatriz32x32 = ControlMatrices.LeerCsv(32);

        long[][,] parMatriz64x64 = new long[2][,];
        parMatriz64x64 = ControlMatrices.LeerCsv(64);

        long[][,] parMatriz128x128 = new long[2][,];
        parMatriz128x128 = ControlMatrices.LeerCsv(128);

        long[][,] parMatriz256x256 = new long[2][,];
        parMatriz256x256 = ControlMatrices.LeerCsv(256);
        
        long[][,] parMatriz512x512 = new long[2][,];
        parMatriz512x512 = ControlMatrices.LeerCsv(512);

        //Calculos de tiempo NOA
        double[] tiemposNOA = new double[8];
        tiemposNOA[0] = CalcularTiempoNOA(parMatriz4x4);
        tiemposNOA[1] = CalcularTiempoNOA(parMatriz8x8);
        tiemposNOA[2] = CalcularTiempoNOA(parMatriz16x16);
        tiemposNOA[3] = CalcularTiempoNOA(parMatriz32x32);
        tiemposNOA[4] = CalcularTiempoNOA(parMatriz64x64);
        tiemposNOA[5] = CalcularTiempoNOA(parMatriz128x128);
        tiemposNOA[6] = CalcularTiempoNOA(parMatriz256x256);
        tiemposNOA[7] = CalcularTiempoNOA(parMatriz512x512);

        crearGrafico(tiemposNOA, "Naive On Array");

        //Calculos de tiempo NLUT
        double[] tiemposNLUT = new double[8];
        tiemposNLUT[0] = CalcularTiempoNLUT(parMatriz4x4);
        tiemposNLUT[1] = CalcularTiempoNLUT(parMatriz8x8);
        tiemposNLUT[2] = CalcularTiempoNLUT(parMatriz16x16);
        tiemposNLUT[3] = CalcularTiempoNLUT(parMatriz32x32);
        tiemposNLUT[4] = CalcularTiempoNLUT(parMatriz64x64);
        tiemposNLUT[5] = CalcularTiempoNLUT(parMatriz128x128);
        tiemposNLUT[6] = CalcularTiempoNLUT(parMatriz256x256);
        tiemposNLUT[7] = CalcularTiempoNLUT(parMatriz512x512);

        crearGrafico(tiemposNLUT, "Naiv Loop Unrolling Two");
        
        //Calculos de tiempo NLUF
        double[] tiemposNLUF = new double[8];
        tiemposNLUF[0] = CalcularTiempoNLUF(parMatriz4x4);
        tiemposNLUF[1] = CalcularTiempoNLUF(parMatriz8x8);
        tiemposNLUF[2] = CalcularTiempoNLUF(parMatriz16x16);
        tiemposNLUF[3] = CalcularTiempoNLUF(parMatriz32x32);
        tiemposNLUF[4] = CalcularTiempoNLUF(parMatriz64x64);
        tiemposNLUF[5] = CalcularTiempoNLUF(parMatriz128x128);
        tiemposNLUF[6] = CalcularTiempoNLUF(parMatriz256x256);
        tiemposNLUF[7] = CalcularTiempoNLUF(parMatriz512x512);

        crearGrafico(tiemposNLUF, "Naiv Loop Unrolling Four");
        
        //Calculos de tiempo Winograd
        double[] tiemposWinograd = new double[8];
        tiemposWinograd[0] = CalcularTiempoWinograd(parMatriz4x4);
        tiemposWinograd[1] = CalcularTiempoWinograd(parMatriz8x8);
        tiemposWinograd[2] = CalcularTiempoWinograd(parMatriz16x16);
        tiemposWinograd[3] = CalcularTiempoWinograd(parMatriz32x32);
        tiemposWinograd[4] = CalcularTiempoWinograd(parMatriz64x64);
        tiemposWinograd[5] = CalcularTiempoWinograd(parMatriz128x128);
        tiemposWinograd[6] = CalcularTiempoWinograd(parMatriz256x256);
        tiemposWinograd[7] = CalcularTiempoWinograd(parMatriz512x512);

        crearGrafico(tiemposWinograd, "Winograd");
        
        //Calculos de tiempo WinogradScaled
        double[] tiemposWinogradScaled = new double[8];
        tiemposWinogradScaled[0] = CalcularTiempoWinogradScaled(parMatriz4x4);
        tiemposWinogradScaled[1] = CalcularTiempoWinogradScaled(parMatriz8x8);
        tiemposWinogradScaled[2] = CalcularTiempoWinogradScaled(parMatriz16x16);
        tiemposWinogradScaled[3] = CalcularTiempoWinogradScaled(parMatriz32x32);
        tiemposWinogradScaled[4] = CalcularTiempoWinogradScaled(parMatriz64x64);
        tiemposWinogradScaled[5] = CalcularTiempoWinogradScaled(parMatriz128x128);
        tiemposWinogradScaled[6] = CalcularTiempoWinogradScaled(parMatriz256x256);
        tiemposWinogradScaled[7] = CalcularTiempoWinogradScaled(parMatriz512x512);

        crearGrafico(tiemposWinogradScaled, "Winograd Scaled");
        
        //Calculos de tiempo Strassen
        double[] tiemposStrassen = new double[8];
        tiemposStrassen[0] = CalcularTiempoStrassen(parMatriz4x4);
        tiemposStrassen[1] = CalcularTiempoStrassen(parMatriz8x8);
        tiemposStrassen[2] = CalcularTiempoStrassen(parMatriz16x16);
        tiemposStrassen[3] = CalcularTiempoStrassen(parMatriz32x32);
        tiemposStrassen[4] = CalcularTiempoStrassen(parMatriz64x64);
        tiemposStrassen[5] = CalcularTiempoStrassen(parMatriz128x128);
        tiemposStrassen[6] = CalcularTiempoStrassen(parMatriz256x256);
        tiemposStrassen[7] = CalcularTiempoStrassen(parMatriz512x512);

        crearGrafico(tiemposStrassen, "Strassen");
        
        //Calculos de tiempo StrassenWinograd
        double[] tiemposStrassenWinograd = new double[8];
        tiemposStrassenWinograd[0] = CalcularTiempoStrassenWinograd(parMatriz4x4);
        tiemposStrassenWinograd[1] = CalcularTiempoStrassenWinograd(parMatriz8x8);
        tiemposStrassenWinograd[2] = CalcularTiempoStrassenWinograd(parMatriz16x16);
        tiemposStrassenWinograd[3] = CalcularTiempoStrassenWinograd(parMatriz32x32);
        tiemposStrassenWinograd[4] = CalcularTiempoStrassenWinograd(parMatriz64x64);
        tiemposStrassenWinograd[5] = CalcularTiempoStrassenWinograd(parMatriz128x128);
        tiemposStrassenWinograd[6] = CalcularTiempoStrassenWinograd(parMatriz256x256);
        tiemposStrassenWinograd[7] = CalcularTiempoStrassenWinograd(parMatriz512x512);

        crearGrafico(tiemposStrassenWinograd, "Strassen Winograd");
        
        //Calculos de tiempo StrassenSequentialBlock
        double[] tiemposSequentialBlock = new double[8];
        tiemposSequentialBlock[0] = CalcularTiempoSequentialBlock(parMatriz4x4);
        tiemposSequentialBlock[1] = CalcularTiempoSequentialBlock(parMatriz8x8);
        tiemposSequentialBlock[2] = CalcularTiempoSequentialBlock(parMatriz16x16);
        tiemposSequentialBlock[3] = CalcularTiempoSequentialBlock(parMatriz32x32);
        tiemposSequentialBlock[4] = CalcularTiempoSequentialBlock(parMatriz64x64);
        tiemposSequentialBlock[5] = CalcularTiempoSequentialBlock(parMatriz128x128);
        tiemposSequentialBlock[6] = CalcularTiempoSequentialBlock(parMatriz256x256);
        tiemposSequentialBlock[7] = CalcularTiempoSequentialBlock(parMatriz512x512);

        crearGrafico(tiemposSequentialBlock, "Sequential Block");
        
        //Calculos de tiempo ParallelBlock
        double[] tiemposParallelBlock = new double[8];
        tiemposParallelBlock[0] = CalcularTiempoParallelBlock(parMatriz4x4);
        tiemposParallelBlock[1] = CalcularTiempoParallelBlock(parMatriz8x8);
        tiemposParallelBlock[2] = CalcularTiempoParallelBlock(parMatriz16x16);
        tiemposParallelBlock[3] = CalcularTiempoParallelBlock(parMatriz32x32);
        tiemposParallelBlock[4] = CalcularTiempoParallelBlock(parMatriz64x64);
        tiemposParallelBlock[5] = CalcularTiempoParallelBlock(parMatriz128x128);
        tiemposParallelBlock[6] = CalcularTiempoParallelBlock(parMatriz256x256);
        tiemposParallelBlock[7] = CalcularTiempoParallelBlock(parMatriz512x512);

        crearGrafico(tiemposParallelBlock, "Parallel Block");
        
        //Calculos de tiempo EnhancedParallelBlock
        double[] tiemposEnhancedParallelBlockk = new double[8];
        tiemposEnhancedParallelBlockk[0] = CalcularTiempoEnhancedParallelBlock(parMatriz4x4);
        tiemposEnhancedParallelBlockk[1] = CalcularTiempoEnhancedParallelBlock(parMatriz8x8);
        tiemposEnhancedParallelBlockk[2] = CalcularTiempoEnhancedParallelBlock(parMatriz16x16);
        tiemposEnhancedParallelBlockk[3] = CalcularTiempoEnhancedParallelBlock(parMatriz32x32);
        tiemposEnhancedParallelBlockk[4] = CalcularTiempoEnhancedParallelBlock(parMatriz64x64);
        tiemposEnhancedParallelBlockk[5] = CalcularTiempoEnhancedParallelBlock(parMatriz128x128);
        tiemposEnhancedParallelBlockk[6] = CalcularTiempoEnhancedParallelBlock(parMatriz256x256);
        tiemposEnhancedParallelBlockk[7] = CalcularTiempoEnhancedParallelBlock(parMatriz512x512);
        

        crearGrafico(tiemposEnhancedParallelBlockk, "Enhanced Parallel Block");
        
        probarAlgoritmo();

         String csvFilePath = "C:/Users/Juan Alejandro/RiderProjects/PruebaGrafica/PruebaGrafica/CSV/tiempos.csv";
        
         using (StreamWriter writer = new StreamWriter(csvFilePath, false, Encoding.UTF8))
         {
             
        
             String[] encabezados =
             {
                 "Matrix Size", "Naive Multiplication", "Naive Loop Unrolling (Two)", "Naive Loop Unrolling (Four)",
                 "Winograd Original", "Winograd Scaled", "Strassen Naive", "Strassen Winograd",
                 "Sequential Block", "Paralel Block", "Enhanced Paralel Block"
             };
             String[] tamanios = { "4", "8", "16", "32", "64", "128", "256", "512" };
        
             //Escribir encabezados
             for (int i = 0; i < encabezados.Length; i++)
             {
                 if (i < encabezados.Length - 1)
                 {
                     writer.Write(encabezados[i]);
                     writer.Write(",");
                 }
                 else
                 {
                     writer.Write(encabezados[i]);
                 }
             }
             writer.WriteLine();
        
             for (int i = 0; i < 8; i++)
             {
                 String aux = "";
        
                 aux = tamanios[i];
                 aux = aux + "," + tiemposNOA[i].ToString(new CultureInfo("en-US"));
                 aux = aux + "," + tiemposNLUT[i].ToString(new CultureInfo("en-US"));
                 aux = aux + "," + tiemposNLUF[i].ToString(new CultureInfo("en-US"));
                 aux = aux + "," + tiemposWinograd[i].ToString(new CultureInfo("en-US"));
                 aux = aux + "," + tiemposWinogradScaled[i].ToString(new CultureInfo("en-US"));
                 aux = aux + "," + tiemposStrassen[i].ToString(new CultureInfo("en-US"));
                 aux = aux + "," + tiemposStrassenWinograd[i].ToString(new CultureInfo("en-US"));
                 aux = aux + "," + tiemposSequentialBlock[i].ToString(new CultureInfo("en-US"));
                 aux = aux + "," + tiemposParallelBlock[i].ToString(new CultureInfo("en-US"));
                 aux = aux + "," + tiemposEnhancedParallelBlockk[i].ToString(new CultureInfo("en-US"));
        
                 Console.WriteLine("Fila" + i + ": " + aux);
                 writer.WriteLine(aux);
             }
        }
    }
    
    static void probarAlgoritmo()
    {
        long[,] matrizA =
        {
            { 631695,670551,943171,441250 },
            { 684521,796351,423165,224769 },
            { 947229,518382,109465,208186 },
            { 656793,717228,801115,856306 }
            
        };
        
        long[,] matrizB =
        {
            { 625590,159654,270516,430215 },
            { 407346,763703,790275,275822 },
            { 104109,738570,267931,998803 },
            { 847281,891874,747564,764478 }
        };
        /* Resultado esperado
         * {1140383673585, 1703091651853, 1283370660346, 1736085324160},
         * {987117674910,  1230464764643, 1095918403692, 1108631754614},
         * {991526158233,  813642032926,  850866361033,  818980871542},
         * {1511978175279, 2008005367900, 1599267454537, 1935172625524}
)
         *
         */ 
         long[,] resultado = AlgoritmosMultiplicacion.MultiplicacionEnhancedParallelBlock(matrizA, matrizB,4);
        //long[,] resultado = AlgoritmosMultiplicacion.MultiplicacionParallelBlock(matrizA, matrizB);
        ControlMatrices.PrlongMatrix(resultado, 4);
    }

    static double CalcularTiempoNOA(long[][,] parMatriz)
    {
        Stopwatch stopWatch = new Stopwatch(); 
        
        stopWatch.Start();
        long[,] res = AlgoritmosMultiplicacion.MultiplicacionNOA(parMatriz[0], parMatriz[1], parMatriz[0].GetLength(0));
        stopWatch.Stop();
        
        //Console.WriteLine($"Tiempo: {stopWatch.Elapsed.TotalMilliseconds} ms");
        
        return stopWatch.Elapsed.TotalMilliseconds;
    }
    
    static double CalcularTiempoNLUT(long[][,] parMatriz)
    {
        Stopwatch stopWatch = new Stopwatch(); 
        
        stopWatch.Start();
        long[,] res = AlgoritmosMultiplicacion.MultiplicacionNLUT(parMatriz[0], parMatriz[1], parMatriz[0].GetLength(0));
        stopWatch.Stop();
        
       //Console.WriteLine($"Tiempo: {stopWatch.Elapsed.TotalMilliseconds} ms");
        
        return stopWatch.Elapsed.TotalMilliseconds;
    }
    
    static double CalcularTiempoNLUF(long[][,] parMatriz)
    {
        Stopwatch stopWatch = new Stopwatch(); 
        
        stopWatch.Start();
        long[,] res = AlgoritmosMultiplicacion.MultiplicacionNLUF(parMatriz[0], parMatriz[1], parMatriz[0].GetLength(0));
        stopWatch.Stop();
        
        //Console.WriteLine($"Tiempo: {stopWatch.Elapsed.TotalMilliseconds} ms");
        
        return stopWatch.Elapsed.TotalMilliseconds;
    }
    
    static double CalcularTiempoWinograd(long[][,] parMatriz)
    {
        Stopwatch stopWatch = new Stopwatch(); 
        
        stopWatch.Start();
        long[,] res = AlgoritmosMultiplicacion.MultiplicacionWinograd(parMatriz[0], parMatriz[1], parMatriz[0].GetLength(0));
        stopWatch.Stop();
        
        //Console.WriteLine($"Tiempo: {stopWatch.Elapsed.TotalMilliseconds} ms");
        
        return stopWatch.Elapsed.TotalMilliseconds;
    }
    
    static double CalcularTiempoWinogradScaled(long[][,] parMatriz)
    {
        Stopwatch stopWatch = new Stopwatch(); 
        
        stopWatch.Start();
        long[,] res = AlgoritmosMultiplicacion.MultiplicacionWinogradScaled(parMatriz[0], parMatriz[1], parMatriz[0].GetLength(0));
        stopWatch.Stop();
        
        //Console.WriteLine($"Tiempo: {stopWatch.Elapsed.TotalMilliseconds} ms");
        
        return stopWatch.Elapsed.TotalMilliseconds;
    }
    
    static double CalcularTiempoStrassen(long[][,] parMatriz)
    {
        Stopwatch stopWatch = new Stopwatch(); 
        
        stopWatch.Start();
        long[,] res = AlgoritmosMultiplicacion.MultiplicacionStrassen(parMatriz[0], parMatriz[1]);
        stopWatch.Stop();
        
        //Console.WriteLine($"Tiempo: {stopWatch.Elapsed.TotalMilliseconds} ms");
        
        return stopWatch.Elapsed.TotalMilliseconds;
    }
    
    static double CalcularTiempoStrassenWinograd(long[][,] parMatriz)
    {
        Stopwatch stopWatch = new Stopwatch(); 
        
        stopWatch.Start();
        long[,] res = AlgoritmosMultiplicacion.MultiplicaionStrassenWinograd(parMatriz[0], parMatriz[1]);
        stopWatch.Stop();
        
        //Console.WriteLine($"Tiempo: {stopWatch.Elapsed.TotalMilliseconds} ms");
        
        return stopWatch.Elapsed.TotalMilliseconds;
    }
    
    static double CalcularTiempoSequentialBlock(long[][,] parMatriz)
    {
        Stopwatch stopWatch = new Stopwatch(); 
        
        stopWatch.Start();
        long[,] res = AlgoritmosMultiplicacion.MultiplicacionSequentialBlock(parMatriz[0], parMatriz[1], parMatriz[0].GetLength(0));
        stopWatch.Stop();
        
        //Console.WriteLine($"Tiempo: {stopWatch.Elapsed.TotalMilliseconds} ms");
        
        return stopWatch.Elapsed.TotalMilliseconds;
    }
    
    static double CalcularTiempoParallelBlock(long[][,] parMatriz)
    {
        Stopwatch stopWatch = new Stopwatch(); 
        
        stopWatch.Start();
        long[,] res = AlgoritmosMultiplicacion.MultiplicacionParallelBlock(parMatriz[0], parMatriz[1]);
        stopWatch.Stop();
        
        //Console.WriteLine($"Tiempo: {stopWatch.Elapsed.TotalMilliseconds} ms");
        
        return stopWatch.Elapsed.TotalMilliseconds;
    }
    
    static double CalcularTiempoEnhancedParallelBlock(long[][,] parMatriz)
    {
        Stopwatch stopWatch = new Stopwatch(); 
        
        stopWatch.Start();
        long[,] res = AlgoritmosMultiplicacion.MultiplicacionEnhancedParallelBlock(parMatriz[0], parMatriz[1], parMatriz[0].GetLength(0));
        stopWatch.Stop();
        
        //Console.WriteLine($"Tiempo: {stopWatch.Elapsed.TotalMilliseconds} ms");
        
        return stopWatch.Elapsed.TotalMilliseconds;
    }
    
    static void crearGrafico(double[] tiemposNOA, string nombreAlgoritmo)
    {
         // Crear una imagen 
        int width = 1280;
        int height = 720;
        Bitmap bitmap = new Bitmap(width, height);

        // Crear un objeto Graphics para dibujar en la imagen
        using (Graphics graphics = Graphics.FromImage(bitmap))
        {
            // Rellenar el fondo con blanco
            graphics.Clear(Color.White);

            // Crear un objeto de fuente para los textos
            Font font = new Font("Arial", 12);
            Font titleFont = new Font("Arial", 16, FontStyle.Bold);  // Fuente para el título
            Brush brush = Brushes.Black;

            // Definir los datos para el gráfico de barras (con valores double)
            string[] categories = { "4x4", "8x8", "16x16", "32x32", "64x64", "128x128", "256x256",  "512x512"};
            double[] values = tiemposNOA;

            // Definir el ancho y la separación de las barras
            int barWidth = 100;
            int spacing = 50;
            int startX = 50;  // posición inicial en X
            int startY = height - 100;  // posición inicial en Y (cerca del borde inferior)
            
            // Encontrar el valor máximo para escalar las barras
            double maxValue = 0;
            foreach (var value in values)
            {
                if (value > maxValue)
                    maxValue = value;
            }
            
            // Definir un factor de escala basado en el máximo valor
            double scaleFactor = (height - 150) / maxValue;  // 150 para dejar espacio para los ejes
            
            // Dibujar el título
            string title = "Tiempos de ejecucion: " + nombreAlgoritmo;  // Título del gráfico
            SizeF titleSize = graphics.MeasureString(title, titleFont);  // Medir el tamaño del título
            float titleX = (width - titleSize.Width) / 2;  // Centrar el título horizontalmente
            float titleY = 20;  // Ubicación vertical del título

            graphics.DrawString(title, titleFont, Brushes.Black, titleX, titleY);  // Dibujar el título

            
            // Dibujar las barras
            for (int i = 0; i < categories.Length; i++)
            {
                // Posición X de cada barra
                int x = startX + i * (barWidth + spacing);
                // Posición Y de cada barra (dibuja de abajo hacia arriba)
                double barHeight = values[i] * scaleFactor;
                int y = startY - (int)barHeight;

                // Dibujar la barra
                graphics.FillRectangle(Brushes.Blue, x, y, barWidth, (int)barHeight);

                // Dibujar el texto encima de la barra (para la categoría y el valor)
                graphics.DrawString(categories[i], font, brush, x + barWidth / 2 - font.Size * 2, startY + 10);
                graphics.DrawString(values[i].ToString("0.000" + " ms"), font, brush, (x + barWidth / 2 - font.Size * 2) - 15, y - font.Size * 2);
            }

            // Dibujar los ejes (X, Y)
            graphics.DrawLine(Pens.Black, startX - 10, startY, startX + categories.Length * (barWidth + spacing), startY); // Eje X
            graphics.DrawLine(Pens.Black, startX - 10, startY, startX - 10, startY - 350); // Eje Y
        }

        // Guardar la imagen como un archivo PNG
        bitmap.Save("C:/Users/Juan Alejandro/RiderProjects/PruebaGrafica/PruebaGrafica/Imgs/grafico_barras"+nombreAlgoritmo+".png", System.Drawing.Imaging.ImageFormat.Png);
        Console.WriteLine("Gráfico de barras guardado como 'grafico_barras.png'");

        // Liberar recursos
        bitmap.Dispose();

    }

    static void escribir_Tiempos(StreamWriter writer)
    {
        
    }
}