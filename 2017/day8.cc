#include <iostream>
#include <fstream>
#include <vector>
#include <sstream>
#include <string>
#include <map>
#include <algorithm> 

using namespace std;

bool performOperation(int operand1, string operation, int operand2){
    bool result = true;
    if(operation.compare(">")==0){
        result = (operand1 > operand2);
    }else if(operation.compare(">=")==0){
        result = (operand1 >= operand2);
    }else if(operation.compare("<")==0){
        result = (operand1 < operand2);
    }else if(operation.compare("<=")==0){
        result = (operand1 <= operand2);
    }else if(operation.compare("==")==0){
        result = (operand1 == operand2);
    }else if(operation.compare("!=")==0){
        result = (operand1 != operand2);
    }
    return result;
}

int main() {
    ifstream in("Input.txt");
    int validPhrases = 0;
    
    string line;
    map<string, int> registry;

    string registerName;
    string incDec;
    int incDecAmt;
    string operandRegister;
    int operand1;
    int operand2;
    string operation;
    string skip;
    int maxValueEva = 0;

    while(getline(in, line)){
        //cout << line << endl;
        istringstream ssin(line);
        ssin >> registerName;
        if (!registry.count(registerName)){
            registry[registerName] = 0;
        }
        ssin >> incDec;
        ssin >> incDecAmt;
        ssin >> skip;
        ssin >> operandRegister;
        if (!registry.count(operandRegister)){
            registry[operandRegister] = 0;
        }
        operand1 = registry[operandRegister];
        ssin >> operation;
        ssin >> operand2; 
        if(performOperation(operand1, operation, operand2)){
            if(incDec == "inc"){
                registry[registerName] += incDecAmt;
            }else{
                registry[registerName] -= incDecAmt;
            }
        }
        if(registry[registerName] > maxValueEva){
            maxValueEva = registry[registerName];
        }
    }

    auto x = std::max_element(registry.begin(), registry.end(),
    [](const pair<string, int>& p1, const pair<string, int>& p2) {
        return p1.second < p2.second; });
        
    cout << "Part 1: " << x->second << endl;
    cout << "Part 2: " << maxValueEva << endl;
    
    return 0;
}   