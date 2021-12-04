#=
for each number called 
    set all instances in bingo cards = nan
    if any card has a row where no numbers or column no numbers
        sum remaining numbers * called number
=#
using DelimitedFiles

a = readdlm("input.txt")
calledNumbers = [parse(Int, x) for x in split(a[1], ",")]
b = a[2:end, 1:end]
cardCount = Int(size(b)[1] / 5) - 1
cards = [b[1+(5i):1+(5i)+4, 1:end] for i ∈ 0:cardCount]

function star1()
    for n ∈ calledNumbers
        for card in cards
            replace!(card, n => '_')
            for r in eachrow(card)
                if (all(r .== '_'))
                    return sum(filter(x -> typeof(x) == Int, card)) * n
                end
            end
            for c in eachcol(card)
                if (all(c .== '_'))
                    return sum(filter(x -> typeof(x) == Int, card)) * n
                end
            end
        end
    end
end

function checkcard(card)
    winner = false
    i = 1
    while !winner && i < 6
        winner = all(card[:, i] .== '_') || all(card[i, :] .== '_')
        i += 1
    end
    return winner
end

function star2()
    for n ∈ calledNumbers
        i = 1
        while i <= length(cards)
            card = cards[i]
            replace!(card, n => '_')
            winner = checkcard(card)
            if (winner)
                if length(cards) == 1
                    return sum(filter(x -> typeof(x) == Int, card)) * n
                else
                    deleteat!(cards, i)
                end
            else
                i += 1
            end
        end
    end
end

println(star1())
println(star2())