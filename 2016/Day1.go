package main

import (
	"flag"
	"fmt"
	"io/ioutil"
	"math"
	"strconv"
	"strings"
)

func navigate(directions []string) int {
	x := 0
	y := 0
	theta := 90

	for _, v := range directions {
		if v[0] == 'R' {
			theta = (theta - 90) % 360
		} else {
			theta = (theta + 90) % 360
		}
		step, err := strconv.Atoi(v[1:])
		if err == nil {
			switch theta {
			case 0:
				x += step
			case 90, -270:
				y += step
			case 180, -180:
				x -= step
			case 270, -90:
				y -= step
			}

		}
		//println("Current Position ", x, y, theta)
	}

	return int(math.Abs(float64(y)) + math.Abs(float64(x)))
}

func getDirectionsFromInputFile(path string) []string {
	// open input file
	// read the whole file at once
	b, err := ioutil.ReadFile("input.txt")
	if err != nil {
		panic(err)
	}
	strbuffer := string(b) // convert read in file to a string
	return strings.Split(strbuffer, ", ")
}

func main() {
	var ifp = flag.String("path", "C:\\", "file path for input text")
	flag.Parse()

	directions := getDirectionsFromInputFile(*ifp)

	distancetravelled := navigate(directions)

	fmt.Println(distancetravelled)

}
