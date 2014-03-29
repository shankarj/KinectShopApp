using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Speech;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using Microsoft.Kinect;
using AppurpleKinectProto.modules;

namespace AppurpleKinectProto
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        KinectSensor purpleSensor;
        Skeleton[] mySkeleton = new Skeleton[6];
        int pagePosition = 1;

        bool startRightToLeft = false;
        bool startLeftToRight = false;
        SpeechSynthesizer speaker = new SpeechSynthesizer();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = objects.VAL;
            sourceFrame.Source = new Uri("mainpage.xaml",  UriKind.Relative);

            
            speaker.Rate = 0;
            speaker.Volume = 100;
            speaker.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Teen);

            Prompt spStatus = speaker.SpeakAsync(("Welcome. Using your right hand, swipe across, and touch a head, to continue."));

            if (KinectSensor.KinectSensors.Count > 0)
            {
                purpleSensor = KinectSensor.KinectSensors[0];
            }


            var parameters = new TransformSmoothParameters
            {
                Smoothing = 0.3f,
                Correction = 0.0f,
                Prediction = 0.0f,
                JitterRadius = 1.0f,
                MaxDeviationRadius = 0.5f
            };

            purpleSensor.SkeletonStream.Enable(parameters);
            purpleSensor.AllFramesReady += new EventHandler<AllFramesReadyEventArgs>(purpleSensor_AllFramesReady);

            
            purpleSensor.Start();


        }

        void purpleSensor_AllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            using (SkeletonFrame skeletonFrameData = e.OpenSkeletonFrame())
            {
                if (skeletonFrameData != null)
                {
                    skeletonFrameData.CopySkeletonDataTo(mySkeleton);

                    Skeleton first = (from s in mySkeleton
                                      where s.TrackingState == SkeletonTrackingState.Tracked
                                      select s).FirstOrDefault();

                    if (first != null)
                    {
                        Canvas.SetLeft(thedot, first.Joints[JointType.HandRight].Position.X * this.Width * 2);
                        Canvas.SetTop(thedot, first.Joints[JointType.HandRight].Position.Y * this.Height * -1 * 2);

                        objects.VAL.X = Convert.ToInt16(first.Joints[JointType.HandRight].Position.X * this.Width * 2);
                        objects.VAL.Y = Convert.ToInt16(first.Joints[JointType.HandRight].Position.Y * this.Height * -1 * 2);

                        if (pagePosition == 1)
                        {
                            mainGC mainPageClassifer = new mainGC();

                            if (mainPageClassifer.classifyPosition(objects.VAL.X, objects.VAL.Y) == "men")
                            {
                                sourceFrame.Source = new Uri("productpage.xaml", UriKind.Relative);
                                pagePosition += 1;
                                statusText.Text = "You have chosen Men's section";
                                Prompt spStatus = speaker.SpeakAsync(("You have chosen Men's section"));

                            }
                            else if (mainPageClassifer.classifyPosition(objects.VAL.X, objects.VAL.Y) == "women")
                            {
                                sourceFrame.Source = new Uri("productpage.xaml", UriKind.Relative);
                                pagePosition += 1;
                                statusText.Text = "You have chosen Women's section";
                                Prompt spStatus = speaker.SpeakAsync(("You have chosen Women's section"));
                            }

                        }
                        else if (pagePosition == 2)
                        {
                            

                            string temp = gestureClassfier(first.Joints[JointType.HandRight].Position.X * this.Width * 2, first.Joints[JointType.HandRight].Position.Y * this.Height * -1 * 2, this.Width, this.Height);

                            if (temp == "right")
                            {
                                productBase.tempLoader.LoadNextProduct();
                                statusText.Text = "Swipe right";
                                Prompt spStatus = speaker.SpeakAsync(("Swipe right"));
                            }
                            else if (temp == "left")
                            {
                                productBase.tempLoader.LoadPreviousProduct();
                                statusText.Text = "Swipe Left";
                                Prompt spStatus = speaker.SpeakAsync(("Swipe Left"));
                            }
                        }
                        
                    }

                }
            }
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            purpleSensor.Stop();
        }


        public string gestureClassfier(double XPosition, double YPosition, double Width, double Height)
        {

            if (Convert.ToInt16(YPosition) < (Height / 2) + 100)
            {

                if (Convert.ToInt16(XPosition) > (Width / 2) + 150)
                {
                    startRightToLeft = false;
                }

                if (Convert.ToInt16(XPosition) < (Width / 2) - 150)
                {
                    startLeftToRight = false;
                }

                if ((Convert.ToInt16(XPosition) > (Width / 2) + 100) && (Convert.ToInt16(XPosition) < (Width / 2) + 150))
                {
                    startRightToLeft = true;
                    
                }


                if ((Convert.ToInt16(XPosition) < (Width / 2) - 100) && (Convert.ToInt16(XPosition) > (Width / 2) - 150))
                {
                    if (startRightToLeft == true)
                    {
                        startRightToLeft = false;
                        startLeftToRight = false;

                        return "left";
                    }

                }

                if ((Convert.ToInt16(XPosition) > (Width / 2) + 100) && (Convert.ToInt16(XPosition) < (Width / 2) + 150))
                {

                    if (startLeftToRight == true)
                    {
                        startLeftToRight = false;
                        startRightToLeft = false;
                        
                        return "right";
                    }

                }


                if ((Convert.ToInt16(XPosition) < (Width / 2) - 100) && (Convert.ToInt16(XPosition) > (Width / 2) - 150))
                {

                    startLeftToRight = true;
                }
            }
            else
            {
                startRightToLeft = false;
                startLeftToRight = false;
            }

            return null;

        }
     
    }
}

