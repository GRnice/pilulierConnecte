﻿/*
 * Crée par SharpDevelop.
 * Utilisateur: Remy
 * Date: 30/01/2017
 * Heure: 16:37
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using WComp.Beans;
using System.Net;
using System.IO;
using System.Text;

namespace WComp.Beans
{
	/// <summary>
	/// This is a sample bean, which has an integer evented property and a method.
	/// 
	/// Notes: for beans creating threads, the IThreadCreator interface should be implemented,
	/// 	providing a cleanup method should be implemented and named `Stop()'.
	/// For proxy beans, the IProxyBean interface should  be implemented,
	/// 	providing the IsConnected property, allowing the connection status to be drawn in
	/// 	the AddIn's graphical designer.
	/// 
	/// Several classes can be defined or used by a Bean, but only the class with the
	/// [Bean] attribute will be available in WComp. Its ports will be all public methods,
	/// events and properties definied in that class.
	/// </summary>
	[Bean(Category="MyCategory")]
	public class Bean1
	{
		/// <summary>
		/// Fill in private attributes here.
		/// </summary>
		private int property;

		/// <summary>
		/// This property will appear in bean's property panel and bean's input functions.
		/// </summary>
		public int MyProperty {
			get { return property; }
			set {
				property = value;
				FireIntEvent(property);		// event will be fired for every property set.
			}
		}

		/// <summary>
		/// A method sending an event, which is here simply the argument + 1.
		/// Note that there is no return type to the method, because we use events to send
		/// information in WComp. Return values don't have to be used.
		/// </summary>
		public void MyMethod(int arg) {
			FireIntEvent(arg+1);
		}
		
		public void sendMail()
		{
			String mailMedecin = GET("http://ocs-ws.890m.com/engine/API_traitement.php?GET_MEDECIN=1");
			String[] dataMailMedecin = mailMedecin.Split(new Char[] {';'});
            WebRequest request = WebRequest.Create("https://api.sendgrid.com/api/mail.send.xml");
            request.Method = "POST";
            string payload = "api_user=domDib&api_key=ta3BousoYaAire&to="+dataMailMedecin[1]+"&subject=Pilulier&text=Le pilulier est vide !&from=sender@example.com";
            byte[] byteArray = Encoding.UTF8.GetBytes(payload);

            request.ContentType = "application/x-www-form-urlencoded";
            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;
            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();
		}
		
		public void sendRappel()
		{
			String mailMedecin = GET("http://ocs-ws.890m.com/engine/API_traitement.php?GET_MEDECIN=1");
			String[] dataMailMedecin = mailMedecin.Split(new Char[] {';'});
            WebRequest request = WebRequest.Create("https://api.sendgrid.com/api/mail.send.xml");
            request.Method = "POST";
            string payload = "api_user=domDib&api_key=ta3BousoYaAire&to="+dataMailMedecin[1]+"&subject=Pilulier&text=Rappel, une pilule a prendre à cette heure ci.&from=sender@example.com";
            byte[] byteArray = Encoding.UTF8.GetBytes(payload);

            request.ContentType = "application/x-www-form-urlencoded";
            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;
            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();
		}

		static string GET(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    String errorText = reader.ReadToEnd();
                }
                throw;
            }
        }
		
		/// <summary>
		/// Here are the delegate and his event.
		/// A function checking nullity should be used to fire events (like FireIntEvent).
		/// </summary>
		public delegate void IntValueEventHandler(int val);
		/// <summary>
		/// the following declaration is the event by itself. Its name, here "PropertyChanged",
		/// is the name of the event as it will be displayed in the bean type's interface.
		/// </summary>
		public event IntValueEventHandler PropertyChanged;
		
		private void FireIntEvent(int i) {
			if (PropertyChanged != null)
				PropertyChanged(i);
		}
	}
}
