using DelimitedFiles

a = readdlm("input.txt", String) #read in as strings
b = reduce(vcat, permutedims.(collect.(a))) #convert to 2d array of chars

function star1()
    gammaBinary, epsilonBinary = "", ""
    for c in eachcol(b)
        counts =Dict([(i, count(==(i), c)) for i in c])
        gammaBinary = string(gammaBinary, (counts['0'] <= counts['1']) ? 1 : 0)
        epsilonBinary = string(epsilonBinary , (counts['0'] <= counts['1']) ? 0 : 1)
    end
    gamma = parse(Int, gammaBinary, base=2)
    epsilon = parse(Int, epsilonBinary, base=2)
    return gamma * epsilon
end

getOxygenRatingKeepValue(counts) = (return (get(counts,'0',0) <= get(counts,'1',0)) ? '1' : '0')

getC02ScrubberRatingKeepValue(counts) = (return (get(counts,'0',0) <= get(counts,'1',0)) ? '0' : '1')

function getLifeSupportRating(isOxygenRating)
    b2 = copy(b)
    index = 1
    while (size(b2)[1]) > 1
        c = b2[:,index]
        counts =Dict([(i, count(==(i), c)) for i in c])
        if(isOxygenRating)
            keepValue = getOxygenRatingKeepValue(counts)
        else
            keepValue = getC02ScrubberRatingKeepValue(counts)
        end
        b2 = b2[b2[:,index] .== keepValue, :]
        index += 1
    end    
    return parse(Int, String(vec(b2)), base=2)
end

star2() = return getLifeSupportRating(true) * getLifeSupportRating(false)

println(star1())
println(star2())
