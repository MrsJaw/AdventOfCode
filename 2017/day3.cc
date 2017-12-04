#include <iostream>
#include <cmath>
using namespace std;

int part1(int input){
    int result = 0;
    
    if(input > 1){
        int sideLength = 1;
        int level = 1;
        int maxNumberOnLevel = 1;
        while(maxNumberOnLevel < input){
            level++;
            sideLength += 2;
            maxNumberOnLevel = pow(sideLength, 2);
        }
        
        int stepsToNextSide = sideLength-1;
        int closestCardinal = maxNumberOnLevel - (stepsToNextSide/2);
        int sideMin = maxNumberOnLevel - stepsToNextSide;
        int sideMax = maxNumberOnLevel;
        bool found = false;
        
        do{
          if(input > sideMin && input <= sideMax){
              found = true;
          }  
          else{
              closestCardinal-=stepsToNextSide;
              sideMin -= stepsToNextSide;
              sideMax -= stepsToNextSide;
          }
        }while(!found);
        
        result = abs(closestCardinal - input) + (level-1);
    }
    
    return result;
}

int main() {
    int input;
    cout << "Puzzle Input: " << endl;
    cin >> input;
    cout << "Part 1: " << part1(input) << endl;
    return 0;
}
