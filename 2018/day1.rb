#part 1
freqchanges = []
if File.file?("day1input.txt")
    File.readlines("day1input.txt").each { |l| freqchanges << l.to_i }
end
sum = 0
freqchanges.each { |i| sum += i }
puts sum

#part 2
freqs = [0]
sum = 0
output = ""
until !output.empty?
    freqchanges.each do |i|
        sum += i
        if freqs.include?sum
            output += "First Duplicate is #{sum}"  
            break      
        else
            freqs << sum
            puts sum
        end
    end
end
puts output