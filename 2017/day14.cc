#include <iostream>
#include <string>
#include <numeric>  
#include <vector>    
#include <functional>
#include <iomanip>
#include <sstream>
#include <bitset>
#include <algorithm>

using namespace std;

struct point{
    int x, y; 

    bool operator==(const point &o) const {
        return x == o.x && y == o.y;
    }

    bool operator<(const point &o)  const {
        return x < o.x || (x == o.x && y < o.y);
    }
};

string knotHash(vector<int> &movements){
    int size = 256;
    int numbers[size];
    iota (numbers,numbers+size,0);
    vector<int> rev; 
    int pos = 0;
    int skip = 0;
    int len; 
    
    movements.push_back(17);
    movements.push_back(31);
    movements.push_back(73);
    movements.push_back(47);
    movements.push_back(23);
    
    for(int l = 0; l < 64; l++){
        for(int k = 0; k <movements.size(); k++){
            len = movements[k];
            if(len >= 0 && len <= size){
                for(int i = 0; i < len; i++){
                    rev.push_back(numbers[(pos+i)%size]);
                }
                
                int j = pos;
                for(int i = rev.size() - 1; i >= 0; i--){
                    numbers[(j++)%size] = rev[i];
                }
                
                rev.clear();
                pos += len+skip++;
            }
        }
    }

    int denseHash[16];
    stringstream knotHash;
    for(int i = 0; i < 16; i++){
        denseHash[i] = accumulate (numbers+(i*16), numbers+((i+1)*16), 0, std::bit_xor<int>());
        knotHash << hex << setfill('0') << setw(2) << denseHash[i];
    }
    
    for(int i = 0; i < 5; i++){
        movements.pop_back();
    }
    
    return knotHash.str();
    
}

string getBinaryString(string denseHash){
    string bin;
    for(int i = 0; i< denseHash.length(); i++){
		int v; 
		if (denseHash[i] >= '0' && denseHash[i] <= '9') {
			v = int(denseHash[i] - '0');
		} else if (denseHash[i] >= 'a' && denseHash[i] <= 'f') {
			v = int(denseHash[i] - 'a' + 10);
		}
		stringstream nibble;
		nibble << (bitset<4>)v;
		bin += nibble.str();
	}
	return bin;
}

void findGroup(point p, string (&disc)[128], vector<point> &checkedPoints){
    point adjacentpoints[4] = {{p.x, p.y+1}, {p.x, p.y-1}, {p.x+1, p.y}, {p.x-1, p.y}};
    for(int i = 0; i < 4; i++){
        if(adjacentpoints[i].x >= 0 && adjacentpoints[i].x < 128 &&
           adjacentpoints[i].y >= 0 && adjacentpoints[i].y < 128 &&
           find(checkedPoints.begin(), checkedPoints.end(), adjacentpoints[i]) == checkedPoints.end()){
            checkedPoints.push_back(adjacentpoints[i]);
            if(disc[adjacentpoints[i].x][adjacentpoints[i].y] == '1'){
                findGroup(adjacentpoints[i], disc, checkedPoints);
            }
        }
    }
}

int countGroups(string (&disc)[128]){
    vector<point> checkedPoints;
    point p;
    int groups = 0;
    for(int i = 0; i < 128; i++){
        for(int j = 0; j < 128; j++){
            p.x = i, p.y = j;
            if(find(checkedPoints.begin(), checkedPoints.end(), p) == checkedPoints.end()){
                checkedPoints.push_back(p);
                if(disc[i][j] == '1'){
                    groups++;
                    findGroup(p, disc, checkedPoints);
                }
            }
        }
    }
    return groups;
}

int main() {
    
    //get input
    int len; 
    char lenChar;
    vector<int> movements;
    string input = "ljoxqyyw";
     
    for(int i = 0; i < input.length(); i ++){
        lenChar = input[i];
        if(lenChar != '\n' && lenChar != EOF && lenChar != '\0'){
            len = lenChar;
            movements.push_back(len);
        }
    }
    
    len = '-';
    movements.push_back(len);
    
    int usedBits = 0;
    string disc[128];
    for(int i = 0; i < 128; i++){

        //append row number
        string row = to_string(i);
        for(int j =0; j < row.length(); j++){
            len = row[j];
            movements.push_back(len);
        }
        
        //create knot hash
        string denseHash = knotHash(movements);
        
        //convert to binary
        string rowBits = getBinaryString(denseHash);
        
        //count used bits
        usedBits += count(rowBits.begin(), rowBits.end(), '1');
        
        //remove row number
        for(int j =0; j < row.length(); j++){
            movements.pop_back();
        }
        
        disc[i] = rowBits;
        
    }
    
    cout << "Part 1: " << usedBits << endl;
    cout << "Part 2: " << countGroups(disc) << endl;
    
    
    return 0;
}
