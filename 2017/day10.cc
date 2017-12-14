#include <iostream>
#include <fstream>
#include <numeric>  
#include <vector>    
#include <functional>
#include <iomanip>

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
    cout << "Part 2: "; 
     
    char lenChar;
    rev.clear();
    vector<int> movements;
    pos = 0;
    skip = 0;
    ifstream in2("Input.txt"); 
    iota (numbers,numbers+size,0);    
    while(in2.get(lenChar)){
        if(lenChar != '\n' && lenChar != EOF && lenChar != '\0'){
            len = lenChar;
            movements.push_back(len);
        }
    }
    movements.push_back(17);
    movements.push_back(31);
    movements.push_back(73);
    movements.push_back(47);
    movements.push_back(23);
    
    for(int l = 0; l < 64; l++){
        for(int k = 0; k <movements.size(); k++){
            len = movements[k];
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
        }
    }
    
    int denseHash[16];
    for(int i = 0; i < 16; i++){
        denseHash[i] = accumulate (numbers+(i*16), numbers+((i+1)*16), 0, std::bit_xor<int>());
        cout << hex << setfill('0') << setw(2) << denseHash[i];
    }
    cout <<  endl;
    
    return 0;
}
