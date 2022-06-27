using System;
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

namespace Smirnov_Georgii_PCS_304_Sessiya1
{
    /// <summary>
    /// Логика взаимодействия для Captcha.xaml
    /// </summary>
    public partial class Captcha : Page
    {
        public Captcha()
        {
            InitializeComponent();
        }
        int attempt = 0;
        private void button1_Click(object sender, RoutedEventArgs e)

        {
            if (attempt == 0)
            {
                string allowchar = "";
                allowchar = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
                allowchar += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,y,z";
                allowchar += "1,2,3,4,5,6,7,8,9,0";
                char[] a = { ',' };
                string[] ar = allowchar.Split(a);
                string pwd = "";
                string temp = "";
                Random r = new Random();
                for (int i = 0; i < 6; i++)
                {
                    temp = ar[(r.Next(0, ar.Length))];
                    pwd += temp;
                }
                Text1.Text = pwd;
                attempt++;
                button1.Content = "проверить";
            }
            else
            if (Text2.Text != Text1.Text)
            {
                string allowchar = "";
                allowchar = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
                allowchar += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,y,z";
                allowchar += "1,2,3,4,5,6,7,8,9,0";
                char[] a = { ',' };
                string[] ar = allowchar.Split(a);
                string pwd = "";
                string temp = "";
                Random r = new Random();
                for (int i = 0; i < 6; i++)
                {
                    temp = ar[(r.Next(0, ar.Length))];
                    pwd += temp;
                }
                Text1.Text = pwd;
            }
            else
            {
                CaptchaFrame.Content = new autorization();
            }
        }
    }
}
