package main

import (
	"bufio"
	"fmt"
	"os"
	"strings"
)

func isValid(input string) bool {
	result := false
	betweenBrackets := false

	//IF YOU CHANGE YOUR MIND, I'M THE FIRST IN LINE
	for i := 0; i < len(input)-3; i++ {
		openBracketIndex := strings.IndexAny(input[i:i+4], "[")
		closeBracketIndex := strings.IndexAny(input[i:i+4], "]")
		if openBracketIndex > 0 {
			//HONEY I'M STILL FREE
			betweenBrackets = true
			i = i + openBracketIndex
		} else if closeBracketIndex > 0 {
			betweenBrackets = false
			i = i + closeBracketIndex
		} else {
			//TAKE A CHANCE ON ME
			if input[i] == input[i+3] && input[i+1] == input[i+2] && input[i] != input[i+1] {
				if betweenBrackets {
					result = false
					break
				} else {
					result = true
				}
			}
		}
	}

	return result
}

func main() {
	validCount := 0

	inFile, _ := os.Open("input.txt")
	defer inFile.Close()
	scanner := bufio.NewScanner(inFile)
	for scanner.Scan() {
		if isValid(scanner.Text()) { //check for valid abba pattern
			validCount++
		}
	}

	fmt.Println(validCount)
}
