#include <iostream>
using namespace std;

int sum(string digits){
    int sum = 0;
    int count = digits.length();
    int incrememt = count / 2;
    for (int i = 0; i < count; i++) {
        if(digits[i] == digits[(i+incrememt)%count]){
            int digit = digits[i] - '0';
            sum+=digit;
        }
    }
    return sum;
}

int main() {
    string digits;
    cout << "Enter the digit string to sum: " << endl;
    cin >> digits;
    cout << "Sum: " << sum(digits) << endl;
    return 0;
}
