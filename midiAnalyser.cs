using Commons.Music.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace midiGame
{
    class MidiAnalyser
    {
        public int maxNoteVal { get; private set; } = 0;
        public int minNoteVal { get; private set; } = 0;
        public double midiMusicAverage { get; private set; } = 0;
        public double largestDistanceFromAverage { get; private set; } = 0;

        public void analyseMidi(MidiMusic midiMusic)
        {

            minNoteVal = midiMusic.Tracks.Min(track => track.Messages.Where(message => message.Event.MetaType > 0).Min(message => message.Event.MetaType));
            maxNoteVal = midiMusic.Tracks.Max(track => track.Messages.Max(message => message.Event.MetaType));
            midiMusicAverage = midiMusic.Tracks.Average(track => track.Messages.Average(message => message.Event.MetaType));

            largestDistanceFromAverage = Math.Max(midiMusicAverage - minNoteVal, maxNoteVal - midiMusicAverage);

            Console.WriteLine($"Midi min value: {minNoteVal}");
            Console.WriteLine($"Midi max value: {maxNoteVal}");
            Console.WriteLine($"Midi mean: {midiMusicAverage}");
        }
    }
}
