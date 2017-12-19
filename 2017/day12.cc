#include <iostream>
#include <fstream>
#include <vector>    
#include <map>
#include <sstream>
#include <iterator>
#include <algorithm>

using namespace std;

bool connectedToZero(int pipe, map<int, vector<int>> &pipes, vector<int> &checkedPipes){
    bool result = false;
        
    if(find(checkedPipes.begin(), checkedPipes.end(), pipe) == checkedPipes.end()){
        checkedPipes.push_back(pipe);
    }
    
    if(pipe == 0){
        result = true;
    }
    else if(pipes[pipe].size()  > 0){
        for(int i = 0; i < pipes[pipe].size() && !result; i++){
            if(pipes[pipe][i]!= pipe && find(checkedPipes.begin(), checkedPipes.end(), pipes[pipe][i]) == checkedPipes.end()){
                result = connectedToZero(pipes[pipe][i], pipes, checkedPipes);
            }
        }
    }

    return result;
}

void updatePipeGroup(vector<int> &updateGroup, vector<int> &checkedGroup){
    for(int i = 0; i < checkedGroup.size(); i++){
        if(find(updateGroup.begin(), updateGroup.end(), checkedGroup[i])== updateGroup.end()){
            updateGroup.push_back(checkedGroup[i]);
        }
    }
}

int main() {

    //get input
    ifstream in("Input.txt");
    int pipe; 
    string separator = "<->";
    char comma = ',';
    vector<int> result;
    map<int, vector<int>> pipes;
    string line; 
    
    while(getline(in, line)){
        istringstream ssin(line);
        ssin >> pipe >> separator;
        int i;
        vector<int> connections;
        while(ssin >> i){
            connections.push_back(i);
            ssin >> comma;
        }
        pipes[pipe] = connections;
    }
    
    //solve puzzle
    map<int, vector<int>>::iterator it;
    vector<int> checkedPipes;
    bool zeroGroupFound = false;
    bool newGroupFound = false;
    vector<vector<int>> pipeGroups;
    
    for ( it = pipes.begin(); it != pipes.end(); it++ )
    {
        vector<int> checkedPipes;
        if(connectedToZero(it->first, pipes, checkedPipes)){
            result.push_back(it->first);
            if(!zeroGroupFound){
                zeroGroupFound = true;
                newGroupFound = true;
            }
        }
        else{
            int min = *std::min_element(checkedPipes.begin(), checkedPipes.end());
            newGroupFound = true;
            for(int i = 0; i < pipeGroups.size() && newGroupFound; i++){
                if(find(pipeGroups[i].begin(), pipeGroups[i].end(), min)!= pipeGroups[i].end()){
                    updatePipeGroup(pipeGroups[i], checkedPipes);
                    newGroupFound = false;
                }
            }
        }
        
        if(newGroupFound){
            pipeGroups.push_back(checkedPipes);
        }
        newGroupFound = false;
    }
    
        
    cout << "Part 1: " << result.size() << endl;
    cout << "Part 2: " << pipeGroups.size() << endl;
    
    return 0;
}
