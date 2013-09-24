// matrixC++.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "windows.h"
#include <iostream>
#include <iomanip>
#include <fstream>
#include <tchar.h>
#include <strsafe.h>

using namespace std;


void StartCounter();
double GetCounter();
void fillMatrix(int startPos, int endPos);




int _tmain(int argc, _TCHAR* argv [])
{


	
	StartCounter();

	int const M_SIZE = 256;

	int NUM_THREADS = 4;

	static float A[M_SIZE][M_SIZE];
	static float B[M_SIZE][M_SIZE];
	static float C[M_SIZE][M_SIZE];



	int startPos;
	int endPos;

	//start counter
	double processTime = GetCounter();

	//Main Process Here


	//write time to screen
	cout << GetCounter() << "\n";

	//write to file
	ofstream myfile("results_t.csv", ios::app);
	if (myfile.is_open()) {

		myfile << M_SIZE << "," << NUM_THREADS << "," << processTime << endl;

		myfile.close();
	}
	else  {
		cout << "Unable to open file";
	}
	return 0;
}

void fillMatrix(int startPos, int endPos) {

	int i, j, k;

	for (i = startPos; i < endPos; i++) {

		for (j = startPos; j < endPos; j++) {

			C[i][j] = 0.0;

			for (k = startPos; k < endPos; k++) {

				C[i][j] += A[i][k] * B[j][k];
			}
		}
	}
	
	
	
	return 0;
}

double PCFreq = 0.0;
__int64 CounterStart = 0;

void StartCounter() {
	LARGE_INTEGER li;
	if (!QueryPerformanceFrequency(&li))
		cout << "QueryPerformanceFrequency failed!\n";

	PCFreq = double(li.QuadPart) / 1000.0;

	QueryPerformanceCounter(&li);
	CounterStart = li.QuadPart;
}

double GetCounter() {
	LARGE_INTEGER li;
	QueryPerformanceCounter(&li);
	return double(li.QuadPart - CounterStart) / PCFreq;
}
