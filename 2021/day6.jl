using DelimitedFiles


a = readdlm("input.txt")
b = [parse(Int, x) for x in split(a[1], ",")] 

timer(x) = (x == 0) ? x = 6 : x -= 1

function star1()
    fish = copy(b)
    eggs = Vector{Int}()
    for i in 1:80
        append!(fish, eggs)
        fish = timer.(fish)
        eggs = repeat([9], count(x -> x == 0, fish))   
    end
    return(length(fish))
end

function star2()    
    fish = copy(b)
    fish_count = [count(f -> f == x, fish) for x in 0:8]
    for i in 1:256
        new = fish_count[1]
        fish_count[1:8] = fish_count[2:9]
        fish_count[7] += new # Reset parent
        fish_count[9] = new    # New fish
    end
    return sum(fish_count)
end

println("star 1: ", star1())
println("star 2: ", star2())