require 'date'

class Guard
    attr_accessor :id, :sleepSchedule
    def initialize(id)
        @id = id
        @sleepSchedule = {}
        @sleepSchedule.default = 0
    end

    def logNap starttime, endtime
        startmin = starttime.min
        endmin = endtime.min
        napminutes = (startmin...endmin)
        napminutes.each{ |m|
             @sleepSchedule[m] += 1} 
    end

    def getLikeliestSnooze
        likeliestSnoozeMinute = 0
        likeliestsnooze =  @sleepSchedule.max_by{|k,v| v}
        if likeliestsnooze 
            likeliestSnoozeMinute = likeliestsnooze[0]
        end
        return likeliestSnoozeMinute
    end
    
    def getTotalNapTime
        sum = 0
        @sleepSchedule.each{|k, v| sum += v}
        return sum
    end

    def to_s
        @id
    end
end

#initialize variables
Note = Struct.new(:shiftdate, :message)
notes = []
guards = {}
guards.default = Guard.new("")

#read input
if File.file?("day4input.txt")
    File.readlines("day4input.txt").each{ |l|        
        shiftdate = DateTime.new(l[1..4].to_i, l[6..7].to_i, l[9..10].to_i, l[12..13].to_i, l[15..16].to_i)
        message = l[19..-1]
        notes << Note.new(shiftdate, message)
    }
end

#sort by date
sortedNotes = notes.sort{|a, b| a[:shiftdate] <=> b[:shiftdate]}

#process naps
id = ""
napstart = DateTime.now
napend = DateTime.now
sortedNotes.each do |note|
    if(note.message.include? "Guard #")
        i = note.message.index(" begins")
        id = note.message[7...i]        
        if !guards.include? id
            guards[id] = Guard.new(id)
        end
    elsif(note.message.include?"falls asleep")
        napstart = note.shiftdate
    elsif(note.message.include?"wakes up")
        napend = note.shiftdate
        guards[id].logNap(napstart, napend)
    end
end

#part 1
sleepiestGuard = guards.max_by{|k, v| v.getTotalNapTime()}[0]
likeliestSnooze = guards[sleepiestGuard].getLikeliestSnooze()
puts (sleepiestGuard.to_i * likeliestSnooze.to_i)

#part 2
repeatSnoozer = guards.max_by{|k, v| v.sleepSchedule[v.getLikeliestSnooze()]}[0]
likeliestSnooze = guards[repeatSnoozer].getLikeliestSnooze()
puts (repeatSnoozer.to_i * likeliestSnooze.to_i)