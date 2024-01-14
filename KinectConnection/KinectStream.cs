﻿using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectConnection
{
    /// <summary>
    /// Abstract class for Kinect streams.
    /// </summary>
    public abstract class KinectStream
    {
        protected KinectSensor KinectSensor { get; set; }

        public abstract void Start();

        public abstract void Stop();
    }
}
