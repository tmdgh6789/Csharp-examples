using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Media;
using System.Windows.Threading;

namespace AudioPlayer
{
    public class MainWindowViewModel : BindableBase
    {
        bool isPause = false;
        #region Property

        private ObservableCollection<AudioItem> m_PlayList;
        public ObservableCollection<AudioItem> PlayList
        {
            get
            {
                if (m_PlayList == null)
                {
                    m_PlayList = new ObservableCollection<AudioItem>();
                }
                return m_PlayList;
            }
            set
            {
                m_PlayList = value;
                RaisePropertyChanged("PlayList");
            }
        }

        private AudioItem m_SelectedItem;
        public AudioItem SelectedItem
        {
            get
            {
                return m_SelectedItem;
            }
            set
            {

                m_SelectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }

        private MediaPlayer m_CurrentMedia;
        public MediaPlayer CurrentMedia
        {
            get
            {
                if (m_CurrentMedia == null)
                {
                    m_CurrentMedia = new MediaPlayer();
                }
                return m_CurrentMedia;
            }
            set
            {
                m_CurrentMedia = value;
                RaisePropertyChanged("CurrentMedia");
            }
        }

        private double m_Duration;
        public double Duration
        {
            get
            {
                if (CurrentMedia == null)
                {
                    m_Duration = 0;
                }
                return m_Duration;
            }
            set
            {
                m_Duration = value;
                RaisePropertyChanged("Duration");
            }
        }


        private string m_CurrentTimeStr;
        public string CurrentTimeStr
        {
            get
            {
                return m_CurrentTimeStr;
            }
            set
            {
                m_CurrentTimeStr = value;
                RaisePropertyChanged("CurrentTimeStr");
            }
        }

        private double m_CurrentTime;
        public double CurrentTime
        {
            get
            {
                return m_CurrentTime;
            }
            set
            {
                m_CurrentTime = value;
                RaisePropertyChanged("CurrentTime");
            }
        }


        private double m_Volume;
        public double Volume
        {
            get
            {
                return m_Volume;
            }
            set
            {
                if (CurrentMedia != null)
                {
                    CurrentMedia.Volume = value;
                }
                m_Volume = value;
                RaisePropertyChanged("Volume");
            }
        }

        #endregion

        #region Command


        public DelegateCommand OpenFileCommand
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "MP3 files (*.mp3)|*.mp3|WAV files (*.wav)|*.wav|All files (*.*)|*.*";
                
                    if (openFileDialog.ShowDialog() == true)
                    {
                        PlayList.Add(new AudioItem() { Num = PlayList.Count + 1, Name = openFileDialog.SafeFileName, Uri = new Uri(openFileDialog.FileName) });
                    }
                });
            }
        }

        public DelegateCommand PlayCommand
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    if (SelectedItem != null)
                    {
                        if (!isPause)
                        {
                            CurrentMedia.MediaEnded += new EventHandler(MediaEnded);
                            CurrentMedia.Open(SelectedItem.Uri);
                        }

                        CurrentMedia.Play();
                        isPlaying = true;

                        //Task.Factory.StartNew(() =>
                        //{
                        //    while(isPlaying)
                        //    {
                        //        System.Windows.Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Action)delegate
                        //        {
                        //            Console.Out.WriteLineAsync(string.Format(@"{0}", CurrentMedia.Position.TotalSeconds));
                        //            CurrentTime = CurrentMedia.Position.TotalSeconds;
                        //        });
                        //        Thread.Sleep(100);
                        //    }
                        //});

                        thread = new Thread(UpdateLabelThreadProc);
                        thread.Start();
                    }
                    

                });
            }
        }

        public DelegateCommand TenSecondsStartCommand
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    if (CurrentMedia != null)
                    {
                        CurrentMedia.Stop();
                        CurrentMedia.Position = new TimeSpan(0, 0, 10);
                        CurrentMedia.Play();
                    }
                });
            }
        }

        public DelegateCommand PauseCommand
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    if (CurrentMedia != null)
                    {
                        CurrentMedia.Pause();
                        isPause = true;
                    }
                });
            }
        }

        public DelegateCommand StopCommand
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    if (CurrentMedia != null)
                    {
                        CurrentMedia.Stop();
                    }   
                });
            }
        }



        #endregion

        #region Method

        Thread thread;
        bool isPlaying = false;
        void UpdateLabelThreadProc()
        {
            while (isPlaying)
            {
                System.Windows.Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Action)delegate
                {
                    UpdateLabel();
                });

                Thread.Sleep(100);
            }
        }

        private void UpdateLabel()
        {
            if (CurrentMedia.Source != null && CurrentMedia.NaturalDuration.HasTimeSpan)
            {
                Duration = CurrentMedia.NaturalDuration.TimeSpan.TotalSeconds;
            }
            CurrentTime = CurrentMedia.Position.TotalSeconds;
            CurrentTimeStr = CurrentMedia.Position.ToString(@"mm\:ss");
        }

        private void MediaEnded(object sender, EventArgs e)
        {
            isPlaying = true;
        }
        #endregion
    }

    public class AudioItem
    {
        public int Num { get; set; }
        public string Name { get; set; }
        public Uri Uri { get; set; }
    }
}
