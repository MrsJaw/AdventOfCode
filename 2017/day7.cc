#include <iostream>
#include <vector>
#include <fstream>
#include <sstream>
#include <algorithm>
#include <map>
#include <iterator>

using namespace std;

struct programNode{
    string name;
    int weight = 0;
    vector<string> children;
};

programNode getProgramFromString(string line){
    programNode result;
    istringstream ssin(line);
    ssin >> result.name;
    char paren;
    ssin >> paren;
    ssin >> result.weight;
    ssin >> paren;
    
    string word;
    if(ssin >> word){
        while(ssin >> word){
            word.erase(remove(word.begin(), word.end(), ','), word.end());
            result.children.push_back(word);
        }
    }
    return result;
}

bool hasParent(string p, map<string, programNode>& programStack){
    bool result = false;
    map<string, programNode>::iterator it;
    for ( it = programStack.begin(); it != programStack.end() && !result; it++ )
    {
        int childCount = (it->second).children.size();
        int j = 0;
        while(!result && j < childCount){
            result = p.compare( (it->second).children[j++]) == 0;
        }
    }
    return result;
}

int getTotalWeight(programNode& p, map<string, programNode>& programStack){
    int result = p.weight;
    int childCount = p.children.size();
    if(childCount > 0 ){
        int i = 0;
        while(i < childCount){
            result += getTotalWeight(programStack[p.children[i++]], programStack);
        }
    }
    return result;
}


bool isBalanced(programNode& p, map<string, programNode>& programStack){
    bool result = true;
    int childCount = p.children.size();
    if(childCount > 0){
        map <int, vector<programNode>> childrenByWeight; 
        int totalWeight = 0;
        bool childrenBalanced = true;
        for(int i = 0; i < childCount && childrenBalanced; i++){
            childrenBalanced &= isBalanced(programStack[p.children[i]], programStack);
            totalWeight = getTotalWeight(programStack[p.children[i]], programStack);
            childrenByWeight[totalWeight].push_back(programStack[p.children[i]]);
        }
        if(childrenByWeight.size() > 1){
            result = false;
            if(childrenBalanced){
                int desiredWeight = 0;
                int wrongWeight = 0;
                programNode problemNode;
                map<int, vector<programNode>>::iterator it;
                for ( it = childrenByWeight.begin(); it != childrenByWeight.end(); it++ ){
                    if((it->second).size() == 1){
                        problemNode = (it->second)[0];
                        wrongWeight = (it->first);
                    }else{
                        desiredWeight = (it->first);
                    }
                }
                cout << "Part 2: " << problemNode.weight + (desiredWeight - wrongWeight) << endl;
            }
        }
        childrenByWeight.clear();
    }
    
    return result;
}

int main() {
    //get input
    ifstream in("Input.txt");
    string line;
    map<string, programNode> programStack;
    while(getline(in, line)){
        programNode p = getProgramFromString(line);
        programStack[p.name] = p;
    }
    
    //part 1
    bool baseFound;
    int programCount = programStack.size();
    string baseProgram;
    map<string, programNode>::iterator it;
    for ( it = programStack.begin(); it != programStack.end() && !baseFound; it++ )
    {
        baseProgram = it->first,
        baseFound = !hasParent(baseProgram, programStack);
    }
    
    cout << "Part 1: " << baseProgram << endl;
    
    //part 2
    bool balanced;
    for ( it = programStack.begin(); it != programStack.end() && balanced; it++ )
    {
        balanced = isBalanced(it->second, programStack);
    }
    
    return 0;
}   