using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Linq;
using Commons.Music.Midi;
using System;
using System.Collections.Generic;

namespace midiGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        int windowWidth = 480;
        int windowHeight = 800;

        int widthMiddle;

        float spawnArc = 100f;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        MidiPlayer midiPlayer;
        MidiMusic midiMusic;
        NoteManager noteManager;

        Texture2D ballTexture;
        Texture2D playerTexture;

        Player player;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            widthMiddle = this.windowWidth / 2;


        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            graphics.PreferredBackBufferWidth = this.windowWidth;
            graphics.PreferredBackBufferHeight = this.windowHeight;
            graphics.ApplyChanges();


            base.Initialize();

            player = new Player(playerTexture, new Vector2(widthMiddle, windowHeight - 50), new Rectangle(0, (int)(windowHeight * 0.66f), windowWidth, (int)(windowHeight - windowHeight * 0.66f)));

            var access = MidiAccessManager.Default;
            var output = access.OpenOutputAsync(access.Outputs.Last().Id).Result;
            midiMusic = MidiMusic.Read(System.IO.File.OpenRead("midiFiles/something_doin'_(nc)smythe.mid"));
            MidiAnalyser midiAnalyser = new MidiAnalyser();
            midiAnalyser.analyseMidi(midiMusic);

            noteManager = new NoteManager(new GameObject(ballTexture, new Vector2(this.widthMiddle, 0)), spawnArc, midiAnalyser, widthMiddle, 200, 800);

            midiPlayer = new MidiPlayer(midiMusic, output);
            midiPlayer.EventReceived += (MidiEvent midiEvent) => {
                if (midiEvent.EventType == MidiEvent.NoteOn)
                {
                    noteManager.addNote(midiEvent);
                }
                    
            };
            midiPlayer.PlayAsync();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ballTexture = Content.Load<Texture2D>("note");
            playerTexture = Content.Load <Texture2D>("player");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            List<GameObject> objsToRemove = new List<GameObject>();
            // TODO: Add your update logic here

            noteManager.update(gameTime);
            player.update(gameTime);
            InputManager.update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            base.Draw(gameTime);
            spriteBatch.Begin();
            noteManager.draw(spriteBatch);
            player.draw(spriteBatch);
            spriteBatch.End();

        }
    }
}
