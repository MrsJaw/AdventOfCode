input = readline("input.txt")
🦀 = [parse(Int, x) for x in split(input, ",")]
🦀.+=1 #julia is 1-based

function star1()
    ⛽= zeros(maximum(🦀))
    for i in  minimum(🦀) : maximum(🦀)
        ⛽[i] = sum(abs.(🦀.-i))
    end
    return findmin(⛽)
end

function star2()
    ⛽= zeros(BigInt, maximum(🦀))
    for i in  minimum(🦀) : maximum(🦀)
        ⛽[i] = sum([sum(1:j) for j in abs.(🦀.-i)])
    end    
    return findmin(⛽)
end

println(star1())
println(star2())
