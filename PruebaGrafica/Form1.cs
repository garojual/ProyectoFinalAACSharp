namespace PruebaGrafica;
using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        Chart chart1 = new Chart();
        // Crear datos de ejemplo
        List<int> datos = new List<int> { 5, 10, 15, 20, 25 };

        // Agregar una serie al gráfico
        chart1.Series.Add("Serie1");
        chart1.Series["Serie1"].ChartType = SeriesChartType.Column;

        // Agregar los puntos de datos a la serie
        foreach (int valor in datos)
        {
            chart1.Series["Serie1"].Points.Add(valor);
        }
    }
}
