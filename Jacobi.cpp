#include <stdio.h>
#include <iostream>
#include "math.h"
#include <fstream>
#include <omp.h>
#include <cstdlib> 
using namespace std;

void PrintMatrix(int N, double **A)
{
    for (int i = 0; i < N; i++)
    {
        for (int j = 0; j < N; j++)
            cout << A[i][j] << " ";
        cout << endl;
    }
}
void PrintVec(int N, double *A)
{
    for (int i = 0; i < N; i++)
    {
        cout << A[i] << " ";
    }
    cout << endl;
}
void Print(int N, double *A)
{
    for (int i = 0; i < N; i++)
    {
        cout <<"x"<<i+1<<"="<< A[i] << " ";
    }
    cout << endl;
}
void Jacobi(int N, double**A, double* F, double* X)
{
    //Dx^(n+1)=b-(L+V)x^n - запись метода Якоби
    double e = 0.001;
    
    double norm, *TempX = new double[N];
 
    for (int k = 0; k < N; k++)
        TempX[k] = X[k];
    int count = 0;
    do {
        for (int i = 0; i < N; i++)
        {
            TempX[i] = F[i];
            for (int g = 0; g < N; g++)
                if (i != g)
                    TempX[i] -= A[i][g] * X[g];
            TempX[i] /= A[i][i];
        }
        norm = abs(X[0] - TempX[0]);
        for (int h = 0; h < N; h++)
        {
        //Метод Якоби сходится, если есть диагональное преобладание
            if (abs(X[h] - TempX[h]) > norm)
                norm = abs(X[h] - TempX[h]);
            X[h] = TempX[h];
        }
        count++;
    } while (norm > e);
    cout << "Количество итераций = " << count << endl;
    delete[] TempX;
    
    /*
    n - размерность матрицы
    A[n][n] - Матрица
    F[n] - вектор свободных членов
    norm - наибольшая разница между столбцами иксов соседних итераций
    X[n] - начальное приближение (записывается ответ)
    TempX - массив, куда записывается начальное приближение и с ним происходят вычисления
    */
}
void input(int &N, double **&A, double *&F)
{
	N = 3;
    F = new double[N];
    A = new double *[N];
    for (int i = 0; i < N; i++)
        A[i] = new double[N];
    //массив
    /*
	A[0][0]=7; A[0][1]=2; A[0][2]=3;
    A[1][0]=1; A[1][1]=5; A[1][2]=-1;
    A[2][0]=-4; A[2][1]=0; A[2][2]=8;
    //вектор
    F[0]=1; F[1]=0; F[2]=-1;
	*/
    
    //пример из интернета
    A[0][0]=10; A[0][1]=1; A[0][2]=-1;
    A[1][0]=1; A[1][1]=10; A[1][2]=-1;
    A[2][0]=-1; A[2][1]=1; A[2][2]=10;
    F[0]=11; F[1]=10; F[2]=10;
    //Ответ x1 = 1.102,  x2 = 0.991,  x3 = 1.101
}
 
int main()
{
    double **Matrix, *b, *y, *x;
    int n;
    input(n, Matrix, b);
    cout << "Матрица:\n";
    PrintMatrix(n, Matrix);
    cout << "b: ";
    PrintVec(n, b);
    //начальное приближение
    x = new double[n];
    for (int i = 0; i < n; i++)
        x[i] = 1.0;
    y = new double[n];
    Jacobi(n, Matrix, b, x);
    cout << "Результат: ";
    Print(n, x);
    return 1;
}