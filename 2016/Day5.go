package main

import (
	"crypto/md5"
	"fmt"
	"strconv"
	"strings"
)

func main() {
	input := "cxdnnyjw"
	var result = ""
	index := 0
	for len(result) < 8 {
		index++
		data := []byte(input + strconv.Itoa(index))
		//fmt.Println(index, data, input+strconv.Itoa(index))
		hash := fmt.Sprintf("%x", md5.Sum(data))
		//fmt.Println(hash)
		if strings.HasPrefix(hash, "00000") {
			result += string(hash[5])
		}
	}
	fmt.Println(result)

}
