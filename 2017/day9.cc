#include <iostream>
#include <fstream>
#include <string>
using namespace std;

int score(string stream){
    int total = 0;
    int level = 0;
    int garbageCounter = 0;
    bool inTrash = false;
    bool cancelCharacter = false;
    for (int i = 0; i < stream.length(); i++) {
        if(!inTrash){
            if(stream[i] == '{'){
                level++;
            }
            else if(stream[i] == '}'){
                total += level;
                level--;
            }
            if(!cancelCharacter && stream[i] == '<'){
                inTrash = true;
            }
        }else if(!cancelCharacter && stream[i] == '>'){
            inTrash = false;
        }
        else if(!cancelCharacter && stream[i] != '!'){
            garbageCounter++;
        }
        if(stream[i] == '!' || cancelCharacter){
                cancelCharacter = !cancelCharacter;
        }
    }
    cout << "Garbage: " << garbageCounter << endl;
    return total;
}

int main() {
     ifstream in("Input.txt");
     string stream; 
     in >> stream;
     cout << "Part 1: " << score(stream) << endl;
    
    return 0;
}
