def getManhattenDistance x1, y1, x2, y2

    ((x1-x2).abs) + ((y1-y2).abs)

end



def getDistancesToAllCoordinates x, y, coordinates

    distances = {}

    distances.default = 0

    coordinates.each{ |c|

        distances[c] = getManhattenDistance(x, y, c[:point][:x], c[:point][:y])

    }

    return distances

end



def findClosestCoordinate x, y, coordinates

    distances = getDistancesToAllCoordinates(x, y, coordinates)

    return [distances.min_by{|k, v| v}].to_h.keys

end



def findTotalDistanceFromCoordinates x, y, coordinates

    distances = getDistancesToAllCoordinates(x, y, coordinates)

    total = 0

    distances.each{ |k, v| total += v}

    return total

end



ChronalCoordinate = Struct.new(:point, :isInfinite, :area)

chronalCoordinates = []

gridWidth = 0



#read in chronal coordinates

if File.file?("day6input.txt")

    File.readlines("day6input.txt").each { |l|

        p = l.split(',').map(&:strip)

        newPoint =  {:x => (p[0].to_i), :y => (p[1].to_i)}

        chronalCoordinates << ChronalCoordinate.new(newPoint, false, 0)

        if newPoint[:x] > gridWidth

            gridWidth = newPoint[:x] 

        end

        if newPoint[:y] > gridWidth

            gridWidth = newPoint[:y] 

        end

    }

end



gridWidth += 1

safeRegionArea = 0



#create grid

gridWidth.times{ |x|

    gridWidth.times{ |y|

        #part 1

        closest = findClosestCoordinate(x, y, chronalCoordinates)

        if(closest.count == 1)

            closest[0][:area] += 1

            if(x == 0 || y == 0 || x == (gridWidth-1 )|| y == (gridWidth-1))

                closest[0][:isInfinite] = true

            end

        end        

        #part 2

        if(findTotalDistanceFromCoordinates(x, y, chronalCoordinates) < 10000)

            safeRegionArea += 1

        end        

    }

}



#part 1 

puts chronalCoordinates.select{|c| !c[:isInfinite]}.max_by{|c| c[:area]}



#part 2 

puts safeRegionArea
