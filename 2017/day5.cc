#include <iostream>
#include <fstream>
#include <vector>

using namespace std;

int main() {
    ifstream in("Input.txt");

    vector<int> instructions;
    int i;
    int moveCount = 0;
    
    while( in >> i ){
        instructions.push_back(i);
    }

    if(instructions.size() > 0){
        i = 0;
        int move = 0;
        do{
            i += instructions[i]++;
            moveCount++;
        }while(i < instructions.size());
    }
    
    cout << "Part 1: " << moveCount << endl;
    return 0;
}   