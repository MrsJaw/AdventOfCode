input = [split.(x, " ") for x in split.(readlines("input.txt"), " | ")]
uniqueblinks =  [input[i][1] for i in 1:length(input)]
output = [input[i][2] for i in 1:length(input)]

numCount(c, n) = count(x -> length(x) == n, c) 
outputCount(n) = sum(numCount.(output, n))

num1 = outputCount(2)
num4 = outputCount(4)
num7 = outputCount(3)
num8 = outputCount(7)
star1 = num1 + num4 + num7 + num8
println(star1)

findAllByLength(r, n) = (findall(x -> length(x) == n, r))
findnum(r,n) = r[first(findAllByLength(r, n))]

function getdigits(r)
    digits = Dict{String, String}()
    digits["1"] = findnum(r, 2)
    digits["4"] = findnum(r, 4)
    digits["7"] = findnum(r, 3)
    digits["8"] = findnum(r, 7)
    for i in findAllByLength(r, 6)
        digit = r[i]
        if (digits["4"] ⊆ digit)
            digits["9"] = digit
        elseif (digits["1"] ⊆ digit)
                digits["0"] = digit
        else
            digits["6"] = digit
        end
    end    
    for i in findAllByLength(r, 5)
        digit = r[i]
        if (digits["1"] ⊆ digit)
            digits["3"] = digit
        elseif (digit ⊆ digits["6"])
            digits["5"] = digit
        else
            digits["2"] = digit
        end
    end
    return Dict([(digits[i], i) for i in keys(digits)])
end

function star2()
    sum = 0
    for i in 1:length(output)
        digits = getdigits(uniqueblinks[i])
        digitkeys = [i for i in keys(digits) ]
        num = ""
        for j in output[i]
            key = digitkeys[first(findall(x -> length(x) == length(j) && x ⊆ j,digitkeys ))]
            num *= digits[key]
        end
        sum += parse(Int, num)
    end
end

println(star2())