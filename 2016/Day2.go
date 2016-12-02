package main

import (
	"flag"
	"fmt"
	"io/ioutil"
	"strings"
)

var keyPad = [3][3]int{{7, 8, 9}, {4, 5, 6}, {1, 2, 3}}

func navigate(directions []string) []int {
	var keycode []int
	x := 1
	y := 1
	for _, l := range directions {
		if l != "" {
			for _, v := range strings.TrimSpace(l) {
				switch v {
				case 'U':
					if x < (len(keyPad[y]) - 1) {
						x++
					}
				case 'D':
					if x > 0 {
						x--
					}
				case 'R':
					if y < (len(keyPad) - 1) {
						y++
					}
				case 'L':
					if y > 0 {
						y--
					}
				default:
					println("NOPE", v)
				}
				//println(l[i], x, y, keyPad[x][y])
			}
			keycode = append(keycode, keyPad[x][y])
		} else {
			println("NOOOOOOOOOPE")
		}
	}
	return keycode
}

func getDirectionsFromInputFile(path string) []string {
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
	var ifp = flag.String("path", "C:\\", "file path for input text")
	flag.Parse()

	directions := getDirectionsFromInputFile(*ifp)

	distancetravelled := navigate(directions)

	fmt.Println(distancetravelled)

}
