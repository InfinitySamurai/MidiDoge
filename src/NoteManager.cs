using Commons.Music.Midi;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace midiGame
{
    class NoteManager
    {
        private List<GameObject> bufferedObjects;
        private List<GameObject> noteObjects;
        private GameObject baseNote;
        private float spawnArc;
        private MidiAnalyser analyser;

        private float minX;
        private float maxX;
        private float maxY;

        public NoteManager (GameObject defaultNote, float spawnArc, MidiAnalyser analyser, float middleX, float xSpread, float ySpread)
        {
            noteObjects = new List<GameObject>();
            bufferedObjects = new List<GameObject>();
            baseNote = defaultNote;
            this.spawnArc = spawnArc;
            this.analyser = analyser;

            minX = middleX - xSpread;
            maxX = middleX + xSpread;
            maxY = ySpread;
        }

        public void addNote(MidiEvent midiEvent)
        {
            double rotationAmount = ((midiEvent.MetaType - analyser.midiMusicAverage) / analyser.largestDistanceFromAverage) * this.spawnArc;
            var newNote = baseNote.clone();

            var defaultDirecton = new Vector2(0, 3);
            Convert.ToSingle(Math.Cos(Helper.DegreeToRad(rotationAmount)));
            var directionModVector = new Vector2(Convert.ToSingle(Math.Sin(Helper.DegreeToRad(rotationAmount))), Convert.ToSingle(Math.Cos(Helper.DegreeToRad(rotationAmount))));
            newNote.velocity = defaultDirecton + directionModVector;
            noteObjects.Add(newNote);
        }

        public void update(GameTime gameTime)
        {
            var copiedNotes = new List<GameObject>(noteObjects);
            foreach (var copiedNote in copiedNotes)
            {
                copiedNote.update(gameTime);
                if (copiedNote.position.X > maxX || copiedNote.position.X < minX || copiedNote.position.Y > maxY)
                {
                    noteObjects.Remove(copiedNote);
                }
            }
        }

        public void draw(SpriteBatch sb)
        {
            var copiedNotes = new List<GameObject>(noteObjects);
            foreach (var copiedNote in copiedNotes)
            {
                copiedNote.draw(sb);
            }
        }
    }
}
