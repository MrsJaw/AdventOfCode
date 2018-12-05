def collapsePolymer s
    polymerUnits = s.chars
    recheck = true
    
    while recheck
        polymerLength = polymerUnits.length
        recheck = false
        (0...polymerLength-1).each{ |i|
            c = polymerUnits[i]
            cNext = polymerUnits[i+1]
            if c && cNext && c != cNext && (c.ord - cNext.ord).abs == 32
                polymerUnits.delete_at(i+1)
                polymerUnits.delete_at(i)
                recheck = true
            end
            i+=2
        }
    end
    return polymerUnits.join
end

polymer = ""
if File.file?("day5input.txt")
    polymer = File.read("day5input.txt").chop
end

#part 1 
result = collapsePolymer(polymer)
puts result.length

#part 2
unitTypes = polymer.downcase.chars.uniq
minSize = polymer.length
unitTypes.each{ |u|
    collapsed = collapsePolymer(polymer.tr(u, '').tr(u.upcase, ''))
    if collapsed.length < minSize
        minSize = collapsed.length
    end
}
puts minSize
