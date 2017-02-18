using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace CalendarNet35To4Adapter
{
    [ComVisible(true)]
    [Guid("A6574755-925A-4E41-A01B-B6A0EEF72DF8")]
    public interface IMyClassAdapter
    {
        void connexion();
        void ajouterSimpleEvent(String mail, String summary, DateTime dateDebut, DateTime dateFin, String location);
    }
}
