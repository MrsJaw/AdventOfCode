#part 1
totalTriples = 0
totalDuples = 0
if File.file?("day2input.txt")
    File.readlines("day2input.txt").each do |l| 
        freqs = {}
        freqs.default = 0
        l.each_char { |c| freqs[c] += 1} 
        if freqs.key(3)
            totalTriples+=1
        end
        if freqs.key(2)
            totalDuples += 1
        end
    end
end
puts (totalDuples * totalTriples)

#part 2
boxIDs = []
result = ""
if File.file?("day2input.txt")
    boxIDs = File.readlines("day2input.txt")
end
boxIDs.each do |b1|
    boxIDs.each do  |b2|
        if b1 != b2
            diffcharindices = []
            b1.each_char.with_index do |c, i|
                if c != b2[i]
                    diffcharindices << i
                    if diffcharindices.length >= 2
                        break
                    end
                end
            end
            if diffcharindices.length == 1
                charArray = b1.chars
                charArray.delete_at(diffcharindices[0])
                result = charArray.join
                break
            end
        end
    end
    if result.length > 0
        puts result
        break
    end
end