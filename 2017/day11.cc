#include <iostream>
#include <fstream>
#include <cmath>
#include <string>
using namespace std;

int distance(float &n, float &w){
    if(abs(n) > (abs(w))/2){
        return abs(n) + (abs(w))/2;
    }else{
        return abs(w);
    }
}

int move(string &direction, float &n, float &w){
    if(direction.compare("n")==0){
        n++;
    }
    else if(direction.compare("s")==0){
        n--;
    }
    else if(direction.compare("nw")==0){
        n+=0.5;
        w++;
    }
    else if(direction.compare("ne")==0){
        n+=0.5;
        w--;
    }else if(direction.compare("sw")==0){
        n-=0.5;
        w++;
    }else if(direction.compare("se")==0){
        n-=0.5;
        w--;
    }
    return distance(n,w);
}

int main() {
    float n = 0;
    float w = 0;
    int max = 0;
    int d =0;
    
    ifstream in("Input.txt");
    string direction;
    char i; 
    char comma = ',';
    
    while(in.get(i)){
        if(i == comma){
            d = move(direction, n, w);
            if(d > max){
                max = d;
            }
            direction = "";
        }
        else{
            direction += i;
        }
    }
    
    d = move(direction, n, w);
    if(d > max){
        max = d;
    }
    cout << "Part 1: " << d  << endl;
    cout << "Part 2: " << max  << endl;
    
    return 0;
}
