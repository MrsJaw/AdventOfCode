#include <iostream>
#include <fstream>
#include <vector>

using namespace std;

int main() {
    ifstream in("Input.txt");

    vector<int> column;
    vector<vector<int>> row;
    int checksum = 0;
    int rowMin = -1;
    int rowMax = 0;
    int i;
    
    while( in >> i ){
        column.push_back(i);
        if(rowMin < 0 || i < rowMin)
            rowMin = i;
        if(i > rowMax)
            rowMax = i;
            
        if(in.peek() == EOF || in.peek() == '\n'){
            checksum += (rowMax - rowMin);
            rowMax = 0;
            rowMin = -1;
            row.push_back(column);
            column.clear();
        }
    }

    cout << "Part 1: " << checksum << endl;
    
    int evenlyDivisibleSum = 0;
    for(i = 0; i < row.size(); i++){
        int columnCount = row[i].size();
        for(int j = 0; j < columnCount; j++){
            int operand1 = row[i][j];
            for(int k = 0; k < columnCount; k++){
                int operand2 = row[i][k];
                if(j!=k && operand2!=0 && operand1%operand2==0)
                    evenlyDivisibleSum += (operand1/operand2);
            }
        }
    }
    
    cout << "Part 2: " << evenlyDivisibleSum << endl;
    
    return 0;
}   