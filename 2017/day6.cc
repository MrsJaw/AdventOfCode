#include <iostream>
#include <vector>
#include <fstream>

using namespace std;

void Redistribute(vector<int> *banks){
    int size = banks->size();
    int maxNum = 0;
    int startRedisIndex = 0;
    for(int i = 0; i < size; i++){
        if((*banks)[i] > maxNum){
            maxNum = (*banks)[i];
            startRedisIndex = i;
        }
    }
    
    (*banks)[startRedisIndex++] = 0;
    while(maxNum > 0){
        (*banks)[(startRedisIndex++)%size]++;
        maxNum--;
    }
    
}

bool VectorsEqual(vector<int> *comparand1, vector<int> *comparand2){
    bool result = false;
    int size = comparand1->size();
    if(size == comparand2->size()){
        int i = 0;
        while(i < size && (*comparand1)[i] == (*comparand2)[i]){
            i++;
        }
        result = (i==size);
    }
    return result;
}

bool PatternExists(vector<int> *testvector, vector<vector<int>> *patterns){
    bool matchFound = false;
    int i = 0;
    int patternCount = patterns->size();
    
    while(!matchFound && i < patternCount){
        matchFound = VectorsEqual(testvector, &((*patterns)[i++]));
    }
    
    if(!matchFound){
        vector<int> newPattern; 
        int size = testvector -> size();
        for(int i = 0; i < size; i++){
            newPattern.push_back((*testvector)[i]);
        }
        patterns->push_back(newPattern);
    }
    else{
        cout << "Part 2: " << patternCount - (--i) << endl;
    }
    
    return matchFound;
}

int main() {
    //get input
    ifstream in("Input.txt");
    vector<int>banks;
    int i = 0;
    while( in >> i ){
        banks.push_back(i);
    }
    
    //part one
    vector<vector<int>> usedPatterns;
    usedPatterns.push_back(banks);
    int redisCount = 0;
    do{
        Redistribute(&banks);
        redisCount++;
    }while(!PatternExists(&banks, &usedPatterns));
    cout << "Part 1: " << redisCount << endl;
    
    return 0;
}   