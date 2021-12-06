using DelimitedFiles

a = readdlm("input.txt") 
a =  a[1:end, 1:end .!= 2]
maxX, maxY = 0,0
for i in CartesianIndices(a)
    point = [parse(Int, x) for x in split(a[i], ",")]
    a[i] = point
    a[i] .+= 1 #julia is 1-based indexed
    global maxX = max(maxX, point[1])
    global maxY = max(maxY, point[2])
end

b = zeros(maxX, maxY)

function star1()
    for r in eachrow(a)
        x1, y1 = r[1][1], r[1][2]
        x2, y2 = r[2][1], r[2][2]
        if(x1== x2||y1 ==y2 )
            minY, maxY = min(y1,y2), max(y1, y2)
            minX, maxX =  min(x1,x2), max(x1,x2)
            b[minY:maxY,minX:maxX] .+= 1
        end
    end
    return count(i -> (i > 1), b)
end

println(star1())