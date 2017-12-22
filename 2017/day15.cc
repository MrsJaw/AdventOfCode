#include <iostream>
#include <sstream>
#include <bitset>

using namespace std;

bool judge(int a, int b){
    string binA, binB;
    stringstream bin;
    
    bin << (bitset<32>) a;
    binA = bin.str();
    
    bin.str("");
    
    bin <<(bitset<32>) b;
    binB = bin.str();
    
    return (binA.substr(16, 16).compare(binB.substr(16,16)) == 0);
}

int main() {
    
    //get input
    const int fA = 16807;
    const int fB = 48271;
    long a = 703;
    long b = 516;
    const int c = 2147483647;
    int matches = 0;
    
    /*
    for(int i = 0; i < 40000000; i++){
        a = (a*fA)%c;
        b = (b*fB)%c;
        if(judge(a, b)){
            matches++;
        }
    }
    
    cout << "Part 1: " << matches << endl;*/
    
    int i = 0;
    int needA = true;
    int needB = true;
    while(i < 5000000){
        if(needA){
         a = (a*fA)%c;
         needA = (a%4!=0);
        }
        if(needB){
            b = (b*fB)%c;
            needB = (b%8!=0);
        }
        if(!needA && !needB){
            i++;
            if(judge(a, b)){
                matches++;
            }
            needA = true;
            needB = true;
        }
    }
    
    cout << "Part 2: " << matches << endl;
    
    return 0;
}
