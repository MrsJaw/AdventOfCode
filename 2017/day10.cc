#include <iostream>
#include <fstream>
#include <numeric>  
#include <vector>

using namespace std;

int main() {
    int size = 256;
    int numbers[size];
    iota (numbers,numbers+size,0);
    int pos = 0;
    int skip = 0;
    
     ifstream in("Input.txt");
     int len; 
     char comma = ',';
     vector<int> rev; 
     
     while(in){
         in >> len;
         
         if(len >= 0 && len <= size){
             for(int i = 0; i < len; i++){
                 rev.push_back(numbers[(pos+i)%size]);
             }
             
             int j = pos;
             for(int i = rev.size() - 1; i >= 0; i--){
                 numbers[(j++)%size] = rev[i];
             }
             
             rev.clear();
             pos += len+skip++;
         }
         
         in >> comma;
     }
     
     cout << "Part 1: " << numbers[0] * numbers[1] << endl;
    
    return 0;
}
