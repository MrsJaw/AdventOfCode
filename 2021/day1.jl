using DelimitedFiles

report = readdlm("input.txt", Int64)
reportln = length(report)

function depthChange(x)
    if(x == 1)
        "N/A"
    elseif (report[x-1] < report[x])
        "increased"
    elseif (report[x-1] > report[x])
        "decreased"
    else
        "same"
    end
end

function firstStar()
    indexes = [i for i ∈ 1:reportln]
    changes = depthChange.(indexes)
    println(count(i->(i=="increased"), changes))
end

firstStar()
