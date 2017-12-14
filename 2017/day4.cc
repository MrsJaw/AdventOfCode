#include <iostream>
#include <fstream>
#include <vector>
#include <sstream>
#include <string>
#include <algorithm>
#include <list>

using namespace std;

int main() {
    ifstream in("Input.txt");
    int validPhrases = 0;
    int extraValidPhrases = 0;
    
    string line;
    vector<string> words;
    string word;
    bool valid = true;
    bool extraValid = true;

    while(getline(in, line)){
        istringstream ssin(line);
        while(valid && ssin >> word){
            valid = !any_of(words.begin(), words.end(), bind2nd(equal_to<string>(), word));
            if(valid){
                for(int i = 0; i < words.size() && extraValid; i++){
                    if(words[i].length() == word.length()){
                        list<char> wordToCheck(words[i].begin(), words[i].end());
                        int j = 0;
                        bool charMatchFound = true;
                        do{
                            list<char>::iterator iter = find (wordToCheck.begin(), wordToCheck.end(), word[j++]);
                            if(wordToCheck.end() == iter){
                                charMatchFound = false;
                            }else{
                                wordToCheck.erase(iter);   
                            }
                        }while(j < word.length() && wordToCheck.size() > 0 && charMatchFound);
                        if(charMatchFound && wordToCheck.size() == 0){
                            extraValid = false;
                        }
                    }
                }
                words.push_back(word);
            }
        }
        if(valid){
            validPhrases++;
            if(extraValid){
                extraValidPhrases++;
            }
        }
        words.clear();
        valid = true;
        extraValid = true;
    }

    cout << "Part 1: " << validPhrases << endl;
    cout << "Part 2: " << extraValidPhrases << endl;
    
    return 0;
}   