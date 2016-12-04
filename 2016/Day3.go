package main

import (
	"flag"
	"fmt"
	"io/ioutil"
	"sort"
	"strconv"
	"strings"
)

func checkTriangles(triangles [][]int) int {
	validTriangles := 0
	for _, t := range triangles {
		sort.Ints(t)
		if t[2] < t[1]+t[0] {
			validTriangles++
		}
	}
	return validTriangles
}

func getDirectionsFromInputFile(path string) [][]int {
	// open input file
	// read the whole file at once
	b, err := ioutil.ReadFile("input.txt")
	if err != nil {
		panic(err)
	}
	strbuffer := string(b) // convert read in file to a string
	triangleStrings := strings.Split(strbuffer, "\n")
	var triangles [][]int
	for _, v := range triangleStrings {
		var triangle []int
		sideStrings := strings.Fields(v)
		for _, s := range sideStrings {
			side, error := strconv.Atoi(strings.TrimSpace(s))
			if error == nil {
				triangle = append(triangle, side)
			}
		}
		if len(triangle) == 3 {
			triangles = append(triangles, triangle)
		}
	}
	return triangles
}

func main() {
	var ifp = flag.String("path", "C:\\", "file path for input text")
	flag.Parse()

	directions := getDirectionsFromInputFile(*ifp)

	result := checkTriangles(directions)

	fmt.Println(result)

}
