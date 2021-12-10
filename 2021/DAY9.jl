a = [c for c in readlines("input.txt")]
b = reduce(vcat, permutedims.(collect.(a))) #convert to 2d array of chars
map = parse.(Int, c for c in b)

function getneighbors(m, i, j)
    mapsize = size(m)
    neighbors = Array{Any}(missing, 5)
    neighbors[1] = m[i,j]
    if(i-1 > 0)
        neighbors[2] = m[i-1, j]
    end
    if (i+1 <= mapsize[1])
        neighbors[3] = m[i+1, j]
    end
    if(j-1 > 0)
        neighbors[4] = m[i, j-1]
    end
    if (j+1 <= mapsize[2])
        neighbors[5] = m[i, j+1]
    end
    return skipmissing(neighbors)
end

function star1()
    mapsize = size(map)
    lowpoints = []
    for i ∈ 1:mapsize[1], j ∈ 1:mapsize[2]
        neighbors = getneighbors(map, i, j)
        point = map[i,j]
        if point == minimum(neighbors) && count(x -> x==point, neighbors) == 1
            append!(lowpoints, point)
        end
    end
    return sum(lowpoints.+=1)
end

println(star1())


