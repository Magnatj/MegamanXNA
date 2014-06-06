using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MegamanXNALibrary
{
    public class SongPlaylist
    {
        static readonly SongPlaylist _instance = new SongPlaylist();
        private static List<Song> _songsList = new List<Song>();
        private int playQueue = 0;
        private bool songStart = true;
        private static float volume = 0.5f;

        private SongPlaylist()
        {
        }

        public static SongPlaylist UniqueInstance()
        {
            return _instance;
        }

        public List<Song> SongsList { get { return _songsList; } }

        public static void AddSong(Microsoft.Xna.Framework.Game game, string path)
        {
            _songsList.Add(game.Content.Load<Song>("Music\\" + path));
        }

        public void Play()
        {
            MediaPlayer.Volume = volume;
            if (songStart)
            {
                if (MediaPlayer.State.Equals(MediaState.Stopped))
                {
                    if (playQueue == SongsList.Count)
                        playQueue = 0;

                    MediaPlayer.Play(SongsList[playQueue]);
                    playQueue++;
                }

            }
        }

        public void IncreaseVolume()
        {
            volume += 0.1f;
            MediaPlayer.Volume = volume;
        }

        public void DecreaseVolume()
        {
            volume -= 0.1f;
            MediaPlayer.Volume = volume;
        }

        public void Stop()
        {
            songStart = false;
            MediaPlayer.Stop();
        }
    }
}
