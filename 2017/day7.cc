#include <iostream>
#include <vector>
#include <fstream>
#include <sstream>
#include <algorithm>

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

bool hasParent(string p, vector<programNode>& programStack){
    bool result = false;
    int i = 0;
    int programCount = programStack.size();
     while(!result && i < programCount)
    {
        int childCount = programStack[i].children.size();
        int j = 0;
        while(!result && j < childCount){
            result = p.compare(programStack[i].children[j++]) == 0;
        }
        i++;
    }
    return result;
}

int main() {
    //get input
    ifstream in("Input.txt");
    string line;
    vector<programNode> programStack;
    while(getline(in, line)){
        programStack.push_back(getProgramFromString(line));
    }
    
    bool baseFound;
    int programCount = programStack.size();
    string baseProgram;
    for(int i = 0; i < programCount && !baseFound; i++){
        baseProgram=programStack[i].name;
        baseFound = !hasParent(baseProgram, programStack);
    }
    
    cout << baseProgram << endl;
    
    return 0;
}   