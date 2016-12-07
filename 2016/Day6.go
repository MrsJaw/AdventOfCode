package main

import (
	"fmt"
	"io/ioutil"
	"strings"
)

func getDirectionsFromInputFile() []string {
	// open input file
	// read the whole file at once
	b, err := ioutil.ReadFile("input.txt")
	if err != nil {
		panic(err)
	}
	strbuffer := string(b) // convert read in file to a string
	return strings.Split(strbuffer, "\n")
}

func main() {
	directions := getDirectionsFromInputFile()
	var message []byte
	var wordLength = len(strings.TrimSpace(directions[0]))

	for i := 0; i < wordLength; i++ {

		letterCount := make(map[byte]int)
		for _, v := range directions {
			if len(v) > 0 {
				letterCount[v[i]]++
			}
		}

		var maxLetter byte
		maxValue := 0
		for letter, count := range letterCount {
			if count > maxValue {
				maxValue = count
				maxLetter = letter
			}
		}

		message = append(message, maxLetter)
	}
	fmt.Println(string(message))
}
