package main

import (
	"bufio"
	"fmt"
	"os"
	"regexp"
	"strconv"
	"strings"
)

var screen [6][50]int

func getColumn(c int) []int {

	var result []int

	for _, v := range screen {
		result = append(result, v[c])
	}

	return result
}

func getRow(r int) []int {
	var result []int

	for _, v := range screen[r] {
		result = append(result, v)
	}

	return result
}

func updateScreen(input string) {
	re := regexp.MustCompile("[0-9]+")
	dimensions := re.FindAllString(input, -1)
	w := len(screen)
	l := len(screen[0])
	//fmt.Println(dimensions)

	if len(dimensions) == 2 {
		a, aOK := strconv.Atoi(dimensions[0])
		b, bOK := strconv.Atoi(dimensions[1])
		if aOK == nil && bOK == nil {
			if strings.Contains(input, "rect") {
				for i := 0; i < b; i++ {
					for j := 0; j < a; j++ {
						screen[i][j] = 1
					}
				}
			} else if strings.Contains(input, "rotate column") {
				orig := getColumn(a)
				for i := range orig {
					screen[(i+b)%w][a] = orig[i]
				}
			} else if strings.Contains(input, "rotate row") {
				orig := getRow(a)
				for i := range orig {
					screen[a][(i+b)%l] = orig[i]
				}
			}
		}
	}
}

func main() {

	inFile, _ := os.Open("input.txt")
	defer inFile.Close()

	scanner := bufio.NewScanner(inFile)
	for scanner.Scan() {
		updateScreen(scanner.Text())
		/*for _, v := range screen {
			fmt.Println(v)
		}
		println(" ")*/
	}

	sum := 0
	for _, r := range screen {
		for _, c := range r {
			sum += c
		}
	}

	fmt.Println(sum)
}
