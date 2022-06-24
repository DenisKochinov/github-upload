using System;
using System.IO;
class Zeidel
{
    float[,] Matrix;
    float[] Answer;
    double eps = 1e-5;

    public Zeidel(string Path)
    {
        string[] s = File.ReadAllLines(Path);
        Matrix = new float[s.Length, s.Length + 1];
        Answer = new float[s.Length];
        for (int i = 0; i < Matrix.GetLength(0); i++)
        {
            string[] str = s[i].Trim().Split(' ');
            for (int j = 0; j <= Matrix.GetLength(0); j++)
            {
                Matrix[i, j] = float.Parse(str[j].Trim());
                if (i == j) Matrix[i, j] = 5 - Matrix[i, j];
                if (j == Matrix.GetLength(0) || j == i)
                    Matrix[i, j] /= 5;
                else
                    Matrix[i, j] /= -5;
            }
        }
        for (int i = 0; i < Matrix.GetLength(0); i++)
        {
            for (int j = 0; j < Matrix.GetLength(1); j++)
                Console.Write(Matrix[i, j] + " ");
            Console.WriteLine();
        }
        Console.WriteLine();

    }

    public void Solution()
    {
        bool Flag_Norm = true;
        float Norm = 0;
        for (int i = 0; i < Matrix.GetLength(0); i++)
        {
            Norm = 0;
            for (int j = 0; j < Matrix.GetLength(0); j++)
                Norm += Math.Abs(Matrix[i, j]);
            if (Norm >= 1)
            {
                Flag_Norm = false;
                break;
            }
        }
        if (Flag_Norm)
        {
            Console.WriteLine("Кубическая норма матрицы = " + Norm + ". Метод сходится");
            Console.WriteLine();
        }
        else
        {
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                Norm = 0;
                for (int j = 0; j < Matrix.GetLength(0); j++)
                    Norm += Math.Abs(Matrix[j, i]);
                if (Norm >= 1)
                {
                    Console.WriteLine("Метод не сходится, т.к. октаэдрическая и кубические нормы больше 1");
                   
                    Console.ReadLine();
                    Environment.Exit(1);
                }
            }
            Console.WriteLine("Октаэдрическая норма матрицы = " + Norm + ". Метод сходится.");
           
            Console.WriteLine();
        }
        for (int i = 0; i < Matrix.GetLength(0); i++)
            Answer[i] = Matrix[i, Matrix.GetLength(1) - 1];
        bool End_Flag;
        float x_k;
        int k = 0;
        do
        {
            k++;
            End_Flag = true;
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                x_k = 0;
                for (int j = 0; j < Matrix.GetLength(0); j++)
                    x_k += Matrix[i, j] * Answer[j];
                x_k += Matrix[i, Matrix.GetLength(1) - 1];
                float c = (x_k - Answer[i]) / x_k;
                Answer[i] = x_k;
                if (Math.Abs(c) > eps) End_Flag = false;

            }

        } while (!End_Flag);
        for (int i = 0; i < Matrix.GetLength(0); i++)
            Console.WriteLine("x" + (i + 1) + " = " + Answer[i]);
    }
}
namespace LAB_CM
{
    class Program
    {
        static void Main(string[] args)
        {
            Zeidel One = new Zeidel("Matrix.txt");
            One.Solution();
        }


    }
}