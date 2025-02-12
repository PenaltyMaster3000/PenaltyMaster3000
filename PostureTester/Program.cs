﻿using KinectUtils;
using Microsoft.Kinect;
using MyGestureBank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PostureTester
{
    public class Program
    {
        static void Main(string[] args)
        {
            /*PostureHandUpRight postureHandUpRight = new PostureHandUpRight();
            PostureHandUpLeft postureHandUpLeft = new PostureHandUpLeft();
            PostureHandDownLeft postureHandDownLeft = new PostureHandDownLeft();
            PostureHandDownRight postureHandDownRight = new PostureHandDownRight();
            PostureTwoHandsDown postureTwoHandsDown = new PostureTwoHandsDown();
            PostureTwoHandsUp postureTwoHandsUp = new PostureTwoHandsUp();*/

            SoccerShootGesture soccerShootGesture = new SoccerShootGesture();

            IGestureFactory factory = new PenaltyMasterGestureFactory();

            GestureManager.AddGestures(factory);

            foreach (var gesture in GestureManager.KnownGestures)
            {
                gesture.GestureRecognized += (sender, arg) =>
                {
                    Console.WriteLine(arg.GestureName);
                };
            }

            GestureManager.StartAcquiringFrames(GestureManager.KinectManager);

            // Keep the program running until a key is pressed
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

    /* private static KinectSensor kinectSensor;
     private static PostureHandUpRight postureHandUpRight = new PostureHandUpRight();


     static void Main(string[] args)
     {
         kinectSensor = KinectSensor.GetDefault();
         postureHandUpRight.GestureRecognized += PostureHandUpRight_GestureRecognized;

         if (kinectSensor != null)
         {
             kinectSensor.Open();

             // Utilisation du bloc using pour bodyFrameReader
             using (var bodyFrameReader = kinectSensor.BodyFrameSource.OpenReader())
             {
                 if (bodyFrameReader != null)
                 {
                     // Abonnement à l'événement FrameArrived
                     bodyFrameReader.FrameArrived += BodyFrameReader_FrameArrived;

                     Console.WriteLine("Lecture des données du corps en cours... Appuyez sur une touche pour quitter.");
                     Console.ReadKey();
                 }
             }
         }

         if (kinectSensor != null)
         {
             kinectSensor.Close();
             kinectSensor = null;
         }
     }

     private static void BodyFrameReader_FrameArrived(object sender, BodyFrameArrivedEventArgs e)
     {
         using (var bodyFrame = e.FrameReference.AcquireFrame())
         {
             if (bodyFrame != null)
             {
                 Body[] bodies = new Body[bodyFrame.BodyCount];
                 bodyFrame.GetAndRefreshBodyData(bodies);

                 foreach (Body body in bodies)
                 {
                     if (body.IsTracked)
                     {
                         postureHandUpRight.TestGesture(body);
                     }
                 }
             }
         }
     }

     private static void PostureHandUpRight_GestureRecognized(object sender, GestureRecognizedEventArgs e)
     {
         Console.WriteLine("Posture Hand Up Right reconnue !");
     }*/
}
}
