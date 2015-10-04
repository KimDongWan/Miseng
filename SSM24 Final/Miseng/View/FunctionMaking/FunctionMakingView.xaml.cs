﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Miseng.ViewModel.FunctionMaking;
using Miseng.ViewModel.UICanvas;
namespace Miseng.View.FunctionMaking
{
    using HelixToolkit;
    using HelixToolkit.Wpf;
    using System.Windows.Media.Media3D;
    using System.Windows.Media.Animation;
    public partial class FunctionMakingView : UserControl
    {
        public Model3DGroup model;
        Transform3DGroup transform3DGroup;
        Material material;
        ObjReader importer;
        //ModelImporter importer;
        AxisAngleRotation3D ax3d;
        RotateTransform3D myRotateTransform;
        AxisAngleRotation3D ax3d2;
        RotateTransform3D myRotateTransform2;
        TranslateTransform3D transT3D;
        ModelVisual3D modelV3D;
        Viewport3D helixV3D;
        public PerspectiveCamera myPCamera;
        public DoubleAnimation transAnimation;
        Duration duration;
        public Storyboard sb;
        ControlViewModel ctrVM;
        GeometryModel3D modelScull;
        PointLight light;
        public FunctionMakingView()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ViewLoded);
        }

        private void ViewLoded(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow != null)
            {
                Window window = Application.Current.MainWindow;
                ctrVM = window.DataContext as ControlViewModel;
                ctrVM.functionMV = this;

            }
            //doc = (mshtml.HTMLDocument)_webBrowser.Document;

        }

        public void initModel(string currentMovingBlack)
        {
            importer = new ObjReader();
            model = importer.Read("..\\..\\ObjectFile\\hand_obj.obj");
            transform3DGroup = new Transform3DGroup();

            double _x = 0.0, _y = 0.0, _z = 0.0;

            MakeModel(_x, _y, _z, currentMovingBlack);

        }

        private void MakeModel(double x, double y, double z, string _currentMovingBlack)
        {
            double from = 0.0, to = 0.0;
            double Rx = 1, Ry = -0.5, Rz = -1, Ra = 180;
            if (_currentMovingBlack.Equals("is SnowBoard Jump"))
            {
                ctrVM.ContainTooltip = "손목을 위로 올리는 동작";
            }
            else if (_currentMovingBlack.Equals("Turn Left"))
            {
                x = 1; y = 1; z = 1; from = 0; to = 45;
                ctrVM.ContainTooltip = "손목을 왼쪽으로 90도 꺽는 동작";
            }
            else if (_currentMovingBlack.Equals("Turn Right"))
            {
                x = 0; y = -1; z = -1; from = 0; to = 45;
                ctrVM.ContainTooltip = "손목을 오른쪽으로 90도 꺽는 동작";
            }
            else if (_currentMovingBlack.Equals("Accel"))
            {
                x = 3; y = -1; z = 0; from = 0; to = 60;
                ctrVM.ContainTooltip = "손목을 앞쪽으로 90도 꺽는 동작";
            }
            else if (_currentMovingBlack.Equals("Break"))
            {
                x = -3; y = 1; z = 0; from = 0; to = 80;
                ctrVM.ContainTooltip = "손목을 뒤쪽으로 90도 꺽는 동작";
            }
            else if (_currentMovingBlack.Equals("Raised up a gear"))
            {
                Rx = 3; Ry = -1; Rz = -1; Ra = 90;
                x = -3; y = 3; z = 0; from = 0; to = 80;
                ctrVM.ContainTooltip = "팔을 하늘방향으로 90도 꺽어 올리는 동작";
            }
            else if (_currentMovingBlack.Equals("fell down gear"))
            {
                Rx = 3; Ry = -1; Rz = -1; Ra = 90;
                x = 4; y = -2; z = 1; from = 0; to = 100;
                ctrVM.ContainTooltip = "팔을 땅방향으로 90도 꺽어 내리는 동작";
            }
            else if (_currentMovingBlack.Equals("Gear Move to Right"))
            {
                Rx = 3; Ry = -1; Rz = -1; Ra = 90;
                x = -1; y = -1; z = -11;
                from = 0; to = 100;
                ctrVM.ContainTooltip = "팔을 오른쪽 안쪽으로 90도 꺽는 동작";
            }
            else if (_currentMovingBlack.Equals("Gear Move to Left"))
            {
                Rx = 3; Ry = -1; Rz = -1; Ra = 90;
                x = -1; y = -1; z = 11;
                from = 0; to = 100;
                ctrVM.ContainTooltip = "팔을 왼쪽 바깥쪽으로 90도 꺽는 동작";
            }
            else if (_currentMovingBlack.Equals("Gear Rotate Clockwise"))
            {
                Rx = 3; Ry = -1; Rz = -1; Ra = 90;
                x = -3; y = -3; z = -1; from = 0; to = 60;
                ctrVM.ContainTooltip = "손목을 시계방향으로 90도 꺽는 동작";
            }
            else if (_currentMovingBlack.Equals("Gear Rotate CounterClockWise"))
            {
                Rx = 3; Ry = -1; Rz = -1; Ra = 90;
                x = 6; y = 6; z = 1; from = 0; to = 60;
                ctrVM.ContainTooltip = "손목을 반시계방향으로 90도 꺽는 동작";
            }
            else if (_currentMovingBlack.Equals("Is Shot"))
            {
                Rx = 16; Ry = 0; Rz = -1; Ra = 180;
                x = -12; y = 6; z = 6;
                from = 0; to = 210;
                ctrVM.ContainTooltip = "골프채를 잡고 스윙하는 팔 동작";
            }
            else
            {
                ctrVM.ContainTooltip = "선택된 애니메이션이 없습니다.";
            }

            ax3d = new AxisAngleRotation3D(new Vector3D(Rx, Ry, Rz), Ra);
            myRotateTransform = new RotateTransform3D(ax3d);
            transform3DGroup.Children.Add(myRotateTransform);

            ax3d2 = new AxisAngleRotation3D(new Vector3D(x, y, z), 0);
            myRotateTransform2 = new RotateTransform3D(ax3d2);
            transform3DGroup.Children.Add(myRotateTransform2);
            transT3D = new TranslateTransform3D(0, 0, 0);
            transform3DGroup.Children.Add(transT3D);
            model.Transform = transform3DGroup;

            modelV3D = new ModelVisual3D();
            modelV3D.Content = model;
            helixV3D = new Viewport3D();
            makeCamera();

            var mesh = new MeshGeometry3D();
            modelScull = new GeometryModel3D();
            modelScull = (GeometryModel3D)model.Children[0];
            mesh = (MeshGeometry3D)modelScull.Geometry;

            var model2 = new GeometryModel3D();
            var group = new Model3DGroup();
            group.Transform = transform3DGroup;
            var myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri("../../ObjectFile/texture.jpg", UriKind.Relative));
            material = new DiffuseMaterial(myBrush);
            model2 = new GeometryModel3D(mesh, material);
            model2.BackMaterial = new DiffuseMaterial(new SolidColorBrush(Color.FromRgb(200, 149, 121)));
            group.Children.Add(model2);


            light = new PointLight(Colors.White, new Point3D(15, 0, 0));
            group.Children.Add(light);
            light = new PointLight(Colors.White, new Point3D(0, 15, 0));
            group.Children.Add(light);
            light = new PointLight(Colors.White, new Point3D(0, 0, 15));
            group.Children.Add(light);
            light = new PointLight(Colors.White, new Point3D(-15, 0, 0));
            group.Children.Add(light);
            light = new PointLight(Colors.White, new Point3D(0, -15, 0));
            group.Children.Add(light);
            light = new PointLight(Colors.White, new Point3D(0, 0, -15));
            group.Children.Add(light);
            light = new PointLight(Colors.White, new Point3D(10, 5, 10));
            group.Children.Add(light);
            light = new PointLight(Colors.White, new Point3D(0, 0, 0));
            group.Children.Add(light);

            var Model = new ModelVisual3D();
            Model.Content = group;
            helixV3D.Children.Add(Model);
            contain.Children.Add(helixV3D);

            string _propertyPath = "rotate";
            if (_currentMovingBlack.Equals("is SnowBoard Jump"))
            {
                _propertyPath = "trans";
            }
            MakeAnimation(_propertyPath, _currentMovingBlack, from, to);
        }

        private void MakeAnimation(string propertyPath, string __currentMovingBlack, double _from, double _to)
        {
            transAnimation = new DoubleAnimation();
            duration = new Duration(TimeSpan.FromSeconds(2));

            NameScope.SetNameScope(this, new NameScope());
            this.RegisterName("Trans3D", transT3D);

            if (propertyPath.Equals("trans"))
            {
                transAnimation.BeginTime = TimeSpan.FromSeconds(0.5);
                transAnimation.From = 0;
                transAnimation.To = 12;
                transAnimation.SpeedRatio = 1.5;
                transAnimation.Duration = duration;
                transAnimation.AutoReverse = true;
                Storyboard.SetTargetName(transAnimation, "Trans3D");

                if (__currentMovingBlack.Equals("is SnowBoard Jump"))
                {
                    Storyboard.SetTargetProperty(transAnimation, new PropertyPath(TranslateTransform3D.OffsetZProperty));
                }

                sb = new Storyboard();
                sb.Children.Add(transAnimation);
                sb.RepeatBehavior = RepeatBehavior.Forever;
                sb.Begin(this);

            }
            else if (propertyPath.Equals("rotate"))
            {
                transAnimation.BeginTime = TimeSpan.FromSeconds(0.5);
                transAnimation.From = _from;
                transAnimation.To = _to;
                transAnimation.SpeedRatio = 1.5;
                transAnimation.Duration = new Duration(TimeSpan.FromSeconds(1.5));
                transAnimation.AutoReverse = true;

                NameScope.SetNameScope(this, new NameScope());
                this.RegisterName("Trans3D", ax3d2);
                Storyboard.SetTargetName(transAnimation, "Trans3D");
                Storyboard.SetTargetProperty(transAnimation, new PropertyPath(AxisAngleRotation3D.AngleProperty));

                sb = new Storyboard();
                sb.Children.Add(transAnimation);
                sb.RepeatBehavior = RepeatBehavior.Forever;
                sb.Begin(this);

            }
        }

        private void makeCamera()
        {
            myPCamera = new PerspectiveCamera();
            myPCamera.Position = new Point3D(ctrVM.DISTANCE, ctrVM.DISTANCE, ctrVM.DISTANCE);
            myPCamera.LookDirection = new Vector3D(-1, -1, -1);
            // myPCamera.FieldOfView = 60;
            myPCamera.UpDirection = new Vector3D(0, 0, 1);
            AxisAngleRotation3D dax3d = new AxisAngleRotation3D(new Vector3D(ctrVM.ROTX, ctrVM.ROTY, ctrVM.ROTZ), 5);
            RotateTransform3D dmyRotateTransform = new RotateTransform3D(dax3d);
            myPCamera.Transform = dmyRotateTransform;
            helixV3D.Camera = myPCamera;
          //  helixV3D.Camera.Transform = dmyRotateTransform;

        }
    }


}