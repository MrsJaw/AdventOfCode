#include <iostream>
#include <cmath>
#include <map>
using namespace std;

struct Point{
    int x, y; 

    bool operator==(const Point &o) const {
        return x == o.x && y == o.y;
    }

    bool operator<(const Point &o)  const {
        return x < o.x || (x == o.x && y < o.y);
    }
};

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

void setNextCorner(Point &nxtCorner){
    if(nxtCorner.y < 0){
        if( nxtCorner.x > 0){
            nxtCorner.y = ++nxtCorner.x;    
        }else{
            nxtCorner.x *= -1;
        }
    }else{
        if(nxtCorner.x > 0){
            nxtCorner.x *= -1;
        }else{
            nxtCorner.y *= -1;
        }
    }
}

int part2(int input){
    map<Point, int> grid;
    Point nxtCorner;
    nxtCorner.x = 1;
    nxtCorner.y = 1;
    Point pos;
    pos.x = 0;
    pos.y = 0;
    int value = 1;
    grid[pos] = value;

    do{
        value = grid[pos];
        if(pos == nxtCorner){
            setNextCorner(nxtCorner);
        }
        Point temp = {pos.x, pos.y};
        Point temp2 = {0,0};
        Point temp3 = {0,0};
        if(nxtCorner.x > 0 && pos.x < nxtCorner.x){
            temp.y++;
            pos.x++;
            temp2 = {pos.x, temp.y};
            temp3 = {pos.x+1, pos.y+1};
        }else if(nxtCorner.y >0 && pos.y < nxtCorner.y){
            temp.x--;
            pos.y++;
            temp2 = {temp.x, pos.y};
            temp3 = {pos.x-1, pos.y+1};
        }else if(nxtCorner.x < 0 && pos.x > nxtCorner.x){
            temp.y--;
            pos.x --;
            temp2 = {pos.x, temp.y};
            temp3 = {pos.x-1, pos.y-1};
        }else if(nxtCorner.y < 0 && pos.y > nxtCorner.y){
            temp.x++;
            pos.y--;
            temp2 = {temp.x, pos.y};
            temp3 = {pos.x+1, pos.y-1};
        }
        if(grid.count(temp)){
            value += grid[temp];
        }
        if(grid.count(temp2)){
            value += grid[temp2];
        }
        if(grid.count(temp3)){
            value += grid[temp3];
        }
        grid[pos] = value;
    }while(value <= input);
    
    return value; 
}

int main() {
    int input;
    cout << "Puzzle Input: " << endl;
    cin >> input;
    cout << "Part 1: " << part1(input) << endl;
    cout << "Part 2: " << part2(input) << endl;
    return 0;
}
