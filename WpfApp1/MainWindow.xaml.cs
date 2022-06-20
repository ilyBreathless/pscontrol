using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;
using System.Net;
using System.Data;
using System.Windows.Threading;
using System.Threading;
namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    using System.Windows.Threading;
    public partial class MainWindow : Window
    {

        private int countObject;
        private delegate void EmptyDelegate();
       /*  static class D
         {
             public static int DATA { get; set; }
         }*/
        // MainWindow mainWindow = (MainWindow)App.Current.MainWindow;
        public MainWindow()
        {
            InitializeComponent();

            psControl.SetCollorLowLevel(Brushes.Red.Color);
            psControl.SetCollorHighLevel(Brushes.Green.Color);
            psControl.ActiveSectorDelegate += PsControl_ActiveSectorDelegate;
          /*  for (int i = 0; i < psControl?.activeSectorsList.Count; i++)
            {
                ActiveSecTest.Text += psControl?.activeSectorsList[i].ToString() +"\r\n";
            }*/
            int.TryParse(tbCountIteration.Text, out int countIter);
            if (countIter < 0)
                countIter = 0;
            psControl?.SetCountRoundIteration(countIter);
            psControl?.SetAmplitude(3);

            int.TryParse(tbCountSectors.Text, out int countSecotrs);
            if (countSecotrs < 0)
                countSecotrs = 0;
            psControl.isDegrees = true;
            psControl?.SetCountSectors(countSecotrs);
            psControl?.GetCountSectors(countSecotrs);
            
          
                psControl.actSec = new bool[21];
           
           
            
            psControl?.SetActiveSector(countSecotrs);
            //  btnRefreshSector.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
           // psControl?.SupplyRefresh();
            psControl.SetF(0.34324);
            psControl.SetTheta(24.5324);
        }

        private void PsControl_ActiveSectorDelegate(object sender, bool[] e)
        {
          /*  for (int i = 0; i < psControl?.activeSectorsList.Count; i++)
            {
                ActiveSecTest.Text += psControl?.activeSectorsList[i].ToString() + "\r\n";
            }*/
            ActiveSecTest.Text += e.ToString();
        }

      

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(tbCountSectors.Text, out int countSecotrs);
            if (countSecotrs < 0)
                countSecotrs = 0;
            double[] randomValue = new double[countSecotrs];
            
            Random random = new Random();
            for (int i = 0; i < randomValue.Length; i++)
            {
                randomValue[i] = random.Next(0, int.Parse(tbLengthAmplitude.Text));
            }
          
            psControl.RefreshSectors();
            psControl.GetCountSectors(countSecotrs);
            psControl.SetActiveSector(countSecotrs);
            psControl.Draw(randomValue);
      //      psControl.SupplyRefresh();



        }
        protected void DoEvents()
        {
            Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Background, new EmptyDelegate(delegate { }));
        }
        private void sliderSizeCar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            psControl?.SetCarWidth((int)Math.Round(e.NewValue));
        }

        private void sliderRotateCar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            psControl?.SetCarAngleWithGraphics(e.NewValue);

        }

        private void tbCountSectors_TextChanged(object sender, TextChangedEventArgs e)
        {
            //  psControl?.SupplyRefresh();
            // psControl?.RefreshSectors();
            int.TryParse(tbCountSectors.Text, out int countSecotrs);
            if (countSecotrs < 0)
                countSecotrs = 0;



            for (var i = 0; i < psControl?.actSec.Length; i++)
            {
                psControl.actSec[i] = false;
            }
            if (countSecotrs > 21)
            {
                countSecotrs = 16;
            }
            //  psControl?.GetCountSectors(countSecotrs);
            
                 psControl?.SetCountSectors(countSecotrs);
                 psControl?.SetActiveSector(countSecotrs);
                 psControl?.SupplyRefresh();
            
           
            // psControl?.refCount();
        }


        private void tbCountIteration_TextChanged(object sender, TextChangedEventArgs e)
        {
            int.TryParse(tbCountIteration.Text, out int countIter);
            if (countIter < 0)
                countIter = 0;
            psControl?.SetCountRoundIteration(countIter);

        }

        private void tbLengthAmplitude_TextChanged(object sender, TextChangedEventArgs e)
        {
            int.TryParse(tbLengthAmplitude.Text, out int lenghtAmplitude);
            if (lenghtAmplitude < 1)
                lenghtAmplitude = 1;
            psControl?.SetAmplitude(lenghtAmplitude);
        }

        private void ColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            psControl.SetCollorLowLevel((Color)e.NewValue);
        }

        private void ColorPicker_SelectedColorChanged_1(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            psControl.SetCollorHighLevel((Color)e.NewValue);
        }

        private void sliderChart_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
        
       
          
        private void sliderRotateChartWithCar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            
            psControl.SetAngleChartWithCar(e.NewValue);
          
            int.TryParse(tbCountSectors.Text, out int countSecotrs);
            if (countSecotrs < 0)
                countSecotrs = 0;

            int.TryParse(tbCountIteration.Text, out int countIter);
            if (countIter < 0)
                countIter = 0;
            psControl?.SetCountRoundIteration(countIter);
        
            psControl.SetAxisWithCircle(e.NewValue);
           // psControl.SupplyRefresh();

        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Minimized)
            {
               psControl.SupplyRefresh();
            }
         
        }
        private void btnRefreshPoint_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(tbCountSectors.Text, out int countSecotrs);
            if (countSecotrs < 0)
                countSecotrs = 0;
            double[] randomValue = new double[countSecotrs];

            Random random = new Random();
            for (int i = 0; i < randomValue.Length; i++)
            {
                randomValue[i] = random.Next(0, int.Parse(tbLengthAmplitude.Text));
            }
           
            psControl.RefreshSectors();
            psControl.SetActiveSector(countSecotrs);

            psControl.Draw(randomValue);
          //  psControl.SupplyRefresh();

        }

        private void tbCountObject_TextChanged(object sender, TextChangedEventArgs e)
        {
            int.TryParse(tbCountObject.Text, out countObject);
            if (countObject < 3)
                countObject = 3;
        }

        private void ColorPicker_SelectedColorChanged_2(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            psControl.SetCollorMidLevel((Color)e.NewValue);
        }

        private void sd_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (e.NewValue < 0)
                return;
            psControl?.SetOpacityCar(e.NewValue);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(UpdateFunc);
            thread.Start();
            
        }
        private void UpdateFunc () {
          /*  Thread.Sleep(TimeSpan.FromSeconds(2));
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                (ThreadStart)delegate ()
                {
                    
                }
            );*/
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

         //   btnRefreshSector.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }
    }
}