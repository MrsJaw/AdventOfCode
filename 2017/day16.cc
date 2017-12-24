#include <iostream>
#include <fstream>  
#include <string>   

using namespace std;

string spin(int x, string programs){
    string back = programs.substr(programs.length()-x, x);
    string front = programs.substr(0, programs.length()-x);
    programs = back + front;
    return programs;
}

string exchange(int a, int b, string programs){
    char temp = programs[a];
    programs[a] = programs[b];
    programs[b] = temp;
    return programs;
}

string partner(char a, char b, string programs){
    int posA, posB;
    size_t found = programs.find(a);
    posA = (int)found;
    found = programs.find(b);
    posB = (int)found;
    return exchange(posA, posB, programs);
}

int main() {
    string programs = "abcdefghijklmnop";

    ifstream in("Input.txt");
    char move, a, b, punctuation;
    int posA, posB;
    
    while(in){
        in >> move;
        
        switch(move){
            case 's':
                in >> posA;
                programs = spin(posA, programs);
                break;
            case 'x':
                in >> posA >> punctuation >> posB;
                programs = exchange(posA, posB, programs);
                break;
            case 'p':
                in >> a >> punctuation >> b;
                programs = partner(a, b, programs);
                break;
        }
        
        in >> punctuation;
    }
    
    cout << "Part 1: " << programs << endl;
    
    
    return 0;
}
