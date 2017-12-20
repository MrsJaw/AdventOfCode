#include <iostream>
#include <fstream>
#include <map>    
#include <iterator>

using namespace std;

struct layer{
  int depth = 0;
  int range = 0;
  int scannerPos = 0;
  int inc = 1;
};

void scan(layer &l){
    int nextMove = l.scannerPos + l.inc;
    if(nextMove < 0 || nextMove > l.range-1){
        l.inc*=-1;
    }
    l.scannerPos += l.inc;
}

int main() {
    ifstream in("Input.txt");
    char colon = ':';
    map<int, layer> firewall;
    
    while(in){
        layer newLayer;
        in >> newLayer.depth >> colon >> newLayer.range;
        if(!firewall.count(newLayer.depth)){
            firewall[newLayer.depth] = newLayer;
        }
    }
    
    int maxLayer = firewall.rbegin()->first;
    map<int, layer>::iterator it;
    
    //part 1
    int danger = 0;
    for(int i = 0; i <= maxLayer; i++){
        if(firewall.count(i)&&firewall[i].scannerPos == 0){
            danger += (i*firewall[i].range);
        }
        for ( it = firewall.begin(); it != firewall.end(); it++ ){
            scan(it->second);
        }
    }
    cout << "Part 1: " << danger << endl;

    //part 2
    int ps = 0;
    int pos = 0;
    int delay =0;
    bool caught = false;
    
    do{
        
        caught = firewall[pos].range > 0 && (ps % ((firewall[pos].range-1)*2)) == 0;
        if(!caught){
            ps++;
            pos++;
        }else{
            pos = 0;
            caught = false;
            ps = ++delay;
        }
    }while(pos <= maxLayer);
    
    cout << "Part 2: " << delay << endl;

    return 0;
}
