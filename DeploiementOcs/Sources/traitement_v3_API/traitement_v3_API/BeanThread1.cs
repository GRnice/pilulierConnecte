/*
 * Crée par SharpDevelop.
 * Utilisateur: Remy
 * Date: 24/01/2017
 * Heure: 10:34
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using System.Net;
using System.IO;
using System.Collections;
using System.Threading; // For the thread demo purposes
using System.Text;
using WComp.Beans;

namespace WComp.Beans
{
    /// <summary>
    /// This is a sample bean, using a thread, which has an integer evented property and a method 
    ///     to start the thread.
    /// 
    /// Notes: this bean uses the IThreadCreator interface providing a cleanup method named `Stop()'.
    /// Several classes can be defined or used by a Bean, but only the class with the
    /// [Bean] attribute will be available in WComp. Its ports will be all public methods,
    ///     events and properties definied in that class.
    /// </summary>
    [Bean(Category = "API_TRAITEMENT_V3")]
    public class BeanThread1 : IThreadCreator
    {
        private Thread t;     // Private attributes of the class
        private volatile bool run = false;

        private string nomTraitement;
        private DateTime dateFinTraitement;
        private DateTime dateDebutTraitement;
        private ArrayList listPrise;
        private bool sendRappel;
        private int cursor;
        private int currentTour = 0;
        private int maxTour = 9;
        private bool traitementSet = false;

        private int sleepVal = 5000;
        private volatile int eventValue;

        
        public BeanThread1()
        {
            // Put here your init instructions
            eventValue = 10;
        }

        public void mange()
        {
        	
        }
        
        public void Start()
        {  // method starting the thread
            cursor = 0;
            listPrise = new ArrayList();
            if (!run)
            {
                run = true;
                t = new Thread(new ThreadStart(ThreadLoopMethod));
                t.Start();
            }
        }
        public void Stop()
        {   // IThreadCreator defines the Stop() method
            run = false;
        }

        // Loop sample
        public void ThreadLoopMethod()
        {
            while (run)
            {

                TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
                int secondsSinceEpoch = (int)t.TotalSeconds;

                Thread.Sleep(sleepVal);
                // Check if the output is connected
                if (Output_Turn_Pilulier != null)
                {
                    if (check_new_Traitement()) // si il y a un nouveau traitement
                    {
                        setNewTraitement(); // mettre à jour le traitement
                        currentTour = 0;
                    }
                    else if (traitementSet && time_to_turn_pilulier(secondsSinceEpoch)) // si il est l'heure de tourner
                    {
                        currentTour++;
                        if (currentTour == maxTour)
                        {
                            currentTour = 0;
                            Output_send_mail();
                            // PLACER LE SEND MAIL ICI	

                        }
                        if (sendRappel)
                        {
                            Output_send_rappel();
                        }

                        Output_Turn_Pilulier(1024); // on tourne
                    }
                }

                // and so on...
            }
        }
		
        bool check_new_Traitement()
        {
            string received = GET("http://ocs-ws.890m.com/engine/API_traitement.php?NW_TRAITEMENT=1");
            return received.Contains("YES");
        }

        bool time_to_turn_pilulier(int currentTimeSinceEpoch)
        {
            DateTime now = DateTime.Now;

            int prochaineHeurePrise = ((int)listPrise[cursor]);
            if ((now.Hour == prochaineHeurePrise) && now.CompareTo(dateFinTraitement) <= 0)
            {
                if (cursor == listPrise.Count - 1)
                {
                    cursor = 0;
                }
                else
                {
                    cursor++;
                }
                return true;
            }
            return false;
        }

        void setNewTraitement()
        {
            //nomDuTraitement;dureeTraitementEnJour;priseEnHeure-priseEnHeure;dateDebutTraitementEnSecondesDepuis1970
            // TraitementPaie;4;8-12-16;06/02/2017
            traitementSet = true;
            string received = GET("http://ocs-ws.890m.com/engine/API_traitement.php?GET_TRAITEMENT=1");
            String[] tabData = received.Split(new char[] { ';' });

            nomTraitement = tabData[0];
            int nbJoursTraitement = int.Parse(tabData[1]); // duree traitement en jours

            String[] horairesPrises = tabData[2].Split(new char[] { '-' }); // en secondes
            String dateDebutTraitementStr = tabData[3]; // 06/02/2017 -> Date du début du traitement
            String[] dateDebutTraitementTab = dateDebutTraitementStr.Split(new char[] { '/' });
            String rappelOuNon = tabData[4];
            sendRappel = rappelOuNon.Substring(0, 3).Equals("oui");
            //Output_send_rappel_check(rappelOuNon);
            int annee = int.Parse(dateDebutTraitementTab[2]);
            int mois = int.Parse(dateDebutTraitementTab[1]);
            int jour = int.Parse(dateDebutTraitementTab[0]);

            listPrise = new ArrayList();
            StringBuilder sb = new StringBuilder();
            dateDebutTraitement = new DateTime(annee, mois, jour, 0, 0, 1, DateTimeKind.Utc);
            dateFinTraitement = new DateTime(annee, mois, jour, 23, 59, 59, DateTimeKind.Utc);
            dateFinTraitement = dateFinTraitement.AddDays(nbJoursTraitement);

            sb.Append(annee);
            sb.Append("/");
            sb.Append(mois);
            sb.Append("/");
            sb.Append(jour);
            sb.Append(";");

            for (int i = 0; i < horairesPrises.Length; i++)
            {
                listPrise.Add(int.Parse(horairesPrises[i]));
                sb.Append(int.Parse(horairesPrises[i]));
                if (i < horairesPrises.Length - 1)
                {
                    sb.Append(",");
                }
            }

            sb.Append(";");
            sb.Append(nbJoursTraitement);
            sb.Append(";");
            sb.Append(nomTraitement);

            // durée traitement
            // sb -> 2016/02/06;9,12,16;4;nom Traitement
            Output_new_traitement(sb.ToString());
            cursor = 0;
            currentTour = 0;

            //Output_getTime_nextTurn(((int)listPrise[cursor]));
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

        // --- Start: Input port sample ---
        // an input port is a public method (like below)
        public void Input_Sample(int intParam)
        {
            eventValue = intParam;
            // No return value is expected in WComp:
            // results are given using events
        }
        // --- End: Input port sample ---

        // --- Start: Output port sample ---

        public delegate void Output_turn_Pilulier_Signature(int i);
        public event Output_turn_Pilulier_Signature Output_Turn_Pilulier;

        public delegate void Output_getTime_Next_Turn_Signature(int i);
        public event Output_getTime_Next_Turn_Signature Output_getTime_nextTurn;

        public delegate void Output_new_Traitement_Signature(string traitement);
        public event Output_new_Traitement_Signature Output_new_traitement;

        public delegate void Output_send_mail_signature();
        public event Output_send_mail_signature Output_send_mail;

        public delegate void Output_send_rappel_check_sign(string check);
        public event Output_send_rappel_check_sign Output_send_rappel_check;

        public delegate void Output_send_rappel_signature();
        public event Output_send_rappel_signature Output_send_rappel;

    }
}
