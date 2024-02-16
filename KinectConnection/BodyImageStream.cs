using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KinectConnection
{
    /// <summary>
    /// Classe représentant un flux d'image du corps pour la Kinect.
    /// Étend la classe KinectStream.
    /// </summary>
    public class BodyImageStream : KinectStream
    {
        /// <summary>
        /// Propriété liée à l'objet GestureManager
        /// </summary>
        //public GestureManager GestureManager { get; set; }

        // Le lecteur pour les données enoyées par le Kinect
        private BodyFrameReader bodyFrameReader = null;

        // DrawingGroup est une classe qui nous permet de créer et manipuler un groupe de dessins comme un seul objet.
        // Nous l'utiliserons pour dessiner le corps.
        private DrawingGroup drawingGroup = new DrawingGroup();

        // Convertit les coordonnées 3D "vues" par le kinect en coordonnées 2D
        // -que nous pouvons utiliser pour dessiner le bodyStream
        private CoordinateMapper coordinateMapper = null;

        /// <summary>
        /// Paramètres et attributs du corps.
        /// </summary>
        // Liste de corps.
        private Body[] bodies = null;

        // Liste de tuples, avec les paires de joints.
        private List<Tuple<JointType, JointType>> bones = new List<Tuple<JointType, JointType>>();

        // Taille de la main
        private const double HandSize = 30;

        // Épaisseur de l'articulation
        private const double JointThickness = 3;

        // Épaisseur de la frontière utilisée pour couper les données du corps
        private const double ClipBoundsThickness = 10;

        /// <summary>
        /// Paramètres et attributs de l'image.
        /// </summary>

        // DrawingImage est une classe fille de ImageSource qu'on utilisera
        // -pour afficher le DrawingGroup du corps qu'on aura dessiné
        private DrawingImage imageSource = new DrawingImage();

        // Contient des informations telles que la largeur, la hauteur et le format des pixels.
        private FrameDescription frameDescription;

        // displayHeight est la hauteur de la zone d'affichage. Elle est définie en fonction de la hauteur décrite dans frameDescription.
        private int displayHeight;

        // displayWidth est la largeur de la zone d'affichage. Elle est définie en fonction de la largeur décrite dans frameDescription.
        private int displayWidth;

        /// <summary>
        /// Paramètres et attributs pour les couleurs.
        /// </summary>
        private List<Pen> bodyColors = new List<Pen>();

        // Couleur de l'articulation suivie
        private readonly Brush trackedJointBrush = new SolidColorBrush(Color.FromArgb(255, 68, 192, 68));

        // Couleur de l'articulation inférée
        private readonly Brush inferredJointBrush = Brushes.Yellow;

        // Couleur de la main fermée
        private readonly Brush handClosedBrush = new SolidColorBrush(Color.FromArgb(128, 255, 0, 0));

        // Couleur de la main ouverte
        private readonly Brush handOpenBrush = new SolidColorBrush(Color.FromArgb(128, 0, 255, 0));

        // Couleur de la main en lasso
        private readonly Brush handLassoBrush = new SolidColorBrush(Color.FromArgb(128, 0, 0, 255));

        // Couleur de l'os inféré
        private readonly Pen inferredBonePen = new Pen(Brushes.Gray, 1);

        /// <summary>
        /// Obtient la source d'image de la classe.
        /// </summary>
        public override ImageSource Source
        {
            get { return this.imageSource; }
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe BodyImageStream.
        /// </summary>
        public BodyImageStream() : base()
        {
            this.coordinateMapper = this.KinectSensor.CoordinateMapper;

            frameDescription = this.KinectSensor.DepthFrameSource.FrameDescription;
            displayHeight = frameDescription.Height;
            displayWidth = frameDescription.Width;

            // Torse
            this.bones.Add(new Tuple<JointType, JointType>(JointType.Head, JointType.Neck));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.Neck, JointType.SpineShoulder));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineShoulder, JointType.SpineMid));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineMid, JointType.SpineBase));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineShoulder, JointType.ShoulderRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineShoulder, JointType.ShoulderLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineBase, JointType.HipRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineBase, JointType.HipLeft));

            // Bras droit 
            this.bones.Add(new Tuple<JointType, JointType>(JointType.ShoulderRight, JointType.ElbowRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.ElbowRight, JointType.WristRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.WristRight, JointType.HandRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.HandRight, JointType.HandTipRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.WristRight, JointType.ThumbRight));

            // Bras gauche
            this.bones.Add(new Tuple<JointType, JointType>(JointType.ShoulderLeft, JointType.ElbowLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.ElbowLeft, JointType.WristLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.WristLeft, JointType.HandLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.HandLeft, JointType.HandTipLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.WristLeft, JointType.ThumbLeft));

            // Jambe droite
            this.bones.Add(new Tuple<JointType, JointType>(JointType.HipRight, JointType.KneeRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.KneeRight, JointType.AnkleRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.AnkleRight, JointType.FootRight));

            // Jambe gauche
            this.bones.Add(new Tuple<JointType, JointType>(JointType.HipLeft, JointType.KneeLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.KneeLeft, JointType.AnkleLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.AnkleLeft, JointType.FootLeft));

            // Couleurs
            this.bodyColors.Add(new Pen(Brushes.Red, 6));
            this.bodyColors.Add(new Pen(Brushes.Orange, 6));
            this.bodyColors.Add(new Pen(Brushes.Green, 6));
            this.bodyColors.Add(new Pen(Brushes.Blue, 6));
            this.bodyColors.Add(new Pen(Brushes.Indigo, 6));
            this.bodyColors.Add(new Pen(Brushes.Violet, 6));

            this.imageSource = new DrawingImage(this.drawingGroup);
        }

        /// <summary>
        /// Démarre la lecture du flux de corps.
        /// </summary>
        public override void Start()
        {
            if (this.KinectSensor != null)
            {
                this.bodyFrameReader = this.KinectSensor.BodyFrameSource.OpenReader();

                if (this.bodyFrameReader != null)
                {
                    this.bodyFrameReader.FrameArrived += this.Reader_BodyFrameArrived;
                }
            }
        }

        /// <summary>
        /// Arrête la lecture du flux de corps.
        /// </summary>
        public override void Stop()
        {
            if (this.bodyFrameReader != null)
            {
                // nettoyer le dessin ! 
                this.imageSource.Drawing = null;
                this.bodyFrameReader.FrameArrived -= this.Reader_BodyFrameArrived;

                // Dispose le lecteur pour libérer les ressources.
                // Si on ne le fait pas manuellement, le GC le fera pour nous, mais nous ne savons pas quand.
                this.bodyFrameReader.Dispose();
                this.bodyFrameReader = null;
            }
        }

        /// <summary>
        /// Méthode appelée lors de l'arrivée d'un nouveau frame du corps
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void Reader_BodyFrameArrived(object sender, BodyFrameArrivedEventArgs e)
        {
            // flag pour savoir si la donnée est reçu ou pas
            bool dataReceived = false;

            using (BodyFrame bodyFrame = e.FrameReference.AcquireFrame())
            {
                if (bodyFrame != null)
                {
                    if (this.bodies == null)
                    {
                        // ajouter des corps en fonction du bodyCount
                        this.bodies = new Body[bodyFrame.BodyCount];
                    }

                    // Une fois que GetAndRefreshBodyData est appele le Kinect va allouer chaque body dans le tableau
                    // Tant que le body est toujours dans le cadre il ne sera pas supprimé donc les body deja alloues
                    // seront réutilisé
                    bodyFrame.GetAndRefreshBodyData(this.bodies);

                    // flag
                    dataReceived = true;
                }
            }

            // Si on reçoit de la donnée, alors dessiner le body
            if (dataReceived)
            {
                using (DrawingContext dc = this.drawingGroup.Open())
                {
                    // Fond transparent
                    dc.DrawRectangle(Brushes.Transparent, null, new Rect(0.0, 0.0, displayWidth, displayHeight));

                    int penIndex = 0;
                    foreach (Body body in this.bodies)
                    {
                        // pour avoir des couleurs differentes pour chaque body
                        Pen drawPen = this.bodyColors[penIndex++];

                        if (body.IsTracked)
                        {
                            this.DrawClippedEdges(body, dc);

                            IReadOnlyDictionary<JointType, Joint> joints = body.Joints;

                            // convert the joint points to depth (display) space
                            Dictionary<JointType, Point> jointPoints = new Dictionary<JointType, Point>();

                            foreach (JointType jointType in joints.Keys)
                            {
                                // parfois la profondeur Z peut venir negatif
                                // avec 0.1 on evite les coordonées negatives du coordinateMapper
                                CameraSpacePoint position = joints[jointType].Position;
                                if (position.Z < 0)
                                {
                                    position.Z = 0.1f;
                                }

                                DepthSpacePoint depthSpacePoint = this.coordinateMapper.MapCameraPointToDepthSpace(position);
                                jointPoints[jointType] = new Point(depthSpacePoint.X, depthSpacePoint.Y);
                            }

                            // dessiner les joints
                            this.DrawBody(joints, jointPoints, dc, drawPen);
                        }
                    }

                    // empeche de dessiner hors du cadre
                    this.drawingGroup.ClipGeometry = new RectangleGeometry(new Rect(0.0, 0.0, displayWidth, displayHeight));
                }
            }
        }

        /// <summary>
        /// Méthode appelée pour le dessin du corps
        /// </summary>
        private void DrawBody(IReadOnlyDictionary<JointType, Joint> joints, IDictionary<JointType, Point> jointPoints, DrawingContext drawingContext, Pen drawingPen)
        {
            // Dessiner les os (lignes entre les joints)
            foreach (var bone in this.bones)
            {
                this.DrawBone(joints, jointPoints, bone.Item1, bone.Item2, drawingContext, drawingPen);
            }

            // Dessiner les joints
            foreach (JointType jointType in joints.Keys)
            {
                Brush drawBrush = null;

                TrackingState trackingState = joints[jointType].TrackingState;

                if (trackingState == TrackingState.Tracked)
                {
                    drawBrush = this.trackedJointBrush;
                }
                else if (trackingState == TrackingState.Inferred)
                {
                    drawBrush = this.inferredJointBrush;
                }

                if (drawBrush != null)
                {
                    drawingContext.DrawEllipse(drawBrush, null, jointPoints[jointType], JointThickness, JointThickness);
                }
            }
        }

        /// <summary>
        /// Méthode appelée pour le dessin d'un os
        /// </summary>
        private void DrawBone(IReadOnlyDictionary<JointType, Joint> joints, IDictionary<JointType, Point> jointPoints, JointType jointType0, JointType jointType1, DrawingContext drawingContext, Pen drawingPen)
        {
            Joint joint0 = joints[jointType0];
            Joint joint1 = joints[jointType1];

            // Si ces joints n'existent pas, return (pas besoin de dessiner)
            if (joint0.TrackingState == TrackingState.NotTracked ||
            joint1.TrackingState == TrackingState.NotTracked)
            {
                return;
            }

            Pen drawPen = this.inferredBonePen;
            if ((joint0.TrackingState == TrackingState.Tracked) && (joint1.TrackingState == TrackingState.Tracked))
            {
                drawPen = drawingPen;
            }

            drawingContext.DrawLine(drawPen, jointPoints[jointType0], jointPoints[jointType1]);
        }

        /// <summary>
        /// Méthode appelée pour le dessin d'un joint
        /// </summary>
        private void DrawClippedEdges(Body body, DrawingContext drawingContext)
        {
            FrameEdges clippedEdges = body.ClippedEdges;

            if (clippedEdges.HasFlag(FrameEdges.Bottom))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(0, displayHeight - ClipBoundsThickness, displayWidth, ClipBoundsThickness));
            }

            if (clippedEdges.HasFlag(FrameEdges.Top))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(0, 0, displayWidth, ClipBoundsThickness));
            }

            if (clippedEdges.HasFlag(FrameEdges.Left))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(0, 0, ClipBoundsThickness, displayHeight));
            }

            if (clippedEdges.HasFlag(FrameEdges.Right))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(displayWidth - ClipBoundsThickness, 0, ClipBoundsThickness, displayHeight));
            }
        }
    }
}
