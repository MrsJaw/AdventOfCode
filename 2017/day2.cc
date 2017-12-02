#include <iostream>
#include <fstream>
using namespace std;

int main() {
    ifstream in("Input.txt");

    int checksum = 0;
    int rowMin = -1;
    int rowMax = 0;
    int i;
    
    while( in >> i ){
        if(rowMin < 0 || i < rowMin)
            rowMin = i;
        if(i > rowMax)
            rowMax = i;
            
        if(in.peek() == EOF || in.peek() == '\n'){
            checksum += (rowMax - rowMin);
            rowMax = 0;
            rowMin = -1;
        }
    }

    cout << checksum << endl;
    return 0;
}   