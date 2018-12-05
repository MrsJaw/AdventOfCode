claims = []
claimIds = []
cloth = {}
cloth.default = []

def map (p, w, h, id, map)
    x = p[:x]
    w.times do
        y = p[:y]
        h.times do
            newPoint = {:x => x, :y => y}
            if map[newPoint].length > 0               
                map[newPoint] << id
            else
                map[newPoint] = [id]
            end
            y += 1
        end
        x += 1
    end
end

if File.file?("day3input.txt")
    claims = File.readlines("day3input.txt")
end

claims.each do |c|
    claimId = ""
    startingPoint = {:x =>0, :y => 0}
    width = 0
    height = 0
    c.split(/[#, @, :, \s, x, \n]/).each.with_index{ |v, i| 
        case i
            when 1
                claimId = v
                claimIds << claimId
            when 4
                startingPoint[:x] = v.to_i
            when 5
                startingPoint[:y] = v.to_i
            when 7
                width = v.to_i
            when 8
                height = v.to_i
        end
    }
    map(startingPoint, width, height,claimId, cloth)
end

#part 1
puts cloth.select{ |k, v| v.length > 1 }.length

#part 2
sharingClaimIds = []
cloth.each{ |k, v|
    if(v.length > 1)
        v.each{|i| sharingClaimIds  << i}
    end
}
puts (claimIds - sharingClaimIds)