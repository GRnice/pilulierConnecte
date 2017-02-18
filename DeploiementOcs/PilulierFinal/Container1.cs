/*
 * Crée par SharpDevelop.
 * Utilisateur: Remy
 * Date: 24/01/2017
 * Heure: 15:35
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */

using System;
using System.Drawing;
using System.Windows.Forms;

using WComp.Beans;

namespace PilulierFinal
{
	/// <summary>
	/// Description of Container1.
	/// </summary>
	public class Container1 : System.Windows.Forms.Form
	{
        [BeanDesignLocation(64,64)]
        private System.Windows.Forms.Button Start;
        [BeanDesignLocation(560,184)]
        private WComp.Beans.Bean1 ComposantMail;
        [BeanDesignLocation(80,248)]
        private System.Windows.Forms.Button Stop;
        [BeanDesignLocation(392,392)]
        private System.Windows.Forms.Label label1;
        [BeanDesignLocation(424,256)]
        private WComp.Beans.CalendarBean calendarBean1;
        [BeanDesignLocation(416,48)]
        private PilulierFinal.PilulierFinal1302 pilulierFinal13021;
        [BeanDesignLocation(288,168)]
        private WComp.Beans.BeanThread1 beanThread11;
		public Container1()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();

			//
			// The InitializeBeans() call is required for WComp.NET designer support.
			//
			InitializeBeans();
			
			//
			// TODO: Add constructor code after the InitializeBeans() call.
			//
		}
		
		[STAThread]
		public static void Main(string[] args)
		{
			Application.Run(new Container1());
		}
		
		#region WComp.NET designer generated code
		/// <summary>
		/// This method is required for WComp.NET designer support.
		/// Do not change the method contents inside the source code editor.
		/// The WComp.NET designer might not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent() {
            this.Start = new System.Windows.Forms.Button();
            this.Stop = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            // 
            // Start
            // 
            this.Start.Controls = null;
            this.Start.DataBindings = null;
            this.Start.Location = new System.Drawing.Point(64, 64);
            // 
            // Stop
            // 
            this.Stop.Controls = null;
            this.Stop.DataBindings = null;
            this.Stop.Location = new System.Drawing.Point(80, 248);
            // 
            // label1
            // 
            this.label1.Text = "2017/2/14;15,16;1;traitementdedom";
            this.label1.Controls = null;
            this.label1.DataBindings = null;
            this.label1.Location = new System.Drawing.Point(392, 392);
            this.label1.Size = new System.Drawing.Size(208, 32);
            // 
            // Container1
            // 
            this.Text = "SharpWComp static application";
            this.Controls.Add(this.Start);
            this.Controls.Add(this.Stop);
            this.Controls.Add(this.label1);
        }
		
		/// <summary>
		/// This method is required for WComp.NET designer support.
		/// Do not change the method contents inside the source code editor.
		/// The WComp.NET designer might not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeBeans() {
            this.ComposantMail = new WComp.Beans.Bean1();
            this.calendarBean1 = new WComp.Beans.CalendarBean();
            this.pilulierFinal13021 = new PilulierFinal.PilulierFinal1302();
            this.beanThread11 = new WComp.Beans.BeanThread1();
            // 
            // ComposantMail
            // 
            this.ComposantMail.MyProperty = 0;
            // 
            // calendarBean1
            // 
            this.calendarBean1.MyProperty = 0;
            // 
            // pilulierFinal13021
            // 
            this.pilulierFinal13021.Uri = "http://192.168.1.199:45555/upnp.xml";
            // 
            Control.CheckForIllegalCrossThreadCalls = false;
            // 
            // Event dispatching
            // 
            this.Start.Click += new System.EventHandler(this.@__Start_to_beanThread11_0);
            this.Stop.Click += new System.EventHandler(this.@__Stop_to_beanThread11_1);
            this.beanThread11.Output_Turn_Pilulier += new WComp.Beans.BeanThread1.Output_turn_Pilulier_Signature(this.@__beanThread11_to_pilulierFinal13021_2);
            this.beanThread11.Output_new_traitement += new WComp.Beans.BeanThread1.Output_new_Traitement_Signature(this.calendarBean1.AjouterEventSimpleCalendrier);
            this.beanThread11.Output_send_mail += new WComp.Beans.BeanThread1.Output_send_mail_signature(this.ComposantMail.sendMail);
            this.beanThread11.Output_send_rappel += new WComp.Beans.BeanThread1.Output_send_rappel_signature(this.ComposantMail.sendRappel);
            this.beanThread11.Output_new_traitement += new WComp.Beans.BeanThread1.Output_new_Traitement_Signature(this.@__beanThread11_to_label1_3);
        }

		private void @__Start_to_beanThread11_0(object sender, System.EventArgs e) {
            this.beanThread11.Start();
        }

		private void @__Stop_to_beanThread11_1(object sender, System.EventArgs e) {
            this.beanThread11.Stop();
        }

		private void @__beanThread11_to_pilulierFinal13021_2(int i) {
            this.pilulierFinal13021.turn();
        }

		private void @__beanThread11_to_label1_3(string traitement) {
            this.label1.Text = traitement;
        }

		private void @__Start_to_beanThread11_0(object sender, System.EventArgs e) {
            this.beanThread11.Start();
        }

		private void @__Stop_to_beanThread11_1(object sender, System.EventArgs e) {
            this.beanThread11.Stop();
        }

		private void @__beanThread11_to_pilulierFinal13021_2(int i) {
            this.pilulierFinal13021.turn();
        }

		private void @__beanThread11_to_label1_3(string traitement) {
            this.label1.Text = traitement;
        }

		private void @__button2_to_pilulierFinal13021_7(object sender, System.EventArgs e) {
            this.pilulierFinal13021.turn();
        }

		private void @__button3_to_pilulierBorrigo1_7(object sender, System.EventArgs e) {
            this.pilulierBorrigo1.turn();
        }

		private void @__button3_to_pilulierBorrigo1_7(object sender, System.EventArgs e) {
            this.pilulierBorrigo1.turn();
        }

		private void @__button1_to_ComposantMail_5(object sender, System.EventArgs e) {
            this.ComposantMail.sendMail();
        }

		private void @__pilulierBorrigo1_to_ComposantMail_6(bool NewValue) {
            this.ComposantMail.sendMail();
        }

		private void @__traitement_to_label1_2(string traitement) {
            this.label1.Text = traitement;
        }

		private void @__traitement_to_label2_3(int i) {
            this.label2.Top = i;
        }

		private void @__traitement_to_pilulierBorrigo1_4(int i) {
            this.pilulierBorrigo1.turn();
        }

		private void @__API_TRAITEMENT_to_sample_Embedded_Device1_1(int i) {
            this.sample_Embedded_Device1.turn();
        }

		private void @__API_TRAITEMENT_to_label1_2(int i) {
            this.label1.Text = this.API_TRAITEMENT.ToString();
        }

		private void @__Start_to_API_TRAITEMENT_3(object sender, System.EventArgs e) {
            this.API_TRAITEMENT.Start();
        }

		private void @__Stop_to_API_TRAITEMENT_4(object sender, System.EventArgs e) {
            this.API_TRAITEMENT.Stop();
        }

		private void @__API_TRAITEMENT_to_sample_Embedded_Device1_1(int i) {
            this.sample_Embedded_Device1.turn();
        }

		private void @__API_TRAITEMENT_to_label1_2(int i) {
            this.label1.Text = this.API_TRAITEMENT.ToString();
        }

		private void @__Start_to_API_TRAITEMENT_3(object sender, System.EventArgs e) {
            this.API_TRAITEMENT.Start();
        }

		private void @__Stop_to_API_TRAITEMENT_4(object sender, System.EventArgs e) {
            this.API_TRAITEMENT.Stop();
        }

		private void @__beanThread11_to_sample_Embedded_Device1_1(int i) {
            this.sample_Embedded_Device1.turn();
        }

		private void @__beanThread11_to_label1_2(int i) {
            this.label1.Text = this.beanThread11.ToString();
        }

		private void @__Start_to_beanThread11_3(object sender, System.EventArgs e) {
            this.beanThread11.Start();
        }

		private void @__Stop_to_beanThread11_4(object sender, System.EventArgs e) {
            this.beanThread11.Stop();
        }

		private void @__sample_Embedded_Device1_to_bean11_0(bool NewValue) {
            this.bean11.sendMail();
        }

		private void @__button1_to_bean11_1(object sender, System.EventArgs e) {
            this.bean11.tourComplet();
        }

		private void @__beanThread11_to_sample_Embedded_Device1_1(int i) {
            this.sample_Embedded_Device1.turn();
        }
		#endregion
	}
}
