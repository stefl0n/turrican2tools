﻿using Audiolib;
using System;
using System.Windows.Forms;

namespace TFXTool.TFX
{
    class Playback
    {
        AudioContext actx;
        Timer timer;
        public PaulaChip PaulaChip;
        public Playroutine Playroutine;
        public Playback(AudioContext actx, TFXFile tfx, byte[] sampledata)
        {
            this.actx = actx;
            PaulaChip = new PaulaChip(actx);


            Playroutine = new Playroutine(tfx);
            Playroutine.sampledata = sampledata;
            Playroutine.Paula = PaulaChip;
            Playroutine.TrackstepPositionChanged += Playroutine_TrackstepPositionChanged;
            Playroutine.TempoChanged += Playroutine_TempoChanged;
            Playroutine.SongEnded += Playroutine_SongEnded;


            /*timer = new Timer();
            timer.Tick += (s, ee) =>
            {
                Playroutine.VBI();

            };*/

        }

        private void Playroutine_SongEnded(object sender, EventArgs e)
        {
        }

        private void Playroutine_TrackstepPositionChanged(object sender, EventArgs e)
        {
            Console.WriteLine("trackstep pos " + Playroutine.TrackstepPosition + " in tick " + Playroutine.tickCounter);
        }

        private void Playroutine_TempoChanged(object sender, EventArgs e)
        {
            //timer.Interval = 1000 / 50;
        }

        public void Start()
        {
            //timer.Interval = 1000 / 50;
            PaulaChip.Interrupt += PaulaChip_Interrupt;
            PaulaChip.Connect(actx.Destination);
            timer?.Start();
        }

        private void PaulaChip_Interrupt(object sender, EventArgs e)
        {
            Playroutine.VBI();
        }

        public void Stop()
        {
            PaulaChip.Disconnect();
            timer?.Stop();
        }
    }
}
