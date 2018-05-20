using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Threading;

namespace Piano_Key.ViewModel
{
  public  class MainWindowViewModel:NotificationObject
    {

        /// <summary>
        /// 0 是 唱名 1 是音名 3 是混合
        /// </summary>
        private int _type = 0;

        private string[] _yinming = new string[8] { "C", "D", "E", "F", "G", "A", "B" ,"C"};

        public DelegateCommand Start_YInMing_Command { get; set; }

        public DelegateCommand Start_ChangMing_Command { get; set; }

        public DelegateCommand<object> Set_Speed_Command { get; set; }

        public DelegateCommand Stop_Command { get; set; }

        private string _txt;

        public string Txt
        {
            get { return _txt; }
            set { _txt = value;this.RaisePropertyChanged(() => this.Txt); }
        }


        private DispatcherTimer _time = null;
        public MainWindowViewModel()
        {
            this._time = new DispatcherTimer();
            this._time.Interval = new TimeSpan(0,0,1);
            this._time.Tick += _time_Tick;
             this._time.Start();

           
            //设置速度
            this.Set_Speed_Command = new DelegateCommand<object>((t) => {
                this._time.Interval = new TimeSpan(0,0,(int)t);
            });

            //暂停
            this.Stop_Command = new DelegateCommand(() =>
            {
                this._time.Stop();
            });

            this.Start_YInMing_Command = new DelegateCommand(() =>
              {
                  this._type = 1;
                  this._time.Start();
              });

            this.Start_ChangMing_Command = new DelegateCommand(() =>
            {
                this._type = 0;
                this._time.Start();
            });
        }

        private void _time_Tick(object sender, EventArgs e)
        {
            //唱名
            if (this._type==0)
            {
                Random r = new Random();
                string temp =(r.Next(1, 8)).ToString();
                this.Txt = temp;
            }

            // 音名
            else if (this._type==1)
            {
                Random r = new Random();
                int temp = (r.Next(1, 8));
              string ym = this._yinming[temp];
                this.Txt = ym;
            }
        }




    }
}
