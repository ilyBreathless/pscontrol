using System.Collections.Generic;

namespace PolarSectorsControl
{
    using System.Linq;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Threading;
    using Arction.Wpf.SemibindableCharting;
    using Arction.Wpf.SemibindableCharting.Axes;
    using Arction.Wpf.SemibindableCharting.SeriesPolar;
    using Arction.Wpf.SemibindableCharting.Views.ViewPolar;
    using System.Windows.Threading;
    using System;
    
    public partial class PSControl : UserControl
    {
        private readonly List<double> localList = new List<double>();
        public List<int> activeSectorsList = new List<int>();
        private bool isDrawSectors = false;
        private bool isAqua = false;
        public event EventHandler<bool[]> ActiveSectorDelegate;
        private double f;
        private double theta;

        private double numDegrees = 0;
        public bool isDegrees = true;
        private bool isAsqwe = true;
        private int testNum = 0;
        public double degreesTest = 0;
        public int axisthird = 0;
  
        private double carRotate = 0;
        private int koefAngle = 0;
    
        public bool isCircleRot = false;
        public int numOfSectors = 0;
        public int rangeCounter = 0;
        public double[] rangeOfSelectedSectors;
        public int boolCount = 0;
        public bool[] selectedSectors;
        public int[] numOfActiveSectors;
        public int countSec;
        public bool[] actSec;
        public bool[] actSectors;
        private  int checkCount = 8;
        public int[] wCord = new int[4] { 60, 47, 0, 0 };
        public PSControl()
        {
           

            string deploymentKey = "lgCAABW2ij + vBNQBJABVcGRhdGVhYmxlVGlsbD0yMDE5LTA2LTE1I1JldmlzaW9uPTACgD + BCRGnn7c6dwaDiJovCk5g5nFwvJ + G60VSdCrAJ + jphM8J45NmxWE1ZpK41lW1wuI4Hz3bPIpT7aP9zZdtXrb4379WlHowJblnk8jEGJQcnWUlcFnJSl6osPYvkxfq / B0dVcthh7ezOUzf1uXfOcEJ377 / 4rwUTR0VbNTCK601EN6 / ciGJmHars325FPaj3wXDAUIehxEfwiN7aa7HcXH6RqwOF6WcD8voXTdQEsraNaTYbIqSMErzg6HFsaY5cW4IkG6TJ3iBFzXCVfvPRZDxVYMuM + Q5vztCEz5k + Luaxs + S + OQD3ELg8 + y7a / Dv0OhSQkqMDrR / o7mjauDnZVt5VRwtvDYm6kDNOsNL38Ry / tAsPPY26Ff3PDl1ItpFWZCzNS / xfDEjpmcnJOW7hmZi6X17LM66whLUTiCWjj81lpDi + VhBSMI3a2I7jmiFONUKhtD91yrOyHrCWObCdWq + F5H4gjsoP0ffEKcx658a3ZF8VhtL8d9 + B0YtxFPNBQs =";
            LightningChartUltimate.SetDeploymentKey(deploymentKey);
            
            InitializeComponent();
           
            ActiveSectorDelegate?.Invoke(this, new bool[1]);
            chart.BeginUpdate();
            viewPolar.Sectors = new SectorCollection();

            chart.ViewPolar.Axes.Add(axis2);
           // AreaSeriesPolar asp2 = new AreaSeriesPolar(chart.ViewPolar, axis2);
           
            axis.SupplyCustomAngleString += Axis_SupplyCustomAngleString;
            axis2.SupplyCustomAngleString += Axis2_SupplyCustomAngleString;
            //axis3.SupplyCustomAngleString += Axis3_SupplyCustomAngleString;
            //  axis2.MajorDivCount = 1;
            viewPolar.AutoSizeMargins = true;
            axis2.AngleOrigin -= 11;
           
            chart.EndUpdate();
        }

        private void Axis3_SupplyCustomAngleString(object sender, SupplyCustomAngleStringEventArgs e)
        {
            int degrees = (int)Math.Round(180f * e.Angle / Math.PI);

            switch (degrees)
            {
                case 0:
                    e.AngleAsString = "N";
                    break;
                case 90:
                    e.AngleAsString = "W";
                    break;
                case 180:
                    e.AngleAsString = "S";
                    break;
                case 270:
                    e.AngleAsString = "E";
                    break;
                default:
                    e.AngleAsString = "";
                    break;
            }
        }

        public void ActiveSectorCount ()
        {
            Color aqua2 = new Color();
            aqua2.A = 160;
            aqua2.R = 66;
            aqua2.G = 145;
            aqua2.B = 255;
            for (var i = 0; i < countSec ; i++)
            {
                actSec[i] = false;
            }
            var ij = 0;
            foreach (var sector in viewPolar.Sectors)
            {
                
                    ij++;
                    if (sector.Fill.Color == aqua2)
                    {
                        actSec[ij-1] = true;
                    }
            }
        
          
        }
        public void SetAxisWithCircle ( double angle)
        {
             axis2.AngleOrigin += angle ;
        }

       public void tryToW()
        {
            
        //    wTest.Margin = new System.Windows.Thickness(wCord[0],wCord[1],wCord[2],wCord[3]);
        }
        public  void SetActiveSector (int valueSector)
        {
            chart.BeginUpdate();
          
               // axis2.AngleOrigin = 139;
                int[] numCount = new int[valueSector];
            double fullCircle = 360;
            int directionCount = valueSector;
            double sectorAngle = fullCircle / directionCount;
            for (int directionIndex = 0; directionIndex < directionCount; directionIndex++)
            {
                
                double beginAngle = directionIndex * sectorAngle + 1 /*+ rotateCar.Angle*/;
                double endAngle = (directionIndex + 1) * sectorAngle;
                viewPolar.Sectors.Add(new Sector()
                {
                    BeginAngle = beginAngle,
                    EndAngle = endAngle,
                    MoveByMouse = false,
                    
                });;
                numCount[directionIndex]++;
            }
            Color aqua2 = new Color();
            aqua2.A = 160;
            aqua2.R = 66;
            aqua2.G = 145;
            aqua2.B = 255;
            Color pinky = Color.FromArgb(160, 255, 182, 193);
           

            var ij = 0;
            foreach (var sector in viewPolar.Sectors)
            {
               
                if (sector.Fill.Color == aqua2)
                {
                    actSec[ij] = true;
                }
                ij++;
            }
            
            var ji = 0;
            foreach (var sector in viewPolar.Sectors) // попробовать вынести в другую функцию/initialize
            {
               sector.MouseUp += Sector_MouseUp;
             
                if (actSec[ji] == true)
                {
                    sector.Fill.Color = aqua2;
                }
                else
                {
                    sector.Fill.Color = default;
                }
                ji++;
            }
            chart.EndUpdate();
        }

        private void Sector_MouseUp(object sender, System.Windows.Input.MouseEventArgs e)
        {
            chart.BeginUpdate();
            var sector = sender as Sector;
           
            if (isAqua == false)
            {
                Color aqua = new Color();
                aqua.A = 160;
                aqua.R = 66;
                aqua.G = 145;
                aqua.B = 255;
                sector.Fill.Color = aqua;              
                isAqua = true;
                
            } else 
          
            {
                Color aqua = new Color();
                aqua.A = 160;
                aqua.R = 66;
                aqua.G = 145;
                aqua.B = 255;
                Color pinky = new Color();
                pinky.A = 160;
                pinky.R = 255;
                pinky.G = 182;
                pinky.B = 193;
                if (sector.Fill.Color == aqua)
                {
                    sector.Fill.Color = default;
                }
                else
                {
                    if (sector.Fill.Color == default)
                    {
                        sector.Fill.Color = aqua;
                    }
                    else
                  
                    {
                        sector.Fill.Color = default;  
                        isAqua = false;
                    }

                }


            }
            chart.EndUpdate();
            ActiveSectorCount();
            ActiveSectorDelegate?.Invoke(this, actSec);
        }


        public void changeColor()
        {
            Color aqua = new Color();
            aqua.A = 160;
            aqua.R = 66;
            aqua.G = 145;
            aqua.B = 255;
            for (int i = 0; i<= checkCount; i++)
            {
                foreach (var sector in viewPolar.Sectors)
                {
                    if (actSec[i] == true)
                    {
                        sector.Fill.Color = aqua;
                    }
                }
               
            }
        }
        public void ResetSectors(int countSec)
        {
            double fullCircle = 360;
            int directionCount = countSec;
            double sectorAngle = fullCircle / directionCount;
            for (int directionIndex = 0; directionIndex < directionCount; directionIndex++)
            {

                double beginAngle = directionIndex * sectorAngle + 1 + rotateCar.Angle;
                double endAngle = (directionIndex + 1) * sectorAngle;
                viewPolar.Sectors.Add(new Sector()
                {
                    BeginAngle = beginAngle,
                    EndAngle = endAngle,
                    MoveByMouse = false
                });

            }
        }
    
       
        
        private void Axis_SupplyCustomAngleString(object sender, SupplyCustomAngleStringEventArgs e)
        {
           // int degrees = ((int)System.Math.Round(180f * e.Angle / System.Math.PI) ) % 360;
            int degrees = (int)Math.Round(180f * e.Angle / Math.PI);
            
                
            
                switch (degrees)
                {
                    case 0:
                        e.AngleAsString = "N";
                        break;
                    case 90:
                        e.AngleAsString = "W";
                        break;
                    case 180:
                        e.AngleAsString = "S";
                        break;
                    case 270:
                        e.AngleAsString = "E";
                        break;

                    default:
                        e.AngleAsString = "";
                        break;
                }
            
        }
     
        public void SupplyRefresh()
        {
            
             chart.BeginUpdate();
            
            axis2.AngleOrigin = 150 + rotateCar.Angle;

          /*  if (isDegrees)
            {
                degreesTest = (int)System.Math.Round(180 * e.Angle / System.Math.PI);
            }*/
               axis2.AngleOrigin -= degreesTest / 2;
           
            /*if (degreesTest != 0 && isDegrees)
            {
                numDegrees = (int)System.Math.Round(360 / degreesTest);
                isDegrees = false;

            }*/

          /*  while (testNum <= numDegrees && isAsqwe)
            {
                testNum++;
                isAsqwe = false;
            }*/
            chart.EndUpdate();
        }
        public void refCount()
        {
            axis2.AngleOrigin -= degreesTest / 2;
        }
        public void Axis2_SupplyCustomAngleString(object sender, SupplyCustomAngleStringEventArgs e) 
        {
            chart.BeginUpdate();
            
          //  axis2.MajorDivCount = 1;
            if (isDegrees)
            {
                degreesTest = (int)System.Math.Round(180 * e.Angle / System.Math.PI);
            }
        //    axis2.AngleOrigin -= degreesTest / 2;
            
            if (degreesTest != 0 && isDegrees)
            {
                numDegrees = (int)System.Math.Round(360 / degreesTest);
                isDegrees = false;
            }

            while (testNum <= numDegrees && isAsqwe)
            {
                testNum++;
                isAsqwe = false;
            }

            switch (degreesTest)
            {
                case 0:
                    e.AngleAsString = checkCount.ToString();
                    break;

                default:
                    e.AngleAsString = testNum.ToString();
                    isAsqwe = true;
                    break;
            }

            if (testNum == numDegrees)
            {
                switch (degreesTest)
                {
                    case 360:
                        e.AngleAsString = numDegrees.ToString();
                        break;
                }

                isDegrees = true;
                numDegrees = 0;
                testNum = 0;
            }
            chart.EndUpdate();

        }
       

      
        protected void DoEvents()
        {
            //Dispatcher.Invoke(DispatcherPri)
            // Dispatcher.Invoke( new EmptyDelegate(delegate { }));
          // Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Background, new EmptyDelegate(delegate { }));
            
        }
        private void DrawSectors(in double[] valueSector)
        {
            chart.BeginUpdate();
            localList.Clear();
            localList.AddRange(valueSector);

            List<Sector> listSectors = new List<Sector>();
       
            int directionCount = valueSector.Length;

            double fullCircle = 360;
            double sectorAngle = fullCircle / directionCount;
            for (int directionIndex = 0; directionIndex < directionCount; directionIndex++)
            {
                double beginAngle = directionIndex * sectorAngle + 1;
                double endAngle = (directionIndex + 1) * sectorAngle - 1;

                Sector sector = new Sector
                {
                    BeginAngle = beginAngle,
                    EndAngle = endAngle,                  
                    MinAmplitude = 0,
                    MaxAmplitude = valueSector[directionIndex],
                    MouseInteraction = false,
                    ShowInLegendBox = false
                };
               
                plsp.ValueRangePalette.GetColorByValue(valueSector[directionIndex], out Color c);
                c.A = 60;
                sector.Fill.Color = c;
                 listSectors.Add(sector);

            }
            plsp.Points = new PolarSeriesPoint[0];
            viewPolar.Sectors.Clear();
            RefreshSectors();
            SetActiveSector(countSec);
            viewPolar.Sectors.AddRange(listSectors);
           
            chart.EndUpdate();
        }
        public void RefreshSectors()
        {
            viewPolar.Sectors.Clear();
        }
      
        private void DrawPoints(in double[] valuePoints)    
        {
            chart.BeginUpdate();
            localList.Clear();
            localList.AddRange(valuePoints);

            PolarSeriesPoint[] points = new PolarSeriesPoint[valuePoints.Length + 1];

            double step = 360.0 / valuePoints.Length;
            for (int i = 0; i < valuePoints.Length; i++)
            {
                points[i] = new PolarSeriesPoint(step * i, valuePoints[i]);
            }

            points[points.Length - 1] = new PolarSeriesPoint(points[0].Angle, valuePoints[0]);
           // viewPolar.Sectors.Clear();
            plsp.Points = points;
            chart.EndUpdate();
           
        }

      

        private void UpdateLableContent()
        {
            labelF.Content = "F: " + f.ToString("F1") + " MHz";
            labelTheta.Content = '\u25B3' + "F" + ": " + theta.ToString("F1") + " " + '\u05AF';
        }

        private void RbSelectPoints_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            isDrawSectors = false;
            RefreshSectors();
            
            SetActiveSector(countSec); 
           // SupplyRefresh();
            if (localList.Count != 0)     
                Draw(localList.ToArray());
            RefreshSectors();
        }

     
        private void RbSelectSectors_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
           RefreshSectors();
            isDrawSectors = true;
            if (localList.Count != 0)
                Draw(localList.ToArray());
            RefreshSectors();
            // SupplyRefresh();
        }

        /// <summary>
        /// Draw in polar sectors or points, count sectors or points = length array
        /// </summary>
        /// <param name="valuePoints">array with value coordinate </param>
        /// <returns></returns>
        public void Draw(in double[] valuePoints)
        {
            if (valuePoints.Length == 0)
                return;
            if (isDrawSectors)
                DrawSectors(valuePoints);
            else
                DrawPoints(valuePoints);
        }

        /// <summary>
        /// Set count breaking the polar
        /// </summary>
        /// <param name="countSectors">count breaking the polar </param>
        /// <returns></returns 
        public int GetCountSectors(int countSectors)
        {
            return  countSec = countSectors;
        }
        public void SetCountSectors(int countSectors)
        {
           // UpdateLayout();
            viewPolar.Sectors.Clear();
            if (countSectors < 2)
                countSectors = 2;
            axis.AngularAxisMajorDivCount = countSectors;
         
            axis2.AngularAxisMajorDivCount = countSectors;
            checkCount = countSectors;
           
        }
       

        /// <summary>
        /// Set maximum value in polar
        /// </summary>
        /// <param name="amplitude">amplitude or max value</param>
        /// <returns></returns>
        public void SetAmplitude(int amplitude)
        {
            if (amplitude < 1)
                amplitude = 1;
            axis.MaxAmplitude = amplitude;
            palette.Steps[1].MaxValue = amplitude / 2;
            palette.Steps[2].MaxValue = amplitude;
        }

        /// <summary>
        /// Set count drawing circles in polar
        /// </summary>
        /// <param name="countRoundIteration">value > 1 and < 255</param>
        /// <returns></returns>
        public void SetCountRoundIteration(int countRoundIteration)
        {
            if (countRoundIteration < 1 || countRoundIteration > 255)
                countRoundIteration = 1;
            axis.MajorDivCount = countRoundIteration ;
            
        }

        /// <summary>
        /// Set the color bottom pallete
        /// </summary>
        /// <param name="color">color pallete</param>
        /// <returns></returns>
        public void SetCollorLowLevel(Color color)
        {
            color.A = 60;
            palette.Steps[0].Color = color;
        }

        /// <summary>
        /// Set the color middle pallete
        /// </summary>
        /// <param name="color">color pallete</param>
        /// <returns></returns>
        public void SetCollorMidLevel(Color color)
        {
            color.A = 30;
            palette.Steps[1].Color = color;
        }

        /// <summary>
        /// Set the color top pallete
        /// </summary>
        /// <param name="color">color pallete</param>
        /// <returns></returns>
        public void SetCollorHighLevel(Color color)
        {
            color.A = 30;
            palette.Steps[2].Color = color;
        }

        /// <summary>
        /// Set the axis color
        /// </summary>
        /// <param name="color">color axis</param>
        /// <returns></returns>
        public void SetAxisColor(Color color)
        {
           
            axis.AxisColor = color;
        }

        /// <summary>
        /// Set width car image in polar
        /// </summary>
        /// <param name="width">width car image</param>
        /// <returns></returns>
        public void SetCarWidth(int width)
        {
            imgCar.Width = width;
        }

        /// <summary>
        /// Set car clockwise angle without turning the polar and without graphics
        /// </summary>
        /// <param name="angle">angle of rotation</param>
        /// <returns></returns>
        public void SetCarAngle(double angle)
        {
            rotateCar.Angle = angle;
        }

        /// <summary>
        /// Set car clockwise angle with turning the graphics and without rotate polar
        /// </summary>
        /// <param name="angle">angle of rotation</param>
        /// <returns></returns>
        public void SetCarAngleWithGraphics(double angle)
        {
            rotateCar.Angle = angle;
            
            Draw(localList.ToArray());
        }

        /// <summary>
        /// Set car clockwise angle with turning the graphics and with rotate polar
        /// </summary>
        /// <param name="angle">angle of rotation</param>
        /// <returns></returns>
        public void SetAngleChartWithCar(double angle)
        {
            axis.AngleOrigin = angle - 90f;
            rotateCar.Angle = angle;
           

           // Draw(localList.ToArray());
            
        }

        /// <summary>
        /// Set clockwise angle only polar
        /// </summary>
        /// <param name="angle">angle of rotation</param>
        /// <returns></returns>
     
        /// <summary>
        /// Set concavity of a polar
        /// </summary>
        /// <param name="roundGridTickmark">RoundGridTickmarkLocation.Inside or RoundGridTickmarkLocation.Outside</param>
        /// <returns></returns>
        public void SetTickMarkLocation(RoundGridTickmarkLocation roundGridTickmark)
        {
            axis.TickMarkLocation = roundGridTickmark;
        }

        /// <summary>
        /// Opacity car image in round
        /// </summary>
        /// <param name="opacity">range 0-1</param>
        /// <returns></returns>
        public void SetOpacityCar(double opacity)
        {
            imgCar.Opacity = opacity;
        }

        /// <summary>
        /// Set value of F
        /// </summary>
        /// <param name="value">double F</param>
        /// <returns></returns>
        public void SetF(double value)
        {
            f = value;
            UpdateLableContent();
        }

        /// <summary>
        /// Set value of Theta
        /// </summary>
        /// <param name="value">double Theta</param>
        /// <returns></returns>
        public void SetTheta(double value)
        {
            theta = value;
            UpdateLableContent();
        }

        /// <summary>
        /// Clear value F and Theta
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public void ClearLableContent()
        {
            labelF.Content = "";
            labelTheta.Content = "";
        }

        /// <summary>
        /// Set color of theme
        /// </summary>
        /// <param name="color">color theme</param>
        /// <returns></returns>
        public void SetThemeColor(Color color)
        {
            color.A = 50;
            axis.AxisColor = color;
            labelF.Foreground = new SolidColorBrush(color);
            labelTheta.Foreground = new SolidColorBrush(color);
        }

        private void control_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
         //   SupplyRefresh();
          //  axis2.AngleOrigin -= degreesTest / 2;
        
        }
    }
}