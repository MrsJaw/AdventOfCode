using DelimitedFiles

course = readdlm("input.txt")

function star1()
    x,y=0,0
    for i in eachrow(course)
        direction, distance = i[1], i[2]
        if(direction=="forward")
            x+=distance
        elseif(direction=="up")
            y-=distance
        else
            y+=distance
        end
    end
    return x*y
end

function star2()
    x,y,a=0,0,0
    for i in eachrow(course)
        direction, distance = i[1], i[2]
        if(direction=="forward")
            x+=distance
            a*=distance
            y+=(distance*a)
        elseif(direction=="up")
            y-=distance
            a-=distance
        else
            y+=distance
            a+=distance
        end
    end
    return x*y
end

println(star1())
println(star2())
