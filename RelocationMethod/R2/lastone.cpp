#include <stdio.h>
#include <iostream>
#include <fstream>
#include <time.h>
using namespace std;

ifstream in("matrix.txt");

int n=2;
double *matrix=new double [n*n],*matrix1=new double [n*n],*f=new double [n],*x_pr=new double [n],*c=new double [n*n], *d=new double [n],*x_next=new double [n];
double om=1.5;

void print_x(double *x);
void print_matrix(double *m);

//generiruem sluchajnuyu matricu razmera, zadannogo parametrom n
void generate_matrix(int n, double *m){
//	for(int i=0;i<n*n;i++) matrix[i]=rand()%100;
	m[0] = 1;
	m[1] = 1;
	m[2] = 1;
	m[3] = 3;
	/*m[4] = 1;
	m[5] = 4;
	m[6] = 3;
	m[7] = 4;
	m[8] = 1;
	/*matrix[9] = 2;
	matrix[10] = 1;
	matrix[11] = 1;
	matrix[12] = 4;
	matrix[13] = -2;
	matrix[14] = 1;
	matrix[15] = 1;*/


}

//matrica m1 dubliruetsya v m2
void dubl_matrix(double *m1,double *m2){
	for(int i=0;i<n*n;i++) m2[i]=m1[i];
}

//proveryaet yavlyaetsya li nulevym element (i,j) matricy m
bool is_null(int i,int j, double *m){
	return (m[j*n+i]==0);
}

///proveryaet yavlyaetsya li nulevoj stroka i-aya matricy m
bool is_null_line(int i, double *m){
	for (int k=0;k<n;k++){
		if (m[i*n+k]!=0) return false;
	}
	return true;
}

//proveryaet yavlyaetsya li nulevym stolbec i  matricy m
bool is_null_col(int i, double *m){
	for (int k=0;k<n;k++){
		if (m[i*k+i]!=0) return false;
	}
	return true;
}

//menyaet mestami iuyu i juyu stroki
void swap (int i, int j, double *m){
		double *tmp=new double [n];		
		
		for (int k=0;k<n;k++){
				tmp[k]=m[i*n+k];
				m[i*n+k]=m[j*n+k];
				m[j*n+k]=tmp[k];
		}
	
}
//ischet stroku s nomerom >k v kotoroj iyj element ne raven nulyu
int search_not_null(int i, double *m){
	for (int k=i+1;k<n;k++) {
		if(m[k*n+i]!=0) return k;
	}
	return 0;
}


// s pomosh'yu metoda Gaussa priveryaet a ne vyrozdena li matrica
bool is_degen(double *ma){
	double koef;
	bool fl=false;
	int t;
	int k;
	for (k=0;k<n;k++) {
		if (is_null(k,k,ma)){
			t=search_not_null(k,ma);
			if ((t==0)&&(k==0)) return true;	
			if (t==0){ 
				
				for (int l=0;l<k;l++){
					if (!is_null(k,l,ma)) break;
					if (l==k-1) return true;
				}
				continue;
			}
			if (t!=0){
				swap(t,k,ma);
			
			}
		}
		
		for (int m=k+1;m<n;m++){
			fl=false;
			for (int p=0;p<n;p++){
				if (fl) break;
				if ((ma[m*n+k]==0)&&(p==0)){ fl=true; break;}
				else{  
					if (p==0) koef=ma[m*n+k]/ma[k*n+k];
					ma[m*n+p]=ma[m*n+p]-ma[k*n+p]*koef;
				}
			}
		}
	}
	for (k=0;k<n;k++){
		if (is_null_line(k,ma)) return true;
	}
	for (k=0;k<n;k++){
		if (is_null_col(k,ma)) return true;
	}
	return false;
}

// generiruet pravuyu chast sistemy
void generate_f(int n, double *f){
	//for(int i=0;i<n;i++){ f[i]=rand()%100;}
	f[0] = 3; f[1] = 7;// f[2] = 8; /*f[0] = 7;*/
	//f[1] = 5;
}

//inicializiruet nachalnoe priblizhenie
void init_x0(int n, double *x0){
	for(int k=0;k<n;k++) x0[k]=0;
}

// n - число уравнений
// x_pr - предыдущее приближение, массив из n элементов
// x_next - текущее приближение, массив из n элементов
// matrix - матрица A
// d - вектор f
void next_iteration_z(double *x_pr, double *x_next, int n, double *matrix, double *d){
	int i,j;
	double s1,s2;
	for(i=0;i<n;i++){
		s1=0;
		s2=0;
		for (j=0;j<i;j++){
			c[n*i+j]=-matrix[n*i+j]/matrix[n*i+i];
			s1=s1+c[n*i+j]*x_next[j];
		}
		for (j=i+1;j<n;j++){
			c[n*i+j]=-matrix[n*i+j]/matrix[n*i+i];
			s2=s2+c[n*i+j]*x_pr[j];
		}
		d[i]=f[i]/matrix[n*i+i];
		x_next[i]=s1+s2+d[i];
	}	
}

// n - число уравнений
// x_pr - предыдущее приближение, массив из n элементов
// x_next - текущее приближение, массив из n элементов
// matrix - матрица A
// d - вектор f
// om - параметр ω
void next_iteration_r(double *x_pr, double *x_next, int n, double *matrix, double *d, double om){
	int i,j;
	double s1,s2;
	for(i=0;i<n;i++){
		s1=0;
		s2=0;
		for (j=0;j<i;j++){
			c[n*i+j]=-matrix[n*i+j]*om/matrix[n*i+i];
			s1=s1+c[n*i+j]*x_next[j];
		}
		for (j=i+1;j<n;j++){
			c[n*i+j]=-matrix[n*i+j]*om/matrix[n*i+i];
			s2=s2+c[n*i+j]*x_pr[j];
		}
		d[i]=f[i]*om/matrix[n*i+i];
		x_next[i]=s1+s2+d[i]-x_pr[i]*(om-1);
	}	
}

//vybor metoda dlya scheta sleduyuschego priblizheniya
void next_iteration(int param, double *m){
	switch (param){
	case 1:next_iteration_z(x_pr,x_next,n,m,f);break;
	case 2:next_iteration_r(x_pr,x_next,n,m,f,om);break;
	}

	//if (param==1) next_iteration_z(x_pr); else next_iteration_it(x_pr);
}


void print_x(double *x){
	for(int i=0;i<n;i++) cout <<x[i]<<',';
	cout<<"\n";
}

void print_matrix(double *m){
	for (int i=0;i<n;i++){
		for(int j=0;j<n;j++){
			cout <<m[i*n+j]<<' ';
		}
		cout<< "\n";
	}
	cout<<"\n";
}

//ocenivaet rasstoyanie mezhdu x1 и x2, true esli rasstoyanie <eps
bool less_than_eps(double eps, double *x1, double *x2){
//	print_x(x1);
//	print_x(x2);
	for (int i=0;i<n;i++){
		if ((x1[i]-x2[i])*(x1[i]-x2[i])>=eps) return false;
	}
	return true;

}

/*    1)schitaet reshenie metodom sootvetstvuyuschim parametru
2) Schitaet chislo iteracij, vozvraschaet ih chislo? neobhodimoe dlya polucheniya resultata nuzhnoj tochnosti
3) esli chislo iteracij>100 ostanavlivaem poisk resheniya -> metod ne soshelsya , vozvraschaet -1  */
int result(double eps, int param, double *m){
	int iter=0;
	next_iteration(param,m);
//	print_x(x_next);
	while (!less_than_eps(eps,x_pr,x_next)){
		for (int i=0;i<n;i++)	x_pr[i]=x_next[i];
		next_iteration(param,m);
		iter++;
		if (iter>=100) return -1;
	}
	
	return iter;
}


int main(){
	int k,param=2;
	double eps=0.0001;
	srand( (unsigned)time( NULL ) );
	init_x0(n,x_pr);
	generate_matrix(n,matrix);
	print_matrix(matrix);
	dubl_matrix(matrix,matrix1);
	if (is_degen(matrix1)){ cout << "Matrica vyrozhdena!!!\n";print_matrix(matrix1);cout <<"\n";  cin>> k;return 0;}
	print_matrix(matrix1);
	cout<<"\n f:\n";
	generate_f(n,f);
	print_x(f);
	k=result(eps,param,matrix);
	if (k==-1) cout <<" Metod ne shoditsya";
	
	cout<<"\nSolution in "<<k<<" iteration: \n";
	print_x(x_next);
	cout<<"\n\n";
	cin>> k;
	return 0;
}


