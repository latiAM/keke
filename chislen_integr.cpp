#include <cmath>
#include <cstdlib>
#include <iostream>
#define PI 3.1415926535
using namespace std;

static double INTEGR(const double& x)
{
    return exp(-(x*x)/2) ;
}
 
static double Trapez(const double& left, const double& right, const double& h)
{
    double sum = 0;
    double runner;
    for(runner = left + h; runner < right; runner += h)
    {
        sum += INTEGR(runner) ;
    }
 
    sum = (sum +  0.5*(INTEGR(left) + INTEGR(right)) ) * h;
 
    return sum * 1 / sqrt(2 * PI);
}
static double Pryamoug(const double& a, const double& b){
	double f = (a + b)/2;
	double i = (b - a)*INTEGR(f);
	return i;
}
static double Chinter(const double& n){
	double sum = 0;
	double h = 1/n;
	for(int i = 1; i<=n; i++){
		sum=sum+(1/n)*((((i-1)/n)*((i-1)/n)+((i)/n)*((i)/n))/2);
		cout<<i<<": "<<sum<<endl;
	}
	return;
}
int main(int argc, char ** argv)
{
    setlocale(LC_ALL, "Russian");
 
    double a, b, n;
    double h;
	double arr[] = {4, 8 ,16, 32,64};
    /*cout << "Нижнее значение интеграла: " ;
    cin >> a;
    cout << "Верхнее значение интеграла: ";
    cin >> b;
    cout << "Шаг интегрирования: ";
    cin >> h;
 
    cout <<"Ответ по методу трапеции: " << Trapez(a, b, h) << endl; 
	cout <<"Ответ по методу прямоугольника: "<< Pryamoug(a,b)<<endl;*/
	cin>>n;
	for(int j=0;j<5;j++){
		cout<<Chinter(arr[j])<<endl;
	}
	//cout<<Chinter(n);
	system("pause");
    return 0;
}