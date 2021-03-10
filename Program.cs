using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoXOR
{
    class Program
    {
        static void Main(string[] args)
        {
            //Generador de número aleatorios
            Random azar = new Random();

            //Matriz de enseñanza compuerta XOR
            double[,] matrizXOR = { { 1, 1, 0 }, { 1, 0, 1 }, { 0, 1, 1}, { 0, 0, 0} };

            //Se declaran los pesos, factor de aprendizaje y variables
            double p1X1;
            double p2X1;
            double p1X2;
            double p2X2;
            double p1Y1;
            double p1Y2;
            double umbral1;
            double umbral2;
            double umbral3;
            double fa = 0.5;
            double errorDelta1;
            double errorDelta2;
            double errorDelta3;
            double y1;
            double y2;
            double y3;
            int fila = 0;
            int iteracion = 0;

            //Ciclo que recorrerá las cuatro filas respectivas de la tabla de verdad de XOR
            while(fila <= 3)
            {
                y1 = 0; y2 = 0; y3 = 0; errorDelta1 = 0; errorDelta2 = 0; errorDelta3 = 0; iteracion = 0;
                //Se le asignan valores aleatorios a los pesos
                p1X1 = azar.NextDouble();
                p2X1 = azar.NextDouble();
                p1X2 = azar.NextDouble();
                p2X2 = azar.NextDouble();
                p1Y1 = azar.NextDouble();
                p1Y2 = azar.NextDouble();
                umbral1 = azar.NextDouble();
                umbral2 = azar.NextDouble();
                umbral3 = azar.NextDouble();

                //Ciclo para controlar las iteraciones de entrenamiento
                while(iteracion <= 1000)
                {
                    //Se calcula la salida de las neuronas de la capa oculta
                    y1 = (matrizXOR[fila, 0] * p1X1) + (matrizXOR[fila, 1] * p1X2) + (1 * umbral1);
                    y2 = (matrizXOR[fila, 0] * p2X1) + (matrizXOR[fila, 1] * p2X2) + (1 * umbral2);

                    //Se aplica la función de activación sigmoide
                    y1 = 1.0 / (1 + Math.Pow(Math.E, (-1) * y1));
                    y2 = 1.0 / (1 + Math.Pow(Math.E, (-1) * y2));

                    //Se calcula el resultado de la neurona de salida
                    y3 = (y1 * p1Y1) + (y2 * p1Y2) + (1 * umbral3);
                    y3 = 1.0 / (1 + Math.Pow(Math.E, (-1) * y3));

                    //(Backpropagation)
                    //Se calcula el error de la neurona de salida
                    errorDelta3 = (y3 * (1 - y3)) * (matrizXOR[fila, 2] - y3);

                    //Ajusta los pesos de la neurona de salida
                    p1Y1 = p1Y1 + (fa * errorDelta3 * y1);
                    p1Y2 = p1Y2 + (fa * errorDelta3 * y2);
                    umbral3 = umbral3 + (errorDelta3);

                    //Calcula el error de las neuronas de la capa oculta
                    errorDelta1 = (y1 * (1 - y1)) * errorDelta3 - p1Y1;
                    errorDelta2 = (y2 * (1 - y2)) * errorDelta3 - p1Y2;

                    //Ajusta los pesos de las neuronas de las capas ocultas
                    p1X1 = p1X1 + (fa * errorDelta1 * matrizXOR[fila, 0]);
                    p1X2 = p1X2 + (fa * errorDelta1 * matrizXOR[fila, 1]);
                    umbral1 = umbral1 + errorDelta1;

                    p2X1 = p2X1 + (fa * errorDelta2 * matrizXOR[fila, 0]);
                    p2X2 = p2X2 + (fa * errorDelta2 * matrizXOR[fila, 1]);
                    umbral2 = umbral2 + errorDelta2;

                    iteracion++;
                }
                //Imprime cada fila de la matriz que almacena la tabla de verdad de XOR
                Console.WriteLine((int)matrizXOR[fila, 0] + " XOR " + (int)matrizXOR[fila, 1] + " = " + (int)matrizXOR[fila, 2] + " Calculado: " + y3);
                fila++;
            }
            Console.ReadKey();
        }
    }
}